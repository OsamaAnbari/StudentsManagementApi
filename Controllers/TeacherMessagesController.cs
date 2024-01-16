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
    [Authorize(Roles = "2")]
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherMessagesController : ControllerBase
    {
        private readonly LibraryContext _context;

        public TeacherMessagesController(LibraryContext context)
        {
            _context = context;
        }

        // GET: api/TeacherMessages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeacherMessage>>> GetTeacherMessage()
        {
          if (_context.TeacherMessage == null)
          {
              return NotFound();
          }
            return await _context.TeacherMessage.ToListAsync();
        }

        // GET: api/TeacherMessages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TeacherMessage>> GetTeacherMessage(int id)
        {
          if (_context.TeacherMessage == null)
          {
              return NotFound();
          }
            var teacherMessage = await _context.TeacherMessage.FindAsync(id);

            if (teacherMessage == null)
            {
                return NotFound();
            }

            return teacherMessage;
        }

        // PUT: api/TeacherMessages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*[HttpPut("{id}")]
        public async Task<IActionResult> PutTeacherMessage(int id, TeacherMessage teacherMessage)
        {
            if (id != teacherMessage.Id)
            {
                return BadRequest();
            }

            _context.Entry(teacherMessage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherMessageExists(id))
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

        // POST: api/TeacherMessages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TeacherMessage>> PostTeacherMessage(TeacherMessage teacherMessage)
        {
          if (_context.TeacherMessage == null)
          {
              return Problem("Entity set 'LibraryContext.TeacherMessage'  is null.");
          }

            foreach (var studentId in teacherMessage.ReceiverIds)
            {
                if (StudentExists(studentId))
                {
                    _context.StudentTeacherMessage.Add(
                    new StudentTeacherMessage()
                    {
                        Received = teacherMessage,
                        ReceiversId = studentId
                    });
                }
            }

            _context.TeacherMessage.Add(teacherMessage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTeacherMessage", new { id = teacherMessage.Id }, teacherMessage);
        }

        // DELETE: api/TeacherMessages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacherMessage(int id)
        {
            if (_context.TeacherMessage == null)
            {
                return NotFound();
            }
            var teacherMessage = await _context.TeacherMessage.FindAsync(id);
            if (teacherMessage == null)
            {
                return NotFound();
            }

            _context.TeacherMessage.Remove(teacherMessage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TeacherMessageExists(int id)
        {
            return (_context.TeacherMessage?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private bool StudentExists(int id)
        {
            return (_context.Student?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
