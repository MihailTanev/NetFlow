namespace NetFlow.Web.ViewModels.Post
{
    using NetFlow.Services.Blog.Models;
    using NetFlow.Services.Mapping;
    using System;

    public class EditPostViewModel : IMapFrom<BlogPostServiceModel>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
       
    }
}
