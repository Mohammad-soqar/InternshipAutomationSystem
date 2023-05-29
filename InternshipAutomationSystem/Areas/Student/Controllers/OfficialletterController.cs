using Internship.DataAccess.Data;
using Internship.DataAccess.Repository.IRepository;
using Internship.Models;
using Internship.Models.ViewModels;
using Internship.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace InternshipAutomationSystem.Areas.Student.Controllers
{
    [Area("Student")]
    [Authorize(Roles = SD.Role_User_Student)]
    public class OfficialletterController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _HostEnvironment;
        private readonly PdfGenerator _pdfGenerator;
        private readonly ApplicationDbContext _dbContext;

        public IActionResult Index()
        {
            return View();
        }

        public OfficialletterController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment, PdfGenerator pdfGenerator, ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _pdfGenerator = pdfGenerator;
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _HostEnvironment = hostEnvironment;
        }

        public IActionResult Letter_Upsert(int? Id)
        {

            OfficialLetterVM OfficialLetterVM = new()
            {
                OfficialLetters = new(),

                StudentList = _unitOfWork.Students.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.StudentId.ToString(),
                    Selected = i.HealthInsuranceId != null
                }),
                CoordinatorList = _unitOfWork.Coordinators.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString(),

                }),

            };


            if (Id == null || Id == 0)
            {
                return View(OfficialLetterVM);
            }

            else
            {
                OfficialLetterVM.OfficialLetters = _unitOfWork.OfficialLetters.GetFirstOrDefault(u => u.Id == Id);
                return View(OfficialLetterVM);
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Letter_Upsert(OfficialLetterVM obj)
        {
            var Student = _unitOfWork.Students.GetFirstOrDefault(u => u.StudentId == obj.OfficialLetters.StudentId);
            var InternshipCoordinator = _unitOfWork.Coordinators.GetFirstOrDefault(u => u.Id == obj.OfficialLetters.InternshipCoordinatorId);
            obj.OfficialLetters.InternshipCoordinator = InternshipCoordinator;
            obj.OfficialLetters.Student = Student;

            if (ModelState.IsValid)
            {
               

                if (obj.OfficialLetters.Id == 0)
                {
                    _unitOfWork.OfficialLetters.Add(obj.OfficialLetters);
                }
                else
                {
                    _unitOfWork.OfficialLetters.Update(obj.OfficialLetters);
                }
                if (obj.OfficialLetters.NumOfInternships == 0)
                {
                    return RedirectToAction("noooope");

                }
                _unitOfWork.Save();

                var student2 = _dbContext.Students.FirstOrDefault(s => s.StudentId == obj.OfficialLetters.StudentId);

                if (student2 != null)
                {
                    student2.OfficialLetterId = obj.OfficialLetters.Id.ToString();
                 

                    _dbContext.SaveChanges();
                }
                return RedirectToAction("Success");
            }
            return View(obj);
        }

    }
}
