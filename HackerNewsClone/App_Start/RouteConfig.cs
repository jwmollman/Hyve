using System.Web.Mvc;
using System.Web.Routing;

namespace HackerNewsClone.App_Start {
    public class RouteConfig {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.LowercaseUrls = true;

            routes.MapRoute(
                name: "Account",
                url: "account/{action}",
                defaults: new { controller = "Account", action = "Index" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{action}",
                defaults: new { controller = "Home", action = "Index" }
            );
        }
    }
}
