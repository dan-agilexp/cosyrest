// // <copyright file="Device.cs" company="">
// // Copyright (c) Daniel Cook. All rights reserved.
// // </copyright>
namespace GeoCosy.ResponseModels
{
    /// <summary>
    /// The device.
    /// </summary>
    public class Device
    {
        /// <summary>
        /// Gets or sets the device type.
        /// </summary>
        public string DeviceType { get; set; }

        /// <summary>
        /// Gets or sets the sensor type.
        /// </summary>
        public int SensorType { get; set; }

        /// <summary>
        /// Gets or sets the node id.
        /// </summary>
        public int NodeId { get; set; }

        /// <summary>
        /// Gets or sets the version number.
        /// </summary>
        public VersionNumber VersionNumber { get; set; }

        /// <summary>
        /// Gets or sets the paired timestamp.
        /// </summary>
        public int PairedTimestamp { get; set; }

        /// <summary>
        /// Gets or sets the pairing code.
        /// </summary>
        public string PairingCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether upgrade required.
        /// </summary>
        public bool UpgradeRequired { get; set; }
    }
}