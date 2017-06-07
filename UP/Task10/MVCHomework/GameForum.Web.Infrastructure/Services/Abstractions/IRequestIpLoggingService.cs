namespace GameForum.Web.Infrastructure.Services.Abstractions
{
    public interface IRequestIpLoggingService
    {
        void LogMessage(string msg, string path);
    }
}
