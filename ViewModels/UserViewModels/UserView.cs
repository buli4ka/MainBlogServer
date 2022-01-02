using System.Collections.Generic;
using Server.Models;
using Server.ViewModels.PostViewModels;

namespace Server.ViewModels.UserViewModels
{
    public class UserView : BaseModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public string Email { get; set; }

        public string Username { get; set; }

        public string IconUrl { get; set; }
        
        public bool isPrivate { get; set; }

        public string Biography { get; set; }
    }
}