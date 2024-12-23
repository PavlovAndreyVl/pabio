namespace pabio.Models.Events
{
    public class UpdateEventCommand : EditEventBase
    {
        public int Id { get; set; }

        public void UpdateEvent(Event @event) 
        {
            @event.Caption = Caption;
            @event.Description = Description;
            @event.Date = Date;
            @event.Url = Url;
        }
    }
}
