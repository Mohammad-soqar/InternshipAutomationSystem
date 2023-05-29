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
    public class ApplicationRepository : Repository<ApplicationForm>, IApplicationRepository
    {
        private readonly ApplicationDbContext _db;

        public ApplicationRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;

        }


        public void Update(ApplicationForm obj)
        {
            var objFromDb = _db.ApplicationForms.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.TC = obj.TC;
                objFromDb.Name = obj.Name;
                objFromDb.SID = obj.SID;
                objFromDb.PhoneNumber = obj.PhoneNumber;
                objFromDb.Faculty = obj.Faculty;
                objFromDb.Class = obj.Class;
                objFromDb.Department = obj.Department;
                objFromDb.StartDate = obj.StartDate;
                objFromDb.EndDate = obj.EndDate;
                objFromDb.Type = obj.Type;
                objFromDb.DependendParent = obj.DependendParent;
                objFromDb.InstitutionName = obj.InstitutionName;
                objFromDb.InstitutionAddress = obj.InstitutionAddress;
                objFromDb.InstitutionPerson = obj.InstitutionPerson;
                objFromDb.InstitutionPhoneNumber = obj.InstitutionPhoneNumber;
            }
        }
    }
}
