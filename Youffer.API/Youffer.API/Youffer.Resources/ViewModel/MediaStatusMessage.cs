// ---------------------------------------------------------------------------------------------------
// <copyright file="MediaStatusMessage.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-12-19</date>
// <summary>
//     The MediaStatusMessage class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.ViewModel
{
    /// <summary>
    /// The MediaStatusMessage class
    /// </summary>
    public class MediaStatusMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediaStatusMessage"/> class.
        /// </summary>
        public MediaStatusMessage()
        {
            this.ErrorMessage = string.Empty;
        }

        /// <summary>
        /// Gets or sets a value indicating whether is sucess or not
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Gets or sets the Media Url
        /// </summary>
        public string MediaUrl { get; set; }

        /// <summary>
        /// Gets or sets the Error Message
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
