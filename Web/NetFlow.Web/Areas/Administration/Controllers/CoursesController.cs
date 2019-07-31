namespace NetFlow.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using NetFlow.Data.Models;
    using NetFlow.Services.Courses.Interface;

    public class CoursesController : AdminController
    {
        private readonly ICourseService courseService;
        private readonly UserManager<User> userManager;

        public CoursesController(ICourseService  courseService, UserManager<User>userManager)
        {
            this.userManager = userManager;
            this.courseService = courseService;
        }
    }
}