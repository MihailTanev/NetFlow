namespace NetFlow.Services.Courses.Interface
{
    using NetFlow.Services.Courses.Models;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICourseService
    {
        Task<IEnumerable<CourseServiceModel>> GetAllCourses();

        Task<CourseServiceModel> GetCourseById(int id);

        Task<IEnumerable<CourseServiceModel>> GetUpComingCourses();

        Task<IEnumerable<CourseServiceModel>> GetActiveCourses();

        Task CreateCourse(string name, string description, int credit, DateTime startDate, DateTime endDate, string teacherId);
    }
}
