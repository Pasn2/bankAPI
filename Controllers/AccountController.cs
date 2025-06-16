using api.Data;
using BankApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankApi.Controllers
{
    [ApiController]
    [Route("BankApi/account")]
    public class AccountController : ControllerBase
    {
        readonly AplicationDBContext _context;
        public AccountController(AplicationDBContext aplicationDBContext)
        {
            _context = aplicationDBContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAccount()
        {
            var accounts = await _context.BankAccounts.ToListAsync<BankAccount>();
            return Ok(accounts);


        }
        public async Task<IActionResult> GetAccountById(int _id)
        {
            var accounts = await _context.BankAccounts.ToListAsync<BankAccount>();
            return Ok(accounts);


        }
    }
}
