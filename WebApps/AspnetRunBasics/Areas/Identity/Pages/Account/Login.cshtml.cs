using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using AspnetRunBasics.Services.Interfaces;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using RestSharp;
using AspnetRunBasics.Models.Authenticate;

namespace AspnetRunBasics.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly IUserService _userService;
        private readonly HttpClient _httpClient;


        public LoginModel(SignInManager<IdentityUser> signInManager, 
            ILogger<LoginModel> logger,
            IUserService userService,
            HttpClient httpClient,
            UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _userService = userService;
            _httpClient = httpClient;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true


                var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();
                var generateToken = new RestClient(config.GetSection("ApiSettings:AuthenticationUrl").Value);
                var request = new RestRequest(("api/v1/Users/authenticate"), Method.Post);
                var newRequest = new
                {
                    username = Input.Email,
                    password = Input.Password
                };
                request.AddJsonBody(newRequest);
                var httpPost = generateToken.ExecuteAsync<AuthenticateResponse>(request);

                httpPost.Result.ToString();
                bool isSuccessful = httpPost.Result.IsSuccessful;
                httpPost.IsCompletedSuccessfully.ToString();
                httpPost.Status.ToString();


                //if (!httpPost.Status .IsSuccessful)
                //    Console.WriteLine("Not able to generate Access Token, Invalid username or password: " + httpPost.StatusCode + httpPost.ErrorMessage);






                //var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                //if (result.Succeeded)
                //{
                //    //var user = _userService.Authenticate.GetFirstOrDefault(u => u.Email == Input.Email);

                //    //int count = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == user.Id).Count();
                //    //HttpContext.Session.SetInt32(SD.ssShoppingCart, count);

                //    _logger.LogInformation("User logged in.");
                //    return LocalRedirect(returnUrl);
                //}
                //if (result.RequiresTwoFactor)
                //{
                //    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                //}
                //if (result.IsLockedOut)
                //{
                //    _logger.LogWarning("User account locked out.");
                //    return RedirectToPage("./Lockout");
                //}
                //else
                //{
                //    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                //    return Page();
                //}

                return Page();

            }

            // If we got this far, something failed, redisplay form

            //return httpPost.Data.Token;

            return Page();
        }


    }
}
