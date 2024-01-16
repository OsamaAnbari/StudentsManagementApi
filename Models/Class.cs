using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Students_Management_Api.Models
{
    public class Class
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public Teacher Teacher { get; set; }
        [JsonIgnore]
        public List<Student> Students { get; set; }
        [JsonIgnore]
        [NotMapped]
        public List<int> StudentIds { get; set; }
    }

    public class ClassStudent
    {
        public int StudentsId { get; set; }
        public Student Student { get; set; }
        public int ClassesId { get; set; }
        public Class Class { get; set; }
    }
}