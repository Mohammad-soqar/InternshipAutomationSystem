using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship.Models
{
    public class Student_User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Range(200000000, 299999999)]
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string TC { get; set; }
        public string Faculty { get; set; }
        public string Department { get; set; }
        public string Class { get; set; }
        public bool IsApproved { get; set; }
        public int CoordinatorId { get; set; }

        [ValidateNever]
        public InternshipCoordinator_User Coordinator { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public bool HasTakenFirstInternship { get; set; }
        public bool HasTakenSecondInternship { get; set; }
        public ApplicationForm ApplicationForm { get; set; }


        [ValidateNever]
        public string? submittedApplicationFormId { get; set; }

        public submittedApplicationForms? submittedApplicationForm { get; set; }

        [ValidateNever]
        public string? HealthInsuranceId { get; set; }

        public HealthInsurance? HealthInsurance { get; set; }

        [ValidateNever]
        public string? ReportId { get; set; }

        public Report? Report { get; set; }

        [ValidateNever]
        public string? OfficialLetterId { get; set; }

        public OfficialLetter? OfficialLetter { get; set; }

        public string Status { get; set; } = "N/A";

        [ValidateNever]
        public ICollection<SavedInternship> SavedInternships { get; set; }

        [ValidateNever]

        public string ApplicationFormStatus { get; set; } = "N/A";
        [ValidateNever]

        public string OfficialLetterStatus { get; set; } = "N/A";
        [ValidateNever]

        public string ReportStatus { get; set; } = "N/A";
        [ValidateNever]

        public string HealthInsuranceStatus { get; set; } = "N/A";

    }
  
}
