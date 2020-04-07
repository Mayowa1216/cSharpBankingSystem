using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using OnlineBankingSystem.Core;
using OnlineBankingSystem.Persistence;
using Owin;

[assembly: OwinStartupAttribute(typeof(OnlineBankingSystem.Startup))]
namespace OnlineBankingSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
        private static void DefaultUser()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var store = new UserStore<ApplicationUser>(context);
            var manager = new UserManager<ApplicationUser>(store);

            ApplicationUser op = new ApplicationUser();
            op.Firstname = "admin";
            op.Lastname = "admin";
            op.Email = "admin@yahoo.com";
            op.EmailConfirmed = true;
            op.UserName = "admin@yahoo.com";

            var user = manager.FindByEmail(op.Email);
            if (user == null)
            {
                manager.Create(op, "password1234$");
                manager.AddToRole(op.Id, "Admin");
            }
        }
    }
}
