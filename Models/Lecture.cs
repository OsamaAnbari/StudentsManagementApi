using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Students_Management_Api.Models
{
    public class Lecture
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime Date { get; set; }
        public int? TeacherID { get; set; }
        [JsonIgnore]
        public virtual Teacher? Teacher { get; set; }
        [JsonIgnore]
        public List<Student>? Students { get; set; } = new List<Student>();
        [NotMapped]
        [JsonIgnore]
        public List<int> StudentIds { get; set; } = new List<int>();
        
    }
    public class LectureStudent
    {
        //public int Id { get; set; }
        public int StudentsId { get; set; }
        public Student Student { get; set; }
        public int LecturesId { get; set; }
        public Lecture Lecture { get; set; }
    }
}
