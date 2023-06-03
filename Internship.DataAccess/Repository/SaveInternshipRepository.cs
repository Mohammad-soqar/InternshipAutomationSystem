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
    public class SaveInternshipRepository : Repository<SavedInternship>, ISaveInternshipRepository
    {
        private readonly ApplicationDbContext _db;

        public SaveInternshipRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;

        }


        public void Update(SavedInternship obj)
        {
            var objFromDb = _db.SavedInternships.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.InternshipOpportunityId = obj.InternshipOpportunityId;
                objFromDb.StudentId = obj.StudentId;



            }
        }
    }
}
