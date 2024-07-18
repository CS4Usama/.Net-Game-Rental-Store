using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GameRentalStore.Models
{
    public class GameRating
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5.")]
        public int Rating { get; set; }
        public string? Review { get; set; }
        [ValidateNever]
        public string Status { get; set; }

        public string? ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

        public int CartGameId { get; set; }
        [ForeignKey("CartGameId")]
        [ValidateNever]
        public ShoppingCart ShoppingCart { get; set; }

        public int GameId { get; set; }
        [ForeignKey("GameId")]
        [ValidateNever]
        public Game Game { get; set; }
    }
}
