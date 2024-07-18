using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GameRentalStore.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public string Platform { get; set; }
        [Required]
        [Display(Name = "Release Date")]
        public DateOnly ReleaseDate { get; set; }
        public int Quantity { get; set; }

        public int GenreId { get; set; }
        [ForeignKey("GenreId")]
        [ValidateNever]
        public Genre Genre { get; set; }

        [ValidateNever]
        public List<GameMedia> GameMedias { get; set; }

        [ValidateNever]
        public List<GameRating> GameRatings { get; set; }
    }
}
