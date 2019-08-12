namespace NetFlow.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using NetFlow.Common.GlobalConstants;
    using NetFlow.Common.Messages.User;
    using NetFlow.Data.Models;
    using NetFlow.Services.Users.Interface;
    using NetFlow.Web.ViewModels.Users;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class UsersController : BaseAdminController
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

            return this.View(users);
        }

        public IActionResult Add()
        {
            ViewBag.Name = new SelectList(roleManager.Roles.Select(u => u.Name)
                                                        .ToList());
            return this.View();
        }


        [HttpPost]
        public async Task<IActionResult> Add(CreateUserViewModel addUser)
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

                this.TempData[UserMessagesConstants.TEMPDATA_SUCCESS_MESSAGE] = UserMessagesConstants.USER_WAS_CREATED;

                return this.RedirectToAction(nameof(Index), new { area = AreaConstants.ADMINISTRATION_AREA });
            }
            else
            {
                this.TempData[UserMessagesConstants.TEMPDATA_ERROR_MESSAGE] = UserMessagesConstants.USER_WAS_NOT_CREATED;

                return this.View(addUser);
            }
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);

            if (user == null)
            {
                this.TempData[UserMessagesConstants.TEMPDATA_ERROR_MESSAGE] = UserMessagesConstants.USER_DOES_NOT_EXIST;

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

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, EditUserViewModel model)
        {
            var user = await this.userManager.FindByIdAsync(id);

            if (user == null)
            {
                this.TempData[UserMessagesConstants.TEMPDATA_ERROR_MESSAGE] = UserMessagesConstants.USER_WAS_NOT_UPDATED;
                return this.View(model);
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.BirthDate = model.BirthDate;
            user.UserName = model.Username;

            if (ModelState.IsValid)
            {
                await this.userManager.UpdateAsync(user);

                this.TempData[UserMessagesConstants.TEMPDATA_SUCCESS_MESSAGE] = UserMessagesConstants.USER_WAS_UPDATED;

                return this.RedirectToAction(nameof(Index), new { area = AreaConstants.ADMINISTRATION_AREA });
            }
            else
            {
                return this.View(model);
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
                Roles = currentUserRole,
                RoleList = roles
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(UserRoleViewModel model)
        {
            var user = await this.userManager.Users.FirstOrDefaultAsync(u => u.Id == model.UserId);

            bool roleExists = await this.roleManager.RoleExistsAsync(model.Role);

            if (!roleExists)
            {
                ModelState.AddModelError(string.Empty, "Invalid Idenity details.");
            }

            var result = await this.userManager.RemoveFromRoleAsync(user, model.Role);

            if (result.Succeeded)
            {
                this.TempData[UserMessagesConstants.TEMPDATA_SUCCESS_MESSAGE] = UserMessagesConstants.USER_ROLE_WAS_DELETED;

                return this.RedirectToAction(nameof(Index), new { area = AreaConstants.ADMINISTRATION_AREA });
            }
            else
            {
                return this.View(model);
            }
        }

        public async Task<IActionResult> Role(string id)
        {
            var user = await this.userService.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            var roles = await this.roleManager
                .Roles
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                })
                .ToListAsync();

            var model = new ChangeUserRoleViewModel
            {
                User = user,
                Roles = roles
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(UserRoleViewModel model)
        {
            var user = await this.userManager.Users.FirstOrDefaultAsync(u => u.Id == model.UserId);

            bool roleExists = await this.roleManager.RoleExistsAsync(model.Role);

            if (!roleExists)
            {
                return this.View(model);
            }
           
            await this.userManager.AddToRoleAsync(user, model.Role);

            this.TempData[UserMessagesConstants.TEMPDATA_SUCCESS_MESSAGE] = UserMessagesConstants.USER_ROLE_WAS_ADDED;

            return this.RedirectToAction(nameof(Index), new { area = AreaConstants.ADMINISTRATION_AREA });
        }

        public async Task<IActionResult> ChangePassword(string id)
        {
            var user = await this.userManager.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            var model = new ChangePasswordViewModel
            {
                Username = user.UserName,
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(string id, ChangePasswordViewModel model)
        {
            var user = await this.userManager.Users.FirstOrDefaultAsync(u => u.Id == id);

            var code = await this.userManager.GeneratePasswordResetTokenAsync(user);

            var result = await this.userManager.ResetPasswordAsync(user, code, model.Password);

            if (result.Succeeded)
            {
                this.TempData[UserMessagesConstants.TEMPDATA_SUCCESS_MESSAGE] = UserMessagesConstants.PASSWORD_HAS_BEEN_CHANGED;

                return this.RedirectToAction(nameof(Index) ,new { area = AreaConstants.ADMINISTRATION_AREA });
            }
            else
            {
                return this.View(model);
            }
        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = await this.userManager.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            var model = new DeleteUserViewModel
            {
                UserId = user.Id,                
                Username = user.UserName,
            };

            return this.View(model);
        }

        public async Task<IActionResult> DeleteUser(DeleteUserViewModel model, string id)
        {
            var user = await this.userManager.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            var result = await this.userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                this.TempData[UserMessagesConstants.TEMPDATA_SUCCESS_MESSAGE] = UserMessagesConstants.USER_HAS_BEEN_DELETED;

                return this.RedirectToAction(nameof(Index), new { area = AreaConstants.ADMINISTRATION_AREA });
            }
            else
            {
                return this.View(model);
            }
        }
    }
}