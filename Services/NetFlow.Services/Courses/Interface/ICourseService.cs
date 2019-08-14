namespace NetFlow.Services.Courses.Interface
{
    using NetFlow.Services.Courses.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface ICourseService
    {
        Task<IEnumerable<CourseServiceModel>> GetAllCourses();

        CourseServiceModel GetCourseById(int id);

        Task<IEnumerable<CourseServiceModel>> GetActiveCourses();

        Task<IEnumerable<CourseServiceModel>> GetIndexCourses();

        Task<IEnumerable<CourseServiceModel>> GetUpcomingCourses();

        Task CreateCourse(CourseServiceModel model, string id);

        Task UpdateCourse(CourseServiceModel model);
    }
}
