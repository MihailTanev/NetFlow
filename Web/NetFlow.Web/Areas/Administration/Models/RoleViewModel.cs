namespace NetFlow.Web.Areas.Administration.Models
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;

    public class RoleViewModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public IEnumerable<string> Roles { get; set; }

        public IEnumerable<SelectListItem> ListRole { get; set; }
    }
}
