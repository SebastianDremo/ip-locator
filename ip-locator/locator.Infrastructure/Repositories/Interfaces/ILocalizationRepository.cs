using System.Collections.Generic;
using System.Threading.Tasks;
using locator.Core.Entities;

namespace locator.Infrastructure.Repositories.Interfaces
{
    public interface ILocalizationRepository
    {
        Task<bool> CreateAsync(Localization localization);
        Task<Localization> GetAsync(int id);
        Task<Localization> GetAsync(string ip);
        Task<List<Localization>> GetAllAsync();
        Task<bool> RemoveAsync(int id);
    }
}