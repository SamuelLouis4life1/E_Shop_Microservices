using AuthenticationJWT.API.Entities;


namespace AuthenticationJWT.API.Repositories.Interfaces
{
    public interface IJwtRepository
    {
        public string GenerateToken(User user);
        public int? ValidateToken(string token);
    }
}
