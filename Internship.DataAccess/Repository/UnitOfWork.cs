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
            InternshipJobs = new InternshipJobRepository(_db);
           

        }

        public IInternshipJobRepository InternshipJobs { get; private set; }

       

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
