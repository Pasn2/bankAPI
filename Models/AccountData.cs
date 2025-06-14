using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using Microsoft.AspNetCore.Mvc;
namespace BankApi.Models
{
    public class AccountData
    {

        public int ID { get; set; }
        public double balance { get; set; }



    }
}
