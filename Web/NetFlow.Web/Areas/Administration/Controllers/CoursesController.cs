﻿namespace NetFlow.Web.Areas.Administration.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using NetFlow.Common.GlobalConstants;
    using NetFlow.Data.Models;
    using NetFlow.Services.Cloudinary;
    using NetFlow.Services.Courses.Interface;
    using NetFlow.Services.Courses.Models;
    using NetFlow.Services.Users.Interface;
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
        public async Task<IActionResult> Create()
        {
            var teachers = await this.GetAllTeachers();

            var model = new CreateCourseViewModel
            {
                Teachers = teachers
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseViewModel model)
        {
            var teacher = await this.userManager.GetUsersInRoleAsync(RoleConstants.TEACHER_ROLE);

            if (ModelState.IsValid)
            {
                string pictureUrl = await this.cloudinaryService.UploadCoursePictureAsync(model.Picture, model.Name);

                CourseServiceModel courseServiceModel = Mapper.Map<CourseServiceModel>(model);

                courseServiceModel.Picture = pictureUrl;

                this.courseService.CreateCourse(courseServiceModel, model.TeacherId);

                return this.RedirectToAction("Index", "Courses", new { area = AreaConstants.TRAININGS_AREA });
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
    }
}