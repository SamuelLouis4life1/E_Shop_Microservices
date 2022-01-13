using System.ComponentModel.DataAnnotations;


namespace AuthenticationJWT.API.Models
{
    public class ResponsToken
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Key { get; set; }
    }
}
