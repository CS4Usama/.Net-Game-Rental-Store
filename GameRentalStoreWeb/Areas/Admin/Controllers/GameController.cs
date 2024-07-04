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
        public GameController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
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
            // ViewBag.genreList = genreList;
            // ViewData["genreList"] = genreList;

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
            if (ModelState.IsValid)
            {
                if (gameVM.Game.Id == 0)
                {
                    _unitOfWork.Game.Add(gameVM.Game);
                }
                else
                {
                    _unitOfWork.Game.Update(gameVM.Game);
                }
                _unitOfWork.Save();

                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (files != null)
                {
                    foreach (IFormFile file in files)
                    {
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        string gamePath = @"media\games\game-" + gameVM.Game.Id;
                        string finalPath = Path.Combine(wwwRootPath, gamePath);

                        string mediaExt = Path.GetExtension(file.FileName).ToLower();
                        string mediaType = "";
                        if (mediaExt == ".png" || mediaExt == ".jpg" || mediaExt == ".jpeg" || mediaExt == ".webp")
                        {
                            mediaType = "image";
                        }
                        else if (mediaExt == ".mp4")
                        {
                            mediaType = "video";
                        }

                        if (!Directory.Exists(finalPath))
                            Directory.CreateDirectory(finalPath);

                        using (var fileStream = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }

                        GameMedia gameMedia = new()
                        {
                            MediaUrl = @"\" + gamePath + @"\" + fileName,
                            GameId = gameVM.Game.Id,
                            MediaType = mediaType
                        };

                        if (gameVM.Game.GameMedias == null)
                            gameVM.Game.GameMedias = new List<GameMedia>();

                        gameVM.Game.GameMedias.Add(gameMedia);
                    }

                    _unitOfWork.Game.Update(gameVM.Game);
                    _unitOfWork.Save();
                }

                TempData["success"] = "Action Successful";
                return RedirectToAction("Index", "Game");
            }
            else
            {
                gameVM.GenreList = _unitOfWork.Genre.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(gameVM);
            }
        }


        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Game? genreFromDb = _unitOfWork.Game.Get(u => u.Id == id);

        //    if (genreFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(genreFromDb);
        //}

        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeletePOST(int? id)
        //{
        //    Game? obj = _unitOfWork.Game.Get(u => u.Id == id);
        //    if (obj == null)
        //    {
        //        return NotFound();
        //    }

        //    _unitOfWork.Game.Remove(obj);
        //    _unitOfWork.Save();
        //    TempData["success"] = "Game Deleted Successfully";
        //    return RedirectToAction("Index", "Game");
        //}


        public IActionResult DeleteImage(int imageId)
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

            //var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, gameToBeDeleted.MediaUrl.Trim('\\'));
            //if (System.IO.File.Exists(oldImagePath))
            //{
            //    System.IO.File.Delete(oldImagePath);
            //}

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
