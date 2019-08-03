namespace NetFlow.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Course
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Credit { get; set; }

        public string Picture { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string TeacherId { get; set; }

        public User Teacher { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; } = new HashSet<Enrollment>();
    }
}
