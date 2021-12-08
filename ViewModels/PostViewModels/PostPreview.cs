using System;
using Server.Models;

namespace Server.ViewModels.PostViewModels
{
    public class PostPreview : BaseModel
    {
        public string Title { get; set; }

        public string Text { get; set; }

        public string PreviewImage { get; set; }
        
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public int QuantityOfComments { get; set; }

        public int QuantityOfLikes { get; set; }
    }
}