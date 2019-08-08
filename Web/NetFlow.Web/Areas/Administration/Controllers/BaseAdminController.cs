namespace NetFlow.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using NetFlow.Common.GlobalConstants;

    [Area(AreaConstants.ADMINISTRATION_AREA)]
    [Authorize(Roles = RoleConstants.ADMIN_ROLE)]
    public abstract class BaseAdminController : Controller
    {        
    }
}