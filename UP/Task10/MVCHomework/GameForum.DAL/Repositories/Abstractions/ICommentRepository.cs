using System.Collections.Generic;
using System.Linq;
using GameForum.DAL.DAO;

namespace GameForum.DAL.Repositories.Abstractions
{
    public interface ICommentRepository
    {
        void AddCommentToGame(string key, string comment);
        IEnumerable<CommentItem> GetCommentsByGameKey(string key);
        void Save();
    }
}
