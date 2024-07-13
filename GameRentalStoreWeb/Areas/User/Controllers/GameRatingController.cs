using System.Diagnostics;
using GameRentalStore.DataAccess.Repository.IRepository;
using GameRentalStore.Models;
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

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult RateIt(GameRating obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.GameRating.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Game Rated & Reviewed Successfully";
                return RedirectToAction("RentedList");
            }
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
