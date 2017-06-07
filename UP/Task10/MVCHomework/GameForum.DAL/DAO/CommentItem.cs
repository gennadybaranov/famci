using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameForum.DAL.DAO
{
    public class CommentItem
    {
        [Key]
        public int CommentId { get; set; }
        public string Body { get; set; }

        public string GameKey { get; set; }
        [ForeignKey("GameKey")]
        public virtual GameItem Game { get; set; }
    }
}
