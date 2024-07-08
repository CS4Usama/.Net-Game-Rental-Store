﻿using GameRentalStore.DataAccess.Repository.IRepository;
using GameRentalStore.Models;
using GameRentalStore.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace GameRentalStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;

        public UserController(UserManager<IdentityUser> userManager, IUnitOfWork unitOfWork, RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult UserSubscription()
        {
            return View();
        }



        #region API CALLS

        [HttpGet]
        public IActionResult GetAllUserPackages()
        {
            List<UserPackage> userPkgObj = _unitOfWork.UserPackage.GetAll(includeProperties: "ApplicationUser,SubscriptionPackage").ToList();
            return Json(new { data = userPkgObj });
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            List<ApplicationUser> objUserList = _unitOfWork.ApplicationUser.GetAll().Where(u => u.Role == "user").ToList();
            return Json(new { data = objUserList });
        }


        [HttpPost]
        public IActionResult LockUnlock([FromBody] string id)
        {
            var objFromDb = _unitOfWork.ApplicationUser.Get(u => u.Id == id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while Locking/Unlocking" });
            }

            if (objFromDb.LockoutEnd != null && objFromDb.LockoutEnd > DateTime.Now)
            {
                // User is Currently Locked and We need to Unlock them
                objFromDb.LockoutEnd = DateTime.Now;
            }
            else
            {
                objFromDb.LockoutEnd = DateTime.Now.AddYears(1000);
            }
            _unitOfWork.ApplicationUser.Update(objFromDb);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Operation Successful" });
        }

        #endregion
    }
}
