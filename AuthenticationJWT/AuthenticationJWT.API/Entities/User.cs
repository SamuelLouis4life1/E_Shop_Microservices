using System.Text.Json.Serialization;


namespace AuthenticationJWT.API.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; }
    }
}
