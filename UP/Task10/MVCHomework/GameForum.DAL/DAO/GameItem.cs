using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameForum.DAL.DAO
{
    public class GameItem
    {
        [Key]
        public string Key { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int GenreId { get; set; }
        [ForeignKey("GenreId")]
        public virtual GenreItem Genre { get; set; }

        public int AgeRestriction { get; set; }

        public virtual ICollection<CommentItem> Comments { get; set; }
    }
}
