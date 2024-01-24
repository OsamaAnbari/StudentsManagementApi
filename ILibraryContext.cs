using Microsoft.EntityFrameworkCore;
using Students_Management_Api.Models;

namespace Students_Management_Api
{
    public interface ILibraryContext
    {
        DbSet<Student> Student { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
