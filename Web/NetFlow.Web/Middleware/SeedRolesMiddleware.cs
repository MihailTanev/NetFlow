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
            await roleManager.CreateAsync(new IdentityRole(RoleConstants.USER_ROLE));
        }
    }
}
