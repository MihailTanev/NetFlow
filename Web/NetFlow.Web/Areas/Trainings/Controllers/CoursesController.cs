namespace NetFlow.Web.Areas.Trainings.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using NetFlow.Common.GlobalConstants;
    using NetFlow.Common.Messages.Assignment;
    using NetFlow.Common.Messages.Course;
    using NetFlow.Data.Models;
    using NetFlow.Services.Assignment;
    using NetFlow.Services.Courses.Interface;
    using NetFlow.Services.Mapping;
    using NetFlow.Web.AssignmentForm;
    using NetFlow.Web.ViewModels.Courses;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public class CoursesController : BaseTrainingsController
    {
        private readonly ICourseService courseService;
        private readonly UserManager<User> userManager;
        private readonly IStudentsEnrolledInCourseService studentsEnrolledInCourseService;
        private readonly IAssignmentService assignmentService;

        public CoursesController(IAssignmentService assignmentService, ICourseService courseService, UserManager<User>userManager, IStudentsEnrolledInCourseService studentsEnrolledInCourseService)
        {
            this.assignmentService = assignmentService;
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

        public async Task<IActionResult> ActiveCourses()
        {
            var courses = new CoursesActiveCoursesViewModel
            {
                Courses = await this.courseService.GetActiveCourses()
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
        
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SubmitAssignment(int courseId, IFormFile assignment)
        {
            var fileExtensions = new[] { ".zip", ".pdf" };
            var checkExtensions = Path.GetExtension(assignment.FileName);

            if (!fileExtensions.Contains(checkExtensions) || assignment.Length > AssignmentConstants.ASSIGNMENT_FILE_LENGTH)
            {
                this.TempData[AssignmentMessagesConstants.TEMPDATA_ERROR_MESSAGE] = AssignmentMessagesConstants.ASSIGNMENT_FILE_EXTENSION_AND_SIZE_MESSAGE;

                return RedirectToAction("Details", "Courses", new { courseId });

            }

            var assignmentContent = await assignment.ConvetToByteArrayAsync();

            var userId = this.userManager.GetUserId(User);

            var submitAssignment = await this.assignmentService.SaveAssignmentAsync(courseId, userId, assignmentContent);

            if (!submitAssignment)
            {
                return BadRequest();
            }

            this.TempData[AssignmentMessagesConstants.TEMPDATA_SUCCESS_MESSAGE] = AssignmentMessagesConstants.ASSIGNMENT_FILE_SUCCESSFULLY_UPLOADED_MESSAGE;

            return RedirectToAction("Details", "Courses", new { courseId });

        }
    }
}