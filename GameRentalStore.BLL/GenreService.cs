using GameRentalStore.DataAccess.Repository.IRepository;
using GameRentalStore.Models;
using Microsoft.Extensions.Logging;


namespace GameRentalStore.BLL
{
    public class GenreService
    {
        private readonly ILogger<GenreService> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public GenreService(ILogger<GenreService> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }


        public Result<object> Upsert(Genre genreObj, string userId, bool modelStateValidation)
        {
            if (genreObj.Name == genreObj.DisplayOrder.ToString())
            {
                return new Result<object>
                {
                    Status = false,
                    Message = "DisplayOrder cannot be exactly same like Name.",
                    Data = genreObj,
                    Action = "name"
                };
            }
            else if (genreObj.Name != null && genreObj.Name.ToLower() == "test")
            {
                return new Result<object>
                {
                    Status = false,
                    Message = "Test is an Invalid Value.",
                    Data = genreObj,
                };
            }
            else
            {
                var existingDisplayOrder = _unitOfWork.Genre.Get(g => g.DisplayOrder == genreObj.DisplayOrder || g.Name.ToLower() == genreObj.Name.ToLower());
                if (existingDisplayOrder != null)
                {
                    if (existingDisplayOrder.Id != genreObj.Id)
                    {
                        return new Result<object>
                        {
                            Status = false,
                            Message = "This Genre already exists. Please choose a different one.",
                            Data = genreObj,
                        };
                    }
                }
            }

            if (modelStateValidation)
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
                return new Result<object>
                {
                    Status = true,
                    Message = "Operation Successful",
                    Action = "Index",
                    Controller = "Genre"
                };
            }
            else
            {
                return new Result<object>
                {
                    Status = false,
                    Data = genreObj,
                };
            }
        }
    }
}
