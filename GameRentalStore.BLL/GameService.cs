using GameRentalStore.DataAccess.Repository.IRepository;
using GameRentalStore.Models;
using GameRentalStore.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;


namespace GameRentalStore.BLL
{
    public class GameService
    {
        private readonly ILogger<GameService> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public GameService(ILogger<GameService> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }


        public Result<object> Upsert(GameVM gameVM, List<IFormFile> files, string userId, bool modelStateValidation, string wwwRootPath)
        {
            if (modelStateValidation)
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

                return new Result<object>
                {
                    Status = true,
                    Message = "Action Successful",
                    Action = "Index",
                    Controller = "Game"
                };
            }
            else
            {
                gameVM.GenreList = _unitOfWork.Genre.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return new Result<object>
                {
                    Data = gameVM,
                    Status = false,
                    Action = "Index",
                    Controller = "Game"
                };
            }
        }
    }
}
