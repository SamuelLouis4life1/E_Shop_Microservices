using AuthenticationJWT.API.Data;
using AuthenticationJWT.API.Entities;
using AuthenticationJWT.API.Models;
using AuthenticationJWT.API.Repositories.Interfaces;
using AutoMapper;
using BCryptNet = BCrypt.Net.BCrypt;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;


namespace AuthenticationJWT.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private AuthenticateDbContext _context;
        private IJwtRepository _jwtUtils;
        private readonly IMapper _mapper;

        public UserRepository(AuthenticateDbContext context, IJwtRepository jwtUtils, IMapper mapper)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _context.Users.SingleOrDefault(x => x.UserName == model.UserName);

            // validate
            if (user == null || !BCryptNet.Verify(model.Password, user.PasswordHash))
                throw new AuthenticationException("Username or password is incorrect");

            // authentication successful
            var response = _mapper.Map<AuthenticateResponse>(user);
            response.JwtToken = _jwtUtils.GenerateToken(user);
            return response;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User GetById(int id)
        {
            return getUser(id);
        }

        public void Register(RegisterRequest model)
        {
            // validate
            if (_context.Users.Any(x => x.UserName == model.UserName))
                throw new AuthenticationException("Username '" + model.UserName + "' is already taken");

            // map model to new user object
            var user = _mapper.Map<User>(model);

            // hash password
            user.PasswordHash = BCryptNet.HashPassword(model.Password);

            // save user
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Update(int id, UpdateRequest model)
        {
            var user = getUser(id);

            // validate
            if (model.UserName != user.UserName && _context.Users.Any(x => x.UserName == model.UserName))
                throw new AuthenticationException("Username '" + model.UserName + "' is already taken");

            // hash password if it was entered
            if (!string.IsNullOrEmpty(model.Password))
                user.PasswordHash = BCryptNet.HashPassword(model.Password);

            // copy model to user and save
            _mapper.Map(model, user);
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = getUser(id);
            _context.Users.Remove(user);
            _context.SaveChanges(); ;
        }

        private User getUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }
    }
}
