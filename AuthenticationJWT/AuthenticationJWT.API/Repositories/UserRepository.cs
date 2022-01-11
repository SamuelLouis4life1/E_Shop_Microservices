using AuthenticationJWT.API.Data;
using AuthenticationJWT.API.Entities;
using AuthenticationJWT.API.Models;
using AuthenticationJWT.API.Repositories.Interfaces;
using AutoMapper;
using BCryptNet = BCrypt.Net.BCrypt;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using AuthenticationJWT.API.Exceptions;
using System;

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
            var applicationUser = _context.ApplicationUsers.SingleOrDefault(x => x.UserName == model.UserName);

            // validate
            if (applicationUser == null || !BCryptNet.Verify(model.Password, applicationUser.PasswordHash))
                throw new AuthException("Username or password is incorrect");
            

            // authentication successful
            var response = _mapper.Map<AuthenticateResponse>(applicationUser);
            response.JwtToken = _jwtUtils.GenerateToken(applicationUser);
            return response;
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return _context.ApplicationUsers;
        }

        public ApplicationUser GetById(Guid UserId)
        {
            return getUser(UserId);
        }

        public void Register(RegisterRequest model)
        {
            // validate
            if (_context.ApplicationUsers.Any(x => x.UserName == model.UserName))
                throw new AuthException("Username '" + model.UserName + "' is already taken");

            // map model to new user object
            var user = _mapper.Map<ApplicationUser>(model);

            // hash password
            user.PasswordHash = BCryptNet.HashPassword(model.Password);

            // save user
            _context.ApplicationUsers.Add(user);
            _context.SaveChanges();
        }

        public void Update(Guid userId, UpdateRequest model)
        {
            var applicationUser = getUser(userId);

            // validate
            if (model.UserName != applicationUser.UserName && _context.ApplicationUsers.Any(x => x.UserName == model.UserName))
                throw new AuthException("Username '" + model.UserName + "' is already taken");

            // hash password if it was entered
            if (!string.IsNullOrEmpty(model.Password))
                applicationUser.PasswordHash = BCryptNet.HashPassword(model.Password);

            // copy model to user and save
            _mapper.Map(model, applicationUser);
            _context.ApplicationUsers.Update(applicationUser);
            _context.SaveChanges();
        }

        public void Delete(Guid UserId)
        {
            var user = getUser(UserId);
            _context.ApplicationUsers.Remove(user);
            _context.SaveChanges(); ;
        }

        private ApplicationUser getUser(Guid UserId)
        {
            var user = _context.ApplicationUsers.Find(UserId);
            if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }
    }
}
