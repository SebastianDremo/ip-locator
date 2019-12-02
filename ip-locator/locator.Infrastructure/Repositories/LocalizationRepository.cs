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

        public async Task<bool> CreateAsync(Localization localization)
        {
            await _context.Localizations.AddAsync(localization);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Localization> GetAsync(int id)
        {
            return await _context.Localizations.FirstOrDefaultAsync(localization => localization.Id == id);
        }

        public async Task<Localization> GetAsync(string ip)
        {
            return await _context.Localizations.FirstOrDefaultAsync(localization => localization.Ip.Equals(ip));
        }

        public async Task<List<Localization>> GetAllIpLocalizations(string ip)
        {
            return await _context.Localizations.Where(localization => localization.Ip.Equals(ip)).ToListAsync();
        }

        public async Task<List<Localization>> GetAllAsync()
        {
            return await _context.Localizations.ToListAsync();
        }

        public async Task<bool> RemoveByIpAsync(string ip, bool removeAllRows)
        {
            var localizationToRemove = await GetAsync(ip);
            if(removeAllRows)
            {
                var ipLocalizations = GetAllIpLocalizations(ip); 
            }
            else
            {
                _context.Localizations.Remove(localizationToRemove);
            }

            return await _context.SaveChangesAsync() > 0;
        }
    }
}