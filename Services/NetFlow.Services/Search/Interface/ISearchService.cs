namespace NetFlow.Services.Search.Interface
{
    using NetFlow.Services.Search.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISearchService
    {
        Task<IEnumerable<SearchCourseServiceModel>> SearchCoursesAsync(string searchCourse);

        Task<IEnumerable<SearchUsersServiceModel>> SearchUsersAsync(string searchUser);

        Task<IEnumerable<SearchBlogPostServiceModel>> SearchBlogPostsAsync(string searchPost);


    }
}
