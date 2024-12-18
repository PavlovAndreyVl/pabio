using Microsoft.EntityFrameworkCore;
using pabio.Data;
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
            return await _context.Events.OrderByDescending(x=>x.Seq).ToListAsync();
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
    }
}
