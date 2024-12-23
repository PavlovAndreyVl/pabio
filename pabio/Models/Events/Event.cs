namespace pabio.Models.Events
{
    public class Event
    {
        public int EventId { get; set; }
        public string? Url { get; set; }
        public required string Caption { get; set; }
        public string? Date { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
