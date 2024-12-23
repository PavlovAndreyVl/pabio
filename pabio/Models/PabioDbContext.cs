using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using pabio.Models.Events;
using System;

namespace pabio.Models
{
    public class PabioDbContext : IdentityDbContext<ApplicationUser>
    {
        public PabioDbContext(DbContextOptions<PabioDbContext> options)
        : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
    }
}
