namespace NetFlow.Services.Profile.Models
{
    using AutoMapper;
    using NetFlow.Data.Models;
    using NetFlow.Services.Mapping;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class UserProfileServiceModel : IMapFrom<Course>, IHaveCustomMappings
    {

        public DateTime BirthDate { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Picture { get; set; }

        public string PhoneNumber { get; set; }

        public IEnumerable<ProfileCourseServiceModel> Courses { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<User, UserProfileServiceModel>()
            .ForMember(u => u.Courses, map => map.MapFrom(s => s.Enrollments.Select(c => c.Course)))
            .ForMember(u => u.PhoneNumber, map => map.MapFrom(s => s.PhoneNumber));
        }
    }
}
