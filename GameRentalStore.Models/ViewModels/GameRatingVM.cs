using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GameRentalStore.Models.ViewModels
{
    public class GameRatingVM
    {
        public GameRating GameRating { get; set; }

        [ValidateNever]
        public ShoppingCart ShoppingCart { get; set; }

        [ValidateNever]
        public Game Game { get; set; }
    }
}
