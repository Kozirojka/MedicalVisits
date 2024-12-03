using System.Net;
using MedicalVisits.Infrastructure.Services.GoogleMapsApi;
using MedicalVisits.Infrastructure.Services.Interfaces;
using MedicalVisits.Models;
using MedicalVisits.Models.Configurations;
using MedicalVisits.Models.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;

namespace Test;

public class GoogleApiTest
{
    [TestFixture]
    public class GeocodingServiceTests
    {
        private Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private HttpClient _httpClient;
        private Mock<IOptions<GoogleMapsServiceSettings>> _mockSettings;
        private GeocodingService _service;
        private const string TestApiKey = "test-api-key";

        [SetUp]
        public void Setup()
        {
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_mockHttpMessageHandler.Object);

            _mockSettings = new Mock<IOptions<GoogleMapsServiceSettings>>();
            _mockSettings.Setup(x => x.Value).Returns(new GoogleMapsServiceSettings { ApiKey = TestApiKey });

            _service = new GeocodingService(_httpClient, _mockSettings.Object);
        }
        [TearDown]
        public void TearDown()
        {
            _httpClient?.Dispose();
        }

        [Test]
        public async Task GeocodeAddressAsync_ValidAddress_ReturnsCoordinates()
        {
            // Arrange
            var address = new Address(
                city: "Золочів",
                street: "вулиця Січових Стрільців",
                building: "1",
                region: "Львівська область",
                country: "Україна",
                apartment: ""
            );

            var expectedResponse = @"{
                'status': 'OK',
                'results': [{
                    'geometry': {
                        'location': {
                            'lat': 49.80492539999999,
                            'lng': 24.8392134
                        }
                    }
                }]
            }";

            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(expectedResponse)
                });

            // Act
            var result = await _service.GeocodeAddressAsync(address);

            // Assert
            Assert.That(result.Latitude, Is.EqualTo(49.80492539999999));
            Assert.That(result.Longitude, Is.EqualTo(24.8392134));
        }
    }
}
