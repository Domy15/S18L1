using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using S18L1.Services;
using S18L1.ViewModels;

namespace S18L1.Controllers
{
    public class HomeController : Controller
    {
        private readonly StudentService _studentService;

        public HomeController(StudentService studentService)
        {
            _studentService = studentService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Students/All-Students")]
        public async Task<IActionResult> StudentList()
        {
            var studentList = await _studentService.GetStudents();

            return PartialView("_StudentList", studentList);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var student = await _studentService.GetStudentById(id);

            if (student == null)
            {
                return RedirectToAction("Index");
            };

            var editStudent = new EditStudentViewModel()
            {
                Id = student.Id,
                Name = student.Name,
                Surname = student.Surname,
                Email = student.Email,
                BornDate = student.BornDate
            };

            return PartialView("_EditStudent", editStudent);
        }

        [HttpPost("Home/Edit/Save")]
        public async Task<IActionResult> SaveEdit(EditStudentViewModel editStudentViewModel)
        {
            var result = await _studentService.UpdateStudent(editStudentViewModel);

            if (!result)
            {
                return Json(new
                {
                    success = false,
                    message = "Error while updating entity on database"
                });
            }

            return Json(new
            {
                success = true,
                message = "Entity updated successfully"
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _studentService.DeleteStudentById(id);

            if (!result)
            {
                return Json(new
                {
                    success = false,
                    message = "Error while deleting entity"
                });
            }

            return Json(new
            {
                success = true,
                message = "Entity deleted successfully"
            });
        }

        public IActionResult Add()
        {
            return PartialView("_AddStudent");
        }

        [HttpPost("Home/Add/Save")]
        public async Task<IActionResult> SaveAdd(AddStudentViewModel addStudentViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    success = false,
                    message = "Error while saving entity to database"
                });
            }

            var result = await _studentService.AddStudent(addStudentViewModel);

            if (!result)
            {
                return Json(new
                {
                    success = false,
                    message = "Error while saving entity to database"
                });
            }

            return Json(new
            {
                success = true,
                message = "Entity saved successfully to database"
            });
        }
    }
}
