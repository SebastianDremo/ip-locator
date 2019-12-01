using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using locator.Core.Models;
using locator.Infrastructure.Repositories.Interfaces;
using locator.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace locator.Web.Controllers
{
    [Route("[controller]")]
    public class LocalizationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILocalizationRepository _localizationRepository;
        private readonly IIpStackService _ipService;

        public LocalizationController(ILocalizationRepository localizationRepository, IMapper mapper, IIpStackService ipService)
        {
            _mapper = mapper;
            _localizationRepository = localizationRepository;
            _ipService = ipService;
        }

        [HttpGet("locate/{ip}")]
        public async Task<LocalizationModel> LocateIp(string ip)
        {
            var localization = await _ipService.GetLocalizationByIpAsync(ip);
            await _localizationRepository.CreateAsync(localization);
            return _mapper.Map<LocalizationModel>(localization);
        }

        [HttpGet("all-localizations")]
        public async Task<IActionResult> AllLocalizations()
        {
            var localizations = await _localizationRepository.GetAllAsync();

            if(!localizations.Any())
            {
                return NotFound();
            }

            return Ok(_mapper.Map<List<LocalizationModel>>(localizations));
        }
    }
}