// // <copyright file="IGeoCosyClient.cs" company="">
// // Copyright (c) Daniel Cook. All rights reserved.
// // </copyright>
namespace GeoCosy
{
    using System.Net.Http;
    using System.Threading.Tasks;

    using GeoCosy.Enums;
    using GeoCosy.ResponseModels;

    public interface IGeoCosyClient
    {
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
        Task<ClientResponse<CosyLoginResponse>> LoginAsync(string username, string password);

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
        Task<ClientResponse<CosyDeviceResponse>> GetSystems(string token, bool getPeripherals);

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
        Task<ClientResponse<bool>> SetMode(string token, string deviceId, CosyMode mode, int duration = 60);

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
        Task<ClientResponse<bool>> Hibernate(string token, int zone = 0);
    }
}