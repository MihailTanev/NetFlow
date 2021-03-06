﻿namespace NetFlow.Web.Areas.Trainings.Controllers
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
    using NetFlow.Web.AssignmentForm;
    using NetFlow.Web.ViewModels.Courses;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using X.PagedList;


    public class CoursesController : BaseTrainingsController
    {
        private readonly ICourseService courseService;
        private readonly UserManager<User> userManager;
        private readonly IStudentsEnrolledInCourseService studentsEnrolledInCourseService;
        private readonly IAssignmentService assignmentService;

        public CoursesController(IAssignmentService assignmentService, ICourseService courseService, UserManager<User> userManager, IStudentsEnrolledInCourseService studentsEnrolledInCourseService)
        {
            this.assignmentService = assignmentService;
            this.courseService = courseService;
            this.userManager = userManager;
            this.studentsEnrolledInCourseService = studentsEnrolledInCourseService;
        }

        public async Task<IActionResult> Index(int? page)
        {
            var courses = await this.courseService.GetAllCourses();

            var pageNumber = page ?? 1;

            var model = new CoursesIndexViewModel
            {
                Courses = await courses.ToPagedListAsync(pageNumber, 10)
            };

            return this.View(model);
        }

        [Route("trainings/courses/active-courses")]
        public async Task<IActionResult> ActiveCourses(int? page)
        {
            var courses = await this.courseService.GetActiveCourses();

            var pageNumber = page ?? 1;

            var model = new ActiveCoursesViewModel
            {
                Courses = await courses.ToPagedListAsync(pageNumber, 10)
            };

            return this.View(model);
        }

        [Route("trainings/courses/upcoming-courses")]
        public async Task<IActionResult> UpcomingCourses(int? page)
        {
            var courses = await this.courseService.GetUpcomingCourses();

            var pageNumber = page ?? 1;

            var model = new UpcomingCoursesViewModel
            {
                Courses = await courses.ToPagedListAsync(pageNumber, 10)
            };

            return this.View(model);
        }

        public async Task<IActionResult> Details(int courseId)
        {
            var course = await this.courseService.GetCourseById(courseId);

            var model = new CourseDetailsViewModel
            {
               Course = course
            };

            if (User.Identity.IsAuthenticated)
            {
                var userId = this.userManager.GetUserId(User);

                model.StudentIsEnrolledInCourse = await this.studentsEnrolledInCourseService.IsStudentEnrolledInCourse(userId, courseId);
            }

            return this.View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task <IActionResult> Enroll(int courseId)
        {
            var userId = this.userManager.GetUserId(User);

            var signUpUser = await this.studentsEnrolledInCourseService.RegisterInCourse(userId, courseId);

            if (!signUpUser)
            {
                return BadRequest();
            }

            this.TempData[CourseMessagesConstants.TEMPDATA_SUCCESS_MESSAGE] = CourseMessagesConstants.REGISTER_IN_COURSE;

            return this.RedirectToAction(nameof(Details), new { courseId });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SignOut(int courseId)
        {
            var userId = this.userManager.GetUserId(User);

            var signUpUser = await this.studentsEnrolledInCourseService.SignOutFromCourse(userId, courseId);

            if (!signUpUser)
            {
                return BadRequest();
            }

            this.TempData[CourseMessagesConstants.TEMPDATA_ERROR_MESSAGE] = CourseMessagesConstants.SIGN_OUT_FROM_COURSE;

            return this.RedirectToAction(nameof(Details), new { courseId });
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

                return RedirectToAction(nameof(Details), new { courseId });

            }

            var assignmentContent = await assignment.ConvetToByteArrayAsync();

            var userId = this.userManager.GetUserId(User);

            var submitAssignment = await this.assignmentService.SaveAssignmentAsync(courseId, userId, assignmentContent);

            if (!submitAssignment)
            {
                return BadRequest();
            }

            this.TempData[AssignmentMessagesConstants.TEMPDATA_SUCCESS_MESSAGE] = $"'{assignment.FileName}' {AssignmentMessagesConstants.ASSIGNMENT_FILE_SUCCESSFULLY_UPLOADED_MESSAGE}";

            return RedirectToAction(nameof(Details), new { courseId });

        }
    }
}