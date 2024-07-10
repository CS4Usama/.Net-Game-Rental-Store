using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GameRentalStore.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }

        public int Count { get; set; }

        [Display(Name = "Rented Date")]
        public DateOnly RentedDate { get; set; }


        public int GameId { get; set; }
        [ForeignKey("GameId")]
        [ValidateNever]
        public Game Game { get; set; }

        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

        public int? UserPackageId { get; set; }
        [ForeignKey("UserPackageId")]
        [ValidateNever]
        public UserPackage? UserPackage { get; set; }

        public int PackageId { get; set; }
        [ForeignKey("PackageId")]
        [ValidateNever]
        public SubscriptionPackage SubscriptionPackage { get; set; }
    }
}
