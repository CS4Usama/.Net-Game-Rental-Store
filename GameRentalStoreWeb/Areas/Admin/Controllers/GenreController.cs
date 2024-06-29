using GameRentalStore.DataAccess.Repository.IRepository;
using GameRentalStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameRentalStoreWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = SD.Role_Admin)]
    public class GenreController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;
        public GenreController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
            if (genreObj.Name == genreObj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "DisplayOrder cannot be exactly same like Name.");
            }
            else if (genreObj.Name != null && genreObj.Name.ToLower() == "test")
            {
                ModelState.AddModelError("", "Test is an Invalid Value.");
            }
            else
            {
                var existingDisplayOrder = _unitOfWork.Genre.Get(g => g.DisplayOrder == genreObj.DisplayOrder || g.Name.ToLower() == genreObj.Name.ToLower());
                if (existingDisplayOrder != null)
                {
                    if (existingDisplayOrder.Id != genreObj.Id)
                    {
                        ModelState.AddModelError("", "This Genre already exists. Please choose a different one.");
                    }
                }
            }

            if (ModelState.IsValid)
            {
                if (genreObj.Id == 0)
                {
                    _unitOfWork.Genre.Add(genreObj);
                }
                else
                {
                    _unitOfWork.Genre.Update(genreObj);
                }
                _unitOfWork.Save();
                TempData["success"] = "Operation Successfull";
                return RedirectToAction("Index", "Genre");
            }
            else
            {
                return View(genreObj);
            }
        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Genre? productFromDb = _unitOfWork.Genre.Get(u => u.Id == id);

            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
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
