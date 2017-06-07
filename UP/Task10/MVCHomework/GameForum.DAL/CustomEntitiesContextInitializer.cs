using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameForum.DAL.DAO;

namespace GameForum.DAL
{
    public class CustomEntitiesContextInitializer : DropCreateDatabaseIfModelChanges<GameContext>
    {
        protected override void Seed(GameContext context)
        {
            base.Seed(context);

            var genres = new List<string> { "Shooter", "RPG", "RTS", "For Kids" };

            foreach (var item in genres)
            {
                context.Genres.Add(new GenreItem { Genre = item});
            }

            context.SaveChanges();
        }
    }
}
