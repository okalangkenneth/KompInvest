using Microsoft.AspNetCore.Identity;
using System;

namespace KompInvest.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Properties from the original User class
        public DateTime DateJoined { get; set; }
        public bool IsAdmin { get; set; }

        // Navigation Property for MemberProfile
        public virtual MemberProfile MemberProfile { get; set; }

        
    }
}

