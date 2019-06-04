using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Deloitte_ToDoList9.Startup))]
namespace Deloitte_ToDoList9
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
