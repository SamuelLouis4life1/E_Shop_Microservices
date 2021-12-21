using AuthenticationJWT.API.Authorization;
using AuthenticationJWT.API.Helpers;
using AuthenticationJWT.API.Models;
using AuthenticationJWT.API.Repositories;
using AuthenticationJWT.API.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AuthenticationJWT.API.Authorization.AllowAnonymousAttribute;

namespace AuthenticationJWT.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserInterface _userIterface;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UsersController(IUserInterface userIterface, IMapper mapper, AppSettings appSettings)
        {
            _userIterface = userIterface;
            _mapper = mapper;
            _appSettings = appSettings;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userIterface.Authenticate(model);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register(RegisterRequest model)
        {
            _userIterface.Register(model);
            return Ok(new { message = "Registration successful" });
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userIterface.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userIterface.GetById(id);
            return Ok(user);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateRequest model)
        {
            _userIterface.Update(id, model);
            return Ok(new { message = "User updated successfully" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userIterface.Delete(id);
            return Ok(new { message = "User deleted successfully" });
        }
    }
}
