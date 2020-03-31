using Microsoft.Owin;
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
    }
}
