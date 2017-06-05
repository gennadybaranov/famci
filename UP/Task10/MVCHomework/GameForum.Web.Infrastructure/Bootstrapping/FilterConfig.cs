using System.Web.Mvc;
using GameForum.Web.Infrastructure.Filters;

namespace GameForum.Web.Infrastructure.Bootstrapping
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(DependencyResolver.Current.GetService<LogRequestIpAttribute>());
        }
    }
}