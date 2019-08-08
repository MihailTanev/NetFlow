namespace NetFlow.Web.Areas.Pedagogics.Controllers
{
	using Microsoft.AspNetCore.Identity;
	using Microsoft.AspNetCore.Mvc;
	using NetFlow.Data.Models;
	using NetFlow.Data.Models.Enums;
    using NetFlow.Services.Assignment;
    using NetFlow.Services.Teachers.Interface;
	using NetFlow.Web.ViewModels.Pedagogics;
    using System;
    using System.Threading.Tasks;

	public class TeacherController : BaseTeacherController
	{
		private readonly UserManager<User> userManager;
		private readonly ITeacherService teacherService;
        private readonly IAssignmentService assignmentService;

		public TeacherController(UserManager<User> userManager, ITeacherService teacherService, IAssignmentService assignmentService)
		{
            this.assignmentService = assignmentService;
			this.userManager = userManager;
			this.teacherService = teacherService;
		}

		public async Task<IActionResult> Index()
		{
			var userId = this.userManager.GetUserId(User);

			var model = new CoursesViewModel
			{
				Courses = await this.teacherService.GetCoursesByTeacherIdAsync(userId)
			};

			return this.View(model);
		}

		public async Task<IActionResult> Students(int courseId)
		{
			var students = await this.teacherService.GetStudentEnrolledInCourseAsync(courseId);

			var course = await this.teacherService.GetCourseByIdAsync(courseId);

			if (course == null)
			{
				return BadRequest();
			}

			var model = new StudentsEnrolledInCourseViewModel
			{
				Students = students,
				Course = course
			};

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> AddGrade(Grade grade, string studentId, int courseId)
		{
			if (string.IsNullOrEmpty(studentId))
			{
				return BadRequest();
			}
			
			var success = await this.teacherService.AddGradeAsync(grade, courseId, studentId);

			if (!success)
			{
				return BadRequest();
			}
			else
			{
				return RedirectToAction("Students", "Teacher", new { courseId });
			}
		}

        public async Task<IActionResult> DownloadAssignment(int courseId, string studentId)
        {         

            var assignmentContent = await this.assignmentService.DownloadAssignmentAsync(studentId, courseId);

            if (assignmentContent == null)
            {
                return RedirectToAction("Students","Teacher", new { courseId });
            }

            var studentInCourse = await this.teacherService.GetStudentInCourseNamesAsync(courseId,studentId);

            if (studentInCourse == null)
            {
                return BadRequest();
            }
            //return File(assignmentContent, "application/octet-stream", $"{studentInCourse.CourseName}-{studentInCourse.FirstName}-{DateTime.UtcNow.ToString("MM-dd-yyyy")}");


            return File(assignmentContent, "application/octet-stream", $"{studentInCourse.CourseName}-{studentInCourse.FirstName}");            
        }
    }
}