using Internship.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace InternshipAutomationSystem.Areas.Coordinator.Controllers
{
    [Area("Coordinator")]
    [Authorize(Roles = SD.Role_User_Coordinator)]
    public class CoordinatorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AFA()
        {
            return View();
        }
        public IActionResult Announcements()
        {
            return View();
        }
    }
}
