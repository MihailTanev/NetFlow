namespace NetFlow.Services.Users.Models
{
    using NetFlow.Data.Models;
    using NetFlow.Services.Mapping;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class UserServiceModel : IMapFrom<User>
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
