// // <copyright file="CosyDeviceResponse.cs" company="">
// // Copyright (c) Daniel Cook. All rights reserved.
// // </copyright>
namespace GeoCosy.ResponseModels
{
    using System.Collections.Generic;

    /// <summary>
    /// The cosy device response.
    /// </summary>
    public class CosyDeviceResponse
    {
        /// <summary>
        /// Gets or sets the system roles.
        /// </summary>
        public List<SystemRole> SystemRoles { get; set; }

        /// <summary>
        /// Gets or sets the system details.
        /// </summary>
        public List<SystemDetail> SystemDetails { get; set; }
    }
}