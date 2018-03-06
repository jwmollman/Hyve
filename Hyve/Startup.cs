using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Hyve.Startup))]

namespace Hyve {
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}