namespace NetFlow.Services.Courses
{
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Identity;
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

        public bool IsStudentEnrolledInCourse(string userId, int courseId)
        {
            var isStudentEnrolledIncourse = this.context
                .Courses
                .Any(c => c.Enrollments.Any(s => s.StudentId == userId) && c.Id == courseId);

            return isStudentEnrolledIncourse;
        }

        public bool RegisterInCourse(string userId, int courseId)
        {
            var course = this.GetCourseInformation(userId, courseId);

            if(course.StartDate < DateTime.UtcNow || course.IsStudentRegisterInCourse)
            {
                return false;
            }

            var studentEnrolledInCourse = new Enrollment
            {
                StudentId = userId,
                CourseId = courseId
            };

            this.context.Enrollments.Add(studentEnrolledInCourse);
            this.context.SaveChanges();
            return true;           
        }
       
        private CourseWithStudentsServiceModel GetCourseInformation(string studentId, int courseId)
        {
            var couseDesc = this.context
                .Courses
                .Where(c => c.Id == courseId)
                .Select(c => new CourseWithStudentsServiceModel
                {
                    StartDate = c.StartDate,
                    IsStudentRegisterInCourse = c.Enrollments.Any(s => s.StudentId == studentId)
                })
                .FirstOrDefault();

            return couseDesc;
        }

        public bool SignOutFromCourse(string userId, int courseId)
        {
            var course = this.GetCourseInformation(userId, courseId);

            if (course.StartDate < DateTime.UtcNow || !course.IsStudentRegisterInCourse)
            {
                return false;
            }

            var registerStudent = this.context.Find<Enrollment>(userId, courseId);

            this.context.Enrollments.Remove(registerStudent);
            this.context.SaveChanges();

            return true;
        }

    }
}
