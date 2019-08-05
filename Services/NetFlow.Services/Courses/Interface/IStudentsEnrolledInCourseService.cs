namespace NetFlow.Services.Courses.Interface
{
    public interface IStudentsEnrolledInCourseService
    {
        bool IsStudentEnrolledInCourse(string userId, int courseId);

        bool RegisterInCourse(string userId, int courseId);

        bool SignOutFromCourse(string userId, int courseId);
    }
}
