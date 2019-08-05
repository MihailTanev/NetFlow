namespace NetFlow.Web.Areas.Trainings.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using NetFlow.Common.GlobalConstants;
    using NetFlow.Common.Messages.Course;
    using NetFlow.Data.Models;
    using NetFlow.Services.Courses.Interface;
    using NetFlow.Services.Mapping;
    using NetFlow.Web.ViewModels.Courses;
    using System.Threading.Tasks;

    public class CoursesController : BaseTrainingsController
    {
        private readonly ICourseService courseService;
        private readonly UserManager<User> userManager;
        private readonly IStudentsEnrolledInCourseService studentsEnrolledInCourseService;

        public CoursesController(ICourseService courseService, UserManager<User>userManager, IStudentsEnrolledInCourseService studentsEnrolledInCourseService)
        {
            this.courseService = courseService;
            this.userManager = userManager;
            this.studentsEnrolledInCourseService = studentsEnrolledInCourseService;
        }

        public async Task<IActionResult> Index()
        {
            var courses = new CoursesIndexViewModel
            {
                Courses = await this.courseService.GetAllCourses()
            };

            return this.View(courses);
        }

        public IActionResult Details(int courseId)
        {     
            CourseDetailsViewModel model = this.courseService.GetCourseById(courseId)
                .To<CourseDetailsViewModel>();

            if (User.Identity.IsAuthenticated)
            {
                var userId = this.userManager.GetUserId(User);

                model.StudentIsEnrolledInCourse =  this.studentsEnrolledInCourseService.IsStudentEnrolledInCourse(userId,courseId);
            }

            return this.View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Participate(int courseId)
        {
            var userId = this.userManager.GetUserId(User);

            var signUpUser = this.studentsEnrolledInCourseService.RegisterInCourse(userId, courseId);

            if (!signUpUser)
            {
                return BadRequest();
            }

            this.TempData[CourseMessagesConstants.TEMPDATA_SUCCESS_MESSAGE] = CourseMessagesConstants.REGISTER_IN_COURSE;

            return this.RedirectToAction("Details", "Courses", new { courseId});
        }

        [Authorize]
        [HttpPost]
        public IActionResult SignOut(int courseId)
        {
            var userId = this.userManager.GetUserId(User);

            var signUpUser = this.studentsEnrolledInCourseService.SignOutFromCourse(userId, courseId);

            if (!signUpUser)
            {
                return BadRequest();
            }

            this.TempData[CourseMessagesConstants.TEMPDATA_ERROR_MESSAGE] = CourseMessagesConstants.SIGN_OUT_FROM_COURSE;

            return this.RedirectToAction("Details", "Courses", new { courseId });
        }
    }
}