using System.ComponentModel.DataAnnotations;

namespace Server.ViewModels.UserViewModels
{
    public class RegisterRequest
    {
        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string LastName { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string HashedPassword { get; set; }
    }
}