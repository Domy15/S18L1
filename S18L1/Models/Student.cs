using System.ComponentModel.DataAnnotations;

namespace S18L1.Models
{
    public class Student
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Surname { get; set; }

        [Required]
        public DateTime BornDate { get; set; }

        [Required]
        [StringLength(200)]
        public string Email { get; set; } 
    }
}
