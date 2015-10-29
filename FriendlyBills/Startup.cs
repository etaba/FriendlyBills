using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FriendlyBills.Startup))]
namespace FriendlyBills
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
