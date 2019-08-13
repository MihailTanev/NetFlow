namespace NetFlow.Web.ViewModels.Post
{
    using NetFlow.Services.Blog.Models;
    using System;
    using System.Collections.Generic;

    public class PostDetailsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Content { get; set; }

        public string Picture { get; set; }


        public IEnumerable<CommentsPostDetailsServiceModel> Comments { get; set; }

    }
}
