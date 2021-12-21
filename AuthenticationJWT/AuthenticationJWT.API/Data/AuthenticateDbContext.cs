using AuthenticationJWT.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationJWT.API.Data
{
    public class AuthenticateDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;


        public DbSet<User> Users { get; set; }

        public AuthenticateDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server database
            //options.UseSqlServer(Configuration.GetConnectionString("WebApiDatabase"));
        }

    }
}
