using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Demo.Products.Startup))]
namespace Demo.Products
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
