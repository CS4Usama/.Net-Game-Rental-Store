using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GameRentalStore.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }

        public int Count { get { return 1; } }

        public int GameId { get; set; }
        [ForeignKey("GameId")]
        [ValidateNever]
        public Game Game { get; set; }
        [Range(0, 1)]

        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }


        [Display(Name = "Rented Date")]
        public DateOnly RentedDate { get; set; }

        [Display(Name = "Return Date")]
        public DateOnly ReturnDate
        {
            get
            {
                return RentedDate.AddMonths(1);
            }
        }

        //public string UserPackageId { get; set; }
        //[ForeignKey("UserPackageId")]
        //[ValidateNever]
        //public UserPackage UserPackage { get; set; }
    }
}
