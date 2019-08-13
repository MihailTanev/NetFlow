namespace NetFlow.Web.ViewModels.Courses
{
    using NetFlow.Common.GlobalConstants;
    using NetFlow.Services.Courses.Models;
    using NetFlow.Services.Mapping;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class EditCourseViewModel : IMapFrom<CourseServiceModel>
    {
        public int Id { get; set; }

        [Required]
        [MinLength(CourseConstants.COURSE_NAME_MIN_LENGTH)]
        [MaxLength(CourseConstants.COURSE_NAME_MAX_LENGTH)]
        public string Name { get; set; }

        [Required]
        [MaxLength(CourseConstants.COURSE_DESCRIPTION_MAX_LENGTH)]
        public string Description { get; set; }

        public int Credit { get; set; }

        [Display(Name = CourseConstants.COURSE_START_DATE)]
        public DateTime StartDate { get; set; }

        [Display(Name = CourseConstants.COURSE_END_DATE)]
        public DateTime EndDate { get; set; }

    }
}
