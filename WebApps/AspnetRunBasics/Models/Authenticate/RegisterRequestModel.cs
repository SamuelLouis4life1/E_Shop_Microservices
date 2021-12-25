using System.ComponentModel.DataAnnotations;


namespace AspnetRunBasics.Models.Authenticate
{
    public class RegisterRequestModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
