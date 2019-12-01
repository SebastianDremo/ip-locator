using System.Threading.Tasks;
using locator.Core.Entities;

namespace locator.Web.Services.Interfaces
{
    public interface IIpStackService
    {
         Task<Localization> GetLocalizationByIpAsync(string ip);
         Task<Localization> GetLocalizationByDnsAsync(string dns);
    }
}