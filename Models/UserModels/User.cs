using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Server.Models.PostModels;

namespace Server.Models.UserModels
{
    public class User : BaseModel
    {
        [MaxLength(50)] public string FirstName { get; set; }

        [MaxLength(50)] public string LastName { get; set; }

        public string Email { get; set; }

        [MaxLength(50)] public string Username { get; set; }

        [MaxLength(200)] public string Biography { get; set; }

        public bool IsPrivate { get; set; }

        public UserIcon UserIcon { get; set; }

        public ICollection<Post> Posts { get; set; }

        public ICollection<User> Subscribers { get; set; }

        public ICollection<User> Subscribed { get; set; }

        public ICollection<PostLike> LikedPosts { get; set; }

        [JsonIgnore] public ICollection<PostComment> CommentedPosts { get; set; }


        [JsonIgnore] public string HashedPassword { get; set; }
    }
}