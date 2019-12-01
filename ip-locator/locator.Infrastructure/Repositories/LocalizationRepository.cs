using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using locator.Core.Entities;
using locator.Infrastructure.EntityFramework;
using locator.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace locator.Infrastructure.Repositories
{
    public class LocalizationRepository : ILocalizationRepository
    {
        private LocatorContext _context;

        public LocalizationRepository(LocatorContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(Localization localization)
        {
            await _context.Localizations.AddAsync(localization);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Localization> Get(int id)
        {
            return await _context.Localizations.FirstOrDefaultAsync(localization => localization.Id == id);
        }

        public async Task<Localization> Get(string ip)
        {
            return await _context.Localizations.FirstOrDefaultAsync(localization => localization.Ip.Equals(ip));
        }

        public async Task<IEnumerable<Localization>> GetAll()
        {
            return await _context.Localizations.ToListAsync();
        }

        public async Task<bool> Remove(int id)
        {
            var localizationToRemove = await Get(id);
            _context.Localizations.Remove(localizationToRemove);
            
            return await _context.SaveChangesAsync() > 0;
        }
    }
}