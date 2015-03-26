// ---------------------------------------------------------------------------------------------------
// <copyright file="SendEnterPhoneAndNeedsMessageDto.cs" company="Youffer">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2015-01-28</date>
// <summary>
//     The SendEnterPhoneAndNeedsMessageDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.MySqlDbSchema
{
    /// <summary>
    /// Class SendEnterPhoneAndNeedsMessageDto
    /// </summary>
    public class SendEnterPhoneAndNeedsMessageDto
    {
        /// <summary>
        /// Gets or sets the contactid.
        /// </summary>
        public int contactId { get; set; }

        /// <summary>
        /// Gets or sets the GCM identifier.
        /// </summary>
        public string GCMId { get; set; }

        /// <summary>
        /// Gets or sets the ud identifier.
        /// </summary>
        public string UDId { get; set; }
    }
}
