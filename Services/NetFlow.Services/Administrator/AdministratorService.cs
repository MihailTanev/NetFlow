namespace NetFlow.Services.Administrator
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using NetFlow.Data;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using AutoMapper.QueryableExtensions;
    using NetFlow.Services.Administrator.Interface;
    using NetFlow.Services.Administrator.Models;

    public class AdministratorService : IAdministratorService
    {
        private readonly NetFlowDbContext context;

        public AdministratorService(NetFlowDbContext context)
        {
            this.context = context;
        }      

        public async Task<IEnumerable<AdministratorServiceModel>> GetAllUsers()
        {
            var users = await this.context.Users
                .OrderBy(x => x.UserName)
                .ProjectTo<AdministratorServiceModel>()
                .ToListAsync();

            return users;

        }        

        public async Task<int> GetTotalUsers()
        {
            var totalUser = await this.context.Users.CountAsync();

            return totalUser;
        }

        public async Task<AdministratorServiceModel> GetUserById(string id)
        {
            var userId = await this.context.Users
                .Where(u => u.Id == id)
                .ProjectTo<AdministratorServiceModel>()    
                .FirstOrDefaultAsync();

            return userId;
        }

        public bool IsUsernameExist(string username)
        {
            return this.context.Users.Any(u => u.UserName == username);
        }
    }
}
