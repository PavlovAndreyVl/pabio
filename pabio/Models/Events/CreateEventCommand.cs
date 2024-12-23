namespace pabio.Models.Events
{
    public class CreateEventCommand : EditEventBase
    {        
        public Event ToEvent() 
        {
            return new Event
            {
                Url = Url,
                Date = Date,
                Description = Description,
                Caption = Caption
            };
        }
    }
}
