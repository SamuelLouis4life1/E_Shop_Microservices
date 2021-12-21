using System.ComponentModel.DataAnnotations;


namespace AuthenticationJWT.API.Models
{
    public class RegisterRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
