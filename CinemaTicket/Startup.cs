using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CinemaTicket.Startup))]
namespace CinemaTicket
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
