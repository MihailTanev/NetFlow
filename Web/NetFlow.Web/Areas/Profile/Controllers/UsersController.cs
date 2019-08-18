namespace NetFlow.Web.Areas.Profile.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using NetFlow.Common.GlobalConstants;
    using NetFlow.Data.Models;
    using NetFlow.Services.PdfCreator;
    using NetFlow.Services.Profile.Interface;
    using NetFlow.Web.ViewModels.Users;
    using System.Threading.Tasks;

    [Area(AreaConstants.PROFILE_AREA)]
    public class UsersController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IProfileService profileService;
        private readonly IPdfCreatorService pdfCreatorService;

        public UsersController(UserManager<User>userManager, IProfileService profileService, IPdfCreatorService pdfCreatorService)
        {
            this.pdfCreatorService = pdfCreatorService;
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

        [Authorize]
        public async Task<IActionResult> DownloadCertificate(int id)
        {
            var userId = this.userManager.GetUserId(User);

            var certificate = await this.pdfCreatorService.GetPdfCertificateAsync(userId, id);

            if (certificate == null)
            {
                return BadRequest();
            }

            return File(certificate, "application/pdf", "Certificate.pdf");
        }
    }
}