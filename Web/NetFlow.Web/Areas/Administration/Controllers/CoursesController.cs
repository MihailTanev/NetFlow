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
    using NetFlow.Services.Mapping;
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

                return this.RedirectToAction("Add", "Courses", new { area = AreaConstants.ADMINISTRATION_AREA });
            }
            else
            {
                return this.View(model);
            }
        }

        private async Task<IEnumerable<SelectListItem>> GetAllTeachers()
        {
            var teacherRole = await this.userManager.GetUsersInRoleAsync(RoleConstants.TEACHER_ROLE);

            var teachers = teacherRole.Select(teacher => new SelectListItem
            {
                Text = teacher.FirstName + " " + teacher.LastName,
                Value = teacher.Id
            })
                .ToList();

            return teachers;
        }

        public IActionResult Edit(int id)
        {
            EditCourseViewModel model = this.courseService.GetCourseById(id)
                .To<EditCourseViewModel>();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCourseViewModel model, int id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            CourseServiceModel course = this.courseService.GetCourseById(id)
                       .To<CourseServiceModel>();

            course.Name = model.Name;
            course.Description = model.Description;
            course.StartDate = model.StartDate;
            course.EndDate = model.EndDate;
            course.Credit = model.Credit;

            await this.courseService.UpdateCourse(course);

            this.TempData[CourseMessagesConstants.TEMPDATA_SUCCESS_MESSAGE] = $" '{course.Name}' {CourseMessagesConstants.COURSE_WAS_UPDATED}"; 

            return this.RedirectToAction(nameof(Edit), new { id });
        }
    }
}