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
    [Authorize(Roles = "0")]
    [Route("api/[controller]")]
    [ApiController]
    public class SupervisorsController : ControllerBase
    {
        private readonly LibraryContext _context;

        public SupervisorsController(LibraryContext context)
        {
            _context = context;
        }

        // GET: api/Supervisors
        [HttpGet]
        [Authorize(Roles = "0")]
        public async Task<ActionResult<IEnumerable<Supervisor>>> GetSupervisor()
        {
          if (_context.Supervisor == null)
          {
              return NotFound();
          }
            return await _context.Supervisor.ToListAsync();
        }

        // GET: api/Supervisors/5
        [HttpGet("{id}")]
        //[Authorize(Roles = "0")]
        public async Task<ActionResult<Supervisor>> GetSupervisor(int id)
        {
          if (_context.Supervisor == null)
          {
              return NotFound();
          }
            var supervisor = await _context.Supervisor.FindAsync(id);

            if (supervisor == null)
            {
                return NotFound();
            }

            return supervisor;
        }

        // PUT: api/Supervisors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        //[Authorize(Roles = "0")]
        public async Task<IActionResult> PutSupervisor(int id, Supervisor supervisor)
        {
            if (id != supervisor.Id)
            {
                return BadRequest();
            }

            _context.Entry(supervisor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupervisorExists(id))
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

        // POST: api/Supervisors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        //[Authorize(Roles = "0")]
        public async Task<ActionResult<Supervisor>> PostSupervisor(Supervisor supervisor)
        {
          if (_context.Supervisor == null)
          {
              return Problem("Entity set 'LibraryContext.Supervisor'  is null.");
          }
            User user = new User
            {
                Username = supervisor.Tc,
                Password = BCrypt.Net.BCrypt.HashPassword(supervisor.Tc, workFactor: 10),
                Role = 1
            };

            supervisor.User = user;

            _context.User.Add(user);
            _context.Supervisor.Add(supervisor);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSupervisor", new { id = supervisor.Id }, supervisor);
        }

        // DELETE: api/Supervisors/5
        [HttpDelete("{id}")]
        //[Authorize(Roles = "0")]
        public async Task<IActionResult> DeleteSupervisor(int id)
        {
            if (_context.Supervisor == null)
            {
                return NotFound();
            }

            var supervisor = await _context.Supervisor.FindAsync(id);
            if (supervisor == null)
            {
                return NotFound();
            }
            
            var user = await _context.User.FindAsync(supervisor.UserId);
            if (user == null)
            {
                return NotFound("User is not found");
            }

            _context.Supervisor.Remove(supervisor);
            _context.User.Remove(user);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SupervisorExists(int id)
        {
            return (_context.Supervisor?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
