using Internship.DataAccess.Data;
using Internship.DataAccess.Repository.IRepository;
using Internship.Models;
using Internship.Models.ViewModels;
using Internship.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Data;
using System.Security.Claims;

namespace InternshipAutomationSystem.Areas.Coordinator.Controllers
{
    [Area("Coordinator")]
    [Authorize(Roles = SD.Role_User_Coordinator)]
    public class CoordinatorController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ILogger<CoordinatorController> _logger;
        private readonly IWebHostEnvironment _HostEnvironment;
        private readonly ApplicationDbContext _dbContext;
        private readonly PdfGenerator _pdfGenerator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;

        public CoordinatorController(ILogger<CoordinatorController> logger,
            IWebHostEnvironment hostEnvironment,
            IUnitOfWork unitOfWork,
            PdfGenerator pdfGenerator,
            IWebHostEnvironment env,
            ApplicationDbContext dbContext,
            UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _pdfGenerator = pdfGenerator;
            _env = env;
            _HostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<Announcement> AnnouncementList = _unitOfWork.Announcements.GetAll();
            return View(AnnouncementList);
        }

        public IActionResult Application_Form_Approval()
        {
       
            IEnumerable<Student_User> students = _unitOfWork.Students.GetAll();

            var applicationFormsList = students.Select(student => new submittedApplicationForms
            {
                Student = student,
                StudentId = student.StudentId,
                Id = student.submittedApplicationFormId != null ? int.Parse(student.submittedApplicationFormId) : 0,
                PDFForm = student.submittedApplicationForm != null ? student.submittedApplicationForm.PDFForm : null,
            }); ;

            return View(applicationFormsList);
        }

    

        public IActionResult Application_Form_View(int applicationFormId)
        {

         
            var submittedForm = _unitOfWork.SubmittedApplicationForms.GetAll()
         .FirstOrDefault(form => form.Id == applicationFormId);

            if (submittedForm == null)
            {
                return NotFound();
            }

            submittedForm.Student = _unitOfWork.SubmittedApplicationForms.GetFirstOrDefault(u => u.Id == submittedForm.Id).Student;
            submittedForm.InternshipCoordinator = _unitOfWork.SubmittedApplicationForms.GetFirstOrDefault(u => u.Id == submittedForm.Id).InternshipCoordinator;

          

            return View(submittedForm);


        }

        public IActionResult GetPdf(int applicationFormId)
        {
            var submittedForm = _unitOfWork.SubmittedApplicationForms.GetAll()
         .FirstOrDefault(form => form.Id == applicationFormId);

            if (submittedForm == null || string.IsNullOrEmpty(submittedForm.PDFForm))
            {
                return NotFound();
            }

            string filePath = submittedForm.PDFForm;
            byte[] fileBytes = System.IO.File.ReadAllBytes(Path.Combine(_HostEnvironment.WebRootPath, filePath));
            return File(fileBytes, "application/pdf");

        }


        public IActionResult ApplicationForm_Approvel_Upsert(int? Id)
        {

            FinalApplicationVM FinalApplicationVM = new()
            {
                SubmittedApplicationForms = new(),

                StudentList = _unitOfWork.Students.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.StudentId.ToString(),
                    Selected = i.submittedApplicationFormId != null
                }),

