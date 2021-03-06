﻿namespace NetFlow.Web.Areas.Pedagogics.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using NetFlow.Data.Models;
    using NetFlow.Data.Models.Enums;
    using NetFlow.Services.Assignment;
    using NetFlow.Services.Teachers.Interface;
    using NetFlow.Web.ViewModels.Pedagogics;
    using System.Threading.Tasks;
    using X.PagedList;

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

		public async Task<IActionResult> Index(int? page)
		{
			var userId = this.userManager.GetUserId(User);

            var courses = await this.teacherService.GetCoursesByTeacherIdAsync(userId);

            var pageNumber = page ?? 1;

            var model = new CoursesViewModel
			{
				Courses = await courses.ToPagedListAsync(pageNumber, 10)
            };

			return this.View(model);
		}

		public async Task<IActionResult> Students(int? page,int courseId)
		{
			var students = await this.teacherService.GetStudentEnrolledInCourseAsync(courseId);

            var pageNumber = page ?? 1;

            var course = await this.teacherService.GetCourseByIdAsync(courseId);

			if (course == null)
			{
				return BadRequest();
			}

			var model = new StudentsEnrolledInCourseViewModel
			{
				Students = await students.ToPagedListAsync(pageNumber, 10),

                Course = course
			};

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> AddGrade(Grade grade, string studentId, int courseId, string comment)
		{
			if (string.IsNullOrEmpty(studentId))
			{
				return BadRequest();
			}
			
			var success = await this.teacherService.AddGradeAsync(grade, courseId, studentId, comment);

			if (!success)
			{
				return BadRequest();
			}
			else
			{
				return RedirectToAction(nameof(Students), new { courseId });
			}
		}

        public async Task<IActionResult> DownloadAssignment(int courseId, string studentId, byte[] studentAssignment)
        {         

            var assignmentContent = await this.assignmentService.DownloadAssignmentAsync(studentId, courseId);

            if (assignmentContent == null)
            {
                return RedirectToAction(nameof(Students), new { courseId });
            }

            var studentInCourse = await this.teacherService.GetStudentInCourseNamesAsync(courseId,studentId);

            if (studentInCourse == null)
            {
                return BadRequest();
            }

            return File(assignmentContent, "application/octet-stream", $"{studentInCourse.CourseName}_{studentInCourse.FirstName}_{studentInCourse.LastName}.zip");            
        }
    }
}