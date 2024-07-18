using System.Diagnostics;
using System.Security.Claims;
using GameRentalStore.DataAccess.Repository.IRepository;
using GameRentalStore.Models;
using GameRentalStore.Models.ViewModels;
using GameRentalStore.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameRentalStoreWeb.Areas.User.Controllers
{
    [Area("User")]

    public class GameRatingController : Controller
    {
        private readonly ILogger<GameRatingController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public GameRatingController(ILogger<GameRatingController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(int rentedGameId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var shoppingCart = _unitOfWork.ShoppingCart.Get(c => c.ApplicationUserId == userId && c.Id == rentedGameId, includeProperties: "Game");
            var game = _unitOfWork.Game.Get(g => g.Id == shoppingCart.GameId);
            var viewModel = new GameRatingVM
            {
                ShoppingCart = shoppingCart,
                Game = game,
                GameRating = new GameRating()
            };

            var gameRatingId = _unitOfWork?.GameRating?.Get(r => r.ApplicationUserId == userId && r.CartGameId == rentedGameId)?.Id;

            if (gameRatingId == null || gameRatingId == 0)
            {
                // Create
                return View(viewModel);
            }
            else
            {
                // Update
                viewModel.GameRating = _unitOfWork.GameRating.Get(u => u.ApplicationUserId == userId && u.Id == gameRatingId, includeProperties: "Game");
                return View(viewModel);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(GameRatingVM model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var existingRating = _unitOfWork.GameRating.Get(gr => gr.ApplicationUserId == userId && gr.CartGameId == model.ShoppingCart.Id, includeProperties: "ShoppingCart");

                if (existingRating != null)
                {
                    existingRating.Rating = model.GameRating.Rating;
                    existingRating.Review = model.GameRating.Review;
                    existingRating.Status = "Pending";
                    _unitOfWork.GameRating.Update(existingRating);
                }
                else
                {
                    var newRating = new GameRating
                    {
                        Rating = model.GameRating.Rating,
                        Review = model.GameRating.Review,
                        ApplicationUserId = userId,
                        CartGameId = model.ShoppingCart.Id,
                        GameId = model.Game.Id,
                        Status = "Pending"
                    };
                    _unitOfWork.GameRating.Add(newRating);
                }
                _unitOfWork.Save();
                TempData["success"] = "Game Rated & Reviewed Successfully!";

                return RedirectToAction("RentedList", "UserPackage");
            }

            return View(model);
        }


        [HttpGet]
        [Authorize(Roles = SD.Role_User)]
        public IActionResult GameRatings()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<GameRating> gameRatingObj = _unitOfWork.GameRating.GetAll(u => u.ApplicationUserId == userId, includeProperties: "Game").ToList();
            return View(gameRatingObj);
        }



        #region API CALLS

        [HttpGet]
        public IActionResult GetAllGameRatings()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var gameRatingObj = _unitOfWork.GameRating.GetAll(u => u.ApplicationUserId == userId, includeProperties: "Game")
                .Select(gr => new
                {
                    GameName = gr.Game.Title,
                    Rating = gr.Rating,
                    Review = gr.Review,
                    Status = gr.Status
                }).ToList();
            return Json(new { data = gameRatingObj });
        }

        #endregion



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
