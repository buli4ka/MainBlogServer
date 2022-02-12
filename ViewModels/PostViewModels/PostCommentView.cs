using System;
using System.Collections.Generic;
using Server.Models;
using Server.ViewModels.UserViewModels;

namespace Server.ViewModels.PostViewModels
{
    public class PostCommentView : BaseModel
    {
        public string Text { get; set; }
        
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public Guid UserId { get; set; }
        
        public AuthorPreview User { get; set; }
        
        public bool IsSubComment { get; set; }


        public ICollection<PostCommentView> PostSubComments {get; set;}

    }

    public class PostSubCommentView
    {
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
        
        public string Text { get; set; }

        public Guid UserId { get; set; }
    }

    public class CreateUpdateCommentView 
    {
        public Guid? Id { get; set; }

        public string Text { get; set; }

        public Guid UserId { get; set; }
        
        public Guid PostId { get; set; }
        
        public Guid? MainCommentId { get; set; }


    }
}