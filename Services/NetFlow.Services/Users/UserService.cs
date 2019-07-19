namespace NetFlow.Services.Users
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using NetFlow.Data;
    using NetFlow.Data.Models;
    using NetFlow.Services.Users.Interface;
    using NetFlow.Services.Users.Models;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Stopify.Services.Mapping;

    public class UserService : BaseService, IUserService
    {
        public UserService(NetFlowDbContext context, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
            : base(context, mapper, userManager, signInManager)
        {
        }

        public async Task<IEnumerable<UserServiceModel>> GetAllUsers()
        {
            var users = await this.context.Users
                .OrderByDescending(x => x.LastName)
                .To<UserServiceModel>()
                .ToListAsync();

            return users;

        }

        public async Task<int> GetTotalUsers()
        {
            var totalUser = await this.context.Users.CountAsync();

            return totalUser;
        }

        public async Task<UserServiceModel> GetUserById(string id)
        {
            var userId = await this.context.Users
                .Where(u => u.Id == id)
                .To<UserServiceModel>()    
                .FirstOrDefaultAsync();

            return userId;
        }
    }
}
