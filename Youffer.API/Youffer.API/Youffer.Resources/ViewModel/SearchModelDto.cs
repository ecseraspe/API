// ---------------------------------------------------------------------------------------------------
// <copyright file="SearchModelDto.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2014-12-8</date>
// <summary>
//     The SearchModelDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.ViewModel
{
    using Youffer.Resources.Enum;

    /// <summary>
    /// Class SearchModelDto
    /// </summary>
    public class SearchModelDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchModelDto" /> class.
        /// </summary>
        public SearchModelDto()
        {
            this.FetchCount = int.MaxValue;
        }

        /// <summary>
        /// Gets or sets the name of the interest.
        /// </summary>
        /// <value>
        /// The name of the interest.
        /// </value>
        public string InterestName { get; set; }

        /// <summary>
        /// Gets or sets th sub interest name
        /// </summary>
        public string SubInterestName { get; set; }

        /// <summary>
        /// Gets or sets the age from.
        /// </summary>
        /// <value>
        /// The age from.
        /// </value>
        public int AgeFrom { get; set; }

        /// <summary>
        /// Gets or sets the age to.
        /// </summary>
        /// <value>
        /// The age to.
        /// </value>
        public int AgeTo { get; set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>
        /// The gender.
        /// </value>
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [only active client].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [only active client]; otherwise, <c>false</c>.
        /// </value>
        public bool OnlyActiveClient { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [only reviewed client].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [only reviewed client]; otherwise, <c>false</c>.
        /// </value>
        public bool OnlyReviewedClient { get; set; }

        /// <summary>
        /// Gets or sets my client search.
        /// </summary>
        /// <value>
        /// My client search.
        /// </value>
        public string MyClientSearch { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the sort direction.
        /// </summary>
        /// <value>
        /// The sort direction.
        /// </value>
        public SortDirection SortDirection { get; set; }

        /// <summary>
        /// Gets or sets the sort by column.
        /// </summary>
        /// <value>
        /// The sort by column.
        /// </value>
        public SortBy SortByColumn { get; set; }

        /// <summary>
        /// Gets or sets the last page identifier.
        /// </summary>
        /// <value>
        /// The last page identifier.
        /// </value>
        public int LastPageId { get; set; }

        /// <summary>
        /// Gets or sets the fetch count.
        /// </summary>
        /// <value>
        /// The fetch count.
        /// </value>
        public int FetchCount { get; set; }

        /// <summary>
        /// Gets or sets the radius.
        /// </summary>
        /// <value>
        /// The radius.
        /// </value>
        public int Radius { get; set; }
    }
}
