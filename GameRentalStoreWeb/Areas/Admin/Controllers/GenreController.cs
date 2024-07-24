using System.Security.Claims;
using GameRentalStore.BLL;
using GameRentalStore.DataAccess.Repository.IRepository;
using GameRentalStore.Models;
using GameRentalStore.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameRentalStoreWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class GenreController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly GenreService _genreService;

        public GenreController(IUnitOfWork unitOfWork, GenreService genreService)
        {
            _unitOfWork = unitOfWork;
            _genreService = genreService;
        }

        public IActionResult Index()
        {
            List<Genre> objGenreList = _unitOfWork.Genre.GetAll().ToList();
            return View(objGenreList);
        }


        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                // Create
                return View(new Genre());
            }
            else
            {
                // Update
                Genre? genreFromDb = _unitOfWork.Genre.Get(u => u.Id == id);
                return View(genreFromDb);
            }
        }

        [HttpPost]
        public IActionResult Upsert(Genre genreObj)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            bool modelStateValidation = ModelState.IsValid;

            Result<object> result = _genreService.Upsert(genreObj, userId, modelStateValidation);

            if (result.Status)
            {
                TempData["success"] = result.Message;
                return RedirectToAction(result.Action, result.Controller);
            }
            else
            {
                if (result.Action == "name")
                {
                    ModelState.AddModelError("name", result.Message);
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                }
                return View(result.Data);
            }
        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Genre? gameFromDb = _unitOfWork.Genre.Get(u => u.Id == id);

            if (gameFromDb == null)
            {
                return NotFound();
            }
            return View(gameFromDb);
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Genre? obj = _unitOfWork.Genre.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Genre.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Genre Deleted Successfully";
            return RedirectToAction("Index", "Genre");
        }
    }
}
