using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using NetFlow.Common.GlobalConstants;
using NetFlow.Data.Models;

namespace NetFlow.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = UserConstants.REGISTER_USER_FIRST_NAME)]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = UserConstants.REGISTER_USER_LAST_NAME)]
            [DataType(DataType.Text)]
            public string LastName { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [MinLength(UserConstants.MIN_USERNAME_LENGTH, ErrorMessage =UserConstants.USERNAME_LENGHT_ERROR_MESSAGE)]
            [MaxLength(UserConstants.MAX_USERNAME_LENGTH)]
            [RegularExpression(UserConstants.REGEX_USERNAME)]
            public string Username { get; set; }

            [Required]
            [Display(Name = UserConstants.REGISTER_USER_BIRTHDAY)]
            [DataType(DataType.Date)]
            public DateTime Birthdate { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = UserConstants.EMAIL)]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = UserConstants.PASSWORD_ERROR_MESSAGE, MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = UserConstants.PASSWORD)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = UserConstants.CONFIRM_PASSWORD)]
            [Compare(UserConstants.COMPARE_PASSWORD, ErrorMessage = UserConstants.PASSWORD_DO_NOT_MATCH)]
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
                var user = new User
                {
                    UserName = this.Input.Username,
                    FirstName = this.Input.FirstName,
                    LastName = this.Input.LastName,
                    BirthDate = this.Input.Birthdate,
                    Email = this.Input.Email,
                    CreatedOn = DateTime.UtcNow,
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

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
