namespace NetFlow.Web.ViewModels.Users
{
    using NetFlow.Services.Administrator.Models;
    using System.Collections.Generic;

    public class UsersViewModel
    {
        public IEnumerable<AdministratorServiceModel> Users { get; set; }

    }
}
