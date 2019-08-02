namespace NetFlow.Web.ViewModels
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using NetFlow.Data.Models;
    using NetFlow.Services.Mapping;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CreateCourseViewModel : IMapFrom<Course>
    {
        public int Id { get; set; }
       
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Choose Trainer")]
        public string TeacherId { get; set; }

        public IEnumerable<SelectListItem> Teachers { get; set; }
       
    }
}