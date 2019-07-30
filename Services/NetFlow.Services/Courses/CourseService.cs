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
                .OrderByDescending(x => x.StartDate)
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


    }
}
