namespace NetFlow.Web.ViewModels.Users
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using NetFlow.Services.Administrator.Models;
    using System.Collections.Generic;

    public class ChangeUserRoleViewModel
    {
        public AdministratorServiceModel User { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}
