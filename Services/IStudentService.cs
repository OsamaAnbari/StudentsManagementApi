using Students_Management_Api.Models;

namespace Students_Management_Api.Services
{
    public interface IStudentService
    {
        IEnumerable<Student> GetData();
    }
}
