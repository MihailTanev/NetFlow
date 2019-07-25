namespace NetFlow.Web.Areas.Administration.Models
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using NetFlow.Services.Users.Models;
    using System.Collections.Generic;

    public class ChangeUserRoleViewModel
    {
        public UserServiceModel User { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}
