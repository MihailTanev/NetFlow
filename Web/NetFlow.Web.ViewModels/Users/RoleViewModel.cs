namespace NetFlow.Web.ViewModels.Users
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;

    public class RoleViewModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public IEnumerable<SelectListItem> RoleList { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}
