namespace NetFlow.Web.ViewModels.Courses
{
    using NetFlow.Services.Courses.Models;
    using System.Collections.Generic;

    public class CoursesIndexViewModel
    {
        public IEnumerable<CourseServiceModel> Courses { get; set; }
    }
}
