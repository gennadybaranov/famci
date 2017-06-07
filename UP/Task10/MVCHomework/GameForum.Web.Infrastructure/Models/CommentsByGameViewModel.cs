using System.ComponentModel.DataAnnotations;

namespace GameForum.Web.Infrastructure.Models
{
    public class CommentsByGameViewModel
    {
        [Required]
        public string GameKey { get; set; }
    }
}
