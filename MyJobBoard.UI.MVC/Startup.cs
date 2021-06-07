using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyJobBoard.UI.MVC.Startup))]
namespace MyJobBoard.UI.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
