namespace NetFlow.Data.Models
{
    using NetFlow.Data.Models.Enums;

    public class Enrollment
    {
        public string StudentId { get; set; }

        public User Student { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }

        public Grade? Grade { get; set; }

        public string Assignment { get; set; }
    }
}
