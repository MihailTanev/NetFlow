namespace NetFlow.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using NetFlow.Common.GlobalConstants;
    using NetFlow.Common.Messages.User;
    using NetFlow.Data.Models;
    using NetFlow.Services.Administrator.Interface;
    using NetFlow.Web.ViewModels.Users;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using X.PagedList;

    public class UsersController : BaseAdminController
    {
        private readonly IAdministratorService adminService;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;

        public UsersController(IAdministratorService adminService, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            this.adminService = adminService;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index(int? page, string searchString)
        {
            var users = await this.adminService.GetAllUsers();

            var pageNumber = page ?? 1;

            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(s => s.Username.ToLower().Contains(searchString.ToLower()));
            }

            var model = new UsersViewModel
            {
                Users = await users.ToPagedListAsync(pageNumber, 10)
            };

            return this.View(model);
        }

        public IActionResult Add()
        {
            ViewBag.Name = new SelectList(roleManager.Roles.Select(u => u.Name)
                                                        .ToList());
            return this.View();
        }


        [HttpPost]
        public async Task<IActionResult> Add(CreateUserViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Username,
                Email = model.Email,
                CreatedOn = DateTime.UtcNow,
            };

            var result = await this.userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await this.userManager.AddToRoleAsync(user, model.UserRole);

                this.TempData[UserMessagesConstants.TEMPDATA_SUCCESS_MESSAGE] = $" '{user.UserName}' {UserMessagesConstants.USER_WAS_CREATED} ";

                return this.RedirectToAction(nameof(Index), new { area = AreaConstants.ADMINISTRATION_AREA });
            }
            else
            {
                this.TempData[UserMessagesConstants.TEMPDATA_ERROR_MESSAGE] = $" '{user.UserName}' {UserMessagesConstants.USER_WAS_NOT_CREATED} ";

                return this.View(model);
            }
        }

        public async Task<IActionResult> Edit(string userId)
        {
            var user = await this.userManager.FindByIdAsync(userId);

            if (user == null)
            {
                this.TempData[UserMessagesConstants.TEMPDATA_ERROR_MESSAGE] = $" '{user.UserName}' {UserMessagesConstants.USER_DOES_NOT_EXIST} ";

                return NotFound();
            }

            EditUserViewModel model = new EditUserViewModel
            {
                Id = user.Id,
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                BirthDate = user.BirthDate,
                Description = user.Description
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string userId, EditUserViewModel model)
        {
            var user = await this.userManager.FindByIdAsync(userId);

            if (user == null)
            {
                this.TempData[UserMessagesConstants.TEMPDATA_ERROR_MESSAGE] = $" '{user.UserName}' {UserMessagesConstants.USER_WAS_NOT_UPDATED} ";
                return this.View(model);
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.BirthDate = model.BirthDate;
            user.UserName = model.Username;
            user.Description = model.Description;

            if (ModelState.IsValid)
            {
                await this.userManager.UpdateAsync(user);

                this.TempData[UserMessagesConstants.TEMPDATA_SUCCESS_MESSAGE] = $" '{user.UserName}' {UserMessagesConstants.USER_WAS_UPDATED} ";

                return this.RedirectToAction(nameof(Index), new { area = AreaConstants.ADMINISTRATION_AREA });
            }
            else
            {
                return this.View(model);
            }
        }

        public async Task<IActionResult> ManageRole(string userId)
        {
            var user = await this.userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
          
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
                this.TempData[UserMessagesConstants.TEMPDATA_SUCCESS_MESSAGE] = $" '{model.Role}' {UserMessagesConstants.USER_ROLE_WAS_DELETED} ";

                return this.RedirectToAction(nameof(Index), new { area = AreaConstants.ADMINISTRATION_AREA });
            }
            else
            {
                return this.View(model);
            }
        }

        public async Task<IActionResult> Role(string userId)
        {
            var user = await this.adminService.GetUserById(userId);

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

            this.TempData[UserMessagesConstants.TEMPDATA_SUCCESS_MESSAGE] = $" '{model.Role}' {UserMessagesConstants.USER_ROLE_WAS_ADDED} ";

            return this.RedirectToAction(nameof(Index), new { area = AreaConstants.ADMINISTRATION_AREA });
        }

        public async Task<IActionResult> ChangePassword(string userId)
        {
            var user = await this.userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

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
        public async Task<IActionResult> ChangePassword(string userId, ChangePasswordViewModel model)
        {
            var user = await this.userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

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

        public async Task<IActionResult> Delete(string userId)
        {
            var user = await this.userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

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

        public async Task<IActionResult> DeleteUser(DeleteUserViewModel model, string userId)
        {
            var user = await this.userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return NotFound();
            }

            var result = await this.userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                this.TempData[UserMessagesConstants.TEMPDATA_SUCCESS_MESSAGE] = $" '{user.UserName}' {UserMessagesConstants.USER_HAS_BEEN_DELETED} ";

                return this.RedirectToAction(nameof(Index), new { area = AreaConstants.ADMINISTRATION_AREA });
            }
            else
            {
                return this.View(model);
            }
        }
    }
}