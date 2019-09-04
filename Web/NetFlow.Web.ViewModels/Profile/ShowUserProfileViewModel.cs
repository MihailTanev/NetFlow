using NetFlow.Services.Profile.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Web.ViewModels.Profile
{
    public class ShowUserProfileViewModel
    {
        public string UserId { get; set; }

        public UserProfileServiceModel Profile { get; set; }
    }
}
