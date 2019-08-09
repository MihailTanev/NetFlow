namespace NetFlow.Web.Areas.Trainings.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using NetFlow.Common.GlobalConstants;

    [Area(AreaConstants.TRAININGS_AREA)]
    //[Authorize(Roles = RoleConstants.TEACHER_ROLE)]
    public abstract class BaseTrainingsController : Controller
    {
       
    }
}