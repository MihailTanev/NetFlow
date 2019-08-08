namespace NetFlow.Services.Teachers.Models
{
    using NetFlow.Data.Models;
    using NetFlow.Services.Mapping;
    using System;

    public class CoursesServiceModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
