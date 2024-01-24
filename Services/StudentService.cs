using Microsoft.EntityFrameworkCore;
using Students_Management_Api.Models;

namespace Students_Management_Api.Services
{
    public class StudentService : IStudentService
    {
        private readonly LibraryContext _context;
        public StudentService(LibraryContext context)
        {
            _context = context;
        }
        public IEnumerable<Student> GetData()
        {
            return _context.Student.ToList();
        }
    }
}
