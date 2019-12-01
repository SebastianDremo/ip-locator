using System.Collections.Generic;
using System.Threading.Tasks;
using locator.Core.Entities;

namespace locator.Infrastructure.Repositories.Interfaces
{
    public interface ILocalizationRepository
    {
        Task<bool> Create(Localization localization);
        Task<Localization> Get(int id);
        Task<Localization> Get(string ip);
        Task<IEnumerable<Localization>> GetAll();
        Task<bool> Remove(int id);
    }
}