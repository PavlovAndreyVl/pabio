namespace pabio.Data
{
    public class Event
    {
        public int EventId { get; set; }
        public int Seq { get; set; }
        public string? Url { get; set; }
        public required string Caption { get; set; }
        public string? Date { get; set; }
        public string? Description { get; set; }
    }
}
