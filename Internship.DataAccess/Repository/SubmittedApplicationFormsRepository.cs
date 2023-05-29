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
    public class SubmittedApplicationFormsRepository : Repository<submittedApplicationForms>, ISubmittedApplicationFormsRepository
    {
        private readonly ApplicationDbContext _db;

        public SubmittedApplicationFormsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;

        }


        public void Update(submittedApplicationForms obj)
        {
            var objFromDb = _db.submittedApplicationForms.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.PDFForm = obj.PDFForm;
  
               

            }
        }
    }
}
