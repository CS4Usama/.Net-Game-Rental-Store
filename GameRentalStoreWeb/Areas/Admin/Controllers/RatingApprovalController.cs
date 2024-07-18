using System.Diagnostics;
using System.Security.Claims;
using GameRentalStore.DataAccess.Repository.IRepository;
using GameRentalStore.Models;
using GameRentalStore.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameRentalStoreWeb.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class RatingApprovalController : Controller
    {
        private readonly ILogger<RatingApprovalController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public RatingApprovalController(ILogger<RatingApprovalController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<GameRating> gameRatingObj = _unitOfWork.GameRating.GetAll(includeProperties: "ApplicationUser,Game").ToList();
            return View();
        }



        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var gameRatingObj = _unitOfWork.GameRating.GetAll(includeProperties: "ApplicationUser,Game")
                .Select(gr => new
                {
                    UserName = gr.ApplicationUser.Name,
                    UserEmail = gr.ApplicationUser.Email,
                    GameName = gr.Game.Title,
                    Rating = gr.Rating,
                    Review = gr.Review,
                    Status = gr.Status,
                    Id = gr.Id
                }).ToList();
            return Json(new { data = gameRatingObj });
        }


        public IActionResult Approve(int id)
        {
            var objFromDb = _unitOfWork.GameRating.Get(u => u.Id == id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while Review Approval" });
            }
            objFromDb.Status = "Approved";
            _unitOfWork.GameRating.Update(objFromDb);
            _unitOfWork.Save();

            return Json(new { success = true, message = "User Review Approved Successfully" });
        }

        public IActionResult Reject(int id)
        {
            var objFromDb = _unitOfWork.GameRating.Get(u => u.Id == id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while Review Rejection" });
            }
            objFromDb.Status = "Rejected";
            _unitOfWork.GameRating.Update(objFromDb);
            _unitOfWork.Save();

            return Json(new { success = true, message = "User Review Rejected Successfully" });
        }

        #endregion



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
