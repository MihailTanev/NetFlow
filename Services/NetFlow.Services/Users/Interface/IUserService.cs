namespace NetFlow.Services.Users.Interface
{
    using NetFlow.Services.Users.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IUserService 
    {
        Task<IEnumerable<UserServiceModel>> GetAllUsers();

        Task<UserServiceModel> GetUserById(string id);

        Task<int> GetTotalUsers();

        bool IsUsernameExist(string username);

    }
}
