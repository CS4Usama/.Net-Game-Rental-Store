using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;


namespace GameRentalStore.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public string Role { get; set; }
    }
}
