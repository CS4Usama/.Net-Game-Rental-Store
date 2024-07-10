using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GameRentalStore.Models
{
    public class UserPackage
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

        public int PackageId { get; set; }
        [ForeignKey("PackageId")]
        [ValidateNever]
        public SubscriptionPackage SubscriptionPackage { get; set; }


        [Display(Name = "Total Subscription Month")]
        public int TotalSubscriptionMonths { get; set; }

        [Display(Name = "Subscribed Date")]
        public DateOnly SubscribedDate { get; set; }

        [Display(Name = "Expired Date")]
        public DateOnly ExpiredDate { get; set; }
    }
}
