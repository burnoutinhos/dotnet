using BurnoutinhosProject.Enums;

namespace BurnoutinhosProject.Models
{
    public class Analytics
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public MetricEnum Metric { get; set; }
        public double Value { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
