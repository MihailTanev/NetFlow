namespace NetFlow.Web.ViewModels.Courses
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using NetFlow.Common.GlobalConstants;
    using NetFlow.Services.Courses.Models;
    using NetFlow.Services.Mapping;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CreateCourseViewModel : IMapTo<CourseServiceModel>
    {
        public int Id { get; set; }

        [Required]
        [MinLength(CourseConstants.COURSE_NAME_MIN_LENGTH)]
        [MaxLength(CourseConstants.COURSE_NAME_MAX_LENGTH)]
        public string Name { get; set; }

        [Required]
        [MaxLength(CourseConstants.COURSE_DESCRIPTION_MAX_LENGTH)]
        public string Description { get; set; }

        [Required]
        public int Credit { get; set; }

        [Display(Name = CourseConstants.COURSE_START_DATE)]
        public DateTime StartDate { get; set; } = DateTime.UtcNow;

        public IFormFile Picture { get; set; }


        [Display(Name = CourseConstants.COURSE_END_DATE)]
        public DateTime EndDate { get; set; } = DateTime.UtcNow.AddDays(30);

        [Required]
        [Display(Name = "Choose Teacher")]
        public string TeacherId { get; set; }

        public IEnumerable<SelectListItem> Teachers { get; set; }
    }
}
