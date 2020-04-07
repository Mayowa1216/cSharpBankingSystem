namespace OnlineBankingSystem.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<OnlineBankingSystem.Persistence.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsDirectory = @"Persistence\Migrations";
        }

        protected override void Seed(OnlineBankingSystem.Persistence.ApplicationDbContext context)
        {
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));


            if (!RoleManager.RoleExists("Admin"))
            {
                IdentityResult roleresult = RoleManager.Create(new IdentityRole("Admin"));
            }

            if (!RoleManager.RoleExists("Staff"))
            {
                IdentityResult roleresult = RoleManager.Create(new IdentityRole("Staff"));
            }

            if (!RoleManager.RoleExists("Customer"))
            {
                IdentityResult roleresult = RoleManager.Create(new IdentityRole("Customer"));
            }

           
        }
    }
}
