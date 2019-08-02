namespace NetFlow.Web.ViewModels.Courses
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CreateCourseViewModel
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
       
        public string Description { get; set; }

        public int Credit { get; set; }

        public DateTime StartDate { get; set; } = DateTime.UtcNow;

        public DateTime EndDate { get; set; } = DateTime.UtcNow.AddDays(30);

        [Display(Name = "Choose Teacher")]
        public string TeacherId { get; set; }

        public IEnumerable<SelectListItem> Teachers { get; set; }
    }
}
