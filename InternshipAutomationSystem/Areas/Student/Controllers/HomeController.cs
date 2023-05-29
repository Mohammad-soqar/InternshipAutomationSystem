using Internship.DataAccess.Data;
using Internship.DataAccess.Repository.IRepository;
using Internship.Models;
using Internship.Models.ViewModels;
using Internship.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IWebHostEnvironment _env;
        private readonly ApplicationDbContext _dbContext;


        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, IWebHostEnvironment env,  ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        
            _logger = logger;
            _unitOfWork = unitOfWork;
            _env = env;
        }

        public IActionResult Index()
        {
            IEnumerable<InternshipOpportunity> InternshipList = _unitOfWork.InternshipOpportunities.GetAll();
            return View(InternshipList);

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
      

    }
}