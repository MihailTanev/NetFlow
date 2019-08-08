namespace NetFlow.Services.Teachers.Interface
{
    using NetFlow.Data.Models.Enums;
    using NetFlow.Services.Teachers.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITeacherService
    {
        Task<IEnumerable<CoursesServiceModel>> GetCoursesByTeacherIdAsync(string teacherId);

        Task<IEnumerable<StudentsEnrolledInCourseServiceModel>> GetStudentEnrolledInCourseAsync(int courseId);

        Task<CoursesServiceModel> GetCourseByIdAsync(int courseId);

        Task<bool> IsTeacherAsync(int courseId, string trainerId);

        Task<bool> AddGradeAsync(Grade grade, int courseId, string studentId);

        Task<StudentNamesInCourseServiceModel> GetStudentInCourseNamesAsync(int courseId, string studentId);

    }
}
