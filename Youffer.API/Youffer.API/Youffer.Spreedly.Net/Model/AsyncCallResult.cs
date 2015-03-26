// ---------------------------------------------------------------------------------------------------
// <copyright file="AsyncCallResult.cs" company="Rekurant">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2014-08-18</date>
// <summary>
//     The AsyncCallResult class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Rekurant.Spreedly.Net.Model
{
    using Rekurant.Spreedly.Net.Enum;

    /// <summary>
    /// The AsyncCallResult class
    /// </summary>
    /// <typeparam name="T">Type of the object</typeparam>
    public class AsyncCallResult<T> where T : class
    {
        /// <summary>
        /// The asynchronous call failure reason
        /// </summary>
        private readonly AsyncCallFailureReason asyncCallFailureReason;

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncCallResult{T}"/> class.
        /// </summary>
        /// <param name="asyncCallFailureReason">The asynchronous call failure reason_.</param>
        /// <param name="result">The result.</param>
        public AsyncCallResult(AsyncCallFailureReason asyncCallFailureReason, T result = null)
        {
            this.asyncCallFailureReason = asyncCallFailureReason;
            this.Contents = result;
        }

        /// <summary>
        /// Gets the contents.
        /// </summary>
        /// <value>
        /// The contents.
        /// </value>
        public T Contents { get; private set; }

        /// <summary>
        /// Gets the failure reason.
        /// </summary>
        /// <value>
        /// The failure reason.
        /// </value>
        public AsyncCallFailureReason FailureReason
        {
            get { return this.asyncCallFailureReason; }
        }
    }
}