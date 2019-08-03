namespace NetFlow.Services.Courses
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using NetFlow.Data;
    using System.Linq;
    using NetFlow.Services.Courses.Interface;
    using NetFlow.Services.Courses.Models;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
    using System;
    using NetFlow.Data.Models;

    public class CourseService : ICourseService
    {
        private readonly NetFlowDbContext context;

        public CourseService(NetFlowDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<CourseServiceModel>> GetAllCourses()
        {
            var courses = await this.context
                .Courses
                .OrderBy(x => x.StartDate)                
                .ProjectTo<CourseServiceModel>()
                .ToListAsync();

            return courses;
        }

        public async Task<CourseServiceModel> GetCourseById(int id)
        {
            var course = await this.context
                .Courses
                .Where(x => x.Id == id)
                .OrderByDescending(x => x.StartDate)
                .ProjectTo<CourseServiceModel>()
                .FirstOrDefaultAsync();

            return course;
        }
       
        public async Task<IEnumerable<CourseServiceModel>> GetUpComingCourses()
        {
            var courses = await this.context.Courses
                .Where(x => x.StartDate >= DateTime.UtcNow)
                .OrderByDescending(x => x.StartDate)
                .ProjectTo<CourseServiceModel>()
                .ToListAsync();

            return courses;
        }

        public async Task<IEnumerable<CourseServiceModel>> GetActiveCourses()
        {
            var courses = await this.context.Courses
                .Where(x => x.StartDate>=DateTime.UtcNow && DateTime.UtcNow <= x.EndDate)
                .OrderByDescending(x => x.StartDate)
                .ProjectTo<CourseServiceModel>()
                .ToListAsync();

            return courses;
        }        

        public async Task CreateCourse(string name, int credit, string description, DateTime startDate, DateTime endDate, string teacherId)
        {
            var course = new Course
            {
                Name = name,
                Credit = credit,
                Description = description,
                StartDate = startDate,
                EndDate = endDate,
                TeacherId = teacherId
            };

            await this.context
                .Courses
                .AddAsync(course);

            await this.context.SaveChangesAsync();
        }

        public async Task CreateCourse(string name, string description, int credit, DateTime startDate, DateTime endDate, string teacherId)
        {
            var course = new Course
            {
                Name = name,
                Description = description,
                Credit = credit,
                StartDate = startDate,
                EndDate = endDate,
                TeacherId = teacherId
            };

            await this.context.Courses.AddAsync(course);
            await this.context.SaveChangesAsync();
        }
    }
}
