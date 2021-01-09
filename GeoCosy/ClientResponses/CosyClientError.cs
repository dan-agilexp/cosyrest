// // <copyright file="CosyClientError.cs" company="TopEats">
// // Copyright (c) Daniel Cook. All rights reserved.
// // </copyright>
namespace GeoCosy.ResponseModels
{
    using System;

    /// <summary>
    /// The cosy client error.
    /// </summary>
    public class CosyClientError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CosyClientError"/> class.
        /// </summary>
        /// <param name="errorCode">
        /// The error code.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="innerException">
        /// The inner Exception.
        /// </param>
        public CosyClientError(ErrorCode errorCode, string message, Exception innerException)
        {
            this.Exception = innerException;
            this.ErrorCode = errorCode;
            this.Message = message;
        }

        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        public ErrorCode ErrorCode { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the exception.
        /// </summary>
        public Exception Exception { get; set; }
    }
}