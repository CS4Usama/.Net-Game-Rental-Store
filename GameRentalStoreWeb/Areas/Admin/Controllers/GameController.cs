using System.Security.Claims;
using GameRentalStore.BLL;
using GameRentalStore.DataAccess.Repository.IRepository;
using GameRentalStore.Models;
using GameRentalStore.Models.ViewModels;
using GameRentalStore.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameRentalStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class GameController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly GameService _gameService;
        public GameController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, GameService gameService)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _gameService = gameService;
        }

        public IActionResult Index()
        {
            List<Game> objGameList = _unitOfWork.Game.GetAll(includeProperties: "Genre").ToList();
            return View(objGameList);
        }


        public IActionResult Upsert(int? id)
        {
            IEnumerable<SelectListItem> GenreList = _unitOfWork.Genre.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

            GameVM gameVM = new()
            {
                GenreList = GenreList,
                Game = new Game()
            };

            if (id == null || id == 0)
            {
                // Create
                return View(gameVM);
            }
            else
            {
                // Update
                gameVM.Game = _unitOfWork.Game.Get(u => u.Id == id, includeProperties: "GameMedias");
                return View(gameVM);
            }
        }


        [HttpPost]
        public IActionResult Upsert(GameVM gameVM, List<IFormFile> files)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            bool modelStateValidation = ModelState.IsValid;
            string wwwRootPath = _webHostEnvironment.WebRootPath;

            Result<object> result = _gameService.Upsert(gameVM, files, userId, modelStateValidation, wwwRootPath);

            if (result.Status)
            {
                TempData["success"] = result.Message;
                return RedirectToAction(result.Action, result.Controller);
            }
            else
            {
                TempData["error"] = result.Message;
                return View(result.Data);
            }
        }


        public IActionResult DeleteMedia(int imageId)
        {
            var imageToBeDeleted = _unitOfWork.GameMedia.Get(u => u.Id == imageId);
            int gameId = imageToBeDeleted.GameId;
            if (imageToBeDeleted != null)
            {
                if (!string.IsNullOrEmpty(imageToBeDeleted.MediaUrl))
                {
                    var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, imageToBeDeleted.MediaUrl.TrimStart('\\'));

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                _unitOfWork.GameMedia.Remove(imageToBeDeleted);
                _unitOfWork.Save();

                TempData["success"] = "Deleted Successfully";
            }
            return RedirectToAction(nameof(Upsert), new { id = gameId });
        }





        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Game> objGameList = _unitOfWork.Game.GetAll(includeProperties: "Genre").ToList();
            return Json(new { data = objGameList });
        }


        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var gameToBeDeleted = _unitOfWork.Game.Get(u => u.Id == id);
            if (gameToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }

            string gamePath = @"media\games\game-" + id;
            string finalPath = Path.Combine(_webHostEnvironment.WebRootPath, gamePath);

            if (Directory.Exists(finalPath))
            {
                string[] filePaths = Directory.GetFiles(finalPath);
                foreach (string filePath in filePaths)
                {
                    System.IO.File.Delete(filePath);
                }
                Directory.Delete(finalPath);
            }

            _unitOfWork.Game.Remove(gameToBeDeleted);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Deleted Successfully" });
        }

        #endregion
    }
}
