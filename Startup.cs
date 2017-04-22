using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ChrisConnorBlogAssessment.Startup))]
namespace ChrisConnorBlogAssessment
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
