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

        [HttpPost("locate/{ip}")]
        public async Task<IActionResult> LocateIp(string ip)
        {
            var localization = await _ipService.GetLocalizationByIpAsync(ip);
            if(localization == null)
            {
                return NotFound();
            }

            await _localizationRepository.CreateAsync(localization);
            return Ok(_mapper.Map<LocalizationModel>(localization));
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

        [HttpDelete("remove/{ip}")]
        public async Task<IActionResult> RemoveLocation(string ip, bool removeAllRows = false)
        {
            var result = await _localizationRepository.RemoveByIpAsync(ip, removeAllRows);
            if(!result)
            {
                return NotFound();
            }
            return StatusCode(202);
        }
    }
}