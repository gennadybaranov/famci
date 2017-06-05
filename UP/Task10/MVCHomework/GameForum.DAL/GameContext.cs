using System.Data.Entity;
using GameForum.DAL.DAO;

namespace GameForum.DAL
{
    public class GameContext : DbContext
    {
        public GameContext()
        {
            Database.SetInitializer(new CustomEntitiesContextInitializer());
        }

        public DbSet<GameItem> Games { get; set; }
        public DbSet<CommentItem> Comments { get; set; }
        public DbSet<GenreItem> Genres { get; set; }
    }
}
