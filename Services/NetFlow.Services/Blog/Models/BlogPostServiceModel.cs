namespace NetFlow.Services.Blog.Models
{
    using AutoMapper;
    using NetFlow.Data.Models;
    using NetFlow.Services.Mapping;
    using System;
    using System.Collections.Generic;

    public class BlogPostServiceModel : IMapFrom<Post>,IHaveCustomMappings
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
                .ForMember(p => p.PublisherFirstName, map => map.MapFrom(u => u.Publisher.FirstName))
                .ForMember(p => p.PublisherLastName, map => map.MapFrom(u => u.Publisher.LastName));

        }
    }
}
