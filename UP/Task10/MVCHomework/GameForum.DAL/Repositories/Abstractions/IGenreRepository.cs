using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameForum.DAL.DAO;

namespace GameForum.DAL.Repositories.Abstractions
{
    public interface IGenreRepository
    {
        IEnumerable<GenreItem> GetAllGenres();
    }
}
