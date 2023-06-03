using Internship.DataAccess.Data;
using Internship.DataAccess.Repository.IRepository;
using Internship.Models;
using Internship.Models.ViewModels;
using Internship.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Policy;

namespace InternshipAutomationSystem.Areas.CareerCenter.Controllers
{
    [Area("CareerCenter")]
    [Authorize(Roles = SD.Role_Career_Center)]
    public class CareercenterController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _HostEnvironment;
        public CareercenterController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment, ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            _unitOfWork = unitOfWork;
            _HostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            IEnumerable<Student_User> students = _unitOfWork.Students.GetAll();

            var applicationFormsList = students.Select(student => new HealthInsurance
            {
                Student = student,
                StudentId = student.StudentId,
                Id = student.HealthInsuranceId != null ? int.Parse(student.HealthInsuranceId) : 0,
                HealthInsurancePDF = student.HealthInsurance != null ? student.HealthInsurance.HealthInsurancePDF : null,
            }); ;

            return View(applicationFormsList);
        }

        public IActionResult Health_Insurance_Approval()
        {

            IEnumerable<Student_User> students = _unitOfWork.Students.GetAll();

            var applicationFormsList = students.Select(student => new HealthInsurance
            {
                Student = student,
                StudentId = student.StudentId,
                Id = student.HealthInsuranceId != null ? int.Parse(student.HealthInsuranceId) : 0,
                HealthInsurancePDF = student.HealthInsurance != null ? student.HealthInsurance.HealthInsurancePDF : null,
            }); ;

            return View(applicationFormsList);
        }

        public IActionResult Old_Health_Insurance_Approval()
        {

            IEnumerable<Student_User> students = _unitOfWork.Students.GetAll();

            var applicationFormsList = students.Select(student => new HealthInsurance
            {
                Student = student,
                StudentId = student.StudentId,
                Id = student.HealthInsuranceId != null ? int.Parse(student.HealthInsuranceId) : 0,
                HealthInsurancePDF = student.HealthInsurance != null ? student.HealthInsurance.HealthInsurancePDF : null,
            }); ;

            return View(applicationFormsList);
        }

        public IActionResult Health_Upsert(int? Id)
        {

            HealthVM HealthVM = new()
            {
                HealthInsurances = new(),

                StudentList = _unitOfWork.Students.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.StudentId.ToString(),
                    Selected = i.HealthInsuranceId != null
                }),
                CareerCenterList = _unitOfWork.CareerCenters.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString(),

                }),

            };


            if (Id == null || Id == 0)
            {
                return View(HealthVM);
            }

            else
            {
                HealthVM.HealthInsurances = _unitOfWork.HealthInsurances.GetFirstOrDefault(u => u.Id == Id);
                return View(HealthVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Health_Upsert(HealthVM obj, IFormFile file)
        {


            if (ModelState.IsValid)
            {
                if (obj.HealthInsurances.Id != 0)
                {
                    obj.HealthInsurances.HealthInsurancePDF = _unitOfWork.HealthInsurances.GetFirstOrDefault(u => u.Id == obj.HealthInsurances.Id).HealthInsurancePDF;
                    obj.HealthInsurances.Student = _unitOfWork.HealthInsurances.GetFirstOrDefault(u => u.Id == obj.HealthInsurances.Id).Student;
                    obj.HealthInsurances.CareerCenter = _unitOfWork.HealthInsurances.GetFirstOrDefault(u => u.Id == obj.HealthInsurances.Id).CareerCenter;
                }

                string wwwRootPath = _HostEnvironment.WebRootPath;

                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"PDFCAN\Health_Insurance");
                    var extension = Path.GetExtension(file.FileName);

                    if (obj.HealthInsurances.Id != 0)
                    {
                        var oldFilePath = Path.Combine(wwwRootPath, obj.HealthInsurances.HealthInsurancePDF.TrimStart('\\'));
                        if (System.IO.File.Exists(oldFilePath)) 
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    obj.HealthInsurances.HealthInsurancePDF = @"PDFCAN\Health_Insurance\" + fileName + extension;

                    // Update the Status property for the student
                    var student = _dbContext.Students.FirstOrDefault(s => s.StudentId == obj.HealthInsurances.StudentId);
                    if (student != null)
                    {
                        Student_User studentUser = _dbContext.Students
                            .Include(s => s.User)
                            .FirstOrDefault(s => s.UserId == student.UserId);

                        if (studentUser.HealthInsuranceStatus.ToLower() == "pending")
                        {
                            studentUser.HealthInsuranceStatus = "completed";
                           
                        }
                        else
                        {
                            return NotFound();
                        }
                        if (studentUser.User.Status == "approved")
                        {
                            studentUser.User.Status = "health_insurance";
                        }
                        else if (studentUser.User.Status == "official-letter")
                        {
                            studentUser.User.Status = "health-letter";
                        }
                    }
                    else
                    {
                        return NotFound();
                    }


                }
                

                if (obj.HealthInsurances.Id == 0)
                {
                    _unitOfWork.HealthInsurances.Add(obj.HealthInsurances);
                }
                else
                {
                    _unitOfWork.HealthInsurances.Update(obj.HealthInsurances);
                }
                _unitOfWork.Save();

                var student2 = _dbContext.Students.FirstOrDefault(s => s.StudentId == obj.HealthInsurances.StudentId);

                if (student2 != null)
                {
                    student2.HealthInsuranceId = obj.HealthInsurances.Id.ToString();

                    _dbContext.SaveChanges();
                }
               
                return RedirectToAction("Success");
            }
            return View(obj);
        }

        public IActionResult Success()
        {
            return View();
        }


    }
}
