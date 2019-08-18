namespace NetFlow.Services.Administrator.Interface
{
    using NetFlow.Services.Administrator.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IAdministratorService
    {
        Task<IEnumerable<AdministratorServiceModel>> GetAllUsers();

        Task<AdministratorServiceModel> GetUserById(string id);

        Task<int> GetTotalUsers();

        bool IsUsernameExist(string username);

    }
}
