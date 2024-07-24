using System.Diagnostics;
using System.Security.Claims;
using GameRentalStore.BLL;
using GameRentalStore.DataAccess.Repository.IRepository;
using GameRentalStore.Models;
using GameRentalStore.Models.ViewModels;
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
        private readonly UserPackageService _userPackageService;


        public UserPackageController(ILogger<UserPackageController> logger, IUnitOfWork unitOfWork, UserPackageService userPackageService)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _userPackageService = userPackageService;
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

            Task<Result<object>> resultTask = _userPackageService.SubscriptionPackage(packageId, totalSubscriptionMonths, userId);
            Result<object> result = await resultTask;

            if (result.Status)
            {
                TempData["success"] = result.Message;
            }
            else
            {
                TempData["error"] = result.Message;
            }
            return RedirectToAction(result.Action, result.Controller);
        }



        [HttpGet]
        [Authorize(Roles = SD.Role_User)]
        public IActionResult Subscribed()
        {
            return View();
        }


        [HttpGet]
        [Authorize(Roles = SD.Role_User)]
        public IActionResult RentedList()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var shoppingCart = _unitOfWork.ShoppingCart.GetAll(includeProperties: "UserPackage,SubscriptionPackage")
                .Where(u => u.ApplicationUserId == userId).ToList();
            var userPackage = _unitOfWork.UserPackage.Get(u => u.ApplicationUserId == userId, includeProperties: "SubscriptionPackage");

            var viewModel = new RentedListVM
            {
                UserPackage = userPackage,
                ShoppingCart = shoppingCart
            };

            return View(viewModel);
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


        [HttpGet]
        public IActionResult GetAllRentedGames()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            List<ShoppingCart> userCart = _unitOfWork.ShoppingCart.GetAll(includeProperties: "Game,UserPackage,SubscriptionPackage")
                .Where(u => u.ApplicationUserId == userId && u.IsReplaced == false).ToList();
            return Json(new { data = userCart });
        }


        public IActionResult Replace(int? id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            Result<object> result = _userPackageService.Replace(id, userId);

            if (result.Status)
            {
                return Json(new { success = result.Status, message = result.Message });
            }
            else
            {
                return Json(new { success = result.Status, message = result.Message });
            }
        }

        #endregion



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
