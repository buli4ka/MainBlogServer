using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Server.Models;
using Server.ViewModels.UserViewModels;

namespace Server.ViewModels.PostViewModels
{
    public class PostView : BaseModel
    {
        public string Title { get; set; }
        
        public string Text { get; set; }

        public ICollection<string> ImageUrls { get; set; }
        
        public AuthorPreview Author { get; set; }
        
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
        
        public ICollection<PostCommentView> PostComments {get; set; }
        
        public ICollection<UserLikeView> PostLikes {get; set; }

    }
    
    public class CreateUpdatePost : BaseModel
    {
        [Required] public string Title { get; set; }
        [Required] public string Text { get; set; }

        [Required] public Guid AuthorId { get; set; }
    }
}