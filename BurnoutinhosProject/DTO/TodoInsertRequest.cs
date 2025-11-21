public class TodoInsertRequest
    {
        public int IdTodo { get; set; }
        public string NameTodo { get; set; }
        public DateTime StartTodo { get; set; }
        public DateTime EndTodo { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Description { get; set; }
        public char IsCompleted { get; set; }
        public int IdUser { get; set; }
    }