﻿namespace NetFlow.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using NetFlow.Common.GlobalConstants;
    using NetFlow.Data.Models;
    using NetFlow.Services.Courses.Interface;
    using NetFlow.Web.Controllers;
    using NetFlow.Web.ViewModels.Courses;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CoursesController : AdminController
    {
        private readonly ICourseService courseService;
        private readonly UserManager<User> userManager;

        public CoursesController(ICourseService  courseService, UserManager<User>userManager,RoleManager<IdentityRole>roleManager)
        {
            this.userManager = userManager;
            this.courseService = courseService;
        }


        public async Task<IActionResult> Create()
        {
            var teachers = await this.GetAllTeachers();

            var model = new CreateCourseViewModel
            {                
                Teachers = teachers
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseViewModel model)
        {
            if (ModelState.IsValid)
            {
                await this.courseService.CreateCourse(model.Name, model.Description, model.Credit, model.StartDate,
                                 model.EndDate.AddDays(1),
                                 model.TeacherId);


                return this.RedirectToAction("Index", "Users", new { area = AreaConstants.ADMINISTRATION_AREA });
            }
            else
            {
                return View(model);
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

        public async Task<IActionResult> Index()
        {
            var users = new CoursesViewModel
            {
                Users = await this.courseService.GetAllCourses()
            };

            return this.View(users);
        }

    }
}