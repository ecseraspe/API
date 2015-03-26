// ---------------------------------------------------------------------------------------------------
// <copyright file="SmsVerification.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-12-27</date>
// <summary>
//     The SmsVerification class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.DataService.DBSchema
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// THe Sms Verification class
    /// </summary>
    [Table("SmsVerification")]
    public class SmsVerification
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SmsVerification"/> class.
        /// </summary>
        public SmsVerification()
        {
            this.IsActive = true;
            this.CreatedOn = DateTime.UtcNow;
            this.ModifiedOn = DateTime.UtcNow;
        }

        /// <summary>
        /// Gets the repository Id.
        /// </summary>
        [NotMapped]
        public object RepositoryId
        {
            get
            {
                return this.Id;
            }
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the User Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the Phone number
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the message status
        /// </summary>
        public int MessageStatus { get; set; }

        /// <summary>
        /// Gets or sets the message Sid
        /// </summary>
        public string MessageSid { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is Active or not
        /// </summary>
        public bool IsActive { get; set; }

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
