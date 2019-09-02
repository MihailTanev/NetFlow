using AutoMapper;
using NetFlow.Data.Models;
using NetFlow.Services.Mapping;
using System;

namespace NetFlow.Services.Blog.Models
{
    public class CommentsPostDetailsServiceModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public string User { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Comment, CommentsPostServiceModel>()
                .ForMember(a => a.User, map => map.MapFrom(p => p.User.UserName));
        }
    }
}
