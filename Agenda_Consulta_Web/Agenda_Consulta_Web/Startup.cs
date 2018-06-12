using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Agenda_Consulta_Web.Startup))]
namespace Agenda_Consulta_Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
