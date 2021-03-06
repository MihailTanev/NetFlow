﻿namespace NetFlow.Web.ViewModels.Users
{
    using NetFlow.Common.GlobalConstants;
    using System.ComponentModel.DataAnnotations;

    public class CreateUserViewModel
    {
        [Display(Name = "User Role")]
        public string UserRole { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = UserConstants.PASSWORD_ERROR_MESSAGE, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = UserConstants.PASSWORD)]
        public string Password { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Display(Name = UserConstants.CONFIRM_PASSWORD)]
        [Compare(UserConstants.COMPARE_PASSWORD, ErrorMessage = UserConstants.PASSWORD_DO_NOT_MATCH)]
        public string ConfirmPassword { get; set; }
    }
}
