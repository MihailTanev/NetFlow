namespace NetFlow.Services.Blog.Models
{
    using AutoMapper;
    using NetFlow.Data.Models;
    using NetFlow.Services.Mapping;
    using System;

    public class BlogPostDetailsServiceModel : IMapFrom<Post>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public string PublisherFirstName { get; set; }

        public string PublisherLastName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Post, BlogPostServiceModel>()
                 .ForMember(a => a.PublisherFirstName, map => map.MapFrom(p => p.Publisher.FirstName))
                 .ForMember(a => a.PublisherLastName, map => map.MapFrom(p => p.Publisher.LastName));

        }
    }
}
