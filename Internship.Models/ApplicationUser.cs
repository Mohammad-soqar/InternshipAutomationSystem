using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int UserType { get; set; }
    }
    public enum UserType
    {
        Student_User = 1,
        InternshipCoordinator_User = 2,
        CareerCenter_User = 3,
        Admin_User = 4,

    }
}
