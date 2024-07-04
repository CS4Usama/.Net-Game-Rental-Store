using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GameRentalStore.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }

        public int GameId { get; set; }
        [ForeignKey("GameId")]
        [ValidateNever]
        public Game Game { get; set; }
        [Range(0, 1)]

        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
