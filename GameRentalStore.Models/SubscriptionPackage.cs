using System.ComponentModel.DataAnnotations;

namespace GameRentalStore.Models
{
    public class SubscriptionPackage
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Package Name")]
        public string PackageName { get; set; }

        [Required]
        [Display(Name = "Games per Month")]
        public int GamesPerMonth { get; set; }

        [Display(Name = "Rent New Released Game")]
        public int RentNewReleasedGame { get; set; }

        [Display(Name = "Game Replacements per Month")]
        public int MaxReplacePerMonth { get; set; }
    }
}
