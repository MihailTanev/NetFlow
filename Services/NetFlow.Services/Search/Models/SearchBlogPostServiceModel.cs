namespace NetFlow.Services.Search.Models
{
    using AutoMapper;
    using NetFlow.Data.Models;
    using NetFlow.Services.Mapping;
    using System;

    public class SearchBlogPostServiceModel : IMapFrom<Post>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Picture { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Publisher { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Post, SearchBlogPostServiceModel>()
                .ForMember(b => b.Publisher, map => map.MapFrom(u=>u.Publisher.UserName));
        }
    }
}
