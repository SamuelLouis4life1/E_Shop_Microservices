using AuthenticationJWT.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace AuthenticationJWT.API.Data
{
    public class AuthenticateDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public AuthenticateDbContext(DbContextOptions<AuthenticateDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // connect to sql server database
            //options.UseSqlServer(Configuration.GetConnectionString("ApplicationConnectionString"));
        }

        public DbSet<User> Users { get; set; }

    }
}
