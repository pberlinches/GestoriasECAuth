using System;
using Microsoft.AspNetCore.Identity;

namespace ECAuth.Domain
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public bool Enabled { get; set; } = true;

        public bool ChangePasswordNextLogin { get; set; } = false;

        public DateTimeOffset? PasswordExpiration { get; set; }
    }
}
