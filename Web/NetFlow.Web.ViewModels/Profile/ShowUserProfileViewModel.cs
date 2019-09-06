namespace NetFlow.Web.ViewModels.Profile
{
    using NetFlow.Services.Profile.Models;

    public class ShowUserProfileViewModel
    {
        public string UserId { get; set; }

        public UserProfileServiceModel Profile { get; set; }
    }
}
