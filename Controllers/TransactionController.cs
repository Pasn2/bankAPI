using api.Data;
using BankApi.DTOS;
using BankApi.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("BankApi/transaction")]
public class TransactionController : ControllerBase
{
    private readonly AplicationDBContext _context;

    public TransactionController(AplicationDBContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTransaction([FromBody] TransactionDTO dto)
    {
        if (dto.Amount <= 0)
            return BadRequest("Kwota musi być większa niż 0");

        var sender = await _context.BankAccounts.FindAsync(dto.SenderAccountId);
        var receiver = await _context.BankAccounts.FindAsync(dto.ReceiverAccountId);

        if (sender == null || receiver == null)
            return NotFound("Konto nadawcy lub odbiorcy nie istnieje");

        if (sender.balance < dto.Amount)
            return BadRequest("Brak wystarczających środków");

        // Aktualizacja salda
        sender.balance -= dto.Amount;
        receiver.balance += dto.Amount;

        // Utworzenie transakcji
        var transaction = new Transaction
        {
            SenderAccountId = dto.SenderAccountId,
            ReceiverAccountId = dto.ReceiverAccountId,
            Amount = dto.Amount,
            CreatedAt = DateTime.UtcNow
        };

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();

        return Ok("Transakcja zakończona sukcesem");
    }
}