﻿using Internship.DataAccess.Repository.IRepository;
using Internship.Models;
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
        private readonly ILogger<CoordinatorController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;

        public CoordinatorController(ILogger<CoordinatorController> logger, IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {

            _logger = logger;
            _unitOfWork = unitOfWork;
            _env = env;
        }


        public IActionResult Index()
        {
            IEnumerable<Announcement> AnnouncementList = _unitOfWork.Announcements.GetAll();
            return View(AnnouncementList);
        }
        public IActionResult AFA()
        {
            return View();
        }
       
    }
}
