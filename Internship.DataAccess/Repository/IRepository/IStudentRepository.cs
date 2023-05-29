using Internship.Models;
using Internship.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship.DataAccess.Repository.IRepository
{
    public interface IStudentRepository : IRepository<Student_User>
    {
        void Update(Student_User obj);
    }
}
