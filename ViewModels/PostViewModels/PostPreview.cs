using System;
using System.Collections.Generic;
using Server.Models;

namespace Server.ViewModels.PostViewModels
{
    public class PostPreview : BaseModel
    {
        public Guid AuthorId { get; set; }
        
        public string Title { get; set; }

        public string Text { get; set; }

        public string PreviewImage { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public int QuantityOfComments { get; set; }

        public ICollection<UserLikeView> PostLikes { get; set; }
    }
}