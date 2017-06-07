using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using GameForum.DAL.DAO;
using GameForum.DAL.Repositories.Abstractions;

namespace GameForum.DAL.Repositories
{
    public class GameRepository : IGameRepository
    {
        private GameContext context = new GameContext();

        public IEnumerable<GameItem> GetAllGames()
        {
            return this.context.Games.AsEnumerable();
        }

        public void CreateNewGame(GameItem newGame)
        {
            this.context.Games.Add(newGame);
        }

        public void EditGame(GameItem editedGame)
        {
            context.Entry(editedGame).State = EntityState.Modified;
        }

        public void DeleteGame(string key)
        {
            GameItem game = this.context.Games.Find(key);
            this.context.Games.Remove(game);
        }

        public GameItem GetGameByKey(string key)
        {
            return context.Games.Find(key);
        }

        public bool IsGameExist(string key)
        {
            return this.context.Games.Any(x => x.Key.Equals(key));
        }

        public void Save()
        {
            try
            {
                this.context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var s = ex.EntityValidationErrors.ElementAt(0).ValidationErrors;
            }
        }
    }
}
