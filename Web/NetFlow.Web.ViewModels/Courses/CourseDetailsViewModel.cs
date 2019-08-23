namespace NetFlow.Web.ViewModels.Courses
{
    using NetFlow.Services.Courses.Models;

    public class CourseDetailsViewModel 
    {
        public CourseServiceModel Course { get; set; }

        public bool StudentIsEnrolledInCourse { get; set; }

    }
}
