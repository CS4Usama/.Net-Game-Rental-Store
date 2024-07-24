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

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly HomeService _homeService;


        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, HomeService homeService)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _homeService = homeService;
        }

        public IActionResult Index()
        {
            IEnumerable<Game> gameList = _unitOfWork.Game.GetAll(includeProperties: "Genre,GameMedias");
            List<GameRating> gameRatingList = _unitOfWork.GameRating.GetAll(g => g.Status == "Approved").ToList();
            var gameRating = gameList.ToDictionary(
                game => game.Id,
                game => gameRatingList.Where(rating => rating.GameId == game.Id).ToList()
            );
            var viewModel = new GameVM
            {
                Games = gameList,
                GameRating = gameRating
            };
            return View(viewModel);
        }

        public IActionResult Details(int gameId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var game = _unitOfWork.Game.Get(u => u.Id == gameId, includeProperties: "Genre,GameMedias");
            var gameRating = _unitOfWork.GameRating.GetAll(includeProperties: "ApplicationUser").Where(r => r.GameId == gameId && r.Status == "Approved").ToList();
            var shoppingCart = _unitOfWork.ShoppingCart.Get(c => c.GameId == gameId && c.ApplicationUserId == userId && c.IsReplaced == false);

            var viewModel = new ShoppingCartVM
            {
                Game = game,
                GameRating = gameRating,
                ShoppingCart = shoppingCart
            };
            return View(viewModel);
        }


        [HttpPost]
        [Authorize(Roles = SD.Role_User)]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            Result<object> result = _homeService.AddtoCart(shoppingCart, userId);

            if (result.Status)
            {
                TempData["success"] = result.Message;
            }
            else
            {
                TempData["error"] = result.Message;
            }

            HttpContext.Session.SetInt32(SD.SessionCart, _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId).Count());
            return RedirectToAction(result.Action, result.Controller);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
