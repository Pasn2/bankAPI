using api.Data;
using BankApi.DTOS;
using BankApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("BankApi/transaction")]
public class TransactionController : ControllerBase
{
    private readonly AplicationDBContext _context;

    public TransactionController(AplicationDBContext context)
    {
        _context = context;
    }
    [HttpGet("{accountId}/get")]
    public async Task<IActionResult> GetTransactions(string _accounemail)
    {
        var account = await _context.BankAccounts
       .Include(a => a.SentTransactions)
       .Include(a => a.ReceivedTransactions)
       .FirstOrDefaultAsync(a => a.user.email == _accounemail);

        if (account == null)
            return NotFound("Nie znaleziono konta.");

        var sent = account.SentTransactions.Select(t => new TransactionDTO
        {
            ID = t.ID,
            Amount = t.Amount,
            CreatedAt = t.CreatedAt
        });

        var received = account.ReceivedTransactions.Select(c => new TransactionDTO
        {
            ID = c.ID,
            Amount = c.Amount,
            CreatedAt = c.CreatedAt
        });

        return Ok(new
        {
            sent,
            received
        });
    }

    [HttpPost("send")]
    public async Task<IActionResult> CreateTransaction([FromBody] TransactionDTO dto)
    {
        if (dto.Amount <= 0)
            return BadRequest("Kwota musi być większa niż 0");

        var reciver = await _context.BankAccounts.FindAsync(dto.ReciveAccountId);
        var sender = await _context.BankAccounts.FindAsync(dto.SendAccountId);

        if (reciver == null || sender == null)
            return BadRequest("DAWD");

        if (reciver.balance < dto.Amount)
            return BadRequest("Brak wystarczających środków");

        // Aktualizacja salda
        reciver.balance -= dto.Amount;
        sender.balance += dto.Amount;

        // Utworzenie transakcji
        var transaction = new Transaction
        {
            SenderAccountId = dto.SendAccountId,
            ReceiverAccountId = dto.ReciveAccountId,
            Amount = dto.Amount,
            CreatedAt = DateTime.UtcNow
        };

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();

        return Ok("Transakcja zakończona sukcesem");
    }
}