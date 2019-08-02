namespace NetFlow.Web.Areas.Administration.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using NetFlow.Data.Models;
    using NetFlow.Services.Courses.Interface;

    public class CoursesController : AdminController
    {
        private readonly ICourseService courseService;
        private readonly UserManager<User> userManager;
        private readonly IUserService userService;

        public CoursesController(ICourseService  courseService, UserManager<User>userManager)
        {
            this.userManager = userManager;
            this.courseService = courseService;
            this.userService = userService;
        }

        [Authorize(Roles = RoleConstants.TEACHER_ROLE)]
        public IActionResult Create()
        {     
            return this.View();
        }
    }
}