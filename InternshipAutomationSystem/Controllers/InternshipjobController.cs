
using Internship.DataAccess.Data;
using Internship.DataAccess.Repository;
using Internship.DataAccess.Repository.IRepository;
using Internship.Models;
using Internship.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace InternshipAutomationSystem.Controllers
{
    public class InternshipjobController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _HostEnvironment;

        public InternshipjobController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _HostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Internship()
        {
            IEnumerable<InternshipJob> InternshipJobList = _unitOfWork.InternshipJobs.GetAll();

            return View(InternshipJobList);
        }

        public IActionResult Upsert(int? Id)
        {
            InternshipVM InternshipVM = new()
            {
                InternshipJob = new(),

                
            };
            if (Id == null || Id == 0)
            {
                return View(InternshipVM);
            }
            else
            {
                InternshipVM.InternshipJob = _unitOfWork.InternshipJobs.GetFirstOrDefault(u => u.Id == Id);
                return View(InternshipVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(InternshipVM obj, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _HostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"Images\InternshipJobs");
                    var extension = Path.GetExtension(file.FileName);

                    if (obj.InternshipJob.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.InternshipJob.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.InternshipJob.ImageUrl = @"\Images\InternshipJobs\" + fileName + extension;
                }
                if (obj.InternshipJob.Id == 0)
                {
                    _unitOfWork.InternshipJobs.Add(obj.InternshipJob);
                }
                else
                {
                    _unitOfWork.InternshipJobs.Update(obj.InternshipJob);
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
        public IActionResult GetAll()
        {
            var InternshipList = _unitOfWork.InternshipJobs.GetAll();
            return Json(new { data = InternshipList });
        }

        [HttpDelete]
        public IActionResult DeletePost(int? Id)
        {
            var obj = _unitOfWork.InternshipJobs.GetFirstOrDefault(u => u.Id == Id);

            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldImagePath = Path.Combine(_HostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.InternshipJobs.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Event deleted successfuly" });
            return RedirectToAction("Index");

            return View(Id);

        }


        #endregion


    }
}
