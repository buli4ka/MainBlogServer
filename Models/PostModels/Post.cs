using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Server.Models.UserModels;

namespace Server.Models.PostModels
{
    public class Post : BaseModel
    {
        public Post()
        {
            PostComments = new List<PostComment>();
            PostLikes = new List<PostLike>();
            PostImages = new List<PostImage>();
        }
        [MaxLength(50)] public string Title { get; set; }

        public string Text { get; set; }

        public Guid AuthorId { get; set; }

        public User Author { get; set; }

        
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public ICollection<PostComment> PostComments { get; set; }

        public ICollection<PostLike> PostLikes { get; set; }

        public ICollection<PostImage> PostImages { get; set; }
    }
}