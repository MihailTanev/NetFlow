namespace NetFlow.Services.Users
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using NetFlow.Data;
    using NetFlow.Services.Users.Interface;
    using NetFlow.Services.Users.Models;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using AutoMapper.QueryableExtensions;

    public class UserService : IUserService
    {
        private readonly NetFlowDbContext context;

        public UserService(NetFlowDbContext context)
        {
            this.context = context;
        }      

        public async Task<IEnumerable<UserServiceModel>> GetAllUsers()
        {
            var users = await this.context.Users
                .OrderBy(x => x.UserName)
                .ProjectTo<UserServiceModel>()
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
                .ProjectTo<UserServiceModel>()    
                .FirstOrDefaultAsync();

            return userId;
        }

        public bool IsUsernameExist(string username)
        {
            return this.context.Users.Any(u => u.UserName == username);
        }
    }
}
