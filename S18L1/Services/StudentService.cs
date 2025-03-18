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

        private async Task<bool> SaveAsync()
        {
            try
            {
                var rowsAffected = await _context.SaveChangesAsync();

                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
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

        public async Task<Student?> GetStudentById(Guid id)
        {
            try
            {
                var student = await _context.Stundents.FindAsync(id);

                if (student == null) 
                {
                    return null;
                }

                return student;
            }
            catch 
            {
                return null;
            }
        }

        public async Task<bool> UpdateStudent(EditStudentViewModel editStudentViewModel)
        {
            var student = await _context.Stundents.FindAsync(editStudentViewModel.Id);

            if (student == null)
            {
                return false;
            }

            student.Name = editStudentViewModel.Name;
            student.Surname = editStudentViewModel.Surname;
            student.Email = editStudentViewModel.Email;
            student.BornDate = editStudentViewModel.BornDate;

            return await SaveAsync();
        }

        public async Task<bool> DeleteStudentById(Guid id)
        {
            try
            {
                var student = await _context.Stundents.FindAsync(id);

                if (student == null)
                {
                    return false;
                };

                _context.Stundents.Remove(student);

                return await SaveAsync();
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AddStudent(AddStudentViewModel addStudentViewModel)
        {
            try
            {
                var student = new Student()
                {
                    Id = Guid.NewGuid(),
                    Name = addStudentViewModel.Name,
                    Surname = addStudentViewModel.Surname,
                    Email = addStudentViewModel.Email,
                    BornDate = addStudentViewModel.BornDate
                };

                _context.Stundents.Add(student);

                return await SaveAsync();
            }
            catch
            {
                return false;
            }
        }
    }
}
