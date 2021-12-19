using System.Collections.Generic;
using Server.Models;
using Server.ViewModels.PostViewModels;

namespace Server.ViewModels.UserViewModels
{
    public class UserView : BaseModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string IconUrl { get; set; }

        public string Biography { get; set; }

        public ICollection<PostPreview> Posts { get; set; }

        public ICollection<PostLikeView> LikedPosts { get; set; }
        
        public ICollection<PostPreview> PostsLiked { get; set; }

        

        public int QuantityOfSubscribers { get; set; }

        public int QuantityOfSubscribed { get; set; }
    }
}