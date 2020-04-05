using Microsoft.AspNet.Identity.EntityFramework;
using OnlineBankingSystem.Core;
using OnlineBankingSystem.Core.Models;
using System.Data.Entity;

namespace OnlineBankingSystem.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<AccountType> accountType { get; set; }
        public DbSet<Account> account { get; set; }
        public DbSet<TransactionType> transactionTypes { get; set; }
        public DbSet<Transaction> transactions { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}