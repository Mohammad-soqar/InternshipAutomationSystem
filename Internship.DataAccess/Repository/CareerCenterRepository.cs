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
    public class CareerCenterRepository : Repository<CareerCenter_User>, ICareerCenterRepository
    {
        private readonly ApplicationDbContext _db;

        public CareerCenterRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;

        }


        public void Update(CareerCenter_User obj)
        {
            var objFromDb = _db.Coordinators.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = obj.Name;
                objFromDb.Email = obj.Email;
                objFromDb.PhoneNumber = obj.PhoneNumber;
               

            }
        }
    }
}
