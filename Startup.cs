using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WritersInn1.Startup))]
namespace WritersInn1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
