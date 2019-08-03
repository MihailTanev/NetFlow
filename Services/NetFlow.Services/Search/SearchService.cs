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

        public async Task<IEnumerable<SearchCourseServiceModel>> SearchCoursesAsync(string searchCourse)
        {
            var searchString = await this.context
                .Courses
                .OrderBy(c => c.Id)
                .Where(c => c.Name.Contains(searchCourse))
                .ProjectTo<SearchCourseServiceModel>()
                .ToListAsync();

            return searchString;

        }
    }
}
