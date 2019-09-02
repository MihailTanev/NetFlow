namespace NetFlow.Services.Blog
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
    using NetFlow.Data;
    using NetFlow.Data.Models;
    using NetFlow.Services.Blog.Interface;
    using NetFlow.Services.Blog.Models;

    public class CommentService : ICommentService
    {
        private readonly NetFlowDbContext context;

        public CommentService(NetFlowDbContext context)
        {
            this.context = context;
        }

        public async Task CreateCommentAsync(string userId, int postId, string description)
        {
            var comment = new Comment
            {
                Content = description,
                UserId = userId,
                PostId = postId
            };

            await this.context.Comments.AddAsync(comment);

            await this.context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CommentsPostDetailsServiceModel>> GetAllCommentsAsync(int id)
        {
            var comments = await this.context
                           .Comments
                           .Where(a => a.PostId == id)
                           .ProjectTo<CommentsPostDetailsServiceModel>()
                           .ToListAsync();

            return comments;
        }
    }
}
