using Internship.DataAccess.Data;
using Internship.DataAccess.Repository.IRepository;
using Internship.Models;
using Internship.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InternshipAutomationSystem.Areas.User.Controllers
{
    [Area("User")]

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {

            _logger = logger;
            _unitOfWork = unitOfWork;
            _env = env;
        }
        public IActionResult Index()
        {
            return Redirect("/Identity/Account/Login");
        }

        public IActionResult Support()
        {
            return View();
        }

        public IActionResult Guide()
        {
            return View();
        }

        public IActionResult InternshipOP()
        {
            return RedirectToAction("Index", "Internshipjob");

        }
        public IActionResult Internship()
        {

            SaveandinternshipVM mymodel = new SaveandinternshipVM();
            mymodel.InternshipOpportunity = _unitOfWork.InternshipOpportunities.GetAll();
            
            return View(mymodel);

        
        }
        public IActionResult Details(int id)
        {
           
            var internship = GetInternshipById(id);

            if (internship == null)
            {
                return NotFound();
            }

            return View(internship);
        }

        public IActionResult GetInternshipDetails(int id)
        {

            var internship = GetInternshipById(id);


            return Json(internship);
        }

        private InternshipOpportunity GetInternshipById(int id)
        {
            var internship = _unitOfWork.InternshipOpportunities.GetFirstOrDefault(i => i.Id == id);

            return internship;
        }
    }
}
