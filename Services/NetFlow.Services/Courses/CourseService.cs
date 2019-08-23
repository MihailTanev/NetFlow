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
    using AutoMapper;
    using NetFlow.Services.Mapping;

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

        public async Task<CourseServiceModel> GetCourseById(int courseId)
        {
            var course = await this.context
                .Courses
                .OrderByDescending(c => c.StartDate)
                .Where(c => c.Id == courseId)
                .ProjectTo<CourseServiceModel>()
                .FirstOrDefaultAsync();

            return course;
        }
        public async Task<IEnumerable<CourseServiceModel>> GetActiveCourses()
        {
            var courses = await this.context
                .Courses
                .Where(x => x.StartDate <= DateTime.UtcNow && x.EndDate >= DateTime.UtcNow)
                .OrderBy(x => x.StartDate)
                .ProjectTo<CourseServiceModel>()
                .ToListAsync();

            return courses;
        }

        public async Task<IEnumerable<CourseServiceModel>> GetUpcomingCourses()
        {
            var courses = await this.context
                .Courses
                .Where(x => x.StartDate >= DateTime.UtcNow && DateTime.UtcNow <= x.EndDate)
                .OrderBy(x => x.StartDate)
                .ProjectTo<CourseServiceModel>()
                .ToListAsync();

            return courses;
        }

        public async Task<IEnumerable<CourseServiceModel>> GetIndexCourses()
        {
            var courses = await this.context
                .Courses
                .Where(x => x.StartDate >= DateTime.UtcNow && DateTime.UtcNow <= x.EndDate)
                .OrderBy(x => x.StartDate)
                .Take(6)
                .ProjectTo<CourseServiceModel>()
                .ToListAsync();

            return courses;
        }

        public async Task CreateCourse(CourseServiceModel model, string id)
        {
            Course course = Mapper.Map<Course>(model);
            course.TeacherId = id;
            await this.context.Courses.AddAsync(course);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateCourse(CourseServiceModel model)
        {
            var course = this.context
                .Courses
                .FirstOrDefault(s => s.Id == model.Id);

            course.Name = model.Name;
            course.Description = model.Description;
            course.StartDate = model.StartDate;
            course.EndDate = model.EndDate;
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteCourse(CourseServiceModel model)
        {
            var course = await this.context
                .Courses
                .FirstOrDefaultAsync(c => c.Id == model.Id);

            if (course != null)
            {
                this.context.Courses.Remove(course);
                await this.context.SaveChangesAsync();
            }
        }
    }
}
