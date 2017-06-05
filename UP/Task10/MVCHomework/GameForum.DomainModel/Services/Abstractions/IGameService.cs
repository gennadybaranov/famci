using System.Collections.Generic;
using GameForum.DomainModel.Domain;

namespace GameForum.DomainModel.Services.Abstractions
{
    public interface IGameService
    {
        IEnumerable<Game> GetAllGames();
        void CreateNewGame(Game newGame);
        void EditGame(Game editedGame);
        void DeleteGame(string key);
        Game GetGameByKey(string key);
        bool IsGameExist(string key);
        IEnumerable<Genre> GetAllGenres();

        void AddCommentToGame(string key, string comment);
        IEnumerable<Comment> GetCommentsByGameKey(string key);
    }
}
