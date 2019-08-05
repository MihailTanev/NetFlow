namespace NetFlow.Web.Areas.Trainings.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using NetFlow.Services.Courses.Interface;
    using NetFlow.Services.Mapping;
    using NetFlow.Web.ViewModels.Courses;
    using System.Threading.Tasks;

    public class CoursesController : BaseTrainingsController
    {
        private readonly ICourseService courseService;
        public CoursesController(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        public async Task<IActionResult> Index()
        {
            var courses = new CoursesIndexViewModel
            {
                Courses = await this.courseService.GetAllCourses()
            };

            return this.View(courses);
        }

        public IActionResult Details(int courseId)
        {     
            CourseDetailsViewModel model = this.courseService.GetCourseById(courseId)
                .To<CourseDetailsViewModel>();

            return this.View(model);
        }


    }
}