using Internship.DataAccess.Data;
using Internship.DataAccess.Repository;
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
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Mail;
using System.Security.Claims;

namespace InternshipAutomationSystem.Areas.Student.Controllers
{
    [Area("Student")]
    [Authorize(Roles = SD.Role_User_Student)]
    public class ApplicationController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _HostEnvironment;
        private readonly PdfGenerator _pdfGenerator;
        private readonly ApplicationDbContext _dbContext;

        public ApplicationController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment, PdfGenerator pdfGenerator, ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _pdfGenerator = pdfGenerator;
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _HostEnvironment = hostEnvironment;
        }
        [HttpGet]
        public IActionResult SubmitForm(int? Id)
        {
            string studentId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ApplicationForm applicationForm = _dbContext.ApplicationForms.FirstOrDefault(f => f.Student.UserId == studentId);

            if (applicationForm != null)
            {
               
                return RedirectToAction("AlreadySubmitted");
            }

            ApplicationVM ApplicationVM = new()
            {
                ApplicationForms = new(),

                StudentList = _unitOfWork.Students.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.StudentId.ToString()
                }),

                CoordinatorList = _unitOfWork.Coordinators.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })



            };

            if (Id == null || Id == 0)
            {
                return View(ApplicationVM);
            }
            else
            {
                ApplicationVM.ApplicationForms = _unitOfWork.Applications.GetFirstOrDefault(u => u.Id == Id);
                return View(ApplicationVM);
            }
        }


        public IActionResult Success()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult SubmitForm(ApplicationVM obj)
        {
            ApplicationUser user = _userManager.GetUserAsync(User).Result;

            Student_User studentUser = _dbContext.Students.FirstOrDefault(s => s.UserId == user.Id);
            obj.ApplicationForms.StudentId = studentUser.StudentId;
            obj.ApplicationForms.InternshipCoordinatorId = studentUser.CoordinatorId;

            string studentId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ApplicationForm applicationForm = _dbContext.ApplicationForms.FirstOrDefault(f => f.Student.UserId == studentId);

            if (ModelState.IsValid)
            {
             
                _dbContext.ApplicationForms.Add(obj.ApplicationForms);
                _dbContext.SaveChanges();
                _pdfGenerator.GeneratePdf(obj.ApplicationForms);

                return RedirectToAction("SubmitedForms");
            }

    
            return View(obj.ApplicationForms);
        }

        public IActionResult SubmitedForms()
        {
            // Retrieve the submitted application form data from the database
            var lastSubmittedForm = _dbContext.ApplicationForms.OrderByDescending(f => f.Id).FirstOrDefault();

            // Display the success page and pass the retrieved form data to the view
            return View(lastSubmittedForm);
        }

        public IActionResult DownloadApplicationForm(int id)
        {
            string studentId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ApplicationForm applicationForm = _dbContext.ApplicationForms.FirstOrDefault(f => f.Student.UserId == studentId);
           


            if (applicationForm == null)
            {
                // Return an error message or redirect to an error page if the application form is not found
                return RedirectToAction("SubmitForm");
            }
            string studentName = applicationForm.Name;
            string studentID = applicationForm.SID;
            // Generate the PDF for the application form
            PdfGenerator pdfGenerator = new PdfGenerator(_HostEnvironment);
            pdfGenerator.GeneratePdf(applicationForm);

            // Get the file path of the generated PDF
            string wwwrootPath = _HostEnvironment.WebRootPath;
            string filePath = Path.Combine(wwwrootPath, "PDF", "hello.pdf");

            // Read the file contents into a byte array
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            // Set the content type and file name for the response
            string contentType = "application/pdf";
            string fileName =  studentName+" "+ studentID + ".pdf";

            // Return the file as a downloadable response
            return File(fileBytes, contentType, fileName);
        }
        public IActionResult AlreadySubmitted()
        {
            return View();
        }






        public IActionResult ApplicationForm_Final_Upsert(int? Id)
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
                FinalApplicationVM.SubmittedApplicationForms = _unitOfWork.SubmittedApplicationForms.GetFirstOrDefault(u => u.Id == Id);
                return View(FinalApplicationVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ApplicationForm_Final_Upsert(FinalApplicationVM obj, IFormFile file)
        {
            ApplicationUser user2 = _userManager.GetUserAsync(User).Result;

            Student_User studentUser = _dbContext.Students.FirstOrDefault(s => s.UserId == user2.Id);
            obj.SubmittedApplicationForms.StudentId = studentUser.StudentId;
            obj.SubmittedApplicationForms.InternshipCoordinatorId = studentUser.CoordinatorId;

            if (ModelState.IsValid)
            {
                var user = _userManager.GetUserAsync(User).GetAwaiter().GetResult();

                string wwwRootPath = _HostEnvironment.WebRootPath;

                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"PDFCAN\Forms");
                    var extension = Path.GetExtension(file.FileName);

                    if (obj.SubmittedApplicationForms.PDFForm != null)
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
                        if (string.IsNullOrEmpty(student.Status))
                        {
                            student.Status = "Completed";
                        }
                        else if (student.Status == "N/A")
                        {
                            student.Status = "Pending";
                        }

                       



                        _dbContext.SaveChanges();
                    }
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
        public IActionResult Health_Upsert(HealthVM obj)
        {
            if (ModelState.IsValid)
            {
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




        public IActionResult Application_Form_View()
        {

            ApplicationUser user = _userManager.GetUserAsync(User).Result;

            // Get the associated Student_User record from the Student_User table
            Student_User studentUser = _dbContext.Students.FirstOrDefault(s => s.UserId == user.Id);

            // Access the name of the student user
            int studentName = int.Parse(studentUser?.submittedApplicationFormId);
            int applicationFormId = studentName;

            var submittedForm = _unitOfWork.SubmittedApplicationForms.GetAll().FirstOrDefault(form => form.Id == applicationFormId);

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



        public IActionResult Health_View()
        {

            ApplicationUser user = _userManager.GetUserAsync(User).Result;

            // Get the associated Student_User record from the Student_User table
            Student_User studentUser = _dbContext.Students.FirstOrDefault(s => s.UserId == user.Id);

            // Access the name of the student user
            int studentName = int.Parse(studentUser?.HealthInsuranceId);
            int applicationFormId = studentName;

            var submittedForm = _unitOfWork.HealthInsurances.GetAll().FirstOrDefault(form => form.Id == applicationFormId);

            if (submittedForm == null)
            {
                return NotFound();
            }

            submittedForm.Student = _unitOfWork.HealthInsurances.GetFirstOrDefault(u => u.Id == submittedForm.Id).Student;
            submittedForm.CareerCenter = _unitOfWork.HealthInsurances.GetFirstOrDefault(u => u.Id == submittedForm.Id).CareerCenter;



            return View(submittedForm);


        }

        public IActionResult Health_GetPdf(int HealthId)
        {
            var submittedForm = _unitOfWork.HealthInsurances.GetAll()
         .FirstOrDefault(form => form.Id == HealthId);

            if (submittedForm == null || string.IsNullOrEmpty(submittedForm.HealthInsurancePDF))
            {
                return NotFound();
            }

            string filePath = submittedForm.HealthInsurancePDF;
            byte[] fileBytes = System.IO.File.ReadAllBytes(Path.Combine(_HostEnvironment.WebRootPath, filePath));
            return File(fileBytes, "application/pdf");
        }


        public IActionResult Official_Letter_View()
        {

            ApplicationUser user = _userManager.GetUserAsync(User).Result;

            // Get the associated Student_User record from the Student_User table
            Student_User studentUser = _dbContext.Students.FirstOrDefault(s => s.UserId == user.Id);

            // Access the name of the student user
            int studentName = int.Parse(studentUser?.OfficialLetterId);
            int OfficialLetterId = studentName;

            var OfficialLetterForm = _unitOfWork.OfficialLetters.GetAll().FirstOrDefault(form => form.Id == OfficialLetterId);

            if (OfficialLetterForm == null)
            {
                return NotFound();
            }

            OfficialLetterForm.Student = _unitOfWork.OfficialLetters.GetFirstOrDefault(u => u.Id == OfficialLetterForm.Id).Student;
            OfficialLetterForm.InternshipCoordinator = _unitOfWork.OfficialLetters.GetFirstOrDefault(u => u.Id == OfficialLetterForm.Id).InternshipCoordinator;



            return View(OfficialLetterForm);


        }

        public IActionResult Official_Letter_GetPdf(int OfficialLetterId)
        {
            var OfficialLetterForm = _unitOfWork.OfficialLetters.GetAll()
         .FirstOrDefault(form => form.Id == OfficialLetterId);

            if (OfficialLetterForm == null || string.IsNullOrEmpty(OfficialLetterForm.OfficialLetterPDF))
            {
                return NotFound();
            }

            string filePath = OfficialLetterForm.OfficialLetterPDF;
            byte[] fileBytes = System.IO.File.ReadAllBytes(Path.Combine(_HostEnvironment.WebRootPath, filePath));
            return File(fileBytes, "application/pdf");
        }



        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            
            return View();
        }



        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var FilesList = _unitOfWork.SubmittedApplicationForms.GetAll();
            return Json(new { data = FilesList });
        }

        [HttpDelete]

        public IActionResult DeletePost(int? Id)
        {
            var obj = _unitOfWork.SubmittedApplicationForms.GetFirstOrDefault(u => u.Id == Id);

            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldFilePath = Path.Combine(_HostEnvironment.WebRootPath, obj.PDFForm.TrimStart('\\'));
            if (System.IO.File.Exists(oldFilePath))
            {
                System.IO.File.Delete(oldFilePath);
            }

            _unitOfWork.SubmittedApplicationForms.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Application Forms deleted successfuly" });
            return RedirectToAction("Index");

            return View(Id);

        }


        #endregion


    }
}

