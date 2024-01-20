using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Students_Management_Api.Models
{
    public class Student
    {
        public int Id { get; set; }
        [ForeignKey("IdentityUser")]
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string Firstname { get; set; }
        public string? Surname { get; set; }
        public DateTime? birth { get; set; }
        public string? Phone { get; set; }
        public string Tc { get; set; }
        public string? Faculty { get; set; }
        public string? Department { get; set; }
        public string? Year { get; set; }
        public List<Class>? Classes { get; set; } = new List<Class>();
        public List<Lecture>? Lectures { get; } = new List<Lecture>();
        public List<StudentMessages>? Sents { get; set; } = new List<StudentMessages>();
        public List<TeacherMessage>? Received { get; set; } = new List<TeacherMessage>();
    }

    public class StudentViewModel
    {
        public string Firstname { get; set; }
        public string? Surname { get; set; }
        public DateTime? birth { get; set; }
        public string? Phone { get; set; }
        public string Tc { get; set; }
        public string? Faculty { get; set; }
        public string? Department { get; set; }
        public string? Year { get; set; }
        public string Email { get; set; }
    }
}
