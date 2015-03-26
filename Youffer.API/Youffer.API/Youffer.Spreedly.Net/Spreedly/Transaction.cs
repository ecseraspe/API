// ---------------------------------------------------------------------------------------------------
// <copyright file="Transaction.cs" company="Rekurant">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2014-08-23</date>
// <summary>
//     The Transaction class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Rekurant.Spreedly.Net.Spreedly
{
    using System;
    using System.Globalization;
    using System.Xml.Linq;

    using Rekurant.Spreedly.Net.Extensions;

    /// <summary>
    /// The transaction.
    /// </summary>
    public class Transaction
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Transaction" /> class.
        /// </summary>
        /// <param name="wasTest">The was test.</param>
        /// <param name="errors">The errors.</param>
        public Transaction(bool wasTest, TransactionErrors errors)
        {
            this.Succeeded = false;
            this.WasTest = wasTest;
            this.Errors = errors;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Transaction" /> class.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <param name="wasTest">The was test.</param>
        /// <param name="succeeded">The succeeded.</param>
        /// <param name="token">The token.</param>
        /// <param name="obfuscatedNumber">The obfuscated number.</param>
        /// <param name="errors">The errors.</param>
        internal Transaction(string amount, string wasTest, string succeeded, string token, string obfuscatedNumber, TransactionErrors errors)
        {
            this.Amount = decimal.Parse(string.IsNullOrWhiteSpace(amount) ? "0" : amount, CultureInfo.InvariantCulture);
            this.WasTest = string.Equals(wasTest, "true", StringComparison.InvariantCultureIgnoreCase);
            this.Succeeded = string.Equals(succeeded, "true", StringComparison.InvariantCultureIgnoreCase);
            this.Token = token;
            this.ObfuscatedNumber = obfuscatedNumber;
            this.Errors = errors;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the amount.
        /// </summary>
        public decimal Amount { get; private set; }

        /// <summary>
        /// Gets the errors.
        /// </summary>
        public TransactionErrors Errors { get; private set; }

        /// <summary>
        /// Gets the obfuscated number.
        /// </summary>
        public string ObfuscatedNumber { get; private set; }

        /// <summary>
        /// Gets a value indicating whether succeeded.
        /// </summary>
        public bool Succeeded { get; private set; }

        /// <summary>
        /// Gets the token.
        /// </summary>
        public string Token { get; private set; }

        /// <summary>
        /// Gets a value indicating whether was test.
        /// </summary>
        public bool WasTest { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The from xml.
        /// </summary>
        /// <param name="doc">The doc.</param>
        /// <returns>
        /// The <see cref="Transaction" />.
        /// </returns>
        public static Transaction FromXml(XDocument doc)
        {
            XElement tran = doc.Element("transaction");
            if (tran == null)
            {
                return null;
            }

            var ret = new Transaction(tran.GetStringChild("amount"), tran.GetStringChild("on_test_gateway"), tran.GetStringChild("succeeded"), tran.GetStringChild("token"), tran.Element("payment_method").GetStringChild("number"), new TransactionErrors(tran));
            if (ret.Succeeded == false && ret.Errors.Count == 0)
            {
                if (string.Equals(tran.GetStringChild("state"), "gateway_processing_failed", StringComparison.InvariantCultureIgnoreCase))
                {
                    ret.Errors = new TransactionErrors(string.Empty, TransactionErrorType.Unknown);
                }
            }

            return ret;
        }

        #endregion
    }
}