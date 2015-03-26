// ---------------------------------------------------------------------------------------------------
// <copyright file="OpportunityModel.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2014-11-19</date>
// <summary>
//     The OpportunityModel class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.CRMModel
{
    using System;
    using Youffer.Resources.Enum;

    /// <summary>
    /// Class OpportunityModel.
    /// </summary>
    public class OpportunityModel : BaseCrmModel
    {
        /// <summary>
        /// Gets or sets the name of the potential.
        /// </summary>
        /// <value>The name of the potential.</value>
        public string PotentialName { get; set; }

        /// <summary>
        /// Gets or sets the potential number.
        /// </summary>
        /// <value>The potential number.</value>
        public string PotentialNumber { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>The amount.</value>
        public double Amount { get; set; }

        /// <summary>
        /// Gets or sets the related to.
        /// </summary>
        /// <value>The related to.</value>
        public string RelatedTo { get; set; }

        /// <summary>
        /// Gets or sets the contact identifier.
        /// </summary>
        /// <value>The contact identifier.</value>
        public string ContactId { get; set; }

        /// <summary>
        /// Gets or sets the closing date.
        /// </summary>
        /// <value>The closing date.</value>
        public string ClosingDate { get; set; }

        /// <summary>
        /// Gets or sets the type of the opportunity.
        /// </summary>
        /// <value>The type of the opportunity.</value>
        public OpportunityType OpportunityType { get; set; }

        /// <summary>
        /// Gets or sets the next step.
        /// </summary>
        /// <value>The next step.</value>
        public string NextStep { get; set; }

        /// <summary>
        /// Gets or sets the lead source.
        /// </summary>
        /// <value>The lead source.</value>
        public LeadSource LeadSource { get; set; }

        /// <summary>
        /// Gets or sets the sales stage.
        /// </summary>
        /// <value>The sales stage.</value>
        public SalesStage SalesStage { get; set; }

        /// <summary>
        /// Gets or sets the probability.
        /// </summary>
        /// <value>The probability.</value>
        public double Probability { get; set; }

        /// <summary>
        /// Gets or sets the campaign identifier.
        /// </summary>
        /// <value>The campaign identifier.</value>
        public string CampaignId { get; set; }

        /// <summary>
        /// Gets or sets the created time.
        /// </summary>
        /// <value>The created time.</value>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// Gets or sets the modified time.
        /// </summary>
        /// <value>The modified time.</value>
        public DateTime ModifiedTime { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="OpportunityModel"/> is call.
        /// </summary>
        /// <value>
        ///   <c>true</c> if call; otherwise, <c>false</c>.
        /// </value>
        public bool Call { get; set; }

        /// <summary>
        /// Gets or sets the interest.
        /// </summary>
        /// <value>
        /// The interest.
        /// </value>
        public string Interest { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>
        /// The gender.
        /// </value>
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets the rank.
        /// </summary>
        /// <value>
        /// The rank.
        /// </value>
        public double Rank { get; set; }

        /// <summary>
        /// Gets or sets the birth day.
        /// </summary>
        /// <value>
        /// The birth day.
        /// </value>
        public string BirthDay { get; set; }

        /// <summary>
        /// Gets or sets the image URL.
        /// </summary>
        /// <value>
        /// The image URL.
        /// </value>
        public string ImageURL { get; set; }

        /// <summary>
        /// Gets or sets the review message.
        /// </summary>
        /// <value>
        /// The review message.
        /// </value>
        public string ReviewMessage { get; set; }

        /// <summary>
        /// Gets or sets the rating.
        /// </summary>
        /// <value>
        /// The rating.
        /// </value>
        public double Rating { get; set; }

        /// <summary>
        /// Gets or sets the name of the company.
        /// </summary>
        /// <value>
        /// The name of the company.
        /// </value>
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [company reported].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [company reported]; otherwise, <c>false</c>.
        /// </value>
        public bool CompanyReported { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [user reported].
        /// </summary>
        /// <value>
        /// <c>true</c> if [user reported]; otherwise, <c>false</c>.
        /// </value>
        public bool UserReported { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [deal closed].
        /// </summary>
        /// <value>
        /// <c>true</c> if [deal closed]; otherwise, <c>false</c>.
        /// </value>
        public bool DealClosed { get; set; }
    }
}