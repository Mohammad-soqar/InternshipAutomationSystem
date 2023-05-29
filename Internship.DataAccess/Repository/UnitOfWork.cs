using Internship.DataAccess.Data;
using Internship.DataAccess.Repository.IRepository;
using Internship.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            InternshipOpportunities = new InternshipOpportunityRepository(_db);
            Coordinators = new CoordinatorRepository(_db);
            Announcements = new AnnouncementRepository(_db);
            Applications = new ApplicationRepository(_db);
            Students = new StudentRepository(_db);
            SubmittedApplicationForms = new SubmittedApplicationFormsRepository(_db);
            HealthInsurances = new HealthInsuranceRepository(_db);
            CareerCenters = new CareerCenterRepository(_db);
            OfficialLetters = new OfficialLetterRepository(_db);



        }

        public IInternshipOpportunityRepository InternshipOpportunities { get; private set; }
        public ICoordinatorRepository Coordinators { get; private set; }
        public IAnnouncementRepository Announcements { get; private set; }
        public IApplicationRepository Applications { get; private set; }
        public IStudentRepository Students { get; private set; }
        public ISubmittedApplicationFormsRepository SubmittedApplicationForms { get; private set; }
        public IHealthInsuranceRepository HealthInsurances { get; private set; }
        public ICareerCenterRepository CareerCenters { get; private set; }
        public IOfficialLetterRepository OfficialLetters { get; private set; }



        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
