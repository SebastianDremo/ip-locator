using locator.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace locator.Infrastructure.EntityFramework
{
    public class LocatorContext : DbContext
    {
        public LocatorContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Localization> Localizations { get; set; }
    }
}