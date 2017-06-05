using System.Web.Mvc;
using System.Web.Routing;

namespace GameForum.Web.Infrastructure.Bootstrapping
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: null,
                url: "games/{action}",
                defaults: new { controller = "Games", action = "AllGames" });

            routes.MapRoute(
                name: null,
                url: "game/{gameKey}/{action}",
                defaults: new { controller = "Game", action = "GameDetails" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            
        }
    }
}