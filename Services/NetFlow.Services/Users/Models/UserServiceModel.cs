namespace NetFlow.Services.Users.Models
{
    using NetFlow.Data.Models;
    using Stopify.Services.Mapping;
    using System;

    public class UserServiceModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }
    }
}
