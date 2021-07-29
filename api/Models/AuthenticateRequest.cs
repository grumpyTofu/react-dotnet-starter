using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class AuthenticateRequest
    {
        [Required]
        public string IdToken { get; set; }
    }
}