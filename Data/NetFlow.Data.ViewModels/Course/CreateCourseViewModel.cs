namespace NetFlow.Data.ViewModels.Course
{
    using AutoMapper;
    using NetFlow.Data.Models;
    using Stopify.Services.Mapping;
    using System;

    public class CreateCourseViewModel : IMapFrom<Course>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Credit { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string TeacherId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Course, CreateCourseViewModel>()
                             .ForMember(m => m.TeacherId, opt => opt.MapFrom(t => t.Teacher.Id))                             
                             .ReverseMap();
        }
    }
}
