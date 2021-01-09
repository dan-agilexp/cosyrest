// // <copyright file="SystemRole.cs" company="">
// // Copyright (c) Daniel Cook. All rights reserved.
// // </copyright>
namespace GeoCosy.ResponseModels
{
    using System.Collections.Generic;

    /// <summary>
    /// The system role.
    /// </summary>
    public class SystemRole
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the system id.
        /// </summary>
        public string SystemId { get; set; }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        public List<string> Roles { get; set; }
    }
}