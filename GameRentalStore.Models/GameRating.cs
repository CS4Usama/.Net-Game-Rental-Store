using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GameRentalStore.Models
{
    public class GameRating
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string? Review { get; set; }
        public bool IsApproved { get; set; }

        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

        public int CartGameId { get; set; }
        [ForeignKey("CartGameId")]
        [ValidateNever]
        public ShoppingCart ShoppingCart { get; set; }
    }
}
