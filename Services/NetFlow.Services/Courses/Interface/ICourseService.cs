namespace NetFlow.Services.Courses.Interface
{
    using NetFlow.Services.Courses.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICourseService
    {
        Task<IEnumerable<CourseServiceModel>> GetAllCourses();

        Task<CourseServiceModel> GetCourseById(int id);
    }
}
