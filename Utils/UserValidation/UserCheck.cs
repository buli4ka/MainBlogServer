using Server.Models.UserModels;
using Server.ViewModels.UserViewModels;

namespace Server.Utils.UserValidation
{
    public static class UserCheck
    {
        public static bool IsUserValid(User user)
        {
            return user.FirstName.Length <= 50 && user.LastName.Length <= 50 && user.Username.Length <= 50 ;
        }
        

        public static string HashPassword(string password)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            return hashedPassword;
        }

        public static bool ValidatePassword(string passwordToCheck, string userPassword)
        {
            return BCrypt.Net.BCrypt.Verify(passwordToCheck, userPassword);
        }
    }
}