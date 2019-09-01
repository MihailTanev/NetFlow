namespace NetFlow.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using NetFlow.Data.Models;
    using NetFlow.Services.Blog.Interface;
    using NetFlow.Services.Courses.Interface;
    using NetFlow.Services.Profile.Interface;
    using NetFlow.Services.Search.Interface;
    using NetFlow.Web.Models;
    using NetFlow.Web.ViewModels.Home;

    public class HomeController : Controller
    {
        private readonly ICourseService courseService;
        private readonly ISearchService searchService;
        private readonly IBlogPostService blogPostService;
        private readonly IProfileService profileService;
        private readonly UserManager<User> userManager;

        public HomeController(UserManager<User> userManager, ICourseService courseService, ISearchService searchService, IBlogPostService blogPostService, IProfileService profileService)
        {
            this.userManager = userManager;
            this.profileService = profileService;
            this.blogPostService = blogPostService;
            this.courseService = courseService;
            this.searchService = searchService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomeViewModel()
            {
                Courses = await this.courseService.GetIndexCourses(),
                Posts = await this.blogPostService.GetIndexBlogPosts(),
            };

            if (User.Identity.IsAuthenticated)
            {
                var userId = this.userManager.GetUserId(User);

                model.Profile = await this.profileService.GetProfileByIdAsync(userId);
            }

            return this.View(model);
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

        [Route("About")]
        public IActionResult About()
        {
            return View();
        }

        [Route("Contact")]
        public IActionResult Contact()
        {
            return View();
        }
    }
}
