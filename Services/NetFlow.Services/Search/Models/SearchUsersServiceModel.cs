namespace NetFlow.Services.Search.Models
{
    using AutoMapper;
    using NetFlow.Data.Models;
    using NetFlow.Services.Mapping;

    public class SearchUsersServiceModel : IMapFrom<User>,IHaveCustomMappings
    {
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Courses { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<User, SearchUsersServiceModel>()
                .ForMember(u => u.Courses, map => map.MapFrom(u => u.Enrollments.Count));
        }
    }
}
