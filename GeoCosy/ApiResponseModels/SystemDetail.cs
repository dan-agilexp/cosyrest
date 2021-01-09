// // <copyright file="SystemDetail.cs" company="">
// // Copyright (c) Daniel Cook. All rights reserved.
// // </copyright>
namespace GeoCosy.ResponseModels
{
    using System.Collections.Generic;

    /// <summary>
    /// The system detail.
    /// </summary>
    public class SystemDetail
    {
        /// <summary>
        /// Gets or sets the devices.
        /// </summary>
        public List<Device> Devices { get; set; }

        /// <summary>
        /// Gets or sets the system id.
        /// </summary>
        public string SystemId { get; set; }
    }
}