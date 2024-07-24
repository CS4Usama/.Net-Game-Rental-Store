using GameRentalStore.DataAccess.Repository.IRepository;
using GameRentalStore.Models;
using Microsoft.Extensions.Logging;


namespace GameRentalStore.BLL
{
    public class UserPackageService
    {
        private readonly ILogger<UserPackageService> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public UserPackageService(ILogger<UserPackageService> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }


        public async Task<Result<object>> SubscriptionPackage(int packageId, int totalSubscriptionMonths, string userId)
        {
            var subscribedDate = DateOnly.FromDateTime(DateTime.Now);
            var expiredDate = subscribedDate.AddMonths(totalSubscriptionMonths);

            var userPackage = new UserPackage
            {
                ApplicationUserId = userId,
                PackageId = packageId,
                TotalSubscriptionMonths = totalSubscriptionMonths,
                SubscribedDate = subscribedDate,
                ExpiredDate = expiredDate,
            };
            var subPkg = _unitOfWork.SubscriptionPackage.Get(p => p.Id == packageId);

            // Check If the User Already has an Active Subscription:>
            var existingSubscription = await _unitOfWork.UserPackage.FirstOrDefaultAsync(rec =>
                rec.ApplicationUserId == userId && rec.ExpiredDate > subscribedDate
            );

            if (existingSubscription != null)
            {
                var existingSubPkg = _unitOfWork.SubscriptionPackage.Get(p => p.Id == existingSubscription.PackageId);
                if (subPkg?.RentNewReleasedGame > existingSubPkg?.RentNewReleasedGame)
                {
                    existingSubscription.PackageId = userPackage.PackageId;
                    existingSubscription.TotalSubscriptionMonths = userPackage.TotalSubscriptionMonths;
                    existingSubscription.SubscribedDate = userPackage.SubscribedDate;
                    existingSubscription.ExpiredDate = userPackage.ExpiredDate;
                    _unitOfWork.UserPackage.Update(existingSubscription);
                    await _unitOfWork.SaveAsync();
                    return new Result<object>
                    {
                        Status = true,
                        Message = "Package Updated Successfully",
                        Action = "Index",
                        Controller = "Home"
                    };
                }
                else
                {
                    return new Result<object>
                    {
                        Status = false,
                        Message = "You already have an active subscription and can't downgrade it.",
                        Action = "Index",
                        Controller = "Home"
                    };
                }
            }

            _unitOfWork.UserPackage.Add(userPackage);
            await _unitOfWork.SaveAsync();

            return new Result<object>
            {
                Status = true,
                Message = "Package Subscribed Successfully",
                Action = "Index",
                Controller = "Home"
            };
        }



        #region API CALLS
        public Result<object> Replace(int? id, string userId)
        {
            var rentedGameToBeReplaced = _unitOfWork.ShoppingCart.Get(u => u.Id == id);

            if (rentedGameToBeReplaced == null)
            {
                return new Result<object>
                {
                    Status = false,
                    Message = "Error while Replacing"
                };
            }

            rentedGameToBeReplaced.IsReplaced = true;

            // Checking User Package
            var userPackage = _unitOfWork.UserPackage.Get(u => u.ApplicationUserId == rentedGameToBeReplaced.ApplicationUserId, includeProperties: "SubscriptionPackage");

            int maxReplaceAllowed = 0;
            if (userPackage.SubscriptionPackage.PackageName == "Premium")
            {
                maxReplaceAllowed = userPackage.SubscriptionPackage.MaxReplacePerMonth;
            }
            else if (userPackage.SubscriptionPackage.PackageName == "Premium Max")
            {
                maxReplaceAllowed = userPackage.SubscriptionPackage.MaxReplacePerMonth;
            }
            else
            {
                maxReplaceAllowed = 0;
            }

            List<ShoppingCart> replacedGames = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId && u.IsReplaced == true).ToList();

            if (replacedGames.Count() >= maxReplaceAllowed)
            {
                return new Result<object>
                {
                    Status = false,
                    Message = $"You can't replace more games because you have already replaced {userPackage.SubscriptionPackage.MaxReplacePerMonth} games according to your subscrption package."
                };
            }

            Game replacedGame = _unitOfWork.Game.Get(g => g.Id == rentedGameToBeReplaced.GameId);
            replacedGame.Quantity += 1;
            _unitOfWork.Game.Update(replacedGame);

            _unitOfWork.ShoppingCart.Update(rentedGameToBeReplaced);
            _unitOfWork.Save();

            return new Result<object>
            {
                Status = true,
                Message = "Game Replaced Successfully"
            };
        }
        #endregion
    }
}
