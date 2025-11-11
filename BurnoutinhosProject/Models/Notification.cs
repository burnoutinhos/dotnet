namespace BurnoutinhosProject.Models
{
    public class Notification
    {

        public int Id { get; set; }
        public int TaskId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }
    }
}
