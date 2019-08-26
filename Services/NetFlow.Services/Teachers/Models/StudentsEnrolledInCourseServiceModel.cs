namespace NetFlow.Services.Teachers.Models
{
    using AutoMapper;
    using NetFlow.Data.Models;
    using NetFlow.Data.Models.Enums;
    using NetFlow.Services.Mapping;
    using System;
    using System.Linq;

    public class StudentsEnrolledInCourseServiceModel : IMapFrom<User>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Comment { get; set; }

        public Grade? Grade { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            int courseId = default(int);

            configuration.CreateMap<User, StudentsEnrolledInCourseServiceModel>()
                .ForMember(s => s.FirstName, map => map.MapFrom(u => u.FirstName))
                .ForMember(s => s.CreatedOn, map => map.MapFrom(u => u.CreatedOn))
                .ForMember(s => s.LastName, map => map.MapFrom(u => u.LastName))
                .ForMember(s => s.Grade, map => map.MapFrom(u => u.Enrollments
                                                                .Where(c => c.CourseId == courseId)
                                                                .Select(c => c.Grade)
                                                                .FirstOrDefault()))
                .ForMember(s => s.Comment, map => map.MapFrom(u => u.Enrollments
                                                                     .Where(c => c.CourseId == courseId)
                                                                     .Select(c => c.Comment)
                                                                     .FirstOrDefault()));
        }
    }
}
