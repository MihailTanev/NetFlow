namespace NetFlow.Services.Interfaces
{
    using NetFlow.Web.ViewModels.User;
    using System.Collections.Generic;

    public interface IUsersService
    {
        IEnumerable<UsersViewModel> GetAllUsers();

    }
}
