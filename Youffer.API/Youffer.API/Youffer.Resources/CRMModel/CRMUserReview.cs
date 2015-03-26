// ---------------------------------------------------------------------------------------------------
// <copyright file="CRMUserReview.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-12-18</date>
// <summary>
//     The CRMUserReview class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.CRMModel
{
    using System;

    /// <summary>
    /// The CRM USer Review class
    /// </summary>
    public class CRMUserReview : BaseCrmModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CRMUserReview" /> class.
        /// </summary>
        public CRMUserReview()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.ModifiedOn = DateTime.UtcNow;
        }

        /// <summary>
        /// Gets or sets the user review no
        /// </summary>
        public string UserReviewsNo { get; set; }

        /// <summary>
        /// Gets or sets the feedback text
        /// </summary>
        public string FeedbackText { get; set; }

        /// <summary>
        /// Gets or sets the contact id
        /// </summary>
        public string ContactId { get; set; }

        /// <summary>
        /// Gets or sets the organisationId
        /// </summary>
        public string OrganisationsId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the isDeleted
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the interestName
        /// </summary>
        public string InterestName { get; set; }

        /// <summary>
        /// Gets or sets the rank
        /// </summary>
        public decimal Rank { get; set; }

        /// <summary>
        /// Gets or sets the rating
        /// </summary>
        public decimal Rating { get; set; }

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
