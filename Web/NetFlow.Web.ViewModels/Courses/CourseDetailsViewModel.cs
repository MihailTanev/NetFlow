namespace NetFlow.Web.ViewModels.Courses
{
    using NetFlow.Data.Models;
    using NetFlow.Services.Courses.Models;
    using NetFlow.Services.Mapping;
    using System;

    public class CourseDetailsViewModel : IMapFrom<CourseServiceModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Credit { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Picture { get; set; }

        public User Teacher { get; set; }

        public bool StudentIsEnrolledInCourse { get; set; }

    }
}
