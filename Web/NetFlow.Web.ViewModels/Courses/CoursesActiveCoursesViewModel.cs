namespace NetFlow.Web.ViewModels.Courses
{
    using NetFlow.Services.Courses.Models;
    using NetFlow.Services.Mapping;
    using System.Collections.Generic;

    public class CoursesActiveCoursesViewModel
    {
        public IEnumerable<CourseServiceModel> Courses { get; set; }

    }
}
