using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GameRentalStore.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(25)]
        [DisplayName("Genre Name")]
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "Display Order Range is not Correct.")]
        public int DisplayOrder { get; set; }
    }
}
