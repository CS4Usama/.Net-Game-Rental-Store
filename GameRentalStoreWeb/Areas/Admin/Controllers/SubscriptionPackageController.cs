using System.Security.Claims;
using GameRentalStore.DataAccess.Repository.IRepository;
using GameRentalStore.Models;
using GameRentalStore.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameRentalStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class SubscriptionPackageController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public SubscriptionPackageController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<SubscriptionPackage> packageList = _unitOfWork.SubscriptionPackage.GetAll().ToList();
            return View(packageList);
        }


        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                // Create
                return View(new SubscriptionPackage());
            }
            else
            {
                // Update
                SubscriptionPackage packageObj = _unitOfWork.SubscriptionPackage.Get(u => u.Id == id);
                return View(packageObj);
            }
        }

        [HttpPost]
        public IActionResult Upsert(SubscriptionPackage packageObj)
        {
            if (ModelState.IsValid)
            {
                if (packageObj.Id == 0)
                {
                    _unitOfWork.SubscriptionPackage.Add(packageObj);
                }
                else
                {
                    _unitOfWork.SubscriptionPackage.Update(packageObj);
                }
                _unitOfWork.Save();
                TempData["success"] = "Action Successful";
                return RedirectToAction("Index");
            }
            else
            {
                return View(packageObj);
            }
        }



        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<SubscriptionPackage> objPackageList = _unitOfWork.SubscriptionPackage.GetAll().ToList();
            return Json(new { data = objPackageList });
        }


        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var packageToBeDeleted = _unitOfWork.SubscriptionPackage.Get(u => u.Id == id);
            if (packageToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }

            _unitOfWork.SubscriptionPackage.Remove(packageToBeDeleted);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Deleted Successfully" });
        }

        #endregion
    }
}
