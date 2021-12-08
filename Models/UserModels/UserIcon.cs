using System;

namespace Server.Models.UserModels
{
    public class UserIcon : BaseImageModel
    {
        public Guid UserId { get; set; }
        
        public User User { get; set; }
    }
}