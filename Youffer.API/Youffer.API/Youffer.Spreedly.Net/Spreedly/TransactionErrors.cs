// ---------------------------------------------------------------------------------------------------
// <copyright file="TransactionErrors.cs" company="Rekurant">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2014-08-23</date>
// <summary>
//     The TransactionErrors class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Rekurant.Spreedly.Net.Spreedly
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    /// <summary>
    /// The transaction errors.
    /// </summary>
    public class TransactionErrors : IEnumerable
    {
        #region Fields

        /// <summary>
        /// The list.
        /// </summary>
        private readonly List<KeyValuePair<string, TransactionErrorType>> list;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionErrors" /> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="type">The type.</param>
        public TransactionErrors(string key, TransactionErrorType type)
        {
            this.list = new List<KeyValuePair<string, TransactionErrorType>>
                             {
                                 new KeyValuePair<string, TransactionErrorType>(key, type)
                             };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionErrors"/> class.
        /// </summary>
        /// <param name="tran">
        /// The transaction.
        /// </param>
        public TransactionErrors(XElement tran)
        {
            IEnumerable<XElement> eles = tran.Descendants("error");
            this.list = eles.Select(ele => new KeyValuePair<string, TransactionErrorType>(GetKey(ele), GetErrorType(ele))).ToList();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the count.
        /// </summary>
        public int Count
        {
            get
            {
                return this.list.Count;
            }
        }

        #endregion

        #region Public Indexers

        /// <summary>
        /// The this.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="TransactionErrorType"/>.
        /// </returns>
        public TransactionErrorType[] this[string key]
        {
            get
            {
                return this.list.Where(kv => string.Equals(kv.Key, key, StringComparison.InvariantCultureIgnoreCase)).Select(kv => kv.Value).ToArray();
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The get enumerator.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerator" />.
        /// </returns>
        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Methods

        /// <summary>
        /// The get error type.
        /// </summary>
        /// <param name="err">The err.</param>
        /// <returns>
        /// The <see cref="TransactionErrorType" />.
        /// </returns>
        private static TransactionErrorType GetErrorType(XElement err)
        {
            XAttribute att = err.Attribute("key");
            if (att == null)
            {
                return TransactionErrorType.Unknown;
            }

            switch (att.Value.ToLowerInvariant())
            {
                case "errors.blank":
                    return TransactionErrorType.Blank;
                case "errors.invalid":
                    return TransactionErrorType.Invalid;
                case "errors.expired":
                    return TransactionErrorType.Expired;
                default:
                    return TransactionErrorType.Unknown;
            }
        }

        /// <summary>
        /// The get key.
        /// </summary>
        /// <param name="err">The err.</param>
        /// <returns>
        /// The <see cref="string" />.
        /// </returns>
        private static string GetKey(XElement err)
        {
            XAttribute att = err.Attribute("attribute");
            return att == null ? string.Empty : att.Value;
        }

        #endregion
    }
}
