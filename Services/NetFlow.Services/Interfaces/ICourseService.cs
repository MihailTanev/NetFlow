using NetFlow.Data.Models;
using NetFlow.Web.ViewModels;

namespace NetFlow.Services.Interfaces
{
    public interface ICourseService
    {
        //Task<IEnumerable<CreateCourseViewModel>> GetAllCourses();

        //Task<CreateCourseViewModel> GetCourseById(int id);

        //Task<IEnumerable<CourseServiceModel>> GetUpComingCourses();

        //Task<IEnumerable<CourseServiceModel>> GetActiveCourses();

        

        Course Create(CreateCourseViewModel model);
    }
}
