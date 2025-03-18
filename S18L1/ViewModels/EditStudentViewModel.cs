using System.ComponentModel.DataAnnotations;

namespace S18L1.ViewModels
{
    public class EditStudentViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime BornDate { get; set; }

        public string Email { get; set; }
    }
}
