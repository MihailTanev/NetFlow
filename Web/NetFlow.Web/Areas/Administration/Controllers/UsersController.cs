namespace NetFlow.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using NetFlow.Data.Models;
    using NetFlow.Services.Users.Interface;
    using NetFlow.Web.Areas.Administration.Models;
    using System.Threading.Tasks;

    public class UsersController : AdminController
    {
        private readonly IUserService userService;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;

        public UsersController(IUserService adminService, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            this.userService = adminService;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = new UsersViewModel
            {
                Users = await this.userService.GetAllUsers(),
            };

            return View(users);
        }
    }
}