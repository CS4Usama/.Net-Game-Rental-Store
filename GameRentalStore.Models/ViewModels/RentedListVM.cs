using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GameRentalStore.Models.ViewModels
{
    public class RentedListVM
    {
        public UserPackage UserPackage { get; set; }
        [ValidateNever]
        public List<ShoppingCart> ShoppingCart { get; set; }
    }
}
