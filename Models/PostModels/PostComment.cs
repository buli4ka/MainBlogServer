using System;
using System.Diagnostics.CodeAnalysis;
using Server.Models.UserModels;
using System.ComponentModel.DataAnnotations;
namespace Server.Models.PostModels
{
    public class PostComment : BaseModel
    {
        public string Text { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set;}
        
        public Guid PostId { get; set; }

        public Post Post { get; set;}
    }
}