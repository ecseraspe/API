// ---------------------------------------------------------------------------------------------------
// <copyright file="Gateway.cs" company="Rekurant">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2014-08-23</date>
// <summary>
//     The Gateway class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Rekurant.Spreedly.Net.Spreedly
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    using Rekurant.Spreedly.Net.Extensions;

    /// <summary>
    /// The gateway.
    /// </summary>
    public class Gateway
    {
        #region Fields

        /// <summary>
        /// The enabled.
        /// </summary>
        private readonly bool enabled;

        /// <summary>
        /// The error.
        /// </summary>
        private readonly string error;

        /// <summary>
        /// The token.
        /// </summary>
        private readonly string token;

        /// <summary>
        /// The type.
        /// </summary>
        private readonly string type;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Gateway" /> class.
        /// </summary>
        /// <param name="node">The node.</param>
        public Gateway(XElement node)
        {
            this.token = node.GetStringChild("token");

            this.type = node.GetStringChild("gateway_type");

            this.error = node.GetStringChild("errors");

            string redacted = node.GetStringChild("redacted");
            this.enabled = redacted != null && string.Equals(redacted, "false", StringComparison.InvariantCultureIgnoreCase);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether enabled.
        /// </summary>
        public bool Enabled
        {
            get
            {
                return this.enabled;
            }
        }

        /// <summary>
        /// Gets the error.
        /// </summary>
        public string Error
        {
            get
            {
                return this.error;
            }
        }

        /// <summary>
        /// Gets the token.
        /// </summary>
        public string Token
        {
            get
            {
                return this.token;
            }
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        public string Type
        {
            get
            {
                return this.type;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The from xml.
        /// </summary>
        /// <param name="doc">The doc.</param>
        /// <returns>
        /// The <see cref="Gateway" />.
        /// </returns>
        public static IEnumerable<Gateway> FromXml(XDocument doc)
        {
            return doc.Descendants("gateway").Select(node => new Gateway(node)).ToList();
        }

        #endregion
    }
}