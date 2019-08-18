namespace NetFlow.Web.Areas.Profile.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using NetFlow.Common.GlobalConstants;
    using NetFlow.Data.Models;
    using NetFlow.Services.Profile.Interface;
    using NetFlow.Web.ViewModels.Users;
    using System.Threading.Tasks;

    [Area(AreaConstants.PROFILE_AREA)]
    public class UsersController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IProfileService profileService;

        public UsersController(UserManager<User>userManager, IProfileService profileService)
        {
            this.profileService = profileService;
            this.userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index(string username)
        {
            var user = await this.userManager.FindByNameAsync(username);

            if (user == null)
            {
                return NotFound();
            }

            var model = new UserProfileViewModel
            {
                UserId = user.Id,
                Profile = await this.profileService.GetProfileByIdAsync(user.Id)
            };

            return this.View(model);
        }
    }
}