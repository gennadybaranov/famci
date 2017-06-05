namespace GameForum.DomainModel.Domain
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Body { get; set; }

        public string GameKey { get; set; }
    }
}
