
using BankApi.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class AplicationDBContext : DbContext
    {
        public AplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<BankAccount>()
                .HasMany(b => b.SentTransactions)
                .WithOne(t => t.SenderAccount)
                .HasForeignKey(t => t.SenderAccountId)
                .OnDelete(DeleteBehavior.NoAction); // zapobiega cyklom usuwania

            modelBuilder.Entity<BankAccount>()
                .HasMany(b => b.ReceivedTransactions)
                .WithOne(t => t.ReceiverAccount)
                .HasForeignKey(t => t.ReceiverAccountId)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<User> users { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        
    }

}