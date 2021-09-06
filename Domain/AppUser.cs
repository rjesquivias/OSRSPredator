using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Domain
{
    public class AppUser: IdentityUser
    {
        public string DisplayName { get; set; }

        public ICollection<UserWatchList> UserWatchList { get; set; }
    }
}