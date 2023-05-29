using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship.Models
{
    public class ApplicationForm
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string TC { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string SID { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Faculty { get; set; }
        [Required]
        public string Class { get; set; }
        [Required]
        public string Department { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public int NumberOfDays { get; set; }
    [Required]
        public string Type { get; set; }
        [Required]
        public bool DependendParent { get; set; }
        [Required]
        public string InstitutionName { get; set; }
        [Required]
        public string InstitutionAddress { get; set; }
        [Required]
        public string InstitutionPerson { get; set; }
        [Required]
        public string InstitutionPhoneNumber { get; set; }

       
        public int StudentId { get; set; }
        [ValidateNever]
        public Student_User Student { get; set; }
  
        public int InternshipCoordinatorId { get; set; }
        [ValidateNever]
        public InternshipCoordinator_User InternshipCoordinator { get; set; }
    }
}
