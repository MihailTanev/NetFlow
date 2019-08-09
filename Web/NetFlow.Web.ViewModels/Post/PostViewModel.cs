namespace NetFlow.Web.ViewModels.Post
{
    using NetFlow.Services.Blog.Models;
    using System.Collections.Generic;

    public class PostViewModel
    {
        public IEnumerable<BlogPostServiceModel> Posts { get; set; }

    }
}
