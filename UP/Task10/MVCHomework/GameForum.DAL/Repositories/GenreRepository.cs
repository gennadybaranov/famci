using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameForum.DAL.DAO;
using GameForum.DAL.Repositories.Abstractions;

namespace GameForum.DAL.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private GameContext context = new GameContext();

        public IEnumerable<GenreItem> GetAllGenres()
        {
            return this.context.Genres.AsEnumerable<GenreItem>();
        }
    }
}
