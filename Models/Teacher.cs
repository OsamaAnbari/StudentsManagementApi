using System.Text.Json.Serialization;

namespace Students_Management_Api.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        [JsonIgnore]
        public User? User { get; set; }
        [JsonIgnore]
        public int? UserId { get; set; }
        public string Firstname { get; set; }
        public string? Surname { get; set; }
        public string? Phone { get; set; }
        public string Tc { get; set; }
        public string? Study { get; set; }
        [JsonIgnore]
        public List<Class>? Classes { get; set; } = new List<Class>();
        [JsonIgnore]
        public List<Lecture>? Lectures { get; set; } = new List<Lecture>();
        [JsonIgnore]
        public List<StudentMessages>? Received { get; set; } = new List<StudentMessages>();
        [JsonIgnore]
        public List<TeacherMessage>? Sents { get; set; } = new List<TeacherMessage>();
    }
}
