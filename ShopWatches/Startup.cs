using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShopWatches.Startup))]
namespace ShopWatches
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
          
        }
    }
}
