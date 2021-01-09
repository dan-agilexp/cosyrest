// // <copyright file="CosySetModeModel.cs" company="">
// // Copyright (c) Daniel Cook. All rights reserved.
// // </copyright>
namespace GeoCosy.RequestModels
{
    using GeoCosy.Enums;

    using Newtonsoft.Json;

    /// <summary>
    /// The cosy set mode model.
    /// </summary>
    public class CosySetModeModel
    {
        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        [JsonProperty("duration")]
        public int Duration { get; set; } = 60;

        /// <summary>
        /// Gets or sets the mode.
        /// </summary>
        [JsonProperty("modeId")]
        public int Mode { get; set; }

        /// <summary>
        /// Gets or sets the start off set.
        /// </summary>
        [JsonProperty("startOffset")]
        public int StartOffSet { get; set; } = 0;

        /// <summary>
        /// Gets or sets a value indicating whether welcome home active.
        /// </summary>
        [JsonProperty("welcomeHomeActive")]
        public bool WelcomeHomeActive { get; set; } = false;

        /// <summary>
        /// Gets or sets the zone.
        /// </summary>
        [JsonProperty("zone")]
        public string Zone { get; set; } = "0";

        /// <summary>
        /// Initializes a new instance of the <see cref="CosySetModeModel"/> class.
        /// </summary>
        /// <param name="mode">
        /// The mode.
        /// </param>
        public CosySetModeModel(CosyMode mode, int duration = 60)
        {
            this.Mode = (int)mode;
            this.Duration = duration;
        }
    }
}