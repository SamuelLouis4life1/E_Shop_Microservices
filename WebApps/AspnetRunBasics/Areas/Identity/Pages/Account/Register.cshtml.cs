﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AspnetRunBasics.Models.Authenticate;
using AspnetRunBasics.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RestSharp;

namespace AspnetRunBasics.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IUserService _userService;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender, 
            IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _userService = userService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }


            [Required]
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string StreetAddress { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string PostalCode { get; set; }
            public string PhoneNumber { get; set; }
            public int? CompanyId { get; set; }
            public string Role { get; set; }

            public IEnumerable<SelectListItem> RoleList { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }



        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {



                var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();
                var createCustomer = new RestClient(config.GetSection("ApiSettings:AuthenticationUrl").Value);
                var request = new RestRequest(("api/v1/Users/register"), Method.Post);
                //https://api-mvno-qld.telecall.com.br/customer
                //request.AddHeader("Authorization", "Bearer " + token);

                request.AddBody(new RegisterRequestModel
                {
                    UserName = Input.Email,
                    LastName = Input.LastName,
                    Password = Input.Password
                });

                //request.AddJsonBody(customerMnvo);
                var httpPost = createCustomer.ExecuteAsync(request);

                httpPost.Status.ToString();
                httpPost.Result.ToString();
                bool isSuccessfull = httpPost.Result.IsSuccessful;
                httpPost.IsCompletedSuccessfully.ToString();

                //customerIdTelecall = httpPost.Data.Data;

                //if (httpPost.Data.Success == "True")
                //{
                //    Console.WriteLine("Customer created sucessfully: " + customerIdTelecall);
                //}
                //else
                //{
                //    Console.WriteLine("Unable to create Customer: " + httpPost.Data.Message);
                //}


                var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }








    //public async Task<IActionResult> OnPostAsync(RegisterRequestModel registerRequest )
    //    {
    //        //returnUrl ??= Url.Content("~/");
    //        ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

    //        //var registerUser = await _userService.Register(registerRequest);
    //        if (ModelState.IsValid)
    //        {

    //            var client = new RestClient("https://localhost:5009/api/v1//Users/register");
    //            var request = new RestRequest("api/item/", Method.Post);
    //            request.RequestFormat = DataFormat.Json;
    //            request.AddBody(new RegisterRequestModel
    //            {
    //                FirstName = Input.Email,
    //                LastName = Input.FirstName,
    //            });
    //            client.ExecuteAsync(request);


    //            var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };
    //            var result = await _userManager.CreateAsync(user, Input.Password);
    //            if (result.Succeeded)
    //            {
    //                _logger.LogInformation("User created a new account with password.");

    //                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
    //                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
    //                //var callbackUrl = Url.Page(
    //                //    "/Account/ConfirmEmail",
    //                //    pageHandler: null,
    //                //    //values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
    //                //    protocol: Request.Scheme);

    //                //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
    //                //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

    //                if (_userManager.Options.SignIn.RequireConfirmedAccount)
    //                {
    //                    //return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
    //                }
    //                else
    //                {
    //                    await _signInManager.SignInAsync(user, isPersistent: false);
    //                    //return LocalRedirect(returnUrl);
    //                }
    //            }
    //            foreach (var error in result.Errors)
    //            {
    //                ModelState.AddModelError(string.Empty, error.Description);
    //            }
    //        }

    //        // If we got this far, something failed, redisplay form
    //        return Page();
    //    }
    //}
}
