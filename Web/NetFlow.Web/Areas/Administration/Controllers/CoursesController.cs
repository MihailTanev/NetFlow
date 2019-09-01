namespace NetFlow.Web.Areas.Administration.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using NetFlow.Common.GlobalConstants;
    using NetFlow.Common.Messages.Course;
    using NetFlow.Data.Models;
    using NetFlow.Services.Cloudinary;
    using NetFlow.Services.Courses.Interface;
    using NetFlow.Services.Courses.Models;
    using NetFlow.Services.HtmlSanitizer;
    using NetFlow.Web.ViewModels.Courses;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CoursesController : BaseAdminController
    {
        private readonly ICourseService courseService;
        private readonly UserManager<User> userManager;
        private readonly ICloudinaryService cloudinaryService;
        private readonly IHtmlSanitizerService htmlSanitizerService;

        public CoursesController(IHtmlSanitizerService htmlSanitizerService, ICourseService courseService, UserManager<User> userManager, ICloudinaryService cloudinaryService)
        {
            this.htmlSanitizerService = htmlSanitizerService;
            this.userManager = userManager;
            this.courseService = courseService;
            this.cloudinaryService = cloudinaryService;
        }       

        public async Task<IActionResult> Add()
        {
            var teachers = await this.GetAllTeachers();

            var model = new CreateCourseViewModel
            {
                Teachers = teachers
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateCourseViewModel model)
        {
            if (ModelState.IsValid)
            {
                string pictureUrl = await this.cloudinaryService.UploadCoursePictureAsync(model.Picture, model.Name);

                CourseServiceModel courseServiceModel = Mapper.Map<CourseServiceModel>(model);

                courseServiceModel.Picture = pictureUrl;

                await this.courseService.CreateCourse(courseServiceModel, model.TeacherId);

                this.TempData[CourseMessagesConstants.TEMPDATA_SUCCESS_MESSAGE] = $"Course '{model.Name}' {CourseMessagesConstants.COURSE_WAS_CREATED}";

                return this.RedirectToAction("Index","Courses", new { area = AreaConstants.TRAININGS_AREA });
            }
            else
            {
                return this.View(model);
            }
        }

        private async Task<IEnumerable<SelectListItem>> GetAllTeachers()
        {
            var usersInTeacherRole = await this.userManager.GetUsersInRoleAsync(RoleConstants.TEACHER_ROLE);

            var teachers = usersInTeacherRole.Select(teacher => new SelectListItem
            {
                Text = teacher.FirstName + " " + teacher.LastName,
                Value = teacher.Id
            })
                .ToList();

            return teachers;
        }

        public async Task<IActionResult> Edit(int courseId)
        {
            var course = await this.courseService.GetCourseById(courseId);

            if (course == null)
            {
                return BadRequest();
            }

            EditCourseViewModel model = new EditCourseViewModel
            {
                Id = course.Id,
                Name = course.Name,
                Description = this.htmlSanitizerService.Sanitize(course.Description),
                Credit = course.Credit,
                EndDate = course.EndDate,
                StartDate = course.StartDate
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCourseViewModel model, int courseId)
        {
            var course = await this.courseService.GetCourseById(courseId);

            if (course == null)
            {
                return BadRequest();
            }

            course.Name = model.Name;
            course.Description = model.Description;
            course.StartDate = model.StartDate;
            course.EndDate = model.EndDate;
            course.Credit = model.Credit;

            if (ModelState.IsValid)
            {
                await this.courseService.UpdateCourse(course);

                this.TempData[CourseMessagesConstants.TEMPDATA_SUCCESS_MESSAGE] = $" '{course.Name}' {CourseMessagesConstants.COURSE_WAS_UPDATED}";

                return this.RedirectToAction("Details", "Courses", new { courseId, area = AreaConstants.TRAININGS_AREA });
            }
            else
            {
                return this.View(model);
            }
        }

        public async Task<IActionResult> Delete(int courseId)
        {
            var course = await this.courseService.GetCourseById(courseId);

            if (course == null)
            {
                return BadRequest();
            }

            var model = new DeleteCourseViewModel
            {
                CourseId = course.Id,
                CourseName = course.Name
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirm(DeleteCourseViewModel model, int courseId)
        {
            var course = await this.courseService.GetCourseById(courseId);

            if (course == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await this.courseService.DeleteCourse(course);

                this.TempData[CourseMessagesConstants.TEMPDATA_SUCCESS_MESSAGE] = $"Course '{course.Name}' {CourseMessagesConstants.COURSE_WAS_DELETED}";

                return this.RedirectToAction("Index", "Courses", new { area = AreaConstants.TRAININGS_AREA });
            }
            else
            {
                return this.View(model);
            }
        }
    }
}