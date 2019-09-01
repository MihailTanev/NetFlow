namespace NetFlow.Services.Search
{
    using NetFlow.Data;
    using NetFlow.Services.Search.Interface;
    using NetFlow.Services.Search.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;

    public class SearchService : ISearchService
    {
        private readonly NetFlowDbContext context;

        public SearchService(NetFlowDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<SearchBlogPostServiceModel>> SearchBlogPostsAsync(string searchPost)
        {
            var searchString = await this.context
                .Posts
                .OrderBy(p => p.CreatedDate)
                .Where(p => p.Title.ToLower().Contains(searchPost.ToLower()))
                .ProjectTo<SearchBlogPostServiceModel>()
                .ToListAsync();

            return searchString;
        }

        public async Task<IEnumerable<SearchCourseServiceModel>> SearchCoursesAsync(string searchCourse)
        {
            var searchString = await this.context
                .Courses
                .OrderBy(c => c.Id)
                .Where(c => c.Name.ToLower().Contains(searchCourse.ToLower()))
                .ProjectTo<SearchCourseServiceModel>()
                .ToListAsync();

            return searchString;

        }

        public async Task<IEnumerable<SearchUsersServiceModel>> SearchUsersAsync(string searchUser)
        {
            var searchString = await this.context
                .Users
                .OrderBy(u => u.UserName)
                .Where(u => u.UserName.ToLower().Contains(searchUser.ToLower()))
                .ProjectTo<SearchUsersServiceModel>()
                .ToListAsync();

            return searchString;
        }
    }
}
