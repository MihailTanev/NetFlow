namespace NetFlow.Services.Blog.Interface
{
    using NetFlow.Services.Blog.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBlogPostService
    {
        Task<IEnumerable<BlogPostServiceModel>> GetAllPostsAsync();

        Task CreatePostAsync(string title, string content, string publisherId);

        Task<BlogPostDetailsServiceModel> GetPostByIdAsync(int id);

    }
}
