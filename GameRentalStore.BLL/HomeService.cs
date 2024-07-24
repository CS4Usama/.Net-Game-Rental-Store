using GameRentalStore.DataAccess.Repository.IRepository;
using GameRentalStore.Models;
using Microsoft.Extensions.Logging;


namespace GameRentalStore.BLL
{
    public class HomeService
    {
        private readonly ILogger<HomeService> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeService(ILogger<HomeService> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }


        public Result<object> AddtoCart(ShoppingCart shoppingCart, string userId)
        {
            shoppingCart.ApplicationUserId = userId;

            // User Package Confirmation before Renting A Game:>
            var isUserHasPackage = _unitOfWork.UserPackage.Get(u => u.ApplicationUserId == userId);
            if (isUserHasPackage == null)
            {
                return new Result<object>
                {
                    Status = false,
                    Message = "You didn't have any subscription package. Please subscribe a package before renting a game.",
                    Action = "Index",
                    Controller = "UserPackage"
                };
            }

            shoppingCart.PackageId = _unitOfWork.UserPackage.Get(u => u.ApplicationUserId == userId).PackageId;
            shoppingCart.RentedDate = DateOnly.FromDateTime(DateTime.Now);
            shoppingCart.Count = 1;
            shoppingCart.IsReplaced = false;
            shoppingCart.Game = null;
            shoppingCart.UserPackageId = _unitOfWork.UserPackage.Get(u => u.ApplicationUserId == userId).Id;


            // Total Rented Games of Current Loggedin User:>
            List<ShoppingCart> userCart = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId && u.IsReplaced == false).ToList();
            List<SubscriptionPackage> packages = _unitOfWork.SubscriptionPackage.GetAll().ToList();
            Game rentedGame = _unitOfWork.Game.Get(g => g.Id == shoppingCart.GameId);
            var newReleasedGameDate = DateOnly.FromDateTime(DateTime.Now).AddMonths(-3);
            List<ShoppingCart> newReleasedGameCount = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId && u.Game.ReleaseDate >= newReleasedGameDate && u.IsReplaced == false).ToList();


            ShoppingCart cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.ApplicationUserId == userId && u.GameId == shoppingCart.GameId);
            if (cartFromDb != null)
            {
                return new Result<object>
                {
                    Status = false,
                    Message = "You have already replaced this game from your rented list.",
                    Action = "Index",
                    Controller = "Home"
                };
            }

            foreach (var package in packages)
            {
                if (package.Id == shoppingCart.PackageId)
                {
                    if (userCart.Count() >= package.GamesPerMonth)
                    {
                        return new Result<object>
                        {
                            Status = false,
                            Message = $"You can't rent more games because you have already added {package.GamesPerMonth} games according to your subscrption package.",
                            Action = "Index",
                            Controller = "Home"
                        };
                    }
                    else
                    {
                        string message = "";
                        bool status = false;
                        if (newReleasedGameCount.Count() >= package.RentNewReleasedGame && rentedGame.ReleaseDate >= newReleasedGameDate)
                        {
                            if (package.PackageName == "Basic")
                            {
                                status = false;
                                message = "You can't rent new games released within the last 3 months.";
                            }
                            else
                            {
                                status = false;
                                message = $"You can only rent {package.RentNewReleasedGame} new game released within the last 3 months.";
                            }
                            return new Result<object>
                            {
                                Status = status,
                                Message = message,
                                Action = "Index",
                                Controller = "Home"
                            };
                        }
                    }
                }
            }

            if (rentedGame.Quantity <= 0)
            {
                return new Result<object>
                {
                    Status = false,
                    Message = "Sorry! This Game is Out of Stock.",
                    Action = "Details",
                    Controller = "Home"
                };
            }

            rentedGame.Quantity -= 1;
            _unitOfWork.Game.Update(rentedGame);

            _unitOfWork.ShoppingCart.Add(shoppingCart);
            _unitOfWork.Save();
            return new Result<object>
            {
                Status = true,
                Message = "Game Rented Successfully",
                Action = "Index",
                Controller = "Home"
            };
        }
    }
}
