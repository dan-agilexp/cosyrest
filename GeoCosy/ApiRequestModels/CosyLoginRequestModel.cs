// // <copyright file="CosyLoginRequestModel.cs" company="">
// // Copyright (c) Daniel Cook. All rights reserved.
// // </copyright>
namespace GeoCosy.RequestModels
{
    using Newtonsoft.Json;

    /// <summary>
    /// The cosy login request model.
    /// </summary>
    public class CosyLoginRequestModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CosyLoginRequestModel"/> class.
        /// </summary>
        /// <param name="emailAddress">
        /// The email address.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        public CosyLoginRequestModel(string emailAddress, string password)
        {
            this.EmailAddress = emailAddress;
            this.Name = emailAddress;
            this.Password = password;
        }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        [JsonProperty("emailAddress")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}