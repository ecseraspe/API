// ---------------------------------------------------------------------------------------------------
// <copyright file="AsyncCallFailureReason.cs" company="Rekurant">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2014-08-18</date>
// <summary>
//     The AsyncCallFailureReason enum
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Rekurant.Spreedly.Net.Enum
{
    /// <summary>
    /// The AsyncCallFailureReason enumerations
    /// </summary>
    public enum AsyncCallFailureReason
    {
        /// <summary>
        /// The none
        /// </summary>
        None,

        /// <summary>
        /// The time out
        /// </summary>
        TimeOut,

        /// <summary>
        /// The results not found
        /// </summary>
        ResultsNotFound,

        /// <summary>
        /// The failed status code
        /// </summary>
        FailedStatusCode,

        /// <summary>
        /// The failed connection
        /// </summary>
        FailedConnection
    }
}
