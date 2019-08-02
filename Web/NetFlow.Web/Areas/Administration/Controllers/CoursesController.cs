namespace NetFlow.Web.Areas.Administration.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using NetFlow.Common.GlobalConstants;
    using NetFlow.Data.Models;
    using NetFlow.Services.Courses.Interface;
    using NetFlow.Services.Users.Interface;
    using System.Security.Claims;

    public class CoursesController : AdminController
    {
        private readonly ICourseService courseService;
        private readonly UserManager<User> userManager;
        private readonly IUserService userService;


        public CoursesController(ICourseService  courseService, UserManager<User>userManager, IUserService userService, IMapper mapper)
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

        //[Authorize(Roles = RoleConstants.TEACHER_ROLE)]
        //[HttpPost]      

    }
}