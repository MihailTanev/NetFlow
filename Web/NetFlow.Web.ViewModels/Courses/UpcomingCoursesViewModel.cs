namespace NetFlow.Web.ViewModels.Courses
{
    using NetFlow.Services.Courses.Models;
    using System.Collections.Generic;

    public class UpcomingCoursesViewModel
    {
        public IEnumerable<CourseServiceModel> Courses { get; set; }

    }
}
