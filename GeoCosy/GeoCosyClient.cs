// // <copyright file="GeoCosyClient.cs" company="">
// // Copyright (c) Daniel Cook. All rights reserved.
// // </copyright>
namespace GeoCosy
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    using GeoCosy.Enums;
    using GeoCosy.RequestModels;
    using GeoCosy.ResponseModels;

    using Newtonsoft.Json;

    /// <summary>
    /// The geo cosy client.
    /// </summary>
    public class GeoCosyClient : IGeoCosyClient
    {
        /// <summary>
        /// The base url.
        /// </summary>
        private const string BaseUrl = "https://cosy.geotogether.com/api/userapi/";

        /// <summary>
        /// The http client factory.
        /// </summary>
        private readonly IHttpClientFactory httpClientFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="GeoCosyClient"/> class.
        /// </summary>
        /// <param name="httpClientFactory">
        /// The http client factory.
        /// </param>
        public GeoCosyClient(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// The login async.
        /// </summary>
        /// <param name="username">
        ///     The username.
        /// </param>
        /// <param name="password">
        ///     The password.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<ClientResponse<CosyLoginResponse>> LoginAsync(string username, string password)
        {
            var response = new ClientResponse<CosyLoginResponse>();
            try
            {
                var loginModel = new CosyLoginRequestModel(username, password);
                HttpClient client = this.httpClientFactory.CreateClient();
                var httpResponse = await client.PostAs<CosyLoginResponse, CosyLoginRequestModel>(
                                       $"{BaseUrl}account/login",
                                       loginModel);

                response.Data = httpResponse;
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == HttpStatusCode.Unauthorized)
                {
                    response.LogError(new CosyClientError(ErrorCode.AuthenticationFailed, "Invalid credentials. Could not authenticate to the Geo Cosy API", ex));
                }
                else
                {
                    throw;
                }
            }

            return response;
        }

        /// <summary>
        /// The get systems.
        /// </summary>
        /// <param name="token">
        /// The token.
        /// </param>
        /// <param name="getPeripherals">
        /// The get peripherals.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<ClientResponse<CosyDeviceResponse>> GetSystems(string token, bool getPeripherals)
        {
            var response = new ClientResponse<CosyDeviceResponse>();
            try
            {
                HttpClient client = this.httpClientFactory.CreateClient();
                var message = new HttpRequestMessage()
                                  {
                                      Headers =
                                          {
                                              { HttpRequestHeader.Authorization.ToString(), $"Bearer {token}" },
                                              { HttpRequestHeader.Accept.ToString(), "application/json" }
                                          },
                                      Method = HttpMethod.Get,
                                      RequestUri = new Uri(
                                          $"{BaseUrl}/user/detail-systems?peripherals={getPeripherals.ToString().ToLower()}")
                                  };
                response.Data = await client.GetAs<CosyDeviceResponse>(message);
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == HttpStatusCode.Forbidden)
                {
                    response.LogError(new CosyClientError(ErrorCode.NotAllowed, "Not allowed. The authenticated user is not allowed to get systems.", ex));
                }
                else
                {
                    throw;
                }
            }

            return response;
        }

        /// <summary>
        /// The set mode.
        /// </summary>
        /// <param name="token">
        /// The token.
        /// </param>
        /// <param name="deviceId">
        /// The device id.
        /// </param>
        /// <param name="mode">
        /// The mode.
        /// </param>
        /// <param name="duration">
        /// The duration.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<ClientResponse<bool>> SetMode(string token, string deviceId, CosyMode mode, int duration = 60)
        {
            var response = new ClientResponse<bool>();
            try
            {
                if (mode == CosyMode.Hibernate)
                {
                    return await this.Hibernate(token);
                }

                var setModeModel = new CosySetModeModel(mode, duration);
                HttpClient client = this.httpClientFactory.CreateClient();
                var message = new HttpRequestMessage()
                                  {
                                      Headers =
                                          {
                                              { HttpRequestHeader.Authorization.ToString(), $"Bearer {token}" },
                                              { HttpRequestHeader.Accept.ToString(), "application/json" },
                                          },
                                      Method = HttpMethod.Post,
                                      Content = new StringContent(
                                          JsonConvert.SerializeObject(setModeModel),
                                          Encoding.UTF8,
                                          "application/json"),
                                      RequestUri = new Uri(
                                          $"{BaseUrl}system/cosy-adhocmode/{deviceId}")
                                  };

                var apiResponse = await client.SendAsync(message);
                response.Data = apiResponse.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == HttpStatusCode.Forbidden)
                {
                    response.LogError(new CosyClientError(ErrorCode.NotAllowed, "Not allowed. The authenticated user is not allowed to get systems.", ex));
                }
                else
                {
                    throw;
                }
            }

            return response;
        }

        /// <summary>
        /// The hibernate.
        /// </summary>
        /// <param name="token">
        /// The token.
        /// </param>
        /// <param name="zone">
        /// The zone.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<ClientResponse<bool>> Hibernate(string token, int zone = 0)
        {
            var response = new ClientResponse<bool>();
            try
            {
                HttpClient client = this.httpClientFactory.CreateClient();
                var message = new HttpRequestMessage()
                                  {
                                      Headers =
                                          {
                                              { HttpRequestHeader.Authorization.ToString(), $"Bearer {token}" },
                                              { HttpRequestHeader.Accept.ToString(), "application/json" },
                                          },
                                      Method = HttpMethod.Delete,
                                      RequestUri = new Uri(
                                          $"{BaseUrl}system/cosy-cancelallevents/8c452779-128d-4aed-9eb3-6da2238732bf?zone={zone}")
                                  };

                var apiResponse = await client.SendAsync(message);
                response.Data = apiResponse.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == HttpStatusCode.Forbidden)
                {
                    response.LogError(new CosyClientError(ErrorCode.NotAllowed, "Not allowed. The authenticated user is not allowed to get systems.", ex));
                }
                else
                {
                    throw;
                }
            }

            return response;
        }
    }
}
