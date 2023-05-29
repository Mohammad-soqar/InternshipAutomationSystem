using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship.Models
{
    public class HealthInsurance
    {
        public int Id { get; set; }

        [ValidateNever]
        public string HealthInsurancePDF { get; set; }

        public int StudentId { get; set; }
        [ValidateNever]
        public Student_User Student { get; set; }

        public int CareerCenterId { get; set; }
        [ValidateNever]
        public CareerCenter_User CareerCenter { get; set; }

    }
}
