using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Students_Management_Api.Models
{
    public class Supervisor
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
    }

    public class SupervisorViewModel
    {
        public string Firstname { get; set; }
        public string? Surname { get; set; }
        public DateTime? birth { get; set; }
        public string? Phone { get; set; }
        public string Tc { get; set; }
        public string Email { get; set; }
    }
}
