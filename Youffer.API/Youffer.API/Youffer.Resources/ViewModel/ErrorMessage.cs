// ---------------------------------------------------------------------------------------------------
// <copyright file="ErrorMessage.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Akanksha VAts</author>
// <date>2014-12-16</date>
// <summary>
//     The ErrorMessage class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.ViewModel
{
    using System.Collections.Generic;

    /// <summary>
    /// Class ErrorMessage
    /// </summary>
    public class ErrorMessage
    {
        /// <summary>
        /// Gets or sets the error MSG.
        /// </summary>
        /// <value>
        /// The error MSG.
        /// </value>
        public string ErrorMsg { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is error.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is error; otherwise, <c>false</c>.
        /// </value>
        public bool IsError
        {
            get
            {
                if (this.ErrorMsg != null && this.ErrorMsg.Length > 0)
                {
                    return true;
                }

                return false;
            }
        }
    }
}
