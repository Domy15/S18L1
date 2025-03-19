using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace S18L1.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public DateOnly? BirthDate { get; set; }

        [Required]
        public ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; }
    }
}
