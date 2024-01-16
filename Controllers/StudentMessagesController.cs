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

namespace Students_Management_Api.Controllers
{
    [Authorize(Roles ="3")]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentMessagesController : ControllerBase
    {
        private readonly LibraryContext _context;

        public StudentMessagesController(LibraryContext context)
        {
            _context = context;
        }

        // GET: api/StudentMessagess
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentMessages>>> GetStudentMessages()
        {
          if (_context.StudentMessages == null)
          {
              return NotFound();
          }
            return await _context.StudentMessages.ToListAsync();
        }

        // GET: api/StudentMessagess/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentMessages>> GetStudentMessages(int id)
        {
          if (_context.StudentMessages == null)
          {
              return NotFound();
          }
            var StudentMessages = await _context.StudentMessages.FindAsync(id);

            if (StudentMessages == null)
            {
                return NotFound();
            }

            return StudentMessages;
        }

        // PUT: api/StudentMessagess/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*[HttpPut("{id}")]
        public async Task<IActionResult> PutStudentMessages(int id, StudentMessages StudentMessages)
        {
            if (id != StudentMessages.Id)
            {
                return BadRequest();
            }

            _context.Entry(StudentMessages).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentMessagesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }*/

        // POST: api/StudentMessagess
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StudentMessages>> PostStudentMessages(StudentMessages StudentMessages)
        {
          if (_context.StudentMessages == null)
          {
              return Problem("Entity set 'LibraryContext.StudentMessages'  is null.");
          }

            StudentMessages.SenderId = Convert.ToInt32(HttpContext.Items["userId"]);

            if (StudentExists(StudentMessages.SenderId))
            {
                _context.StudentMessages.Add(StudentMessages);
            }
            else
            {
                return NotFound("The user is not found");
            }
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudentMessages", new { id = StudentMessages.Id }, StudentMessages);
        }

        // DELETE: api/StudentMessagess/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentMessages(int id)
        {
            if (_context.StudentMessages == null)
            {
                return NotFound();
            }
            var StudentMessages = await _context.StudentMessages.FindAsync(id);
            if (StudentMessages == null)
            {
                return NotFound();
            }

            _context.StudentMessages.Remove(StudentMessages);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentMessagesExists(int id)
        {
            return (_context.StudentMessages?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private bool StudentExists(int id)
        {
            return (_context.Student?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
