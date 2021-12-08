namespace Server.ViewModels.UserViewModels
{
    public class UpdateRequest
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Biography { get; set; }

        public bool IsPrivate { get; set; }
        public string Username { get; set; }
        
        public string Email { get; set; }
        
        public string HashedPassword { get; set; }
    }
}