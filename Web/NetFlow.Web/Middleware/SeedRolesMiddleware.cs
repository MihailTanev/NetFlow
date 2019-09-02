namespace NetFlow.Web.Middleware
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using NetFlow.Common.GlobalConstants;
    using NetFlow.Data;
    using NetFlow.Data.Models;

    public class SeedRolesMiddleware
    {
        private readonly RequestDelegate next;

        public SeedRolesMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            var dbContext = serviceProvider.GetService<NetFlowDbContext>();

            if (!dbContext.Roles.Any())
            {
                await this.SeedDatabaseWithRoles(userManager, roleManager);
            }

            await this.next(context);
        }

        private async Task SeedDatabaseWithRoles(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(RoleConstants.ADMIN_ROLE));
            await roleManager.CreateAsync(new IdentityRole(RoleConstants.TEACHER_ROLE));
            await roleManager.CreateAsync(new IdentityRole(RoleConstants.PUBLISHER_ROLE));
            await roleManager.CreateAsync(new IdentityRole(RoleConstants.STUDENT_ROLE));

            var adminUserName = RoleConstants.ADMIN_ROLE;
            var publisherUserName = RoleConstants.PUBLISHER_ROLE;
            var studentUserName = RoleConstants.STUDENT_ROLE;
            var teacherUserName = RoleConstants.TEACHER_ROLE;

            var admin = await userManager.FindByNameAsync(adminUserName);

            if (admin == null)
            {
                admin = new User
                {
                    Email = "admin@user.com",
                    UserName = adminUserName,
                };

                await userManager.CreateAsync(admin, "admin123");

                await userManager.AddToRoleAsync(admin, adminUserName);
            }

            var publisher = await userManager.FindByNameAsync(publisherUserName);

            if (publisher == null)
            {
                publisher = new User
                {
                    Email = "publisher@user.com",
                    UserName = publisherUserName,
                };

                await userManager.CreateAsync(publisher, "publisher123");

                await userManager.AddToRoleAsync(publisher, publisherUserName);
            }

            var student = await userManager.FindByNameAsync(studentUserName);

            if (student == null)
            {
                student = new User
                {
                    Email = "student@user.com",
                    UserName = studentUserName,
                };

                await userManager.CreateAsync(student, "student123");

                await userManager.AddToRoleAsync(student, studentUserName);
            }

            var teacher = await userManager.FindByNameAsync(teacherUserName);

            if (teacher == null)
            {
                teacher = new User
                {
                    Email = "teacher@user.com",
                    UserName = teacherUserName,
                };

                await userManager.CreateAsync(teacher, "teacher123");

                await userManager.AddToRoleAsync(teacher, teacherUserName);
            }
        }
    }
}
