using Internship.DataAccess.Data;
using Internship.DataAccess.Repository.IRepository;
using Internship.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship.DataAccess.Repository
{
    public class AnnouncementRepository : Repository<Announcement>, IAnnouncementRepository
    {
        private readonly ApplicationDbContext _db;

        public AnnouncementRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;

        }


        public void Update(Announcement obj)
        {
            var objFromDb = _db.Announcements.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Subject = obj.Subject;
                objFromDb.Content = obj.Content;
                objFromDb.CareerCenterId = obj.CareerCenterId;
                objFromDb.DatePosted = obj.DatePosted;
           

            }
        }
    }
}
