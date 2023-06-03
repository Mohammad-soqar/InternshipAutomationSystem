using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Internship.Models
{
    public class InternshipOpportunity
    {
        [Required]
        public int Id { get; set; }//
        public string CompanyName { get; set; }//
        public string JobTitle { get; set; }//
        public string Location { get; set; }//


        [ValidateNever]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm tt}")]
        public DateTime? DatePosted { get; set; }//
        public string SkillsReq { get; set; }//
        public string type { get; set; }//
        public string Site { get; set; }//

        [ValidateNever]
        public string ImageUrl { get; set; }//
        public string Description { get; set; }//

        private ICollection<Student_User> _savedByStudents;

        [ValidateNever]
        public ICollection<Student_User> SavedByStudents
        {
            get => _savedByStudents ??= new List<Student_User>();
            set => _savedByStudents = value;
        }
    }
}
