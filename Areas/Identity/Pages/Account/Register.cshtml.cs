﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using crowsoftmvc.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using crowsoftmvc.Models;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;
using crowsoftmvc.Controllers;
using crowsoftmvc.Data;
using Microsoft.Extensions.Configuration;
// Updated by Charles 12 April 2019: This code is autogenerated, 
// but I added code to link the AspNetUser to the UserAccount record

namespace crowsoftmvc.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<CrowsoftUser> _signInManager;
        private readonly UserManager<CrowsoftUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private string _userRole;


        // Manually added for controller usage
        private readonly ApplicationDbContext _context;

        // Code added
        [BindProperty]
        public UserAccount userAccount { get; set; }

        public RegisterModel(
            UserManager<CrowsoftUser> userManager,
            SignInManager<CrowsoftUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
            userAccount = new UserAccount();
            userAccount.DateCreated = DateTime.Now;
            _userRole = "Client";
        }

        

        public IEnumerable<SelectListItem> CountyList = Counties.GetCounties();

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

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

        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {   
                
                var user = new CrowsoftUser { UserName = Input.Email, Email = Input.Email, PhoneNumber = userAccount.TelephoneNo };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    // Manually added by Charles - Link UserAccount to new User
                    UserAccountsController userAccountsController = new UserAccountsController(_context);
                    userAccount.EmailAddress = user.Email;
                    userAccount.TypeUser = "Client";
                    userAccount.Country = "Ireland";
                    userAccount.AspNetUserID = user.Id;
                    var addUser = await userAccountsController.Create(userAccount);
                    
                    var roleresult = await _userManager.AddToRoleAsync(user, _userRole);

                    // The following code is commented out - not in use for this project
                    //
                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.Page(
                    //    "/Account/ConfirmEmail",
                    //    pageHandler: null,
                    //    values: new { userId = user.Id, code = code },
                    //    protocol: Request.Scheme);
                    //
                    //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
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
}
