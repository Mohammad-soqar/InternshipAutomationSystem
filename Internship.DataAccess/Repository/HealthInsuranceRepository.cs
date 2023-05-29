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
    public class HealthInsuranceRepository : Repository<HealthInsurance>, IHealthInsuranceRepository
    {
        private readonly ApplicationDbContext _db;

        public HealthInsuranceRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;

        }


        public void Update(HealthInsurance obj)
        {
            var objFromDb = _db.HealthInsurances.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.HealthInsurancePDF = obj.HealthInsurancePDF;
  
               

            }
        }
    }
}
