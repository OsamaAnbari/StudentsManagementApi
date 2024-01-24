using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Students_Management_Api.Models;
using Students_Management_Api.Services;

namespace Students_Management_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TempStudentsController : ControllerBase
    {
        IStudentService _studentService;
        public TempStudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudent()
        {
            var data = _studentService.GetData();
            return Ok(data);
        }
    }
}
