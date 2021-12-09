using System;

namespace Server.ViewModels.PostViewModels
{
    public class PostLikeView
    {
        public Guid PostId { get; set; }
    }

    public class UserLikeView
    {
        public Guid UserId { get; set; }
    }

    public class AddLike
    {
        public Guid UserId { get; set; }
        
        public Guid PostId { get; set; }
    }
}