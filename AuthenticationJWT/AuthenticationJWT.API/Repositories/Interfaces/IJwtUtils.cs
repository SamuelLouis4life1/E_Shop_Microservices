using AuthenticationJWT.API.Entities;


namespace AuthenticationJWT.API.Repositories.Interfaces
{
    public interface IJwtUtils
    {
        public string GenerateToken(User user);
        public int? ValidateToken(string token);
    }
}
