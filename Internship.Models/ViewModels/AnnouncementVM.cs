using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship.Models.ViewModels
{
    public class AnnouncementVM
    {
        public int CareerCenterId { get; set; }
        public Announcement Announcements { get; set; }

    }
}
