using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IInternshipOpportunityRepository InternshipOpportunities { get; }
        ICoordinatorRepository Coordinators { get; }
        IAnnouncementRepository Announcements { get; }
        IApplicationRepository Applications { get; }
        IStudentRepository Students { get; }
        ISubmittedApplicationFormsRepository SubmittedApplicationForms { get; }
        IHealthInsuranceRepository HealthInsurances { get; }
        ICareerCenterRepository CareerCenters { get; }
        IOfficialLetterRepository OfficialLetters { get; }
        IReportRepository Reports { get; }
        ISaveInternshipRepository SavedInternships { get; }

        void Save();
    }
}
