using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship.Models
{
    public class Announcement
    {
        [Required]
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public int? CareerCenterId { get; set; }
        [ValidateNever]
        public CareerCenter_User CareerCenter { get; set; }



        [ValidateNever]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm tt}")]
        public DateTime? DatePosted { get; set; }//





    //AnnouncementID(Primary Key)
    //SenderID(Foreign Key to User Table)
    //ReceiverID(Foreign Key to User Table)
    //Subject
    //Content
    //DatePosted
}
}
