using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Internship.Models.ViewModels
{
    public class SaviewVM
    {
        public IEnumerable<Student_User> Student_User { get; set; }

        public IEnumerable<submittedApplicationForms> submittedApplicationForms { get; set; }
    }
}
