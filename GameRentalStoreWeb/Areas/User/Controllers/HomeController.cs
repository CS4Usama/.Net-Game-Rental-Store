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

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Game> gameList = _unitOfWork.Game.GetAll(includeProperties: "Genre,GameMedias");
            return View(gameList);
        }

        public IActionResult Details(int gameId)
        {
            ShoppingCart cart = new()
            {
                Game = _unitOfWork.Game.Get(u => u.Id == gameId, includeProperties: "Genre,GameMedias"),
                //Count = 1,
                GameId = gameId
            };
            return View(cart);
        }


        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            shoppingCart.ApplicationUserId = userId;

            // User Package Confirmation before Renting A Game:>
            var isUserHasPackage = _unitOfWork.UserPackage.Get(u => u.ApplicationUserId == userId);
            if (isUserHasPackage == null)
            {
                TempData["error"] = "You didn't have any subscription package. Please subscribe a package before renting a game.";
                return RedirectToAction("Index", "UserPackage");
            }

            shoppingCart.PackageId = _unitOfWork.UserPackage.Get(u => u.ApplicationUserId == userId).PackageId;
            shoppingCart.RentedDate = DateOnly.FromDateTime(DateTime.Now);
            shoppingCart.Count = 1;
            shoppingCart.UserPackageId = _unitOfWork.UserPackage.Get(u => u.ApplicationUserId == userId).Id;


            // Total Rented Games of Current Loggedin User:>
            List<ShoppingCart> userCart = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId).ToList();
            List<SubscriptionPackage> packages = _unitOfWork.SubscriptionPackage.GetAll().ToList();
            Game rentedGame = _unitOfWork.Game.Get(g => g.Id == shoppingCart.GameId);
            var newReleasedGameDate = DateOnly.FromDateTime(DateTime.Now).AddMonths(-3);
            List<ShoppingCart> newReleasedGameCount = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId && u.Game.ReleaseDate >= newReleasedGameDate).ToList();


            ShoppingCart cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.ApplicationUserId == userId && u.GameId == shoppingCart.GameId);
            if (cartFromDb != null)
            {
                TempData["error"] = "You have already added this game in your rented list.";
                return RedirectToAction(nameof(Index));
            }

            foreach (var package in packages)
            {
                if (package.Id == shoppingCart.PackageId)
                {
                    if (userCart.Count() >= package.GamesPerMonth)
                    {
                        TempData["error"] = $"You can't rent more games because you have already added {package.GamesPerMonth} games according to your subscrption package.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    { 
                        if (newReleasedGameCount.Count() >= package.RentNewReleasedGame && rentedGame.ReleaseDate >= newReleasedGameDate)
                        {
                            if (package.PackageName == "Basic")
                            {
                                TempData["error"] = $"You can't rent new games released within the last 3 months.";
                            }
                            else
                            {
                                TempData["error"] = $"You can only rent {package.RentNewReleasedGame} new game released within the last 3 months.";
                            }
                            return RedirectToAction(nameof(Index));
                        }
                    }
                }
            }


            _unitOfWork.ShoppingCart.Add(shoppingCart);
            _unitOfWork.Save();
            TempData["success"] = "Game Rented Successfully";
            HttpContext.Session.SetInt32(SD.SessionCart, _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId).Count());

            return RedirectToAction(nameof(Index));
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
