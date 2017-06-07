using System.Collections.Specialized;

namespace GameForum.Web.Infrastructure.Services.Abstractions
{
    public interface IConfigurationManager
    {
        NameValueCollection AppSettings { get; }
    }
}
