namespace NetFlow.Services.Courses.Interface
{
    using System.Threading.Tasks;

    public interface IStudentsEnrolledInCourseService
    {
        Task<bool> IsStudentEnrolledInCourse(string userId, int courseId);

        Task<bool> RegisterInCourse(string userId, int courseId);

        Task<bool> SignOutFromCourse(string userId, int courseId);
    }
}
