namespace NetFlow.Services.Courses
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using NetFlow.Data;
    using NetFlow.Data.Models;
    using NetFlow.Services.Courses.Interface;
    using NetFlow.Services.Courses.Models;

    public class StudentsEnrolledInCourseService : IStudentsEnrolledInCourseService
    {
        private readonly NetFlowDbContext context;

        public StudentsEnrolledInCourseService(NetFlowDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> IsStudentEnrolledInCourse(string userId, int courseId)
        {
            var isStudentEnrolledIncourse = await this.context
                .Courses
                .AnyAsync(c => c.Enrollments.Any(s => s.StudentId == userId) && c.Id == courseId);

            return isStudentEnrolledIncourse;
        }

        public async Task<bool> RegisterInCourse(string userId, int courseId)
        {
            var course = await this.GetCourseInformation(userId, courseId);

            if(course.StartDate < DateTime.UtcNow || course.IsStudentRegisterInCourse)
            {
                return false;
            }

            var studentEnrolledInCourse = new Enrollment
            {
                StudentId = userId,
                CourseId = courseId
            };

            await this.context.Enrollments.AddAsync(studentEnrolledInCourse);
            await this.context.SaveChangesAsync();
            return true;           
        }
       
        private async Task<CourseWithStudentsServiceModel> GetCourseInformation(string studentId, int courseId)
        {
            var couseDesc = await this.context
                .Courses
                .Where(c => c.Id == courseId)
                .Select(c => new CourseWithStudentsServiceModel
                {
                    StartDate = c.StartDate,
                    IsStudentRegisterInCourse = c.Enrollments.Any(s => s.StudentId == studentId)
                })
                .FirstOrDefaultAsync();

            return couseDesc;
        }

        public async Task<bool> SignOutFromCourse(string userId, int courseId)
        {
            var course = await this.GetCourseInformation(userId, courseId);

            if (course.StartDate < DateTime.UtcNow || !course.IsStudentRegisterInCourse)
            {
                return false;
            }

            var registerStudent = await this.context.FindAsync<Enrollment>(userId, courseId);

            this.context.Enrollments.Remove(registerStudent);
            this.context.SaveChanges();

            return true;
        }

    }
}
