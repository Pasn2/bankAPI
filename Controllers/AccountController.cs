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
        [HttpGet("{ID}/getaccount")]
        public async Task<IActionResult> GetAccountById([FromRoute] int ID)
        {
            var account = await _context.BankAccounts.FindAsync(ID);
            if (account == null)
                return NotFound("Nie znaleziono konta o podanym ID.");
            return Ok(account);


        }
        [HttpGet("{ID}/getID")]
        public async Task<IActionResult> GetAccountID([FromRoute] int ID)
        {
            var account = await _context.BankAccounts.FindAsync(ID);
            if (account == null)
                return NotFound("Nie znaleziono konta o podanym ID.");
            return Ok(new { id = account.AccountId});


        }
        [HttpGet("{_email}/getemail")]
        public async Task<IActionResult> GetIdByEmail(string _email)
        {
            if (string.IsNullOrWhiteSpace(_email))
                return BadRequest("Email is required.");

            var user = await _context.users.FirstOrDefaultAsync(u => u.email == _email);

            if (user == null)
                return NotFound($"User with email {_email} not found.");

            return Ok(new { user.ID });
        }
        [HttpGet("{_login}/getlogin")]
        public async Task<IActionResult> GetIdByLogin(string _login)
        {
            if (string.IsNullOrWhiteSpace(_login))
                return BadRequest("Login is required.");

            var user = await _context.users.FirstOrDefaultAsync(u => u.login == _login);

            if (user == null)
                return NotFound($"User with Login {_login} not found.");

            return Ok(new { user.ID });
        }

    }

}
