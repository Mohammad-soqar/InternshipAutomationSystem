using Internship.DataAccess.Repository.IRepository;
using Internship.Models;
using Internship.Models.ViewModels;
using Internship.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;

namespace InternshipAutomationSystem.Areas.CareerCenter.Controllers
{ 
    
    [Area("CareerCenter")]
    [Authorize(Roles = SD.Role_Career_Center)]

    public class AnnouncementsController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
            private readonly IWebHostEnvironment _HostEnvironment;
            public AnnouncementsController(UserManager<IdentityUser> userManager , IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
            {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
                _HostEnvironment = hostEnvironment;
            }
            public IActionResult Announcement()
            {
            IEnumerable<Announcement> AnnouncementList = _unitOfWork.Announcements.GetAll();
             return View(AnnouncementList);
             }

     





        public IActionResult Upsert_Announcement(int? Id)
        {
            AnnouncementVM AnnouncementVM = new()
            {
                Announcements = new(),


            };
            if (Id == null || Id == 0)
            {
                return View(AnnouncementVM);
            }
            else
            {
                AnnouncementVM.Announcements = _unitOfWork.Announcements.GetFirstOrDefault(u => u.Id == Id);
                return View(AnnouncementVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert_Announcement(AnnouncementVM obj)
        {
            if (ModelState.IsValid)
             {
               
               
                if (obj.Announcements.Id == 0)
                {
                  
                    _unitOfWork.Announcements.Add(obj.Announcements);
                }
                else
                {
                    _unitOfWork.Announcements.Update(obj.Announcements);
                }

                _unitOfWork.Save();
                return RedirectToAction("Index", "CareerCenter");
            }

            return View(obj);
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
     

        [HttpPost]
        public IActionResult DeletePost_Announcement(int? Id)
        {
            var obj = _unitOfWork.Announcements.GetFirstOrDefault(u => u.Id == Id);

            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

          
         

            _unitOfWork.Announcements.Remove(obj);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Internship deleted successfully" });

        }


        #endregion


    }


}
