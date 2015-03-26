// ---------------------------------------------------------------------------------------------------
// <copyright file="LeadOpportunityMappingDto.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2014-11-28</date>
// <summary>
//     The LeadOpportunityMappingDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.ViewModel
{
    using System;

    /// <summary>
    /// Class LeadOpportunityMappingDto.
    /// </summary>
    public class LeadOpportunityMappingDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LeadOpportunityMappingDto" /> class.
        /// </summary>
        public LeadOpportunityMappingDto()
        {
            this.IsPending = true;
            this.IsApproved = false;
            this.IsPaid = false;
            this.CreatedOn = DateTime.UtcNow;
            this.ModifiedOn = DateTime.UtcNow;
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The identifier.</value>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the lead identifier.
        /// </summary>
        /// <value>
        /// The lead identifier.
        /// </value>
        public string LeadId { get; set; }

        /// <summary>
        /// Gets or sets the opportunity identifier.
        /// </summary>
        /// <value>
        /// The opportunity identifier.
        /// </value>
        public string OpportunityId { get; set; }

        /// <summary>
        /// Gets or sets the company identifier.
        /// </summary>
        /// <value>The company identifier.</value>
        public string CompanyId { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>The price.</value>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the Modified date.
        /// </summary>
        /// <value>The modified on.</value>
        public DateTime ModifiedOn { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>The created on.</value>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is pending.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is pending; otherwise, <c>false</c>.
        /// </value>
        public bool IsPending { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is approved.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is approved; otherwise, <c>false</c>.
        /// </value>
        public bool IsApproved { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is paid.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is paid; otherwise, <c>false</c>.
        /// </value>
        public bool IsPaid { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is sent for approval.
        /// </summary>
        public bool IsSentForApproval { get; set; }

        /// <summary>
        /// Gets or sets the interest.
        /// </summary>
        /// <value>
        /// The interest.
        /// </value>
        public string Interest { get; set; }

        /// <summary>
        /// Gets or sets the contact identifier.
        /// </summary>
        /// <value>
        /// The contact identifier.
        /// </value>
        public string ContactId { get; set; }
    }
}
