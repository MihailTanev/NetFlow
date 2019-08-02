using System;

namespace NetFlow.Web.ViewModels.User
{
    public class UsersViewModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
