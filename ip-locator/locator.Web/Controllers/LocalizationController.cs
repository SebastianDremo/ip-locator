using locator.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace locator.Web.Controllers
{
    public class LocalizationController : ControllerBase
    {
        private readonly ILocalizationRepository _localizationRepository;

        public LocalizationController(ILocalizationRepository localizationRepository)
        {
            _localizationRepository = localizationRepository;
        }
    }
}