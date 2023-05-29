using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship.Models.ViewModels
{
    public class submissionstatus
    {
        public int Id { get; set; }

        public int StudentId { get; set; }
        [ValidateNever]
        public Student_User Student { get; set; }

        public bool Thesubmissionstatus { get; set; }

    }
}
