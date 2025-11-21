namespace BurnoutinhosProject.Models
{
    public class Suggestion
    {
        public int Id { get; set; }
        public string SuggestionDescription { get; set; }
        public int TodoId { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
