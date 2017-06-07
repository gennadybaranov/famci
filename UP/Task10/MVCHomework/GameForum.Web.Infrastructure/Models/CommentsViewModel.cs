using System.Collections.Generic;

namespace GameForum.Web.Infrastructure.Models
{
    public class CommentsViewModel
    {
        public IEnumerable<CommentViewModel> Comments { get; set; }
    }
}
