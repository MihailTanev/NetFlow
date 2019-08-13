namespace NetFlow.Services.Blog
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using NetFlow.Data;
    using NetFlow.Services.Blog.Interface;
    using NetFlow.Services.Blog.Models;
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
    using NetFlow.Data.Models;
    using System;

    public class BlogPostService : IBlogPostService
    {
        private readonly NetFlowDbContext context;

        public BlogPostService(NetFlowDbContext context)
        {
            this.context = context;
        }       

        public async Task<IEnumerable<BlogPostServiceModel>> GetAllPostsAsync()
        {
            var posts = await this.context
                .Posts
                .OrderByDescending(p => p.CreatedDate)
                .ProjectTo<BlogPostServiceModel>()
                .ToListAsync();

            return posts;
        }

        public async Task CreatePostAsync(string title, string description, string publisherId, string picture)
        {
            var post = new Post
            {
                Picture = picture,
                Title = title,
                Description = description,
                PublisherId = publisherId,
                CreatedDate = DateTime.UtcNow
            };

            await this.context.Posts.AddAsync(post);

            await this.context.SaveChangesAsync();
        }

        public async Task<BlogPostDetailsServiceModel> GetPostByIdAsync(int id)
        {
            var post = await this.context
                .Posts
                .Where(a => a.Id == id)
                .ProjectTo<BlogPostDetailsServiceModel>()
                .FirstOrDefaultAsync();

            return post;
        }
    }
}
