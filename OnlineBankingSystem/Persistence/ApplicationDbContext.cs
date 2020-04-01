using Microsoft.AspNet.Identity.EntityFramework;
using OnlineBankingSystem.Core;

namespace OnlineBankingSystem.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}