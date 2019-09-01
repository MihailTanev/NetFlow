namespace NetFlow.Web.ViewModels.Home
{
    using NetFlow.Services.Search.Models;
    using System.Collections.Generic;

    public class SearchViewModel
    {
        public IEnumerable<SearchCourseServiceModel> Courses { get; set; } = new List<SearchCourseServiceModel>();

        public string SearchString { get; set; }
      
    }
}
