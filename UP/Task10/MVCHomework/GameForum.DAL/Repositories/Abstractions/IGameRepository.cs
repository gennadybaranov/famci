using System.Collections.Generic;
using System.Linq;
using GameForum.DAL.DAO;

namespace GameForum.DAL.Repositories.Abstractions
{
    public interface IGameRepository
    {
        IEnumerable<GameItem> GetAllGames();
        void CreateNewGame(GameItem newGame);
        void EditGame(GameItem editedGame);
        void DeleteGame(string key);
        GameItem GetGameByKey(string key);
        bool IsGameExist(string key);
        void Save();
    }
}
