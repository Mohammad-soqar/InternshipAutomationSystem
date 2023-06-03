using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship.Models
{
  
        public class SavedInternship
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }

            public int InternshipOpportunityId { get; set; }
            public InternshipOpportunity InternshipOpportunity { get; set; }

            public int StudentId { get; set; }
            public Student_User Student { get; set; }
        }
    
}
