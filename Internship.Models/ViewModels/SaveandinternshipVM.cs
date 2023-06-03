using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship.Models.ViewModels
{
    public class SaveandinternshipVM
    {
        public IEnumerable<InternshipOpportunity> InternshipOpportunity { get; set; }
        public IEnumerable<SavedInternship> SavedInternshipsEn { get; set; }
        public SavedInternship SavedInternships { get; set; }

    }
}
