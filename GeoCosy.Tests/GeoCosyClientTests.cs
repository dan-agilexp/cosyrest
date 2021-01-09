// // <copyright file="GeoCosyClientTests.cs" company="TopEats">
// // Copyright (c) Daniel Cook. All rights reserved.
// // </copyright>
namespace GeoCosy.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    using GeoCosy.Enums;
    using GeoCosy.ResponseModels;

    using Moq;
    using Moq.Protected;

    using Newtonsoft.Json;

    using NUnit.Framework;

    using Shouldly;

    public class GeoCosyClientTests
    {
        /// <summary>
        /// The setup.
        /// </summary>
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// The login aync_ with valid credentials_ should return token.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Test]
        public async Task LoginAync_WithValidCredentials_ShouldReturnToken()
        {
            // arrange
            var mockFactory = new Mock<IHttpClientFactory>();
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            var mockContentResponse = new CosyLoginResponse() { Token = "xyz"};
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()).ReturnsAsync(
                    new HttpResponseMessage
                        {
                            StatusCode = HttpStatusCode.OK,
                            Content = new StringContent(JsonConvert.SerializeObject(mockContentResponse), Encoding.UTF8, "application/json"),
                        });


            var client = new HttpClient(mockHttpMessageHandler.Object);
            
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            var geoCosyClient = new GeoCosyClient(mockFactory.Object);

            // act
            var response = await geoCosyClient.LoginAsync("username", "password");

            // assert
            response.Data.ShouldNotBeNull();
            response.Data.Token.ShouldBe("xyz");
        }

        /// <summary>
        /// The login_ with invalid credentials_ returns error response.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Test]
        public async Task Login_WithInvalidCredentials_ReturnsErrorResponse()
        {
            // arrange
            var mockFactory = new Mock<IHttpClientFactory>();
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()).ReturnsAsync(
                    new HttpResponseMessage
                        {
                            StatusCode = HttpStatusCode.Unauthorized
                        });


            var client = new HttpClient(mockHttpMessageHandler.Object);

            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            var geoCosyClient = new GeoCosyClient(mockFactory.Object);

            // act
            var response = await geoCosyClient.LoginAsync("username", "password");

            // assert
            response.Data.ShouldBeNull();
            response.Errors.ShouldContain(x => x.ErrorCode == ErrorCode.AuthenticationFailed);
        }

        /// <summary>
        /// The login_ with some other exception_ throws the exception.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Test]
        public async Task Login_WithSomeOtherException_ThrowsTheException()
        {
            // arrange
            var mockFactory = new Mock<IHttpClientFactory>();
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()).ReturnsAsync(
                    new HttpResponseMessage
                        {
                            StatusCode = HttpStatusCode.BadGateway
                        });


            var client = new HttpClient(mockHttpMessageHandler.Object);

            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            var geoCosyClient = new GeoCosyClient(mockFactory.Object);

            // act / assert
            Should.Throw<Exception>(async () => await geoCosyClient.LoginAsync("username", "password"));
        }

        /// <summary>
        /// The get systems_ with invalid token_ returns error response.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Test]
        public async Task GetSystems_WithInvalidToken_ReturnsErrorResponse()
        {
            // arrange
            var mockFactory = new Mock<IHttpClientFactory>();
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()).ReturnsAsync(
                    new HttpResponseMessage
                        {
                            StatusCode = HttpStatusCode.Forbidden
                        });


            var client = new HttpClient(mockHttpMessageHandler.Object);

            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            var geoCosyClient = new GeoCosyClient(mockFactory.Object);

            // act
            var response = await geoCosyClient.GetSystems("incorrect-token", false);

            // assert
            response.Data.ShouldBeNull();
            response.Errors.ShouldContain(x => x.ErrorCode == ErrorCode.NotAllowed);
        }

        /// <summary>
        /// The get systems_ with valid response_ returns systems.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Test]
        public async Task GetSystems_WithValidResponse_ReturnsSystems()
        {
            // arrange
            var mockFactory = new Mock<IHttpClientFactory>();
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            var mockContentResponse = GetFakeCosyDeviceReponse();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()).ReturnsAsync(
                    new HttpResponseMessage
                        {
                            StatusCode = HttpStatusCode.OK,
                            Content = new StringContent(JsonConvert.SerializeObject(mockContentResponse), Encoding.UTF8, "application/json"),
                    });


            var client = new HttpClient(mockHttpMessageHandler.Object);

            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            var geoCosyClient = new GeoCosyClient(mockFactory.Object);

            // act
            var response = await geoCosyClient.GetSystems("incorrect-token", false);

            // assert
            response.Data.ShouldNotBeNull();
            response.Data.SystemDetails[0].ShouldBeEquivalentTo(mockContentResponse.SystemDetails[0]);
            response.Data.SystemRoles[0].ShouldBeEquivalentTo(mockContentResponse.SystemRoles[0]);
        }

        /// <summary>
        /// The get systems_ with valid response_ returns systems.
        /// </summary>
        /// <param name="mode">
        /// The mode.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [TestCase(CosyMode.Cosy)]
        [TestCase(CosyMode.Comfy)]
        [TestCase(CosyMode.Hibernate)]
        public async Task SetMode_WithValidParametersParameters_SetsMode(CosyMode mode)
        {
            // arrange
            var mockFactory = new Mock<IHttpClientFactory>();
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            var mockContentResponse = GetFakeCosyDeviceReponse();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()).ReturnsAsync(
                    new HttpResponseMessage
                        {
                            StatusCode = HttpStatusCode.OK,
                            Content = new StringContent(
                                JsonConvert.SerializeObject(mockContentResponse),
                                Encoding.UTF8,
                                "application/json"),
                        });


            var client = new HttpClient(mockHttpMessageHandler.Object);

            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            var geoCosyClient = new GeoCosyClient(mockFactory.Object);

            // act
            var response = await geoCosyClient.SetMode("token", "device-id", mode, 60);

            // assert
            response.Data.ShouldBe(true);
        }

        /// <summary>
        /// The get fake cosy device reponse.
        /// </summary>
        /// <returns>
        /// The <see cref="CosyDeviceResponse"/>.
        /// </returns>
        private static CosyDeviceResponse GetFakeCosyDeviceReponse()
        {
            return new CosyDeviceResponse()
                       {
                            SystemRoles = new List<SystemRole>()
                                              {
                                                  new SystemRole()
                                                      {
                                                          Name = "testrole",
                                                          Roles = new List<string>()
                                                                      {
                                                                          "x", "y", "z"
                                                                      },
                                                          SystemId = "test-system-id"
                                                      }
                                              },
                           SystemDetails = new List<SystemDetail>()
                                               {
                                                   new SystemDetail()
                                                       {
                                                           Devices = new List<Device>()
                                                                         {
                                                                             new Device()
                                                                                 {
                                                                                     DeviceType = "device-type",
                                                                                     NodeId = 99,
                                                                                     PairedTimestamp = 99,
                                                                                     PairingCode = "pairingcode",
                                                                                     SensorType = 99,
                                                                                     UpgradeRequired = false,
                                                                                     VersionNumber = new VersionNumber()
                                                                                                         {
                                                                                                             Major = 99,
                                                                                                             Minor = 98
                                                                                                         }

                                                                                 }
                                                                         }
                                                       }
                                               }
                       };
        }
    }
}