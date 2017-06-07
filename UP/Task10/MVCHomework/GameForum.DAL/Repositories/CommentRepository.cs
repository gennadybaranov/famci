using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using GameForum.DAL.DAO;
using GameForum.DAL.Repositories.Abstractions;

namespace GameForum.DAL.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private GameContext context = new GameContext();

        public void AddCommentToGame(string key, string comment)
        {
            var commentItem = new CommentItem
            {
                Body = comment,
                GameKey = key,
            };
            this.context.Comments.Add(commentItem);
        }

        public IEnumerable<CommentItem> GetCommentsByGameKey(string key)
        {
            return this.context.Comments.Where(x => x.GameKey == key).AsEnumerable();
        }

        public void Save()
        {
            this.context.SaveChanges();
        }
    }
}
