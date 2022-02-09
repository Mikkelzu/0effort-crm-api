using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace _0effort_crm_api.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage="First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email field is required.")]
        [StringLength(maximumLength: 100, MinimumLength = 2)]
        [EmailAddress]
        public string Email { get; set; }


        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "Postcode is required")]
        public string Postcode { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        public string phone { get; set; }
    }
}
