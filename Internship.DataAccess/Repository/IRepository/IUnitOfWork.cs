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

        void Save();
    }
}
