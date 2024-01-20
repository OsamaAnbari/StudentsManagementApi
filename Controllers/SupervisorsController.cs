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
    public class SupervisorsController : ControllerBase
    {
        private readonly LibraryContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SupervisorsController(
            LibraryContext context,
            UserManager<ApplicationUser> userManager
            )
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Supervisors
        [HttpGet]
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
        public async Task<ActionResult<Supervisor>> GetSupervisor(int id)
        {
            if (_context.Supervisor == null)
            {
                return NotFound();
            }
            var Supervisor = await _context.Supervisor.FindAsync(id);

            if (Supervisor == null)
            {
                return NotFound();
            }

            return Supervisor;
        }

        // PUT: api/Supervisors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSupervisor(int id, Supervisor Supervisor)
        {
            if (id != Supervisor.Id)
            {
                return BadRequest();
            }

            _context.Supervisor.Attach(Supervisor);
            _context.Entry(Supervisor).Property(x => x.Firstname).IsModified = true;
            _context.Entry(Supervisor).Property(x => x.Surname).IsModified = true;
            _context.Entry(Supervisor).Property(x => x.Phone).IsModified = true;
            _context.Entry(Supervisor).Property(x => x.Tc).IsModified = true;
            _context.Entry(Supervisor).Property(x => x.birth).IsModified = true;

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
        public async Task<ActionResult<Supervisor>> PostSupervisor(SupervisorViewModel model)
        {
            if (_context.Supervisor == null)
            {
                return Problem("Entity set 'LibraryContext.Supervisor'  is null.");
            }
            var user = new ApplicationUser() { UserName = model.Tc, Email = model.Email };
            var result = await _userManager.CreateAsync(user, $"Aa.{model.Tc}");

            if (result.Succeeded)
            {
                var addToRoleResult = await _userManager.AddToRoleAsync(user, "Supervisor");

                if (addToRoleResult.Succeeded)
                {
                    var Supervisor = new Supervisor()
                    {
                        UserId = user.Id,
                        Firstname = model.Firstname,
                        Surname = model.Surname,
                        birth = model.birth,
                        Phone = model.Phone,
                        Tc = model.Tc,
                    };

                    _context.Supervisor.Add(Supervisor);
                    await _context.SaveChangesAsync();

                    return Ok($"User '{model.Tc}' created successfully and assigned to the role 'Supervisor'");
                }
                else
                {
                    await _userManager.DeleteAsync(user);
                    return BadRequest($"Failed to assign the user to the role: {string.Join(", ", addToRoleResult.Errors)}");
                }
            }
            return BadRequest(result.Errors);
        }

        // DELETE: api/Supervisors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupervisor(int id)
        {
            if (_context.Supervisor == null)
            {
                return NotFound();
            }
            var Supervisor = await _context.Supervisor.FindAsync(id);
            if (Supervisor == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(Supervisor.UserId);
            if (user == null)
            {
                return NotFound("User is not found");
            }

            _context.Supervisor.Remove(Supervisor);
            await _userManager.DeleteAsync(user);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SupervisorExists(int id)
        {
            return (_context.Supervisor?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}