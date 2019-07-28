namespace NetFlow.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using NetFlow.Common.GlobalConstants;
    using NetFlow.Data.Models;
    using NetFlow.Services.Users.Interface;
    using NetFlow.Web.ViewModels.Users;
    using System;
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
                CreatedOn = DateTime.UtcNow,
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

        public async Task<IActionResult> ManageRole(string id)
        {
            var user = await this.userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
           
            var currentUserRole = await this.userManager.GetRolesAsync(user);

            var roles = currentUserRole
                .Select(role => new SelectListItem
                {
                    Text = role,
                    Value = role
                })
                .ToList();

            var model = new RoleViewModel
            {
                Id = user.Id,
                Username = user.UserName,
                RoleList = roles,
                Roles = await this.userManager.GetRolesAsync(user),
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(DeleteUserRoleViewModel model)
        {
            var user = await this.userManager.FindByIdAsync(model.UserId);           

            if (!ModelState.IsValid)
            {
                return this.RedirectToAction("Index", "Users", new { area = AreaConstants.ADMINISTRATION_AREA });
            }

            var result = await this.userManager.RemoveFromRoleAsync(user, model.Role);

            if (result.Succeeded)
            {
                return this.RedirectToAction("Index", "Users", new { area = AreaConstants.ADMINISTRATION_AREA });
            }
            else
            {
                return View(model);
            }
        }

        public async Task<IActionResult> AddRole(string id)
        {
            var user = await this.userService.GetUserById(id);           
          
            var roles = await this.roleManager
                .Roles
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
    }
}