using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Students_Management_Api;
using Students_Management_Api.Models;
using Students_Management_Api.Services;

namespace Students_Management_Api.Controllers
{
    [Authorize(Roles = "1")]
    [Route("api/[controller]")]
    [ApiController]
    public class LecturesController : ControllerBase
    {
        private readonly LibraryContext _context;

        public LecturesController(LibraryContext context)
        {
            _context = context;
        }

        // GET: api/Lectures
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lecture>>> GetLecture()
        {
            if (_context.Lecture == null)
            {
                return NotFound();
            }
            return await _context.Lecture.ToListAsync();
        }

        // GET: api/Lectures/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lecture>> GetLecture(int id)
        {
            if (_context.Lecture == null)
            {
                return NotFound();
            }
            var lecture = await _context.Lecture.FindAsync(id);

            if (lecture == null)
            {
                return NotFound();
            }

            return lecture;
        }

        // PUT: api/Lectures/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLecture(int id, Lecture lecture)
        {
            if (id != lecture.Id)
            {
                return BadRequest();
            }

            _context.Entry(lecture).State = EntityState.Modified;

            var toRemove = _context.LectureStudent.Where(e => e.LecturesId == id).ToList();
            _context.LectureStudent.RemoveRange(toRemove);

            foreach (var studentId in lecture.StudentIds)
            {
                if (StudentExists(studentId))
                {
                    _context.LectureStudent.Add(
                    new LectureStudent()
                    {
                        Lecture = lecture,
                        StudentsId = studentId
                    });
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LectureExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Lectures
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Lecture>> PostLecture(Lecture lecture)
        {
            if (_context.Lecture == null)
            {
                return Problem("Entity set 'LibraryContext.Lecture'  is null.");
            }

            foreach (var studentId in lecture.StudentIds)
            {
                if (StudentExists(studentId))
                {
                    _context.LectureStudent.Add(
                    new LectureStudent()
                    {
                        Lecture = lecture,
                        StudentsId = studentId
                    });
                }
            }

            //lecture.Teacher = _context.Teacher.First(e => e.Id == 1);

            _context.Lecture.Add(lecture);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLecture", new { id = lecture.Id }, lecture);
        }
        /*
        [HttpPost("addstudents/{id}")]
        public async Task<ActionResult<Lecture>> AddStudents(int id, [FromBody] List<int> studentIds)
        {
            if (_context.Lecture == null)
            {
                return Problem("Entity set 'LibraryContext.Lecture'  is null.");
            }

            var lecture = await _context.Lecture.FindAsync(id);

            if (lecture == null)
            {
                return NotFound();
            }

            foreach (var studentId in studentIds)
            {
                if (StudentExists(studentId))
                {
                    if (!_context.LectureStudent.Any(eab => eab.StudentsId == studentId && eab.LecturesId == id))
                    {
                        _context.LectureStudent.Add(
                        new LectureStudent()
                        {
                            Lecture = lecture,
                            StudentsId = studentId
                        });
                    }
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Caught exception: {ex.Message}");
            }

            return CreatedAtAction("GetLecture", lecture);
        }

        [HttpPost("removestudents/{id}")]
        public async Task<ActionResult<Lecture>> RemoveStudents(int id, [FromBody] List<int> studentIds)
        {
            if (_context.Lecture == null)
            {
                return Problem("Entity set 'LibraryContext.Lecture'  is null.");
            }

            var lecture = await _context.Lecture.FindAsync(id);

            if (lecture == null)
            {
                return NotFound();
            }

            var lectureStudents = _context.LectureStudent.Where(c => studentIds.Contains(c.StudentsId)).ToList();

            foreach (var student in lectureStudents)
            {
                _context.LectureStudent.Remove(student);
            }

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLecture", lectureStudents);
        }*/

        // DELETE: api/Lectures/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLecture(int id)
        {
            if (_context.Lecture == null)
            {
                return NotFound();
            }
            var lecture = await _context.Lecture.FindAsync(id);
            if (lecture == null)
            {
                return NotFound();
            }

            _context.Lecture.Remove(lecture);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LectureExists(int id)
        {
            return (_context.Lecture?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private bool StudentExists(int id)
        {
            return (_context.Student?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private bool LectureExistsInStudent(int lectureId, int StudentId)
        {
            return (_context.Student?.Any(e => e.Id == StudentId)).GetValueOrDefault();
        }
    }
}
