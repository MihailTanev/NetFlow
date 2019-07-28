namespace NetFlow.Web.ViewModels.Users
{
    using NetFlow.Services.Users.Models;
    using System.Collections.Generic;

    public class UsersViewModel
    {
        public IEnumerable<UserServiceModel> Users { get; set; }

    }
}
