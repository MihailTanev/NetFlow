namespace NetFlow.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using NetFlow.Common.GlobalConstants;
    using NetFlow.Data.Models;
    using NetFlow.Services.Users.Interface;
    using NetFlow.Web.Areas.Administration.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UsersController : AdminController
    {
        private readonly IUserService userService;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;

        public UsersController(IUserService userService, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            this.userService = userService;
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

        public async Task<IActionResult> Edit(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            EditUserViewModel model = new EditUserViewModel
            {
                Id = user.Id,
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                BirthDate = user.BirthDate
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, EditUserViewModel model)
        {
            var user = await this.userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.BirthDate = model.BirthDate;
            user.UserName = model.Username;

            if (ModelState.IsValid)
            {
                await this.userManager.UpdateAsync(user);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(model);
            }
        }


        public async Task<IActionResult> ChangeRole(string id)
        {
            var user = await this.userService.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            var roles = await this.roleManager
                .Roles
                .OrderBy(x => x.Name)
                .Select(role => new SelectListItem
                {
                    Text = role.Name,
                    Value = role.Name
                })
                .ToListAsync();

            var model = new ChangeUserRoleViewModel
            {
                User = user,
                Roles = roles
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddToRole(AddUserToRoleViewModel addToRole)
        {
            var user = await this.userManager.FindByIdAsync(addToRole.UserId);

            if (ModelState.IsValid)
            {
                await this.userManager.AddToRoleAsync(user, addToRole.RoleName);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return this.View(addToRole);
            }
        }

        public IActionResult AddUser()
        {
            ViewBag.Name = new SelectList(roleManager.Roles.Select(u => u.Name)
                                                        .ToList());
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(CreateUserViewModel addUser)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(addUser);
            }           

            var user = new User
            {                
                FirstName = addUser.FirstName,
                LastName = addUser.LastName,
                UserName = addUser.Username,
                Email = addUser.Email,                
            };          

            var result = await this.userManager.CreateAsync(user, addUser.Password);      

            if (result.Succeeded)
            {
                await this.userManager.AddToRoleAsync(user, addUser.UserRole);

                return this.RedirectToAction("Index", "Users", new { area = AreaConstants.ADMINISTRATION_AREA });
            }
            else
            {
                return this.View(addUser);
            }
        }    
    }
}