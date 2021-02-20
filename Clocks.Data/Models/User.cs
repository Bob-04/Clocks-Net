using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Clocks.Data.Models
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Clock> Clocks { get; set; }
    }
}
