using System;
using System.ComponentModel.DataAnnotations;


namespace AuthenticationJWT.API.Models
{
    public class RegisterRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
