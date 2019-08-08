namespace NetFlow.Web.Areas.Pedagogics.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using NetFlow.Common.GlobalConstants;

    [Area(AreaConstants.PEDAGOGICS_AREA)]
    [Authorize(Roles = RoleConstants.TEACHER_ROLE)]
    public class BaseTeacherController : Controller
    {       
    }
}