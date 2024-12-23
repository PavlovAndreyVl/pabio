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
            return await _context.Events.OrderByDescending(x=>x.CreatedAt).ToListAsync();
        }

        public async Task<Event?> GetEvent(int id)
        {
            return await _context.Events.FindAsync(id);
        }

        public async Task<bool> DoesEventExistAsync(int id)
        {
            return await _context.Events
                .Where(r => r.EventId == id)
                .AnyAsync();
        }

        /// <summary>
        /// Create a new event
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns>The id of the new event</returns>
        public async Task<int> CreateEvent(CreateEventCommand cmd)
        {
            var newEvent = cmd.ToEvent();
            _context.Add(newEvent);
            //newEvent.LastModified = DateTimeOffset.UtcNow;
            await _context.SaveChangesAsync();
            return newEvent.EventId;
        }
    }
}
