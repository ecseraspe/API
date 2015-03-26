// ---------------------------------------------------------------------------------------------------
// <copyright file="IUserReviewService.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-12-18</date>
// <summary>
//     The IUserReviewService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Common.CRMService
{
    using System.Collections.Generic;
    using Youffer.CRM;

    /// <summary>
    /// The User review service interface
    /// </summary>
    public interface IUserReviewService
    {
        /// <summary>
        /// Create User review
        /// </summary>
        /// <param name="review">the VTigerUserReviews model</param>
        /// <returns> VTigerUserReviews obj </returns>
        VTigerUserReviews CreateUserReview(VTigerUserReviews review);

        /// <summary>
        /// Read user reviews
        /// </summary>
        /// <param name="contactId"> The contact Id. </param>
        /// <param name="organisationId"> The Organisation Id.</param>
        /// <param name="interest">The interest</param>
        /// <param name="lastPageId">The last page Id</param>
        /// <param name="fetchCount">the fetchCount</param>
        /// <returns> VTigerUserReviews list </returns>
        List<VTigerUserReviews> ReadUserReviews(string contactId, string organisationId, string interest, int lastPageId, int fetchCount);

        /// <summary>
        /// Update user reviews
        /// </summary>
        /// <param name="userReview">the VTigerUserReviews model</param>
        /// <returns> VTigerUserReviews obj </returns>
        VTigerUserReviews UpdateUserReview(VTigerUserReviews userReview);

        /// <summary>
        /// Gett the user rank
        /// </summary>
        /// <param name="contactId"> The contact Id</param>
        /// <returns>rank of user</returns>
        decimal GetUserRank(string contactId);

        /// <summary>
        /// Gets the avg company rating.
        /// </summary>
        /// <param name="orgId">The org identifier.</param>
        /// <returns>
        /// avg company rating
        /// </returns>
        decimal GetOrgAvgRating(string orgId);

        /// <summary>
        /// Gets the reviewed user ids.
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <returns>List of string.</returns>
        List<string> GetReviewedUserIDs(string companyId);

        /// <summary>
        /// Gets the user reviews.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The list of VTigerUserReviews model.</returns>
        List<VTigerUserReviews> GetUserReviews(string userId);

        /// <summary>
        /// Gets the reviews for company.
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <param name="lastpageId">The lastpage identifier.</param>
        /// <param name="fetchCount">The fetch count.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="direction">The direction.</param>
        /// <returns>List of UserReviewsDto object.</returns>
        List<VTigerUserReviews> GetReviewsForCompany(string companyId, int lastpageId, int fetchCount, string sortBy, string direction);
    }
}
