using Internship.DataAccess.Data;
using Internship.DataAccess.Repository.IRepository;
using Internship.Models;
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
        public IActionResult InternshipOP()
        {
            return RedirectToAction("Index", "Internshipjob");

        }
        public IActionResult Internship()
        {
            IEnumerable<InternshipOpportunity> InternshipList = _unitOfWork.InternshipOpportunities.GetAll();
            return View(InternshipList);
        }
        public IActionResult Details(int id)
        {
            // Retrieve the internship details based on the provided id
            var internship = GetInternshipById(id);

            if (internship == null)
            {
                // Handle the case where the internship with the given id is not found
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

        public IActionResult Messages()
        {
            return View();
        }

    }
}
