namespace NetFlow.Services.Search.Models
{
    using AutoMapper;
    using NetFlow.Data.Models;
    using NetFlow.Services.Mapping;
    using System;

    public class SearchCourseServiceModel : IMapFrom<Course>,IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Credit { get; set; }

        public string Picture { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Teacher { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Course, SearchCourseServiceModel>()
                .ForMember(c => c.Teacher, map => map.MapFrom(a => a.Teacher.UserName));
        }
    }
}
