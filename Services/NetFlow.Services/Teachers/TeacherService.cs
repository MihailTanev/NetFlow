namespace NetFlow.Services.Teachers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
    using NetFlow.Data;
    using NetFlow.Data.Models;
    using NetFlow.Data.Models.Enums;
    using NetFlow.Services.Teachers.Interface;
    using NetFlow.Services.Teachers.Models;

    public class TeacherService : ITeacherService
    {
        private readonly NetFlowDbContext context;

        public TeacherService(NetFlowDbContext context)
        {
            this.context = context;
        }
       
        public async Task<IEnumerable<CoursesServiceModel>> GetCoursesByTeacherIdAsync(string teacherId)
        {
            var courses = await this.context
                .Courses
                .OrderByDescending(c => c.StartDate <= DateTime.UtcNow && c.EndDate >= DateTime.UtcNow)
                .Where(c => c.TeacherId == teacherId)
                .ProjectTo<CoursesServiceModel>()
                .ToListAsync();

            return courses;
        }

        public async Task<IEnumerable<StudentsEnrolledInCourseServiceModel>> GetStudentEnrolledInCourseAsync(int courseId)
        {
            var students = await this.context
                .Courses
                .Where(c => c.Id == courseId)
                .SelectMany(c => c.Enrollments.Select(s => s.Student))
                .ProjectTo<StudentsEnrolledInCourseServiceModel>(new { courseId })
                .ToListAsync();

            return students;
        }

        public async Task<CoursesServiceModel> GetCourseByIdAsync(int courseId)
        {
            var course = await this.context
                .Courses
                .Where(c => c.Id == courseId)
                .ProjectTo<CoursesServiceModel>()
                .FirstOrDefaultAsync();

            return course;
        }

        public async Task<bool> IsTeacherAsync(int courseId, string teacherId)
        {
            var isTeacher = await this.context
                .Courses
                .AnyAsync(c => c.Id == courseId && c.TeacherId == teacherId);

            return isTeacher;
        }

        public async Task<bool> AddGradeAsync(Grade grade, int courseId, string studentId, string comment)
        {
            var studentInCourse = await this.context.FindAsync<Enrollment>(studentId, courseId);

            if (studentInCourse == null)
            {
                return false;
            }

            studentInCourse.Grade = grade;
            studentInCourse.Comment = comment;

            await this.context.SaveChangesAsync();

            return true;
        }

        public async Task<StudentNamesInCourseServiceModel> GetStudentInCourseNamesAsync(int courseId, string studentId)
        {
            var user = await this.context.Users.Where(u => u.Id == studentId).FirstOrDefaultAsync();

            var course = await this.context.Courses.Where(c => c.Id == courseId).FirstOrDefaultAsync();

            var model = new StudentNamesInCourseServiceModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                CourseName = course.Name
            };

            return model;
        }
    }
}
