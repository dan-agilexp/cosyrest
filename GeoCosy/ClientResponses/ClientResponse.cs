// // <copyright file="ClientResponse.cs" company="">
// // Copyright (c) Daniel Cook. All rights reserved.
// // </copyright>
namespace GeoCosy.ResponseModels
{
    using System.Collections.Generic;

    /// <summary>
    /// The client response.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class ClientResponse<T>
    {
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is success.
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Gets or sets the errors.
        /// </summary>
        public List<CosyClientError> Errors { get; set; } = new List<CosyClientError>();

        /// <summary>
        /// The log error.
        /// </summary>
        /// <param name="error">
        /// The error.
        /// </param>
        public void LogError(CosyClientError error)
        {
            this.Errors.Add(error);
        }
    }
}
