using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Internship.Models.ViewModels
{
    public class OfficialLetterVM
    {
        public OfficialLetter OfficialLetters { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> StudentList { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> CoordinatorList { get; set; }

    }
}
