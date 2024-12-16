using Microsoft.EntityFrameworkCore;
using System;

namespace pabio.Data
{
    public class PabioDbContext: DbContext
    {
        public PabioDbContext(DbContextOptions<PabioDbContext> options)
        : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
    }
}
