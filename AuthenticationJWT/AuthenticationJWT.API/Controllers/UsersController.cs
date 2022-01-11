using AuthenticationJWT.API.Authorization;
using AuthenticationJWT.API.Helpers;
using AuthenticationJWT.API.Models;
using AuthenticationJWT.API.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;

namespace AuthenticationJWT.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserRepository _userService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UsersController(IUserRepository userService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register(RegisterRequest model)
        {
            _userService.Register(model);
            return Ok(new { message = "Registration successful" });
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("{userId}")]
        public IActionResult GetById(Guid userId)
        {
            var user = _userService.GetById(userId);
            return Ok(user);
        }

        [HttpPut("{userId}")]
        public IActionResult Update(Guid userId, UpdateRequest model)
        {
            _userService.Update(userId, model);
            return Ok(new { message = "User updated successfully" });
        }

        [HttpDelete("{userId}")]
        public IActionResult Delete(Guid userId)
        {
            _userService.Delete(userId);
            return Ok(new { message = "User deleted successfully" });
        }


        [HttpPost("Generatetoken")]
        public IActionResult GenerateToken(RegisterRequest model)
        {
            _userService.Register(model);
            return Ok(new { message = "Registration successful" });
        }

    }
}
