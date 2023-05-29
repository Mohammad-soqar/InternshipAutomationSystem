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
    public class OfficialLetterRepository : Repository<OfficialLetter>, IOfficialLetterRepository
    {
        private readonly ApplicationDbContext _db;

        public OfficialLetterRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;

        }


        public void Update(OfficialLetter obj)
        {
            var objFromDb = _db.OfficialLetters.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.CompanyName = obj.CompanyName;
                objFromDb.ReceiverName = obj.ReceiverName;
                objFromDb.CompanyEmail = obj.CompanyEmail;
                objFromDb.Department = obj.Department;
                objFromDb.OfficialLetterPDF = obj.OfficialLetterPDF;
                


            }
        }
    }
}
