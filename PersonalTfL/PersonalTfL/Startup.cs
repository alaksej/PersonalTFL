using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PersonalTfL.Startup))]
namespace PersonalTfL
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
