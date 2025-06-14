
using BankApi.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class AplicationDBContext : DbContext
    {
        public AplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<User> users { get; set; }
        public DbSet<AccountData> accounts { get; set; }

    }
}