                CoordinatorList = _unitOfWork.Coordinators.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })

            };


            if (Id == null || Id == 0)
            {
                return View(FinalApplicationVM);
            }

            else
            {
                FinalApplicationVM.SubmittedApplicationForms = _unitOfWork.SubmittedApplicationForms.GetFirstOrDefault(u => u.Id == Id, includeProperties: "Student,InternshipCoordinator");
                

                return View(FinalApplicationVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ApplicationForm_Approvel_Upsert(FinalApplicationVM obj, IFormFile file)
        {
       


            if (ModelState.IsValid)
            {

                obj.SubmittedApplicationForms = _unitOfWork.SubmittedApplicationForms.GetFirstOrDefault(u => u.Id == obj.SubmittedApplicationForms.Id, includeProperties: "Student,InternshipCoordinator");
                obj.SubmittedApplicationForms.PDFForm = _unitOfWork.SubmittedApplicationForms.GetFirstOrDefault(u => u.Id == obj.SubmittedApplicationForms.Id).PDFForm;
                obj.SubmittedApplicationForms.Student = _unitOfWork.SubmittedApplicationForms.GetFirstOrDefault(u => u.Id == obj.SubmittedApplicationForms.Id).Student;
                obj.SubmittedApplicationForms.InternshipCoordinator = _unitOfWork.SubmittedApplicationForms.GetFirstOrDefault(u => u.Id == obj.SubmittedApplicationForms.Id).InternshipCoordinator;
              
                string wwwRootPath = _HostEnvironment.WebRootPath;

                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"PDFCAN\Forms");
                    var extension = Path.GetExtension(file.FileName);

                    if (obj.SubmittedApplicationForms.Id != null || obj.SubmittedApplicationForms.Id != 0)
                    {
                        var oldFilePath = Path.Combine(wwwRootPath, obj.SubmittedApplicationForms.PDFForm.TrimStart('\\'));
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    obj.SubmittedApplicationForms.PDFForm = @"PDFCAN\Forms\" + fileName + extension;

                    // Update the Status property for the student
                    var student = _dbContext.Students.FirstOrDefault(s => s.StudentId == obj.SubmittedApplicationForms.StudentId);
                    if (student != null)
                    {
                        if (student.Status == "Pending")
                        {
                            student.Status = "Completed";
                        }
                    }
                }
                Student_User studentUser = _dbContext.Students
                .Include(s => s.User)
                .FirstOrDefault(s => s.UserId == obj.SubmittedApplicationForms.Student.UserId);

                if (studentUser.User.Status == "notyet")
                {
                    studentUser.User.Status = "applicationsubmit";
                }
                else
                {
                    return NotFound();
                }
                if (obj.SubmittedApplicationForms.Id == 0)
                {
                    _unitOfWork.SubmittedApplicationForms.Add(obj.SubmittedApplicationForms);
                }
                else
                {
                    _unitOfWork.SubmittedApplicationForms.Update(obj.SubmittedApplicationForms);
                }

                _unitOfWork.Save();

                var student2 = _dbContext.Students.FirstOrDefault(s => s.StudentId == obj.SubmittedApplicationForms.StudentId);

                if (student2 != null)
                {
                    student2.submittedApplicationFormId = obj.SubmittedApplicationForms.Id.ToString();

                    _dbContext.SaveChanges();
                }
                return RedirectToAction("Success");
            }
            return View(obj);
        }


        //Official letter
        public IActionResult Official_Letter_Approval()
        {

            IEnumerable<Student_User> students = _unitOfWork.Students.GetAll();

            var applicationFormsList = students.Select(student => new OfficialLetter
            {
                Student = student,
                StudentId = student.StudentId,
                Id = student.OfficialLetterId != null ? int.Parse(student.OfficialLetterId) : 0,
                OfficialLetterPDF = student.OfficialLetter != null ? student.OfficialLetter.OfficialLetterPDF : null,
            }); ;

            return View(applicationFormsList);
        }

        public IActionResult Official_Letter_Upsert(int? Id)
        {

            OfficialLetterVM OfficialLetterVM = new()
            {
                OfficialLetters = new(),

                StudentList = _unitOfWork.Students.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.StudentId.ToString(),
                    Selected = i.OfficialLetterId != null
                }),

                CoordinatorList = _unitOfWork.Coordinators.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
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
        public IActionResult Official_Letter_Upsert(OfficialLetterVM obj, IFormFile file)
        {
            var Student = _unitOfWork.Students.GetFirstOrDefault(u => u.StudentId == obj.OfficialLetters.StudentId);
            var InternshipCoordinator = _unitOfWork.Coordinators.GetFirstOrDefault(u => u.Id == obj.OfficialLetters.InternshipCoordinatorId);
            obj.OfficialLetters.InternshipCoordinator = InternshipCoordinator;
            obj.OfficialLetters.Student = Student;
            bool FirstInternship = obj.OfficialLetters.Student.HasTakenFirstInternship;
            bool SecondInternship = obj.OfficialLetters.Student.HasTakenSecondInternship;

            if (FirstInternship == false && SecondInternship == false)
            {
                obj.OfficialLetters.NumOfInternships = 1;
            }
            else if(FirstInternship == true && SecondInternship == false)
            {
                obj.OfficialLetters.NumOfInternships = 1;
            }
            else if (FirstInternship == false && SecondInternship == true)
            {
                obj.OfficialLetters.NumOfInternships = 1;
            }
            else
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                OfficialLetter existingOfficialLetter = null;
                if (obj.OfficialLetters.Id != 0)
                {
                    existingOfficialLetter = _unitOfWork.OfficialLetters.GetFirstOrDefault(u => u.Id == obj.OfficialLetters.Id);
                    obj.OfficialLetters.OfficialLetterPDF = existingOfficialLetter.OfficialLetterPDF;
                    obj.OfficialLetters.Student = existingOfficialLetter.Student;
                }

               
             


                PdfGenerator pdfGenerator = new PdfGenerator(_HostEnvironment);
                pdfGenerator.GeneratePdfOfficialLetter(obj.OfficialLetters);

                // Get the file path of the generated PDF
                string wwwrootPath = _HostEnvironment.WebRootPath;
                string filePath = Path.Combine(wwwrootPath, "PDF", "OfficialLetterT.pdf");

                // Read the file contents into a byte array
                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

                // Set the content type and file name for the response
                string contentType = "application/pdf";

                var generatedPdfFile = new FormFile(new MemoryStream(fileBytes), 0, fileBytes.Length, "file", "OfficialLetterT.pdf");

                file = generatedPdfFile;



                if (file != null)
                {
                    string wwwRootPath = _HostEnvironment.WebRootPath;

                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"PDFCAN\OfficialLetter");
                    var extension = Path.GetExtension(file.FileName);

                    if (obj.OfficialLetters.OfficialLetterPDF != null)
                    {
                        var oldFilePath = Path.Combine(wwwRootPath, obj.OfficialLetters.OfficialLetterPDF.TrimStart('\\'));
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    obj.OfficialLetters.OfficialLetterPDF = @"PDFCAN\OfficialLetter\" + fileName + extension;

                    // Update the Status property for the student
                    var student = _dbContext.Students.FirstOrDefault(s => s.StudentId == obj.OfficialLetters.StudentId);
                    if (student != null)
                    {
                        Student_User studentUser = _dbContext.Students
                            .Include(s => s.User)
                            .FirstOrDefault(s => s.UserId == student.UserId);

                        if (studentUser.User.Status == "applicationsubmit")
                        {
                            studentUser.User.Status = "OfficialLetter";
                        }
                        else if (studentUser.User.Status == "HealthInsurance")
                        {
                            studentUser.User.Status = "Letter-Health";
                        }
                    }
                    else
                    {
                        return NotFound();
                    }
                }

                if (obj.OfficialLetters.Id == 0)
                {
                    _unitOfWork.OfficialLetters.Add(obj.OfficialLetters);
                }
                else
                {
                    _unitOfWork.OfficialLetters.Update(obj.OfficialLetters);
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


       

        
        
        public IActionResult AFA()
        {
            return View();
        }



        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var ApplicationList = _unitOfWork.SubmittedApplicationForms.GetAll();
            return Json(new { data = ApplicationList });
        }

        [HttpDelete]

        public IActionResult DeletePost(int? Id)
        {
            var obj = _unitOfWork.SubmittedApplicationForms.GetFirstOrDefault(u => u.Id == Id);

            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldImagePath = Path.Combine(_HostEnvironment.WebRootPath, obj.PDFForm.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.SubmittedApplicationForms.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "blog deleted successfuly" });
            return RedirectToAction("Index");

            return View(Id);

        }


        #endregion

    }
}
