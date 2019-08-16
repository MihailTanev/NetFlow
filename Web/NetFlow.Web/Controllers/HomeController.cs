namespace NetFlow.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using NetFlow.Services.Courses.Interface;
    using NetFlow.Services.Search.Interface;
    using NetFlow.Web.Models;
    using NetFlow.Web.ViewModels.Home;

    public class HomeController : Controller
    {
        private readonly ICourseService courseService;
        private readonly ISearchService searchService;


        public HomeController(ICourseService courseService, ISearchService searchService)
        {
            this.courseService = courseService;
            this.searchService = searchService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomeViewModel()
            {
                Courses = await this.courseService.GetIndexCourses()
            };
            return this.View(model);
        }

        [Route("Product")]
        public IActionResult Product()
        {
            return View();
        }

        [Route("Contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [Route("Search")]
        public async Task<IActionResult> Search(SearchInputModel model)
        {
            var searchViewModel = new SearchViewModel
            {
                SearchString = model.SearchString
            };

            searchViewModel.Courses = await this.searchService.SearchCoursesAsync(model.SearchString);

            return this.View(searchViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
