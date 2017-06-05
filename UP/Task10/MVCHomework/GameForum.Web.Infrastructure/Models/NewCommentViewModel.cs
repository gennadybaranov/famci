using System.ComponentModel.DataAnnotations;

namespace GameForum.Web.Infrastructure.Models
{
    public class NewCommentViewModel
    {
        [Required]
        public string Comment { get; set; }
    }
}
