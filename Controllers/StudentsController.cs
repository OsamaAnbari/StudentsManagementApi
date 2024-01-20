using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;
using NuGet.Protocol.Plugins;
using Students_Management_Api;
using Students_Management_Api.Models;

namespace Students_Management_Api.Controllers
{
    //[Authorize(Roles = "1")]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly LibraryContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public StudentsController(
            LibraryContext context, 
            UserManager<ApplicationUser> userManager
            )
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudent()
        {
          if (_context.Student == null)
          {
              return NotFound();
          }
            return await _context.Student.ToListAsync();
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
          if (_context.Student == null)
          {
              return NotFound();
          }
            var student = await _context.Student.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, StudentViewModel model)
        {

            var student = await _context.Student.FindAsync(id);
            _context.Student.Attach(student);
            _context.Entry(student).CurrentValues.SetValues(model);
            /*_context.Entry(student).Property(x => x.Firstname).IsModified = true;
            _context.Entry(student).Property(x => x.Surname).IsModified = true;
            _context.Entry(student).Property(x => x.Phone).IsModified = true;
            _context.Entry(student).Property(x => x.Tc).IsModified = true;
            _context.Entry(student).Property(x => x.Faculty).IsModified = true;
            _context.Entry(student).Property(x => x.Department).IsModified = true;
            _context.Entry(student).Property(x => x.Year).IsModified = true;
            _context.Entry(student).Property(x => x.birth).IsModified = true;*/

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
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

        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(StudentViewModel model)
        {
            if (_context.Student == null)
            {
                return Problem("Entity set 'LibraryContext.Student'  is null.");
            }
            var user = new ApplicationUser() { UserName = model.Tc, Email = model.Email };
            var result = await _userManager.CreateAsync(user, $"Aa.{model.Tc}");

            if (result.Succeeded)
            {
                var addToRoleResult = await _userManager.AddToRoleAsync(user, "Student");

                if (addToRoleResult.Succeeded)
                {
                    var student = new Student()
                    {
                        UserId = user.Id,
                        Firstname = model.Firstname,
                        Surname = model.Surname,
                        birth = model.birth,
                        Phone = model.Phone,
                        Tc = model.Tc,
                        Faculty = model.Faculty,
                        Department = model.Department,
                        Year = model.Year
                    };

                    _context.Student.Add(student);
                    await _context.SaveChangesAsync();

                    return Ok($"User '{model.Tc}' created successfully and assigned to the role 'Student'");
                }
                else
                {
                    await _userManager.DeleteAsync(user);
                    return BadRequest($"Failed to assign the user to the role: {string.Join(", ", addToRoleResult.Errors)}");
                }
            }
            return BadRequest(result.Errors);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            if (_context.Student == null)
            {
                return NotFound();
            }
            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(student.UserId);
            if (user == null)
            {
                return NotFound("User is not found");
            }

            _context.Student.Remove(student);
            await _userManager.DeleteAsync(user);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentExists(int id)
        {
            return (_context.Student?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}