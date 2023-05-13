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
    public class InternshipOpportunityRepository : Repository<InternshipOpportunity>, IInternshipOpportunityRepository
    {
        private readonly ApplicationDbContext _db;

        public InternshipOpportunityRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;

        }


        public void Update(InternshipOpportunity obj)
        {
            var objFromDb = _db.InternshipOpportunities.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.CompanyName = obj.CompanyName;
                objFromDb.JobTitle = obj.JobTitle;
                objFromDb.Location = obj.Location;
                objFromDb.SkillsReq = obj.SkillsReq;
                objFromDb.type = obj.type;
                objFromDb.Site = obj.Site;
                objFromDb.Description = obj.Description;
            
                if (objFromDb.ImageUrl != null)
                {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }

            }
        }
    }
}
