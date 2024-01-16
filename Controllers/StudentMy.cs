using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg.Sig;
using System.Security.Cryptography;
using Students_Management_Api.Models;

namespace Students_Management_Api.Controllers
{
    [Authorize(Roles = "3")]
    [Route("api/Students/My")]
    [ApiController]
    public class StudentMy : ControllerBase
    {
        private readonly LibraryContext _context;

        public StudentMy(LibraryContext context)
        {
            _context = context;
        }

        [HttpGet("infos")]
        public async Task<ActionResult<Student>> GetMyInfos()
        {
            int _id = Convert.ToInt32(HttpContext.Items["userId"]);

            if (_context.Student == null)
            {
                return NotFound();
            }

            var student = await _context.Student.FindAsync(_id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        [HttpGet("lectures")]
        public async Task<ActionResult<List<Lecture>>> GetMyLectures()
        {
            int _id = Convert.ToInt32(HttpContext.Items["userId"]);

            if (_context.Student == null)
            {
                return NotFound();
            }

            var student = await _context.Student.Include(e => e.Lectures).FirstAsync(e => e.Id == _id);

            if (student == null)
            {
                return NotFound();
            }

            return student.Lectures;
        }

        [HttpGet("sents")]
        public async Task<ActionResult<List<StudentMessages>>> GetMySents()
        {
            int _id = Convert.ToInt32(HttpContext.Items["userId"]);

            if (_context.Student == null)
            {
                return NotFound();
            }

            var student = await _context.Student.Include(e => e.Sents).FirstAsync(e => e.Id == _id);

            if (student == null)
            {
                return NotFound();
            }

            return student.Sents;
        }

        [HttpGet("received")]
        public async Task<ActionResult<List<TeacherMessage>>> GetMyReceived()
        {
            int _id = Convert.ToInt32(HttpContext.Items["userId"]);

            if (_context.Student == null)
            {
                return NotFound();
            }

            var student = await _context.Student.Include(e => e.Received).FirstAsync(e => e.Id == _id);

            if (student == null)
            {
                return NotFound();
            }

            return student.Received;
        }
    }
}
