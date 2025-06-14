
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using BankApi.Controllers;
using BankApi.DTOS;
using BankApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankApi.Controllers
{
    [ApiController]
    [Route("BankApi/login")]
    public class LoginControler : ControllerBase
    {


        readonly AplicationDBContext _context;
        public LoginControler(AplicationDBContext aplicationDBContext)
        {
            _context = aplicationDBContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _context.users.ToListAsync<User>();
            return Ok(users);


        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserDTOS user)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.password);

            var newUser = new User
            {
                email = user.email,
                login = user.login,
                password = hashedPassword
            };

            _context.Add(newUser);
            await _context.SaveChangesAsync();
            return Ok("Zarejestrowano");
        }
        [HttpPost("login")]
        public IActionResult Login(UserDTOS login)
        {
            var user = _context.users.FirstOrDefault(u => u.login == login.login);
            if (user == null || !BCrypt.Net.BCrypt.Verify(login.password, user.password))
            {
                return Unauthorized("Złe dane logowania");
            }

                return Ok("Zalogowano");
            }
    }
}
