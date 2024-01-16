using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Students_Management_Api.Models
{
    public class TeacherMessage : Message
    {
        [JsonIgnore]
        public Teacher? Sender { get; set; }
        [JsonIgnore]
        public List<Student>? Receivers { get; set; } = new List<Student>();
        [NotMapped]
        public List<int> ReceiverIds { get; set; }= new List<int>();
    }

    public class StudentTeacherMessage
    {
        public int ReceiversId { get; set; }
        public Student Receivers { get; set; }
        public int ReceivedId { get; set; }
        public TeacherMessage Received { get; set; }
    }
}
