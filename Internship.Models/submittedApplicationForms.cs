using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship.Models
{
    public class submittedApplicationForms
    {
        public int Id { get; set; }

        [ValidateNever]
        public string PDFForm { get; set; }

        public int StudentId { get; set; }
        [ValidateNever]
        public Student_User Student { get; set; }

        public int InternshipCoordinatorId { get; set; }
        [ValidateNever]
        public InternshipCoordinator_User InternshipCoordinator { get; set; }
    }
}
