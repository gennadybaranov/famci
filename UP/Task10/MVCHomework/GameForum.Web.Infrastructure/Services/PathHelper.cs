using System;
using System.IO;
using GameForum.Web.Infrastructure.Services.Abstractions;

namespace GameForum.Web.Infrastructure.Services
{
    public class PathHelper : IPathHelper
    {
        private readonly IConfigurationManager configurationManager;
        private const string FILE_NAME = "IpLog";
        private const string APP_ROOT = "AppRoot";

        public PathHelper(IConfigurationManager configurationManager)
        {
            if (configurationManager == null)
            {
                throw new ArgumentNullException("IConfigurationManager is null");
            }

            this.configurationManager = configurationManager;
        }

        public string ResolvePathToLog(string requestPath)
        {
            string fileName = this.configurationManager.AppSettings[FILE_NAME];
            int index = this.GetLastRootOccurance(requestPath);

            string filePath = Path.Combine(requestPath.Substring(0, index), fileName);
            BuildPath(filePath);
            return filePath;
        }

        public string MapPathToGame(string path, string gameKey)
        {
            int index = this.GetLastRootOccurance(path);
            string filePath = Path.Combine(path.Substring(0, index), @"GamesCollection\Example.txt");
            return filePath;
        }

        private int GetLastRootOccurance(string path)
        {
            string root = this.configurationManager.AppSettings[APP_ROOT];
            int index = path.LastIndexOf(root, StringComparison.InvariantCultureIgnoreCase);
            if (index < 0)
            {
                throw new FileNotFoundException("Cant find game");
            }

            return index + root.Length;
        }

        private void BuildPath(string path)
        {
            string[] paths = path.Split(new[] { '\\' });
            string curPath = paths[0];
            for (int i = 1; i < paths.Length - 1; i++)
            {
                string item = paths[i];
                curPath = string.Format("{0}\\{1}", curPath, item);
                if (!Directory.Exists(curPath))
                {
                    Directory.CreateDirectory(curPath);
                }
            }
        }
    }
}
