/*using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Students_Management_Api.Models;

namespace Students_Management_Api.Controllers
{
    [Authorize(Roles = "2")]
    [Route("api/Teachers/My")]
    [ApiController]
    public class TeacherMy : ControllerBase
    {
        private readonly LibraryContext _context;

        public TeacherMy(LibraryContext context)
        {
            _context = context;
        }

        [HttpGet("infos")]
        public async Task<ActionResult<Teacher>> GetMyInfos()
        {
            int _id = Convert.ToInt32(HttpContext.Items["userId"]);

            if (_context.Teacher == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teacher.FindAsync(_id);

            if (teacher == null)
            {
                return NotFound();
            }

            return teacher;
        }

        [HttpGet("lectures")]
        public async Task<ActionResult<List<Lecture>>> GetMyLectures()
        {
            int _id = Convert.ToInt32(HttpContext.Items["userId"]);

            if (_context.Teacher == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teacher.Include(e => e.Lectures).FirstAsync(e => e.Id == _id);

            if (teacher == null)
            {
                return NotFound();
            }

            return teacher.Lectures;
        }

        [HttpGet("sents")]
        public async Task<ActionResult<List<TeacherMessage>>> GetMySents()
        {
            int _id = Convert.ToInt32(HttpContext.Items["userId"]);

            if (_context.Teacher == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teacher.Include(e => e.Sents).FirstAsync(e => e.Id == _id);

            if (teacher == null)
            {
                return NotFound();
            }

            return teacher.Sents;
        }

        [HttpGet("received")]
        public async Task<ActionResult<List<StudentMessages>>> GetMyReceived()
        {
            int _id = Convert.ToInt32(HttpContext.Items["userId"]);

            if (_context.Teacher == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teacher.Include(e => e.Received).FirstAsync(e => e.Id == _id);

            if (teacher == null)
            {
                return NotFound();
            }

            return teacher.Received;
        }
    }
}
*/