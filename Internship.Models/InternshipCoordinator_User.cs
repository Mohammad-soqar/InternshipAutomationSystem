using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship.Models
{
    public class InternshipCoordinator_User { 
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public ICollection<ApplicationForm> ApplicationForms { get; set; }
  

    }
}
