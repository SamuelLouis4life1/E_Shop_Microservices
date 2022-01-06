using System.ComponentModel.DataAnnotations;


namespace Shopping.Aggregator.Models.Authenticate
{
    public class AuthenticateRequestModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
