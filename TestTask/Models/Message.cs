namespace TestTask.Models
{
    public class Message
    {
        public long Id { get; set; }

        public string Body { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now.ToUniversalTime();
        
        public int? UserId { get; set; }
        public User User { get; set; }
    }
}
