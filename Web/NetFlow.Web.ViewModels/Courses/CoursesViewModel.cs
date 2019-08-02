namespace NetFlow.Web.ViewModels.Courses
{
    using NetFlow.Services.Courses.Models;
    using System.Collections.Generic;

    public class CoursesViewModel
    {
        public IEnumerable<CourseServiceModel> Users { get; set; }

    }
}
