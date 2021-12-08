using System;

namespace Server.ViewModels.PostViewModels
{
    public class PostCommentView
    {
        public string Text { get; set; }

        public Guid UserId { get; set; }
    }
}