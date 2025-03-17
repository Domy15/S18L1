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
    }
}
