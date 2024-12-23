using Microsoft.EntityFrameworkCore;
using pabio.Models;
using pabio.Models.Events;
using System;

namespace pabio.Services
{
    public class EventService
    {
        readonly PabioDbContext _context;
        readonly ILogger _logger;
        public EventService(PabioDbContext context, ILoggerFactory factory)
        {
            _context = context;
            _logger = factory.CreateLogger<EventService>();
        }

        public async Task<List<Event>> GetEvents()
        {
            return await _context.Events
                .Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<Event?> GetEvent(int id)
        {
            return await _context.Events
                .FindAsync(id);
        }

        public async Task<bool> DoesEventExistAsync(int id)
        {
            return await _context.Events
                .Where(r => r.EventId == id)
                .Where(x => !x.IsDeleted)
                .AnyAsync();
        }

        /// <summary>
        /// Create a new event
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns>The id of the new event</returns>
        public async Task<int> CreateEvent(CreateEventCommand cmd)
        {
            var @event = cmd.ToEvent();
            _context.Add(@event);
            @event.CreatedAt = DateTime.UtcNow;
            @event.LastModified = DateTimeOffset.UtcNow;
            await _context.SaveChangesAsync();
            return @event.EventId;
        }

        public async Task<UpdateEventCommand?> GetEventForUpdate(int eventId)
        {
            return await _context.Events
                .Where(x => x.EventId == eventId)
                .Where(x => !x.IsDeleted)
                .Select(x => new UpdateEventCommand
                {
                    Caption = x.Caption,
                    Description = x.Description!,
                    Date = x.Date!,
                    Url = x.Url
                })
                .SingleOrDefaultAsync();
        }

        /// <summary>
        /// Updateds an existing event
        /// </summary>
        /// <param name="cmd"></param>
        public async Task UpdateEvent(UpdateEventCommand cmd)
        {
            var @event = await _context.Events.FindAsync(cmd.Id);
            if (@event == null) { throw new Exception("Unable to find the event"); }

            cmd.UpdateEvent(@event);
            @event.LastModified = DateTimeOffset.UtcNow;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Marks an existing event as deleted
        /// </summary>
        /// <param name="cmd"></param>
        public async Task DeleteEvent(int eventId)
        {
            var @event = await _context.Events.FindAsync(eventId);
            if (@event is null) { throw new Exception("Unable to find event"); }

            @event.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
    }
}
