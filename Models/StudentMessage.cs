namespace Students_Management_Api.Models
{
    public class StudentMessages : Message
    {
        public Student? Sender { get; set; }
        public int SenderId { get; set; }
        public Teacher? Receiver { get; set; }
        public int ReceiverId { get; set; }
    }
}
