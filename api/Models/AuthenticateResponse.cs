using WebApi.Entities;

namespace WebApi.Models
{
    public class AuthenticateResponse
    {
        public User User { get; set; }
        public string AccessToken { get; set; }


        public AuthenticateResponse(User user, string token)
        {
            User = user;
            AccessToken = token;
        }
    }
}