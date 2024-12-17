using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using pabio.Models;
using System;

namespace pabio.Data
{
    public class PabioDbContext: IdentityDbContext<ApplicationUser>
    {
        public PabioDbContext(DbContextOptions<PabioDbContext> options)
        : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
    }
}
