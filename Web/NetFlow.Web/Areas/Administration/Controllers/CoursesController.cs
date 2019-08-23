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
    using NetFlow.Web.ViewModels.Courses;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CoursesController : BaseAdminController
    {
        private readonly ICourseService courseService;
        private readonly UserManager<User> userManager;
        private readonly ICloudinaryService cloudinaryService;

        public CoursesController(ICourseService courseService, UserManager<User> userManager, ICloudinaryService cloudinaryService)
        {
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

                this.TempData[CourseMessagesConstants.TEMPDATA_SUCCESS_MESSAGE] = $" '{model.Name}' {CourseMessagesConstants.COURSE_WAS_CREATED}";

                return this.RedirectToAction(nameof(Add), new { area = AreaConstants.ADMINISTRATION_AREA });
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
                this.TempData[CourseMessagesConstants.TEMPDATA_ERROR_MESSAGE] = $" '{course.Name}' {CourseMessagesConstants.COURSE_WAS_NOT_CREATED} ";

                return NotFound();
            }

            EditCourseViewModel model = new EditCourseViewModel
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
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
                this.TempData[CourseMessagesConstants.TEMPDATA_ERROR_MESSAGE] = $" '{course.Name}' {CourseMessagesConstants.COURSE_WAS_NOT_CREATED} ";

                return NotFound();
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

                return this.RedirectToAction(nameof(Edit), new { courseId });
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
                return NotFound();
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
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await this.courseService.DeleteCourse(course);

                return this.RedirectToAction("Index", "Courses", new { area = AreaConstants.TRAININGS_AREA });
            }
            else
            {
                return View(model);
            }
        }
    }
}