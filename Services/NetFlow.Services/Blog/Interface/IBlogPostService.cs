namespace NetFlow.Services.Blog.Interface
{
    using NetFlow.Services.Blog.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBlogPostService
    {
        Task<IEnumerable<BlogPostServiceModel>> GetAllPostsAsync();

        Task<IEnumerable<BlogPostServiceModel>> GetIndexBlogPosts();

        Task CreatePostAsync(string title, string content, string publisherId, string picture);

        Task<BlogPostServiceModel> GetPostByIdAsync(int id);

        Task DeletePost(BlogPostServiceModel model);

        Task UpdatePost(BlogPostServiceModel model);

    }
}
