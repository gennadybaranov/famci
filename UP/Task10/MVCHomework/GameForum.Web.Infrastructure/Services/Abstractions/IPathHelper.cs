namespace GameForum.Web.Infrastructure.Services.Abstractions
{
    public interface IPathHelper
    {
        string ResolvePathToLog(string requestPath);
        string MapPathToGame(string path, string gameKey);
    }
}
