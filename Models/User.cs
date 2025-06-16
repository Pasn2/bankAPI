using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using Microsoft.AspNetCore.Mvc;
namespace BankApi.Models
{
    public class User
    {

        public int ID { get; set; }
        public string email { get; set; } = String.Empty;
        public string login { get; set; } = String.Empty;
        public string password { get; set; } = String.Empty;
        public BankAccount account = new BankAccount();

    }
}

