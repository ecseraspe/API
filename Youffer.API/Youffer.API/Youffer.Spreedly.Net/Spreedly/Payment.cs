// ---------------------------------------------------------------------------------------------------
// <copyright file="Payment.cs" company="Rekurant">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2014-08-23</date>
// <summary>
//     The Payment class
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
    /// The payment.
    /// </summary>
    public class Payment
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
        /// The payment type.
        /// </summary>
        private readonly string paymentType;

        /// <summary>
        /// The storage type.
        /// </summary>
        private readonly string storageType;

        /// <summary>
        /// The token.
        /// </summary>
        private readonly string token;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Payment" /> class.
        /// </summary>
        /// <param name="node">The node.</param>
        public Payment(XElement node)
        {
            this.token = node.GetStringChild("token");

            this.paymentType = node.GetStringChild("payment_method_type");

            this.storageType = node.GetStringChild("storage_state");

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
        /// Gets the payment type.
        /// </summary>
        public string PaymentType
        {
            get
            {
                return this.paymentType;
            }
        }

        /// <summary>
        /// Gets the storage type.
        /// </summary>
        public string StorageType
        {
            get
            {
                return this.storageType;
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

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The from xml.
        /// </summary>
        /// <param name="doc">The doc.</param>
        /// <returns>
        /// The <see cref="Payment" />.
        /// </returns>
        public static IEnumerable<Payment> FromXml(XDocument doc)
        {
            return doc.Descendants("payment_method").Select(node => new Payment(node)).ToList();
        }

        #endregion
    }
}