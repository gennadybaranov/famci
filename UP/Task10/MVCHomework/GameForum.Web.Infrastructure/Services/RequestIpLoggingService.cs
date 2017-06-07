using System;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting.Channels;
using GameForum.Web.Infrastructure.Services.Abstractions;

namespace GameForum.Web.Infrastructure.Services
{
    public class RequestIpLoggingService : IRequestIpLoggingService
    {
        private readonly IPathHelper pathHelper;

        public RequestIpLoggingService(IPathHelper pathHelper)
        {
            if (pathHelper == null)
            {
                throw new ArgumentNullException("IPathHelper is null");
            }

            this.pathHelper = pathHelper;
        }

        public void LogMessage(string msg, string path)
        {
            try
            {
                string filePath = this.pathHelper.ResolvePathToLog(path);
                using (var sw = new StreamWriter(filePath, true))
                {
                    sw.Write(msg);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
