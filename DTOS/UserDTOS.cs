using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using Microsoft.AspNetCore.Mvc;
namespace BankApi.DTOS
{
    public class UserDTOS
    {

        public int ID { get; set; }
        public string email { get; set; } = String.Empty;
        public string login { get; set; } = String.Empty;
        public string password { get; set; } = String.Empty;
    }
}

