// ---------------------------------------------------------------------------------------------------
// <copyright file="CRMContactUs.cs" company="Youffer">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2015-1-16</date>
// <summary>
//     The CRMContactUs class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.CRMModel
{
    using System;
    using Youffer.Resources.Enum;

    /// <summary>
    /// Class CRMContactUs
    /// </summary>
    public class CRMContactUs : BaseCrmModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CRMContactUs" /> class.
        /// </summary>
        public CRMContactUs()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.ModifiedOn = DateTime.UtcNow;
        }

        /// <summary>
        /// Gets or sets the contact us no.
        /// </summary>
        public string ContactUsNo { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the dept.
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// Gets or sets the thread identifier.
        /// </summary>
        public long ThreadId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is incoming message.
        /// </summary>
        public bool IsIncomingMessage { get; set; }

        /// <summary>
        /// Gets or sets the created on
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the Modified on
        /// </summary>
        public DateTime ModifiedOn { get; set; }
    }
}
