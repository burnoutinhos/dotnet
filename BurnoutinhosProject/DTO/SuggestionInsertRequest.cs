public class SuggestionInsertRequest
    {
        public int IdSuggestion { get; set; }
        public string SuggestionDesc { get; set; }
        public DateTime CreatedAt { get; set; }
        public int IdTodo { get; set; }
    }