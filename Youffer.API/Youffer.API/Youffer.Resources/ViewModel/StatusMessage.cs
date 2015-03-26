// ---------------------------------------------------------------------------------------------------
// <copyright file="StatusMessage.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-12-10</date>
// <summary>
//     The StatusMessage class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.ViewModel
{
    /// <summary>
    /// The status message Class
    /// </summary>
    public class StatusMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StatusMessage" /> class.
        /// </summary>
        public StatusMessage()
        {
            this.ErrorMessage = string.Empty;
        }

        /// <summary>
        /// Gets or sets a value indicating whether is sucess or not
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Gets or sets the error message
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
