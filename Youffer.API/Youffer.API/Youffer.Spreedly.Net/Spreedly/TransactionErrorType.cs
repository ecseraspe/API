// ---------------------------------------------------------------------------------------------------
// <copyright file="TransactionErrorType.cs" company="Rekurant">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2014-08-23</date>
// <summary>
//     The TransactionErrorType class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Rekurant.Spreedly.Net.Spreedly
{
    /// <summary>
    /// The transaction error type.
    /// </summary>
    public enum TransactionErrorType
    {
        /// <summary>
        /// The unknown.
        /// </summary>
        Unknown,

        /// <summary>
        /// The blank.
        /// </summary>
        Blank,

        /// <summary>
        /// The invalid.
        /// </summary>
        Invalid,

        /// <summary>
        /// The expired.
        /// </summary>
        Expired,

        /// <summary>
        /// The invalid gateway.
        /// </summary>
        InvalidGateway,

        /// <summary>
        /// The call failed.
        /// </summary>
        CallFailed
    }
}
