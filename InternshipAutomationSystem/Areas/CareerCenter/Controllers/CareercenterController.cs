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

       
        public IActionResult Upsert_Internship(int? Id)
        {
            InternshipVM InternshipVM = new()
            {
                InternshipOpportunities = new(),


            };
            if (Id == null || Id == 0)
            {
                return View(InternshipVM);
            }
            else
            {
                InternshipVM.InternshipOpportunities = _unitOfWork.InternshipOpportunities.GetFirstOrDefault(u => u.Id == Id);
                return View(InternshipVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert_Internship(InternshipVM obj, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _HostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"Images\InternshipJobs");
                    var extension = Path.GetExtension(file.FileName);

                    if (obj.InternshipOpportunities.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.InternshipOpportunities.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.InternshipOpportunities.ImageUrl = @"\Images\InternshipJobs\" + fileName + extension;
                }
                if (obj.InternshipOpportunities.Id == 0)
                {
                    _unitOfWork.InternshipOpportunities.Add(obj.InternshipOpportunities);
                }
                else
                {
                    _unitOfWork.InternshipOpportunities.Update(obj.InternshipOpportunities);
                }


                _unitOfWork.Save();
                return RedirectToAction("Index");
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
        [HttpGet]
        public IActionResult GetAll_Internship()
        {
            var InternshipList = _unitOfWork.InternshipOpportunities.GetAll();
            return Json(new { data = InternshipList });
        }

        [HttpPost]
        public IActionResult DeletePost_Internship(int? Id)
        {
            var obj = _unitOfWork.InternshipOpportunities.GetFirstOrDefault(u => u.Id == Id);

            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldImagePath = Path.Combine(_HostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.InternshipOpportunities.Remove(obj);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Internship deleted successfully" });

        }


        #endregion

    }
}
