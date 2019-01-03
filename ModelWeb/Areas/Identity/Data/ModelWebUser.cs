using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ModelWeb.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the ModelWebUser class
    public class ModelWebUser : IdentityUser
    {
        [PersonalData]
        public int PhoneNumber { get; set; }
        [PersonalData]
        public string Name { get; set; }
    }
}
