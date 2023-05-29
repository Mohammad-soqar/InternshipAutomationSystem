using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship.Models
{
    public class OfficialLetter
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyEmail { get; set; }
        public string ReceiverName { get; set; }
        public string Department { get; set; }
        public int NumOfInternships { get; set; }

        [ValidateNever]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm tt}")]
        public DateTime? DatePosted { get; set; }//

        [ValidateNever]
        public string? OfficialLetterPDF { get; set; }

        public int StudentId { get; set; }
        [ValidateNever]
        public Student_User Student { get; set; }

        public int InternshipCoordinatorId { get; set; }
        [ValidateNever]
        public InternshipCoordinator_User InternshipCoordinator { get; set; }

    }
}
