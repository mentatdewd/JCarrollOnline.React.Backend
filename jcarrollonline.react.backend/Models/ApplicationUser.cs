using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jcarrollonline.react.backend.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Custom { get; set; }

        public virtual ICollection<ForumThread> ForumThreads { get; }
    }
}