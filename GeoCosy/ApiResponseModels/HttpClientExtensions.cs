// // <copyright file="HttpClientExtensions.cs" company="Dan Cook">
// // Copyright (c) Daniel Cook. All rights reserved.
// // </copyright>
namespace GeoCosy.ResponseModels
{
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    /// <summary>
    /// The http client extensions.
    /// </summary>
    public static class HttpClientExtensions
    {
        /// <summary>
        /// The get as.
        /// </summary>
        /// <param name="client">
        /// The client.
        /// </param>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public static async Task<T> GetAs<T>(this HttpClient client, string url) where T : class
        {
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<T>();
        }

        /// <summary>
        /// The put.
        /// </summary>
        /// <param name="client">
        /// The client.
        /// </param>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public static Task<HttpResponseMessage> Put<T>(this HttpClient client, string url, T model)
        {
            return client.PutAsync(url, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));
        }

        /// <summary>
        /// The put as.
        /// </summary>
        /// <param name="client">
        /// The client.
        /// </param>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <typeparam name="TReturnType">
        /// </typeparam>
        /// <typeparam name="TInputType">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public static async Task<TReturnType> PutAs<TReturnType, TInputType>(this HttpClient client, string url, TInputType model)
        {
            var response = await client.PutAsync(url, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<TReturnType>();
        }

        /// <summary>
        /// The post as.
        /// </summary>
        /// <param name="client">
        /// The client.
        /// </param>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <typeparam name="TReturnType">
        /// </typeparam>
        /// <typeparam name="TInputType">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public static async Task<TReturnType> PostAs<TReturnType, TInputType>(this HttpClient client, string url, TInputType model)
        {
            var response = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));
            var content = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<TReturnType>();
        }

        /// <summary>
        /// The post.
        /// </summary>
        /// <param name="client">
        /// The client.
        /// </param>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public static Task<HttpResponseMessage> Post<T>(this HttpClient client, string url, T model)
        {
            return client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));
        }

        /// <summary>
        /// The get as.
        /// </summary>
        /// <param name="client">
        /// The client.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public static async Task<T> GetAs<T>(this HttpClient client, HttpRequestMessage message)
        {
            var response = await client.SendAsync(message);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<T>();
        }
    }
}