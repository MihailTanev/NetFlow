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

    public class CreateCourseViewModel : IMapTo<CourseServiceModel>, IValidatableObject
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
        [Range(0,30)]
        public int Credit { get; set; }

        [Required]
        [Display(Name = CourseConstants.COURSE_START_DATE)]
        public DateTime StartDate { get; set; } = DateTime.UtcNow.Date.Add(new TimeSpan(23,59,0));

        [Required]
        [Display(Name = CourseConstants.COURSE_END_DATE)]
        public DateTime EndDate { get; set; } = DateTime.UtcNow.Date.AddDays(30).Add(new TimeSpan(23,59,0));

        [Required]
        public IFormFile Picture { get; set; }

        [Required]
        [Display(Name = "Teacher")]
        public string TeacherId { get; set; }

        public IEnumerable<SelectListItem> Teachers { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.StartDate < DateTime.UtcNow)
            {
                yield return new ValidationResult($" '{Name}' should be created in the future");
            }

            if (this.StartDate > this.EndDate)
            {
                yield return new ValidationResult($" '{StartDate.ToShortDateString()}' have to be greater then '{EndDate.ToShortDateString()}' ");
            }
        }
    }
}
