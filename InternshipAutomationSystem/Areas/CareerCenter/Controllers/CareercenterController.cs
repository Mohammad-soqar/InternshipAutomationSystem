using Internship.DataAccess.Repository.IRepository;
using Internship.Models;
using Internship.Models.ViewModels;
using Internship.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Policy;

namespace InternshipAutomationSystem.Areas.CareerCenter.Controllers
{
    [Area("CareerCenter")]
    [Authorize(Roles = SD.Role_Career_Center)]
    public class CareercenterController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _HostEnvironment;
        public CareercenterController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _HostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

       
       

    }
}
