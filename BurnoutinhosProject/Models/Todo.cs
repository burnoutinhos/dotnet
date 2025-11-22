using BurnoutinhosProject.Enums;

namespace BurnoutinhosProject.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string Description { get; set; }

        public TypeEnum Type { get; set; }

        public int UserId { get; set; }
        public int IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
