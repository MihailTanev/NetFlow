namespace NetFlow.Services.Courses.Interface
{
    using NetFlow.Data.Models;
    using NetFlow.Services.Courses.Models;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICourseService
    {
        Task<IEnumerable<CourseServiceModel>> GetAllCourses();

        CourseServiceModel GetCourseById(int id);

        Task<IEnumerable<CourseServiceModel>> GetActiveCourses();

        Task<IEnumerable<CourseServiceModel>> GetIndexCourses();

        Task<IEnumerable<CourseServiceModel>> GetPastCourses();


        Course CreateCourse(CourseServiceModel model, string id);
    }
}
