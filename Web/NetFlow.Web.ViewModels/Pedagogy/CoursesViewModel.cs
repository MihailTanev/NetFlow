namespace NetFlow.Web.ViewModels.Pedagogics
{
    using NetFlow.Services.Teachers.Models;
    using System.Collections.Generic;

    public class CoursesViewModel
    {
        public IEnumerable<CoursesServiceModel> Courses { get; set; }

    }
}
