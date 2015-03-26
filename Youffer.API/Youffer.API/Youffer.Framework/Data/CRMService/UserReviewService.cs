// ---------------------------------------------------------------------------------------------------
// <copyright file="UserReviewService.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-12-18</date>
// <summary>
//     The UserReviewService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Framework.Data.CRMService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Youffer.Common.CRMService;
    using Youffer.Common.LogService;
    using Youffer.CRM;

    /// <summary>
    /// The user review service
    /// </summary>
    public class UserReviewService : IUserReviewService
    {
        /// <summary>
        /// VTiger service instance
        /// </summary>
        private readonly IVTigerService vTigerService;

        /// <summary>
        ///  Initializes a new instance of the <see cref="UserReviewService" /> class.
        /// </summary>
        /// <param name="vTigerService">the vtiger service</param>
        /// <param name="loggerService">the logger service</param>
        public UserReviewService(IVTigerService vTigerService, ILoggerService loggerService)
        {
            this.vTigerService = vTigerService;
            this.LoggerService = loggerService;
        }

        /// <summary>
        /// Gets the logger service.
        /// </summary>
        protected ILoggerService LoggerService { get; private set; }

        /// <summary>
        /// Create User review
        /// </summary>
        /// <param name="review">the VTigerUserReviews model</param>
        /// <returns> VTigerUserReviews obj </returns>
        public VTigerUserReviews CreateUserReview(VTigerUserReviews review)
        {
            try
            {
                review = this.vTigerService.Create<VTigerUserReviews>(review);
                return review;
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Adding User Review :- " + ex.Message);
            }

            return new VTigerUserReviews();
        }

        /// <summary>
        /// Read user reviews
        /// </summary>
        /// <param name="contactId"> The contact Id. </param>
        /// <param name="organisationId"> The Organisation Id.</param>
        /// <param name="interest">The interest</param>
        /// <param name="lastPageId">The last page Id</param>
        /// <param name="fetchCount">the fetchCount</param>
        /// <returns> VTigerUserReviews list </returns>
        public List<VTigerUserReviews> ReadUserReviews(string contactId, string organisationId, string interest, int lastPageId, int fetchCount)
        {
            List<VTigerUserReviews> reviewlist = new List<VTigerUserReviews>();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Select * from Reviewforuser Where reviewforuser_tks_deleted = 0  and reviewforuser_tks_contact ='" + contactId + "' ");
                if (!string.IsNullOrEmpty(organisationId))
                {
                    sb.Append(" and reviewforuser_tks_organisation ='" + organisationId + "'");
                }

                if (!string.IsNullOrEmpty(interest))
                {
                    sb.Append(" and reviewforuser_tks_interestname = '" + interest + "'");
                }

                lastPageId = lastPageId < 1 ? 1 : lastPageId;
                var startVal = (lastPageId - 1) * fetchCount;

                sb.Append(" limit " + startVal + ", " + fetchCount + ";");
                string query = sb.ToString();
                IEnumerable<VTigerUserReviews> reviews = this.vTigerService.Query<VTigerUserReviews>(query);
                reviewlist = reviews.ToList().OrderByDescending(x => x.modifiedtime).ToList();
                return reviewlist;
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("fetching User Review :- " + ex.Message);
            }

            return reviewlist;
        }

        /// <summary>
        /// Update user reviews
        /// </summary>
        /// <param name="review">the VTigerUserReviews model</param>
        /// <returns> VTigerUserReviews obj </returns>
        public VTigerUserReviews UpdateUserReview(VTigerUserReviews review)
        {
            try
            {
                review = this.vTigerService.Update<VTigerUserReviews>(review);
                return review;
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Updating User Review :- " + ex.Message);
            }

            return new VTigerUserReviews();
        }

        /// <summary>
        /// Gett the user rank
        /// </summary>
        /// <param name="contactId"> The contact Id</param>
        /// <returns>rank of user</returns>
        public decimal GetUserRank(string contactId)
        {
            decimal rank = 0.0M;
            try
            {
                string query = "Select reviewforuser_tks_rating from Reviewforuser Where reviewforuser_tks_deleted = 0 and reviewforuser_tks_contact ='" + contactId + "' Limit 0, " + int.MaxValue + ";";
                IEnumerable<VTigerUserReviews> reviews = this.vTigerService.Query<VTigerUserReviews>(query);
                var reviewList = reviews.ToList();
                if (reviewList.Any())
                {
                    rank = reviewList.Select(x => x.reviewforuser_tks_rating).Average();
                }
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Getting User Rank :-" + ex.Message);
            }

            return rank;
        }

        /// <summary>
        /// Gets the avg company rating.
        /// </summary>
        /// <param name="orgId">The org identifier.</param>
        /// <returns>
        /// avg company rating
        /// </returns>
        public decimal GetOrgAvgRating(string orgId)
        {
            decimal rating = 0.0M;

            try
            {
                string query = "Select reviewforuser_tks_rating from Reviewforuser Where reviewforuser_tks_deleted = 0 and reviewforuser_tks_organisation ='" + orgId + "' Limit 0, " + int.MaxValue + ";";
                IEnumerable<VTigerUserReviews> reviews = this.vTigerService.Query<VTigerUserReviews>(query);
                var reviewList = reviews.ToList();
                if (reviewList.Any())
                {
                    rating = reviewList.Select(x => x.reviewforuser_tks_rating).Average();
                }
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Get Org Avg Rating :-" + ex.Message);
            }

            return rating;
        }

        /// <summary>
        /// Gets the reviewed user ids.
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <returns>List of string.</returns>
        public List<string> GetReviewedUserIDs(string companyId)
        {
            List<string> lstUserId = new List<string>();

            try
            {
                string query = "Select * from Reviewforuser Where reviewforuser_tks_deleted = 0 and reviewforuser_tks_organisation ='" + companyId + "' Limit 0, " + int.MaxValue + ";";
                IEnumerable<VTigerUserReviews> reviews = this.vTigerService.Query<VTigerUserReviews>(query);
                var reviewList = reviews.ToList();

                if (reviewList.Any())
                {
                    lstUserId = reviewList.Select(x => x.reviewforuser_tks_contact).ToList();
                }
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetReviewedUserIDs - " + ex.Message);
            }

            return lstUserId;
        }

        /// <summary>
        /// Gets the user reviews.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The list of VTigerUserReviews model.</returns>
        public List<VTigerUserReviews> GetUserReviews(string userId)
        {
            List<VTigerUserReviews> lstReviewsModel = new List<VTigerUserReviews>();
            try
            {
                string query = "Select * from Reviewforuser Where reviewforuser_tks_deleted = 0 and reviewforuser_tks_contact ='" + userId + "' order by reviewforuserno desc Limit 0, " + int.MaxValue + ";";
                IEnumerable<VTigerUserReviews> reviews = this.vTigerService.Query<VTigerUserReviews>(query);
                lstReviewsModel = reviews.ToList().OrderByDescending(x => x.modifiedtime).ToList();
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetUserReviews - " + ex.Message);
            }

            return lstReviewsModel;
        }

        /// <summary>
        /// Gets the reviews for company.
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <param name="lastpageId">The lastpage identifier.</param>
        /// <param name="fetchCount">The fetch count.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="direction">The direction.</param>
        /// <returns>List of UserReviewsDto object.</returns>
        public List<VTigerUserReviews> GetReviewsForCompany(string companyId, int lastpageId, int fetchCount, string sortBy, string direction)
        {
            List<VTigerUserReviews> reviewList = new List<VTigerUserReviews>();
            lastpageId = lastpageId < 1 ? 1 : lastpageId;
            var startVal = (lastpageId - 1) * fetchCount;
            try
            {
                string query = "Select * from Reviewforuser Where reviewforuser_tks_deleted = 0 and reviewforuser_tks_organisation ='" + companyId + "' Limit " + startVal + ", " + fetchCount + ";";
                IEnumerable<VTigerUserReviews> reviews = this.vTigerService.Query<VTigerUserReviews>(query);
                reviewList = reviews.ToList();
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetReviewsForCompany - " + ex.Message);
            }

            return reviewList;
        }
    }
}
