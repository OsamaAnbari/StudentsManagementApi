using System.Text.Json.Serialization;
using System.Text.Json.Serialization;

namespace Students_Management_Api.Models
{
    public class Supervisor
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
    }
}
