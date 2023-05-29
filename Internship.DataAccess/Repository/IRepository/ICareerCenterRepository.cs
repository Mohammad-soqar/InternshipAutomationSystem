using Internship.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship.DataAccess.Repository.IRepository
{
    public interface ICareerCenterRepository : IRepository<CareerCenter_User>
    {
        void Update(CareerCenter_User obj);
    }
}
