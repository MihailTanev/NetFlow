namespace NetFlow.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System;

    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }
        
        public DateTime CreatedOn { get; set; }
    }
}
