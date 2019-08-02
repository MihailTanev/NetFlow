namespace NetFlow.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using NetFlow.Data;
    using NetFlow.Data.Models;
    using NetFlow.Services.Interfaces;
    using NetFlow.Web.ViewModels.User;

    public class UsersServices : BaseService, IUsersService
    {
        public UsersServices(NetFlowDbContext context, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
          : base(context, mapper, userManager, signInManager)
        {
        }

        public IEnumerable<UsersViewModel> GetAllUsers()
        {
            var users = this.context
                .Users
                .OrderBy(x => x.UserName)
                .AsQueryable();

            var usersAdminViewModel = this.Mapper.Map<IQueryable<User>, IEnumerable<UsersViewModel>>(users);

            return usersAdminViewModel;
        }
    }
}
