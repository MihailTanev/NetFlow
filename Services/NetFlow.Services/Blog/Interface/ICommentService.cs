namespace NetFlow.Services.Blog.Interface
{
    using NetFlow.Services.Blog.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICommentService
    {
        Task<IEnumerable<CommentsPostDetailsServiceModel>> GetAllCommentsAsync(int postId);

        Task CreateCommentAsync(string userId, int postId, string description);

    }
}
