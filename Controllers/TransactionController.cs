using api.Data;
using BankApi.DTOS;
using BankApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

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
    public async Task<IActionResult> GetTransactions(int accountId)
    {
        var account = await _context.BankAccounts
            .Include(a => a.SentTransactions)
            .Include(a => a.ReceivedTransactions)
            .FirstOrDefaultAsync(a => a.AccountId == accountId); // <-- kluczowa poprawka

        if (account == null)
            return NotFound("Nie znaleziono konta.");

        var sent = account.SentTransactions.Select(t => new TransactionDTO
        {
            ID = t.ID,
            Amount = t.Amount,
            SendAccountId = t.SenderAccountId,
            ReciveAccountId = t.ReceiverAccountId,
            CreatedAt = t.CreatedAt,
             Descryption = t.Descryption

        });

        var received = account.ReceivedTransactions.Select(c => new TransactionDTO
        {
            ID = c.ID,
            Amount = c.Amount,
            CreatedAt = c.CreatedAt,
            Descryption = c.Descryption,
            SendAccountId = c.SenderAccountId,
            ReciveAccountId = c.ReceiverAccountId,
        }) ;

        return Ok(new
        {
            sent,
            received
        });
    }
    [HttpGet("{accountID}/getRecived")]
    public async Task<IActionResult> GetRecivedTransaction([FromRoute] int accountID)
    {
        var transactions = await _context.Transactions
        .Where(t => t.ReceiverAccountId == accountID)
        .Select(t => new TransactionDTO
        {
            ID = t.ID,
            SendAccountId = t.SenderAccountId,
            ReciveAccountId = t.ReceiverAccountId,
            Amount = t.Amount,
            CreatedAt = t.CreatedAt,
            Descryption = t.Descryption
        })
        .ToListAsync();

        return Ok(transactions);
    }
    [HttpGet("{accountID}/getSended")]
    public async Task<IActionResult> GetSendedTransaction([FromRoute] int accountID)
    {
        var transactions = await _context.Transactions
       .Where(t => t.SenderAccountId == accountID)
       .Select(t => new TransactionDTO
       {
           ID = t.ID,
           SendAccountId = t.SenderAccountId,
           ReciveAccountId = t.ReceiverAccountId,
           Amount = t.Amount,
           CreatedAt = t.CreatedAt,
           Descryption = t.Descryption
       })
       .ToListAsync();

        return Ok(transactions);
    }
    [HttpGet("{accountID}/getbalance")]
    public async Task<IActionResult> GetBalance([FromRoute] int accountID)
    {
        var account = await _context.BankAccounts.FindAsync(accountID);
        if (account == null)
            return NotFound("Nie znaleziono konta.");

        return Ok(new { balance = account.balance });
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
            Descryption = dto.Descryption,
            CreatedAt = DateTime.UtcNow
        };

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();

        return Ok("Transakcja zakończona sukcesem");
    }
    
}