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

        public string Status { get; set; }

    }
   
}
