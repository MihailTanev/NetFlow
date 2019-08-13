namespace NetFlow.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Post
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Picture { get; set; }

        public DateTime CreatedDate { get; set; }

        public string PublisherId { get; set; }

        public User Publisher { get; set; }

        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    }
}
