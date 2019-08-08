namespace NetFlow.Web.ViewModels.Pedagogics
{
    using NetFlow.Services.Teachers.Models;
    using System.Collections.Generic;

    public class StudentsEnrolledInCourseViewModel
    {
        public IEnumerable<StudentsEnrolledInCourseServiceModel> Students { get; set; }

        public CoursesServiceModel Course { get; set; }
    }
}
