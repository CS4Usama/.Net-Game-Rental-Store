using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameRentalStore.Models
{
    public class GameMedia
    {
        public int Id { get; set; }
        [Required]
        public string MediaUrl { get; set; }
        public string? MediaType { get; set; }
        public int GameId { get; set; }
        [ForeignKey("GameId")]
        public Game Game { get; set; }
    }
}
