namespace NetFlow.Services.Profile.Models
{
    using AutoMapper;
    using NetFlow.Data.Models;
    using NetFlow.Data.Models.Enums;
    using NetFlow.Services.Mapping;
    using System;
    using System.Linq;

    public class ProfileCourseServiceModel : IMapFrom<Course>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string CourseName { get; set; }

        public Grade? Grade { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Credit { get; set; }

        public string Çomment { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            string studentId = null;

            configuration.CreateMap<Course, ProfileCourseServiceModel>()
            .ForMember(u => u.CourseName, map => map.MapFrom(c => c.Name))
            .ForMember(u => u.StartDate, map => map.MapFrom(c => c.StartDate))
            .ForMember(u => u.EndDate, map => map.MapFrom(c => c.EndDate))
            .ForMember(u => u.Grade, map => map.MapFrom(c => c.Enrollments
                                                              .Where(s => s.StudentId == studentId)
                                                              .Select(s => s.Grade)
                                                              .FirstOrDefault()))
            .ForMember(u => u.Çomment, map => map.MapFrom(c => c.Enrollments
                                                              .Where(s => s.StudentId == studentId)
                                                              .Select(s => s.Comment)
                                                              .FirstOrDefault()));
        }
    }
}
