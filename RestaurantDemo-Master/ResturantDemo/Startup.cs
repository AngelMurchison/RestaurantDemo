using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ResturantDemo.Startup))]
namespace ResturantDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
