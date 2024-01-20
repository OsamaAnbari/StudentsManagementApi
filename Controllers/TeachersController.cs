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
    public class TeachersController : ControllerBase
    {
        private readonly LibraryContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TeachersController(
            LibraryContext context,
            UserManager<ApplicationUser> userManager
            )
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Teachers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Teacher>>> GetTeacher()
        {
            if (_context.Teacher == null)
            {
                return NotFound();
            }
            return await _context.Teacher.ToListAsync();
        }

        // GET: api/Teachers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Teacher>> GetTeacher(int id)
        {
            if (_context.Teacher == null)
            {
                return NotFound();
            }
            var teacher = await _context.Teacher.FindAsync(id);

            if (teacher == null)
            {
                return NotFound();
            }

            return teacher;
        }

        // PUT: api/Teachers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeacher(int id, Teacher teacher)
        {
            if (id != teacher.Id)
            {
                return BadRequest();
            }

            _context.Teacher.Attach(teacher);
            _context.Entry(teacher).Property(x => x.Firstname).IsModified = true;
            _context.Entry(teacher).Property(x => x.Surname).IsModified = true;
            _context.Entry(teacher).Property(x => x.Phone).IsModified = true;
            _context.Entry(teacher).Property(x => x.Tc).IsModified = true;
            _context.Entry(teacher).Property(x => x.Study).IsModified = true;
            _context.Entry(teacher).Property(x => x.birth).IsModified = true;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherExists(id))
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

        // POST: api/Teachers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Teacher>> PostTeacher(TeacherViewModel model)
        {
            if (_context.Teacher == null)
            {
                return Problem("Entity set 'LibraryContext.Teacher'  is null.");
            }
            var user = new ApplicationUser() { UserName = model.Tc, Email = model.Email };
            var result = await _userManager.CreateAsync(user, $"Aa.{model.Tc}");

            if (result.Succeeded)
            {
                var addToRoleResult = await _userManager.AddToRoleAsync(user, "Teacher");

                if (addToRoleResult.Succeeded)
                {
                    var teacher = new Teacher()
                    {
                        UserId = user.Id,
                        Firstname = model.Firstname,
                        Surname = model.Surname,
                        birth = model.birth,
                        Phone = model.Phone,
                        Tc = model.Tc,
                        Study = model.Study,
                    };

                    _context.Teacher.Add(teacher);
                    await _context.SaveChangesAsync();

                    return Ok($"User '{model.Tc}' created successfully and assigned to the role 'Teacher'");
                }
                else
                {
                    await _userManager.DeleteAsync(user);
                    return BadRequest($"Failed to assign the user to the role: {string.Join(", ", addToRoleResult.Errors)}");
                }
            }
            return BadRequest(result.Errors);
        }

        // DELETE: api/Teachers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            if (_context.Teacher == null)
            {
                return NotFound();
            }
            var teacher = await _context.Teacher.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(teacher.UserId);
            if (user == null)
            {
                return NotFound("User is not found");
            }

            _context.Teacher.Remove(teacher);
            await _userManager.DeleteAsync(user);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TeacherExists(int id)
        {
            return (_context.Teacher?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}