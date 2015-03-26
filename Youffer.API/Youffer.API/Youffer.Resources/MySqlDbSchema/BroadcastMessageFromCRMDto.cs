// ---------------------------------------------------------------------------------------------------
// <copyright file="BroadcastMessageFromCRMDto.cs" company="Youffer">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2015-02-03</date>
// <summary>
//     The BroadcastMessageFromCRMDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.MySqlDbSchema
{
    /// <summary>
    /// Class BroadcastMessageFromCRMDto
    /// </summary>
    public class BroadcastMessageFromCRMDto
    {
        /// <summary>
        /// Gets or sets the record identifier.
        /// </summary>
        /// <value>
        /// The record identifier.
        /// </value>
        public int RecId { get; set; }

        /// <summary>
        /// Gets or sets the send to.
        /// </summary>
        /// <value>
        /// The send to.
        /// </value>
        public string SendTo { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the GCM identifier.
        /// </summary>
        /// <value>
        /// The GCM identifier.
        /// </value>
        public string GCMId { get; set; }

        /// <summary>
        /// Gets or sets the ud identifier.
        /// </summary>
        /// <value>
        /// The ud identifier.
        /// </value>
        public string UDId { get; set; }
    }
}
