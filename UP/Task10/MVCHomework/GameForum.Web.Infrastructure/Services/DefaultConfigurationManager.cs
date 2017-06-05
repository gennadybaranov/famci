using System.Collections.Specialized;
using System.Configuration;
using GameForum.Web.Infrastructure.Services.Abstractions;

namespace GameForum.Web.Infrastructure.Services
{
    public class DefaultConfigurationManager : IConfigurationManager
    {
        public NameValueCollection AppSettings
        {
            get
            {
                return ConfigurationManager.AppSettings;
            }
        }
    }
}
