using System;
using System.Text;
using System.Web.Mvc;
using GameForum.Web.Infrastructure.Services;
using GameForum.Web.Infrastructure.Services.Abstractions;

namespace GameForum.Web.Infrastructure.Filters
{
    public class LogRequestIpAttribute : ActionFilterAttribute
    {
        private IRequestIpLoggingService loggingService;

        //Injected property.
        public IRequestIpLoggingService LoggingService
        {
            get
            {
                if (this.loggingService == null)
                {
                    this.loggingService = new RequestIpLoggingService(new PathHelper(new DefaultConfigurationManager()));
                }

                return this.loggingService;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("IRequestIpLoggingService cannot be null");
                }

                this.loggingService = value;
            }
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var path = filterContext.HttpContext.Request.PhysicalPath;
            var msg = new StringBuilder();
            msg.AppendLine(filterContext.HttpContext.Request.UserHostAddress);
            LoggingService.LogMessage(msg.ToString(), path);
        }
    }
}
