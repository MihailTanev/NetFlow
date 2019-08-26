namespace NetFlow.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;

    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }
        
        public DateTime CreatedOn { get; set; }

        public string Picture { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; } = new HashSet<Enrollment>();

        public ICollection<Post> Posts { get; set; } = new HashSet<Post>();

        public ICollection<Course> Courses { get; set; } = new HashSet<Course>();

        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
        
    }
}
