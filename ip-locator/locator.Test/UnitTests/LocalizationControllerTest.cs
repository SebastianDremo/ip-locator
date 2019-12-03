using AutoMapper;
using locator.Core.Entities;
using locator.Core.Models;
using locator.Infrastructure.Repositories.Interfaces;
using locator.Web.Controllers;
using locator.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace locator.Test.UnitTests
{
    [TestFixture]
    public class LocalizationControllerTest
    {
        private readonly LocalizationController controller;
        private readonly Mock<ILocalizationRepository> mockLocalizationRepository;
        private readonly Mock<IMapper> mockMapper;
        private readonly Mock<IIpStackService> mockIpStackService;

        public LocalizationControllerTest()
        {
            mockLocalizationRepository = new Mock<ILocalizationRepository>(MockBehavior.Loose);
            mockMapper = new Mock<IMapper>(MockBehavior.Loose);
            mockIpStackService = new Mock<IIpStackService>(MockBehavior.Loose);
            controller = new LocalizationController(mockLocalizationRepository.Object, mockMapper.Object, mockIpStackService.Object);
        }


        [TestCase("153.02.12.81")]
        public void LocateIp_Always_ReturnsModelObject(string ip)
        {
            var expectedLocalization = new Localization
            {
                Ip = ip
            };

            var expectedLocalizationModel = new LocalizationModel
            {
                Ip = expectedLocalization.Ip
            };

            mockIpStackService.Setup(service => service.GetLocalizationByIpAsync(ip)).ReturnsAsync(expectedLocalization);
            mockLocalizationRepository.Setup(repository => repository.CreateAsync(expectedLocalization)).ReturnsAsync(true);
            mockMapper.Setup(mapper => mapper.Map<LocalizationModel>(expectedLocalization)).Returns(expectedLocalizationModel);

            var result = controller.LocateIp(ip).Result;

            Assert.IsTrue(result.Ip.Equals(expectedLocalization.Ip));
        }
        [Test]
        public void AllLocalizations_WhenNoLocalizationsInDb_ReturnsNotFound()
        {
            mockLocalizationRepository.Setup(repository => repository.GetAllAsync()).ReturnsAsync(new List<Localization>());

            var result = controller.AllLocalizations().Result;

            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [TestCase("182.81.10.19")]
        public void RemoveLocation_Always_ReturnsNotFound(string ip)
        {
            mockLocalizationRepository.Setup(repository => repository.RemoveByIpAsync(ip, false)).ReturnsAsync(false);

            var result = controller.RemoveLocation(ip).Result;
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [TestCase("182.81.10.19")]
        public void RemoveLocation_Always_ReturnsAcceptedStatusCode(string ip)
        {
            mockLocalizationRepository.Setup(repository => repository.RemoveByIpAsync(ip, false)).ReturnsAsync(true);

            var result = controller.RemoveLocation(ip).Result;
            Assert.AreEqual((int)HttpStatusCode.Accepted, (result as StatusCodeResult).StatusCode);
        }
    }
}
