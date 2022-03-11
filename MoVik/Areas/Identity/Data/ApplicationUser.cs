using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MoVik.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public String Name { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public String Phone { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public String Address { get; set; }

    }
}
