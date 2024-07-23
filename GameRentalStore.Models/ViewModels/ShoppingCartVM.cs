using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GameRentalStore.Models.ViewModels
{
    public class ShoppingCartVM
    {
        public Game Game { get; set; }

        [ValidateNever]
        public ShoppingCart ShoppingCart { get; set; }

        [ValidateNever]
        public List<GameRating> GameRating { get; set; }
    }
}
