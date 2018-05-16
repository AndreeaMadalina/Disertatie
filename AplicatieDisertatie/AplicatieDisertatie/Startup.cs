using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AplicatieDisertatie.Startup))]
namespace AplicatieDisertatie
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
