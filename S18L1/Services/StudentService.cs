using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using S18L1.Data;
using S18L1.Models;
using S18L1.ViewModels;

namespace S18L1.Services
{
    public class StudentService
    {
        private readonly ApplicationDbContext _context;

        public StudentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<StudentListViewModel> GetStudents()
        {
            var students = new StudentListViewModel();

            try
            {
                students.Students = await _context.Stundents.ToListAsync();
            }
            catch
            {
                students.Students = null;
            }

            return students;
        }
    }
}
