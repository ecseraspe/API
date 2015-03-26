// ---------------------------------------------------------------------------------------------------
// <copyright file="UserReviewsDto.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2014-12-1</date>
// <summary>
//     The UserReviewsDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.ViewModel
{
    using System;

    /// <summary>
    /// Class UserReviewsDto.
    /// </summary>
    public class UserReviewsDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserReviewsDto"/> class.
        /// </summary>
        public UserReviewsDto()
        {
            this.IsDeleted = false;
            this.CreatedOn = DateTime.UtcNow;
            this.ModifiedOn = DateTime.UtcNow;
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the company identifier.
        /// </summary>
        /// <value>The company identifier.</value>
        public string CompanyId { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the rating.
        /// </summary>
        /// <value>The rating.</value>
        public decimal Rating { get; set; }

        /// <summary>
        /// Gets or sets the feedback.
        /// </summary>
        /// <value>The feedback.</value>
        public string Feedback { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>The created by.</value>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the modified by.
        /// </summary>
        /// <value>The modified by.</value>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the Modified date.
        /// </summary>
        public DateTime ModifiedOn { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is deleted.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Interest Name
        /// </summary>
        public string InterestName { get; set; }

        /// <summary>
        /// Gets or sets the Company Image Url
        /// </summary>
        public string CompanyImageUrl { get; set; }
    }
}
