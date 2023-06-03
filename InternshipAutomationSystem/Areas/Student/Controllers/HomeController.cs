using Internship.DataAccess.Data;
using Internship.DataAccess.Repository;
using Internship.DataAccess.Repository.IRepository;
using Internship.Models;
using Internship.Models.ViewModels;
using Internship.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using System.Data;
using System.Diagnostics;

namespace InternshipAutomationSystem.Areas.Student.Controllers
{

    [Area("Student")]
    [Authorize(Roles = SD.Role_User_Student)]

    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IWebHostEnvironment _env;
        private readonly ApplicationDbContext _dbContext;


        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, IWebHostEnvironment env,  ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _env = env;
        }

        public IActionResult Index()
        {
            SaveandinternshipVM mymodel = new SaveandinternshipVM();
            mymodel.InternshipOpportunity = _unitOfWork.InternshipOpportunities.GetAll();
        
            return View(mymodel);

          

        }

        public IActionResult Forms()
        {
            

            return View();

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveInternship(int id)
        {

            ApplicationUser user = _userManager.GetUserAsync(User).Result;

            // Get the associated Student_User record from the Student_User table
            Student_User studentUser = _dbContext.Students.FirstOrDefault(s => s.UserId == user.Id);

            if (studentUser == null)
            {
                
                return NotFound();
            }

            // Retrieve the internship from the database
            var internship = _unitOfWork.InternshipOpportunities.GetFirstOrDefault(s => s.Id == id);
            if (internship == null)
            {
                // Handle error: internship not found
                return NotFound();
            }

            var savedInternship = new SavedInternship
            {
                Student = studentUser,
                InternshipOpportunity = internship
            };

            // Add the internship to the student's saved internships collection
            _unitOfWork.SavedInternships.Add(savedInternship);

            studentUser.SavedInternships.Add(savedInternship);
            internship.SavedByStudents.Add(studentUser);
            // Save the changes to the database
            _dbContext.SaveChanges();

            // Redirect to a relevant view or action
            return RedirectToAction("Index", "Home");
        }
        public IActionResult View_Saved_Internship()
        {

            IEnumerable<SavedInternship> SavedInternships = _unitOfWork.SavedInternships.GetAll();
            IEnumerable<InternshipOpportunity> InternshipOpportunities = _unitOfWork.InternshipOpportunities.GetAll();
            IEnumerable<Student_User> students = _unitOfWork.Students.GetAll();


           

            var SavedInternshipsList = SavedInternships.Select(SavedInternship => new SavedInternship
            {
                Id = SavedInternship.Id,
                StudentId = SavedInternship.StudentId,
                InternshipOpportunityId = SavedInternship.InternshipOpportunityId,
                InternshipOpportunity = InternshipOpportunities.FirstOrDefault(s => s.Id == SavedInternship.InternshipOpportunityId),
                Student = students.FirstOrDefault(s => s.StudentId == SavedInternship.StudentId)


            }); ;

            

            return View(SavedInternshipsList);

        }
    }


   

}
