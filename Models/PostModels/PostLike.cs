using System;
using Server.Models.UserModels;

namespace Server.Models.PostModels
{
    public class PostLike : BaseModel
    {
        public Guid UserId { get; set; }

        public User User { get; set;}
        
        public Guid PostId { get; set; }

        public Post Post { get; set;}
    }
}