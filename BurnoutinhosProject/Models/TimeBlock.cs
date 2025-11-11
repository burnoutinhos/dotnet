using BurnoutinhosProject.Enums;

namespace BurnoutinhosProject.Models
{
    public class TimeBlock
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime? Start { get; set; }

        public TypeTimeEnum TypeTime{ get; set; }

        public int? TodoId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
