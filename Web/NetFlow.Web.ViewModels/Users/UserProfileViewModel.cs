namespace NetFlow.Web.ViewModels.Users
{
    using NetFlow.Services.Profile.Models;

    public class UserProfileViewModel
    {
        public string UserId { get; set; }

        public UserProfileServiceModel Profile { get; set; }
    }
}
