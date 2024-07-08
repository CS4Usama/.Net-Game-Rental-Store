using System.Diagnostics;
using System.Security.Claims;
using GameRentalStore.DataAccess.Repository.IRepository;
using GameRentalStore.Models;
using GameRentalStore.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameRentalStoreWeb.Areas.User.Controllers
{
    [Area("User")]
    public class UserPackageController : Controller
    {
        private readonly ILogger<UserPackageController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public UserPackageController(ILogger<UserPackageController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<SubscriptionPackage> packageList = _unitOfWork.SubscriptionPackage.GetAll().ToList();
            return View(packageList);
        }


        [HttpPost]
        [Authorize(Roles = SD.Role_User)]
        [ActionName("Index")]
        public async Task<IActionResult> IndexPOST(int packageId, int totalSubscriptionMonths)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            var subscribedDate = DateOnly.FromDateTime(DateTime.Now);
            var expiredDate = subscribedDate.AddMonths(totalSubscriptionMonths);

            // Check If the User Already has an Active Subscription:>
            var existingSubscription = await _unitOfWork.UserPackage.FirstOrDefaultAsync(rec =>
                rec.ApplicationUserId == userId && rec.ExpiredDate > subscribedDate
            );
            if (existingSubscription != null)
            {
                TempData["error"] = "You already have an active subscription";
                return RedirectToAction("Index", "Home");
            }

            var userPackage = new UserPackage
            {
                ApplicationUserId = userId,
                PackageId = packageId,
                TotalSubscriptionMonths = totalSubscriptionMonths,
                SubscribedDate = subscribedDate,
                ExpiredDate = expiredDate,
            };

            _unitOfWork.UserPackage.Add(userPackage);
            await _unitOfWork.SaveAsync();

            TempData["success"] = "Package Subscribed Successfully";

            return RedirectToAction("Index", "Home");
        }



        [HttpGet]
        [Authorize(Roles = SD.Role_User)]
        public IActionResult Subscribed()
        {
            return View();
        }


        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            List<UserPackage> userPkgObj = _unitOfWork.UserPackage.GetAll(includeProperties: "SubscriptionPackage")
                .Where(u => u.ApplicationUserId == userId).ToList();
            return Json(new { data = userPkgObj });
        }

        #endregion



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
