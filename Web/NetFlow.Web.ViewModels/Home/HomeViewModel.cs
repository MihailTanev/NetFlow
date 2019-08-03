namespace NetFlow.Web.ViewModels.Home
{
    using NetFlow.Services.Courses.Models;
    using System.Collections.Generic;

    public class HomeViewModel : SearchInputModel
    {
        public IEnumerable<CourseServiceModel> Courses { get; set; }
    }
}
