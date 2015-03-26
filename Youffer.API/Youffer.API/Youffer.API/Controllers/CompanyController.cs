// ---------------------------------------------------------------------------------------------------
// <copyright file="CompanyController.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2014-11-25</date>
// <summary>
//     The CompanyController class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http;
    using Microsoft.AspNet.Identity;
    using Newtonsoft.Json;
    using Youffer.Common.CRMService;
    using Youffer.Common.DataService;
    using Youffer.Common.Helper;
    using Youffer.Common.LogService;
    using Youffer.Common.Mapper;
    using Youffer.Common.Notification;
    using Youffer.CRM;
    using Youffer.Framework.Extensions;
    using Youffer.Resources.Constants;
    using Youffer.Resources.CRMModel;
    using Youffer.Resources.Enum;
    using Youffer.Resources.Models;
    using Youffer.Resources.MySqlDbSchema;
    using Youffer.Resources.ViewModel;

    /// <summary>
    /// The Company Controller.
    /// </summary>
    [RoutePrefix("api/company")]
    [Authorize(Roles = RoleName.CompanyRole)]
    public class CompanyController : BaseApiController
    {
        #region private properties

        /// <summary>
        /// The IContactService service instance
        /// </summary>
        private readonly ICRMManagerService crmManagerService;

        /// <summary>
        /// The IAuthRepository service instance
        /// </summary>
        private readonly IAuthRepository authRepository;

        /// <summary>
        /// The IUserService service instance
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// The IYoufferNoteService service instance
        /// </summary>
        private readonly INoteService noteService;

        /// <summary>
        /// The common service instance
        /// </summary>
        private readonly ICommonService commonService;

        /// <summary>
        /// The youffer message service
        /// </summary>
        private readonly IYoufferMessageService youfferMessageService;

        /// <summary>
        /// The opportunityService service
        /// </summary>
        private readonly IOpportunityService oppService;

        /// <summary>
        /// The IYoufferContactService service instance
        /// </summary>
        private readonly IYoufferContactService youfferContactService;

        /// <summary>
        /// The IYoufferLeadService service instance
        /// </summary>
        private readonly IYoufferLeadService youfferLeadService;

        /// <summary>
        /// The IYoufferFeedbackService service instance
        /// </summary>
        private readonly IYoufferFeedbackService youfferFeedbackService;

        /// <summary>
        /// The mapper factory
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// The youffer note service
        /// </summary>
        private readonly IYoufferNoteService youfferNoteService;

        /// <summary>
        /// The youffer interest service
        /// </summary>
        private readonly IYoufferInterestService youfferInterestService;

        /// <summary>
        /// The push message service
        /// </summary>
        private readonly IPushMessageService pushMessageService;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="CompanyController"/> class.
        /// </summary>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="crmManagerService">The CRM manager service.</param>
        /// <param name="userService">The user service.</param>
        /// <param name="noteService">The note service.</param>
        /// <param name="commonService">The common service.</param>
        /// <param name="youfferMessageService">The youffer message service.</param>
        /// <param name="oppService">The opp service.</param>
        /// <param name="youfferContactService">The youffer contact service.</param>
        /// <param name="youfferLeadService">The youffer lead service.</param>
        /// <param name="youfferFeedbackService">The youffer feedback service.</param>
        /// <param name="mapperFactory">The Mapper Factory</param>
        /// <param name="youfferNoteService">The youffer note service.</param>
        /// <param name="youfferInterestService"> The youffer interest service </param>
        /// <param name="pushMessageService">The push message service</param>
        public CompanyController(ILoggerService loggerService, ICRMManagerService crmManagerService, IUserService userService, INoteService noteService, ICommonService commonService, IYoufferMessageService youfferMessageService, IOpportunityService oppService, IYoufferContactService youfferContactService, IYoufferLeadService youfferLeadService, IYoufferFeedbackService youfferFeedbackService, IMapperFactory mapperFactory, IYoufferNoteService youfferNoteService, IYoufferInterestService youfferInterestService, IPushMessageService pushMessageService)
            : base(loggerService)
        {
            this.mapperFactory = mapperFactory;
            this.crmManagerService = crmManagerService;
            this.userService = userService;
            this.noteService = noteService;
            this.commonService = commonService;
            this.youfferMessageService = youfferMessageService;
            this.oppService = oppService;
            this.youfferContactService = youfferContactService;
            this.youfferLeadService = youfferLeadService;
            this.youfferFeedbackService = youfferFeedbackService;
            this.youfferNoteService = youfferNoteService;
            this.youfferInterestService = youfferInterestService;
            this.pushMessageService = pushMessageService;
        }

        /// <summary>
        /// Gets the company profile details.
        /// </summary>
        /// <returns>IHttpActionResult object</returns>
        [Route("company-details")]
        [HttpGet]
        public async Task<IHttpActionResult> GetCompanyDetails()
        {
            string companyId = User.Identity.GetUserId();

            var orgModel = this.crmManagerService.GetOrganisation(companyId);
            CountryModel countryDet = this.commonService.GetCompanyCountryDetails(companyId);
            var orgResultModel = this.mapperFactory.GetMapper<OrganisationModel, OrgResultModel>().Map(orgModel);
            orgResultModel.Id = companyId;
            orgResultModel.CountryDetails = countryDet;
            orgResultModel.CallingCode = this.commonService.GetCallingCodeFromISO2Code(orgResultModel.CountryCode);
            orgResultModel.UserRole = Roles.Company;
            orgResultModel.PaymentDetails = string.Empty;
            return this.Ok(orgResultModel);
        }

        /// <summary>
        /// Updates the company details.
        /// </summary>
        /// <param name="orgModel">The org model.</param>
        /// <returns>IHttpActionResult object.</returns>
        [Route("updatecompany")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateCompanyDetails(OrganisationModel orgModel)
        {
            string companyId = User.Identity.GetUserId();
            var organisation = this.crmManagerService.GetOrganisation(companyId);

            ////organisation.CountryCode = string.IsNullOrEmpty(orgModel.CountryCode) ? organisation.CountryCode : orgModel.CountryCode;
            organisation.Website = string.IsNullOrWhiteSpace(orgModel.Website) ? organisation.Website : orgModel.Website;
            organisation.FacebookURL = string.IsNullOrWhiteSpace(orgModel.FacebookURL) ? organisation.FacebookURL : orgModel.FacebookURL;
            organisation.GCMId = string.IsNullOrEmpty(orgModel.GCMId) ? organisation.GCMId : orgModel.GCMId;
            organisation.UDId = string.IsNullOrEmpty(orgModel.UDId) ? organisation.UDId : orgModel.UDId;
            organisation.Phone = string.IsNullOrEmpty(orgModel.Phone) ? organisation.Phone : orgModel.Phone;

            ////Only for handling old Android apps
            if (orgModel.SubBusinessType != null && orgModel.SubBusinessType.Length > 0)
            {
                organisation.SubBusinessType = orgModel.SubBusinessType;
                string subBusinessType = orgModel.SubBusinessType.Aggregate((a, b) => Convert.ToString(a) + "," + Convert.ToString(b));
                organisation.MainBusinessType = this.youfferInterestService.GetMainBusinessTypeFromSub(subBusinessType).Select(x => x.ParentBusinessTypeName).Distinct().ToArray();
            }

            if (orgModel.MainBusinessType != null && orgModel.MainBusinessType.Length > 0)
            {
                organisation.MainBusinessType = orgModel.MainBusinessType;
                string mainBusinessType = organisation.MainBusinessType.Aggregate((a, b) => Convert.ToString(a) + "," + Convert.ToString(b));
                string[] subInterest = this.youfferInterestService.GetSubBusinessTypeFromMain(mainBusinessType).Select(x => x.BusinessTypeName).Distinct().ToArray();
                organisation.SubBusinessType = subInterest;
            }

            organisation.IsActive = orgModel.IsActive ?? organisation.IsActive;
            organisation.Latitude = orgModel.Latitude <= 0 ? organisation.Latitude : orgModel.Latitude;
            organisation.Longitude = orgModel.Longitude <= 0 ? organisation.Longitude : orgModel.Longitude;

            orgModel = this.crmManagerService.UpdateOrganisation(companyId, organisation);

            if (orgModel == null)
            {
                return this.BadRequest();
            }

            OrgResultModel orgResModel = this.mapperFactory.GetMapper<OrganisationModel, OrgResultModel>().Map(orgModel);

            CountryModel countryDet = this.commonService.GetCompanyCountryDetails(companyId);
            orgResModel.CountryDetails = countryDet;

            return this.Ok(orgResModel);
        }

        /// <summary>
        /// Gets the user details.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>IHttpActionResult object</returns>
        [Route("userprofile/{userId}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetUserDetails(string userId)
        {
            UserResultModel userResultModel = new UserResultModel();
            string crmContactId = this.userService.GetContact(userId).CRMId;
            string crmLeadId = string.Empty;

            if (!string.IsNullOrEmpty(crmContactId))
            {
                crmLeadId = this.youfferContactService.GetMappingEntryByContactId(userId).LeadId;
            }

            CountryModel countryDet = this.commonService.GetUserCountryDetails(userId);
            decimal rank = this.crmManagerService.GetUserRank(crmContactId);

            var reviews = this.crmManagerService.GetUserReviews(crmContactId);
            List<UserReviewsDto> lstReviews = this.mapperFactory.GetMapper<List<CRMUserReview>, List<UserReviewsDto>>().Map(reviews);

            if (!string.IsNullOrEmpty(crmLeadId))
            {
                LeadModel leadModel = this.crmManagerService.GetLead(crmLeadId);
                userResultModel = this.mapperFactory.GetMapper<LeadModel, UserResultModel>().Map(leadModel);
                userResultModel.PaymentDetails = new PaymentModelDto()
                {
                    PayPalId = leadModel.PaypalId,
                    Mode = (PaymentMode)leadModel.PaymentMode
                };
            }
            else
            {
                ContactModel contactModel = this.crmManagerService.GetContact(userId);
                userResultModel = this.mapperFactory.GetMapper<ContactModel, UserResultModel>().Map(contactModel);
                userResultModel.PaymentDetails = new PaymentModelDto()
                {
                    PayPalId = contactModel.PaypalId,
                    Mode = (PaymentMode)contactModel.PaymentMode
                };
            }

            userResultModel.Id = userId;
            userResultModel.Rank = rank;
            userResultModel.CountryDetails = countryDet;
            userResultModel.UserRole = Roles.Customer;
            userResultModel.UserReviews = lstReviews;

            return this.Ok(userResultModel);
        }

        /// <summary>
        /// Gets the user reviews.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>IHttpActionResult object.</returns>
        [Route("user-reviews/{userId}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetUserReviews(string userId)
        {
            string companyId = User.Identity.GetUserId();

            List<ApplicationUserDto> appUsers = this.userService.GetUsers(userId, companyId);
            string contactId = appUsers.Where(x => x.Id == userId).Select(x => x.CRMId).First().ToString();
            string organisationId = appUsers.Where(x => x.Id == companyId).Select(x => x.CRMId).First().ToString();
            var reviewList = this.crmManagerService.ReadUserReviews(contactId, organisationId);
            List<UserReviewsDto> lstReview = new List<UserReviewsDto>();
            if (reviewList.Any())
            {
                List<CRMUserReview> crmReview = reviewList;
                lstReview = this.mapperFactory.GetMapper<List<CRMUserReview>, List<UserReviewsDto>>().Map(crmReview);
            }

            return this.Ok(lstReview);
        }

        /// <summary>
        /// Saves the user review.
        /// </summary>
        /// <param name="userReview">User Review object.</param>
        /// <returns>IHttpActionResult object.</returns>
        [Route("saveuser-review")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveUserReview(UserReviewsDto userReview)
        {
            string companyId = User.Identity.GetUserId();
            userReview.CompanyId = userReview.CreatedBy = userReview.ModifiedBy = companyId;

            List<ApplicationUserDto> appUsers = this.userService.GetUsers(userReview.UserId, companyId);
            string contactId = appUsers.Where(x => x.Id == userReview.UserId).Select(x => x.CRMId).First().ToString();
            string organisationId = appUsers.Where(x => x.Id == companyId).Select(x => x.CRMId).First().ToString();

            CRMUserReview review = new CRMUserReview()
            {
                FeedbackText = userReview.Feedback,
                Rating = userReview.Rating,
                InterestName = userReview.InterestName,
                OrganisationsId = organisationId,
                ContactId = contactId
            };

            var reviewList = this.crmManagerService.ReadUserReviews(contactId, organisationId, userReview.InterestName);
            foreach (var item in reviewList)
            {
                item.IsDeleted = true;
                this.crmManagerService.UpdateUserReview(item);
            }

            review = this.crmManagerService.AddUserReview(review);
            userReview.Id = review.Id;

            ContactModel contact = this.crmManagerService.GetContact(userReview.UserId);
            OrganisationModel org = this.crmManagerService.GetOrganisation(companyId);
            if (!string.IsNullOrEmpty(contact.GCMId))
            {
                this.pushMessageService.SendRatingNotificationToAndroid(contact.GCMId, companyId, org.AccountName, "Review User", userReview.Rating, Notification.rating.ToString());
            }

            if (!string.IsNullOrEmpty(contact.UDId))
            {
                this.pushMessageService.SendRatingNotificationToiOS(contact.UDId, companyId, org.AccountName, "Review User", userReview.Rating, Notification.rating.ToString());
            }

            return this.Ok(userReview);
        }

        /// <summary>
        /// Gets the company notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="lastSeenId"> The last seen Id. </param>
        /// <param name="fetchCount"> The fetch Count</param>
        /// <returns>IHttpActionResult object.</returns>
        [Route("company-notes/{userId}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetCompanyNotes(string userId, int lastSeenId = 0, int fetchCount = 0)
        {
            if (fetchCount == 0)
            {
                fetchCount = AppSettings.Get<int>(ConfigConstants.NotesDefaultCount);
            }

            string companyId = User.Identity.GetUserId();

            List<ApplicationUserDto> appUsers = this.userService.GetUsers(userId, companyId);
            string contactId = appUsers.Where(x => x.Id == userId).Select(x => x.CRMId).First().ToString();
            string organisationId = appUsers.Where(x => x.Id == companyId).Select(x => x.CRMId).First().ToString();

            List<CRMCompanyNotes> noteList = this.crmManagerService.ReadNotes(contactId, organisationId, lastSeenId, fetchCount);
            var list = noteList.Select(x => new CompanyNotesDto { Id = x.Id, UserId = userId, CompanyId = companyId, NoteText = x.Note, CreatedOn = x.CreatedOn, ModifiedOn = x.ModifiedOn });
            return this.Ok(list);
        }

        /// <summary>
        /// Saves the company note.
        /// </summary>
        /// <param name="note"> NoteModel object.</param>
        /// <returns>IHttpActionResult object.</returns>
        [Route("savecompany-note")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveCompanyNote(CompanyNotesDto note)
        {
            string companyId = User.Identity.GetUserId();

            List<ApplicationUserDto> appUsers = this.userService.GetUsers(note.UserId, companyId);
            string contactId = appUsers.Where(x => x.Id == note.UserId).Select(x => x.CRMId).First().ToString();
            string organisationId = appUsers.Where(x => x.Id == companyId).Select(x => x.CRMId).First().ToString();

            CRMCompanyNotes notes = new CRMCompanyNotes()
            {
                Note = note.NoteText,
                OrganisationId = organisationId,
                ContactId = contactId
            };

            notes = this.crmManagerService.AddCompanyNote(notes);
            note.Id = notes.Id;
            return this.Ok(note);
        }

        /// <summary>
        /// Creates the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>IHttpActionResult object.</returns>
        [Route("createmessage")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateMessage(MessagesDto message)
        {
            message.ModifiedBy = message.FromUser = message.CreatedBy = User.Identity.GetUserId();
            message.IsDeleted = false;
            message.IsReadByCompany = true;
            message.CompanyId = message.FromUser;
            message.UserId = message.ToUser;

            message = this.youfferMessageService.CreateMessage(message);
            int unreadMsgCount = this.youfferMessageService.GetUnreadMsgCount(message.UserId, true);

            ContactModel contact = this.crmManagerService.GetContact(message.ToUser);
            OrganisationModel fromUser = this.crmManagerService.GetOrganisation(message.FromUser);
            if (!string.IsNullOrEmpty(contact.GCMId))
            {
                this.pushMessageService.SendMessageNotificationToAndroid(contact.GCMId, message.Id.ToString(), message.Message, fromUser.AccountName, Notification.companymsg.ToString());
            }

            if (!string.IsNullOrEmpty(contact.UDId))
            {
                this.pushMessageService.SendMessageNotificationToiOS(contact.UDId, message.Id.ToString(), message.Message, fromUser.AccountName, unreadMsgCount, Notification.companymsg.ToString());
            }

            return this.Ok(message);
        }

        /// <summary>
        /// Gets the messages.
        /// </summary>
        /// <param name="threadId">The thread identifier.</param>
        /// <param name="lastpageId">The lastpage identifier.</param>
        /// <param name="fetchCount">The fetch count.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="direction">The direction.</param>
        /// <returns>IHttpActionResult object.</returns>
        [Route("getmessages/{threadId}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetMessages(long threadId, int lastpageId = 0, int fetchCount = 0, string sortBy = "", string direction = "asc")
        {
            if (sortBy == null)
            {
                sortBy = string.Empty;
            }

            if (direction == null)
            {
                direction = "asc";
            }

            if (fetchCount == 0 || fetchCount == null)
            {
                fetchCount = AppSettings.Get<int>(ConfigConstants.MessageDefaultCount);
            }

            string companyId = User.Identity.GetUserId();
            ApplicationUserDto companyDet = this.youfferContactService.GetOrgCRMId(companyId);
            string companyName = companyDet.Name;
            string orgCRMId = companyDet.CRMId;

            List<MessagesDto> lstMessages = this.youfferMessageService.GetAllMessages(companyId, threadId, lastpageId, fetchCount, sortBy, direction);

            List<CRMContactUs> contactUsMsg = this.crmManagerService.ReadAllContactUsMessage(orgCRMId, lastpageId, fetchCount, sortBy, direction);
            foreach (var contactUs in contactUsMsg)
            {
                MessagesDto msg = new MessagesDto()
                {
                    Id = Convert.ToInt32(contactUs.Id.Split('x')[1]),
                    IsRead = true,
                    CreatedOn = contactUs.CreatedOn,
                    ModifiedOn = contactUs.ModifiedOn,
                    FromUser = contactUs.IsIncomingMessage ? companyId : "Youffer Admin",
                    CreatedBy = contactUs.IsIncomingMessage ? companyId : "Youffer Admin",
                    ModifiedBy = contactUs.IsIncomingMessage ? companyId : "Youffer Admin",
                    CompanyId = companyId,
                    ToUser = contactUs.IsIncomingMessage ? "Youffer Admin" : companyId,
                    Message = contactUs.Description,
                    Subject = contactUs.Subject,
                    DeptId = (ContactUsDept)Enum.Parse(typeof(ContactUsDept), contactUs.Department),
                    MediaId = 0,
                    Name = "Youffer Admin",
                    UserId = AppSettings.Get<string>(ConfigConstants.SuperUserId),
                    ThreadId = threadId
                };

                lstMessages.Add(msg);
            }

            if (direction == "asc")
            {
                lstMessages = lstMessages.OrderBy(x => x.CreatedOn).ToList();
            }
            else
            {
                lstMessages = lstMessages.OrderByDescending(x => x.CreatedOn).ToList();
            }

            return this.Ok(lstMessages);
        }

        /// <summary>
        /// Gets the messages.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="lastpageId">The lastpage identifier.</param>
        /// <param name="fetchCount">The fetch count.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="direction">The direction.</param>
        /// <returns>IHttpActionResult object.</returns>
        [Route("{userId}/getmessages")]
        [HttpGet]
        public async Task<IHttpActionResult> GetMessages(string userId, int lastpageId = 0, int fetchCount = 0, string sortBy = "", string direction = "asc")
        {
            if (sortBy == null)
            {
                sortBy = string.Empty;
            }

            if (direction == null)
            {
                direction = "asc";
            }

            if (fetchCount == 0 || fetchCount == null)
            {
                fetchCount = AppSettings.Get<int>(ConfigConstants.MessageDefaultCount);
            }

            List<MessagesDto> lstMessages = new List<MessagesDto>();
            string companyId = User.Identity.GetUserId();

            ApplicationUserDto companyDet = this.youfferContactService.GetOrgCRMId(companyId);
            string companyName = companyDet.Name;
            string orgCRMId = companyDet.CRMId;

            MessageThreadDto msgThreadDto = this.youfferMessageService.GetMessageThread(companyId, userId);
            if (msgThreadDto != null && msgThreadDto.Id > 0)
            {
                lstMessages = this.youfferMessageService.GetAllMessages(companyId, msgThreadDto.Id, lastpageId, fetchCount, sortBy, direction);
            }

            List<CRMContactUs> contactUsMsg = this.crmManagerService.ReadAllContactUsMessage(orgCRMId, lastpageId, fetchCount, sortBy, direction);
            foreach (var contactUs in contactUsMsg)
            {
                MessagesDto msg = new MessagesDto()
                {
                    Id = Convert.ToInt32(contactUs.Id.Split('x')[1]),
                    IsRead = true,
                    CreatedOn = contactUs.CreatedOn,
                    ModifiedOn = contactUs.ModifiedOn,
                    FromUser = contactUs.IsIncomingMessage ? companyId : "Youffer Admin",
                    CreatedBy = contactUs.IsIncomingMessage ? companyId : "Youffer Admin",
                    ModifiedBy = contactUs.IsIncomingMessage ? companyId : "Youffer Admin",
                    CompanyId = companyId,
                    ToUser = contactUs.IsIncomingMessage ? "Youffer Admin" : companyId,
                    Message = contactUs.Description,
                    Subject = contactUs.Subject,
                    DeptId = (ContactUsDept)Enum.Parse(typeof(ContactUsDept), contactUs.Department),
                    MediaId = 0,
                    Name = "Youffer Admin",
                    UserId = AppSettings.Get<string>(ConfigConstants.SuperUserId),
                    ThreadId = msgThreadDto.Id
                };

                lstMessages.Add(msg);
            }

            if (direction == "asc")
            {
                lstMessages = lstMessages.OrderBy(x => x.CreatedOn).ToList();
            }
            else
            {
                lstMessages = lstMessages.OrderByDescending(x => x.CreatedOn).ToList();
            }

            return this.Ok(lstMessages);
        }

        /// <summary>
        /// Gets all company messages.
        /// </summary>
        /// <param name="lastpageId">The lastpage identifier.</param>
        /// <param name="fetchCount">The fetch count.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="direction">The direction.</param>
        /// <returns>IHttpActionResult object.</returns>
        [Route("getcompanymessages")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllCompanyMessages(int lastpageId = 0, int fetchCount = 0, string sortBy = "", string direction = "asc")
        {
            if (sortBy == null)
            {
                sortBy = string.Empty;
            }

            if (direction == null)
            {
                direction = "asc";
            }

            if (fetchCount == 0 || fetchCount == null)
            {
                fetchCount = AppSettings.Get<int>(ConfigConstants.MessageDefaultCount);
            }

            string companyId = User.Identity.GetUserId();
            ApplicationUserDto companyDet = this.youfferContactService.GetOrgCRMId(companyId);
            string companyName = companyDet.Name;
            string orgCRMId = companyDet.CRMId;

            List<MessagesDto> lstMessages = this.youfferMessageService.GetAllCompanyMessages(companyId, lastpageId, fetchCount, sortBy, direction);

            if (lastpageId <= 1)
            {
                CRMContactUs contactUs = this.crmManagerService.ReadContactUsMessage(orgCRMId);

                if (contactUs != null)
                {
                    try
                    {
                        MessageThreadDto msgThread = this.youfferMessageService.GetMessageThread(companyId, "YoufferAdmin");
                        if (msgThread != null)
                        {
                            MessagesDto msg = new MessagesDto()
                            {
                                Id = Convert.ToInt32(contactUs.Id.Split('x')[1]),
                                IsRead = true,
                                CreatedOn = contactUs.CreatedOn,
                                ModifiedOn = contactUs.ModifiedOn,
                                FromUser = contactUs.IsIncomingMessage ? companyId : "Youffer Admin",
                                CreatedBy = contactUs.IsIncomingMessage ? companyId : "Youffer Admin",
                                ModifiedBy = contactUs.IsIncomingMessage ? companyId : "Youffer Admin",
                                CompanyId = companyId,
                                ToUser = contactUs.IsIncomingMessage ? "Youffer Admin" : companyId,
                                Message = contactUs.Description,
                                Subject = contactUs.Subject,
                                DeptId = (ContactUsDept)Enum.Parse(typeof(ContactUsDept), contactUs.Department),
                                MediaId = 0,
                                Name = "Youffer Admin",
                                UserId = AppSettings.Get<string>(ConfigConstants.SuperUserId),
                                ThreadId = msgThread.Id
                            };

                            lstMessages.Add(msg);
                        }

                        lstMessages = lstMessages.Distinct().OrderByDescending(x => x.CreatedOn).GroupBy(x => x.ThreadId).Select(grp => grp.First()).ToList();
                    }
                    catch (Exception ex)
                    {
                        var a = ex.Message;
                    }
                }
            }

            return this.Ok(lstMessages);
        }

        /// <summary>
        /// Deletes the message.
        /// </summary>
        /// <param name="msgId">The MSG identifier.</param>
        /// <returns>IHttpActionResult object.</returns>
        [Route("deletemessage")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteMessage(string msgId)
        {
            string companyId = User.Identity.GetUserId();
            bool isSuccess = true;

            bool isContactUs = this.crmManagerService.CheckIfContactUsMsg(AppSettings.Get<string>(ConfigConstants.ContactUsId) + msgId);
            if (isContactUs)
            {
                isSuccess = this.crmManagerService.DeleteContactusMessage(AppSettings.Get<string>(ConfigConstants.ContactUsId) + msgId);
            }
            else
            {
                isSuccess = this.youfferMessageService.DeleteMessage(msgId, companyId);
            }

            return this.Ok(isSuccess);
        }

        /// <summary>
        /// Creates the opportunity.
        /// </summary>
        /// <param name="buyUserModelDto">The buy user model dto.</param>
        /// <returns>IHttpActionResult object.</returns>
        [Route("buyuserwithcashorcredit")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateOpportunity(BuyUserModelDto buyUserModelDto)
        {
            buyUserModelDto.CompanyId = User.Identity.GetUserId();
            BuyUserStatusMessage status = new BuyUserStatusMessage();
            string companyId = User.Identity.GetUserId();
            string crmContactId = this.youfferContactService.GetMappingEntryByContactId(buyUserModelDto.UserId).ContactCRMId;
            string crmLeadId = this.youfferContactService.GetMappingEntryByContactId(buyUserModelDto.UserId).LeadId;
            ApplicationUserDto compantDet = this.youfferContactService.GetOrgCRMId(companyId);
            string crmOrgId = compantDet.CRMId;
            LeadModel leadModel = this.crmManagerService.GetLead(crmLeadId);
            OrganisationModel org = this.crmManagerService.GetOrganisation(companyId);

            if (buyUserModelDto.PurchasedFromCredit)
            {
                if (org.BillCountry.ToLower() == "india" && org.CreditBalance < 125)
                {
                    status.IsSuccess = false;
                    status.ErrorMessage = "You do not have sufficient credit.";
                    status.CreditBalance = org.CreditBalance;
                    status.CashBalance = org.CashBalance;

                    return this.Ok(status);
                }
                else if (org.BillCountry.ToLower() != "india" && org.CreditBalance < 5)
                {
                    status.IsSuccess = false;
                    status.ErrorMessage = "You do not have sufficient credit.";
                    status.CreditBalance = org.CreditBalance;
                    status.CashBalance = org.CashBalance;

                    return this.Ok(status);
                }
            }
            else if (buyUserModelDto.PurchasedFromCash)
            {
                if (org.BillCountry.ToLower() == "india" && org.CashBalance < 125)
                {
                    status.IsSuccess = false;
                    status.ErrorMessage = "You do not have sufficient cash.";
                    status.CreditBalance = org.CreditBalance;
                    status.CashBalance = org.CashBalance;

                    return this.Ok(status);
                }
                else if (org.BillCountry.ToLower() != "india" && org.CashBalance < 5)
                {
                    status.IsSuccess = false;
                    status.ErrorMessage = "You do not have sufficient cash.";
                    status.CreditBalance = org.CreditBalance;
                    status.CashBalance = org.CashBalance;

                    return this.Ok(status);
                }
            }

            status.IsSuccess = this.crmManagerService.AddOpportunity(buyUserModelDto);

            if (status.IsSuccess)
            {
                double price = Convert.ToDouble(buyUserModelDto.Amount);
                org.AnnualRevenue += price;
                if (buyUserModelDto.PurchasedFromCredit)
                {
                    org.CreditBalance -= Convert.ToDecimal(price);

                    leadModel.PurchasedWithCreditCount += 1;
                    this.crmManagerService.UpdateLead(leadModel.Id, leadModel);
                }
                else if (buyUserModelDto.PurchasedFromCash)
                {
                    org.CashBalance -= Convert.ToDecimal(price);
                }

                status.CreditBalance = org.CreditBalance;
                status.CashBalance = org.CashBalance;
                this.crmManagerService.UpdateOrganisation(buyUserModelDto.CompanyId, org);
            }

            return this.Ok(status);
        }

        /// <summary>
        /// Creates the opportunity.
        /// </summary>
        /// <param name="payPalDetailsDto">The pay pal details dto.</param>
        /// <returns>IHttpActionResult object.</returns>
        [Route("buyuser")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateOpportunity(PayPalDetailsDto payPalDetailsDto)
        {
            StatusMessage status = new StatusMessage();
            string companyId = User.Identity.GetUserId();
            string crmContactId = this.youfferContactService.GetMappingEntryByContactId(payPalDetailsDto.UserId).ContactCRMId;
            string crmLeadId = this.youfferContactService.GetMappingEntryByContactId(payPalDetailsDto.UserId).LeadId;
            ApplicationUserDto compantDet = this.youfferContactService.GetOrgCRMId(companyId);
            string crmOrgId = compantDet.CRMId;
            LeadModel leadModel = this.crmManagerService.GetLead(crmLeadId);
            OrganisationModel org = this.crmManagerService.GetOrganisation(companyId);

            double price = Convert.ToDouble(payPalDetailsDto.Amount);
            status.IsSuccess = this.crmManagerService.AddOpportunity(leadModel, crmOrgId, crmContactId, price, payPalDetailsDto, null);

            if (status.IsSuccess)
            {
                org.AnnualRevenue += price;
                this.crmManagerService.UpdateOrganisation(companyId, org);
            }

            if (!string.IsNullOrEmpty(leadModel.GCMId))
            {
                this.pushMessageService.SendPurchasedNotificationToAndroid(leadModel.GCMId, companyId, compantDet.Name, payPalDetailsDto.Interest, "Purchased", Notification.buyuser.ToString());
            }

            if (!string.IsNullOrEmpty(leadModel.UDId))
            {
                this.pushMessageService.SendPurchasedNotificationToiOS(leadModel.UDId, companyId, compantDet.Name, "Purchased", Notification.buyuser.ToString());
            }

            CRMNotifications notification = new CRMNotifications()
            {
                OrganisationsId = crmOrgId,
                ContactId = crmContactId,
                NotificationType = ConfigConstants.OrganisationPurchasedContact
            };

            this.crmManagerService.CreateNotification(notification);

            return this.Ok(status);
        }

        /// <summary>
        /// Determines whether the user can be purchased or not.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="interest">The interest.</param>
        /// <returns>IHttpActionResult object.</returns>
        [Route("canpurchase")]
        [HttpPost]
        public async Task<IHttpActionResult> CanPurchaseUser(string userId, string interest)
        {
            StatusMessage stat = new StatusMessage();
            string companyId = User.Identity.GetUserId();
            string crmContactId = this.youfferContactService.GetMappingEntryByContactId(userId).ContactCRMId;
            string crmOrgId = this.youfferContactService.GetOrgCRMId(companyId).CRMId;

            stat.IsSuccess = this.crmManagerService.CanPurchaseUser(crmOrgId, crmContactId, interest);

            return this.Ok(stat);
        }

        /// <summary>
        /// Gets the opportunities.
        /// </summary>
        /// <param name="lastpageId">The lastpage identifier.</param>
        /// <param name="fetchCount">The fetch count.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="direction">The direction.</param>
        /// <returns>IHttpActionResult object.</returns>
        [Route("purchasedclients")]
        [HttpGet]
        public async Task<IHttpActionResult> GetOpportunities(int lastpageId = 0, int fetchCount = 0, string sortBy = "", string direction = "asc")
        {
            List<PurchasedClientsDto> lstPurchasedClientsDto = new List<PurchasedClientsDto>();
            if (fetchCount == 0)
            {
                fetchCount = AppSettings.Get<int>(ConfigConstants.DashboardLeadsDefaultCount);
            }

            string companyId = User.Identity.GetUserId();
            List<VTigerPotential> lst = this.crmManagerService.GetOpportunities(companyId, lastpageId, fetchCount, sortBy, direction);

            foreach (var item in lst)
            {
                string contactId = this.userService.GetContactByCrmId(item.contact_id).Id;
                ContactModel contactModel = this.crmManagerService.GetContact(contactId);
                string crmOrgId = this.youfferContactService.GetOrgCRMId(companyId).CRMId;
                var reviewList = this.crmManagerService.ReadUserReviews(item.contact_id, crmOrgId, item.cf_853);
                CountryModel countryDet = this.commonService.GetUserCountryDetailsFromName(contactModel.MailingCountry);

                UserReviewsDto review = new UserReviewsDto();
                if (reviewList.Any())
                {
                    CRMUserReview crmReview = reviewList.First();
                    review = this.mapperFactory.GetMapper<CRMUserReview, UserReviewsDto>().Map(crmReview);
                }

                decimal rank = Convert.ToDecimal(this.crmManagerService.GetUserRank(item.contact_id));
                lstPurchasedClientsDto.Add(new PurchasedClientsDto { ClientId = contactId, Name = item.potentialname, ImageURL = contactModel.ImageURL, PhoneNumber = contactModel.Phone, BirthDay = contactModel.Birthday, Gender = contactModel.Gender, Rank = rank, Rating = review.Rating, Review = review.Feedback, Latitude = contactModel.Latitude, Longitude = contactModel.Longitude, Interest = item.cf_853, IsAvailable = contactModel.IsAvailable, CanCall = item.cf_843, IsOnline = contactModel.IsOnline, MarkPurchased = item.cf_857, CountryDet = countryDet });
            }

            return this.Ok(lstPurchasedClientsDto);
        }

        /// <summary>
        /// Gets the opportunities.
        /// </summary>
        /// <param name="lastpageId">The lastpage identifier.</param>
        /// <param name="fetchCount">The fetch count.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="direction">The direction.</param>
        /// <returns>IHttpActionResult object.</returns>
        [Route("purchasedclients1")]
        [HttpGet]
        public async Task<IHttpActionResult> GetOpportunities1(int lastpageId = 0, int fetchCount = 0, string sortBy = "", string direction = "asc")
        {
            List<PurchasedClientsDto> lstPurchasedClientsDto = new List<PurchasedClientsDto>();
            if (fetchCount == 0)
            {
                fetchCount = AppSettings.Get<int>(ConfigConstants.DashboardLeadsDefaultCount);
            }

            string companyId = User.Identity.GetUserId();
            string crmOrgId = this.userService.GetContact(companyId).CRMId;
            List<VtigerPotentialData> lst = this.crmManagerService.GetMyClients(crmOrgId, string.Empty, lastpageId, fetchCount, sortBy, direction);

            foreach (var item in lst)
            {
                PurchasedClientsDto model = this.mapperFactory.GetMapper<VtigerPotentialData, PurchasedClientsDto>().Map(item);
                string crmContactId = AppSettings.Get<string>(ConfigConstants.ContactId) + item.contactid;
                var reviewList = this.crmManagerService.ReadUserReviews(crmContactId, crmOrgId, item.cf_853);

                CRMUserReview review = new CRMUserReview();
                if (reviewList.Any())
                {
                    review = reviewList.First();
                }

                model.ClientId = this.userService.GetContactByCrmId(crmContactId).Id;
                model.Rating = review.Rating;
                model.Review = review.FeedbackText;
                model.CountryDet = this.commonService.GetUserCountryDetailsFromName(item.mailingcountry);
                lstPurchasedClientsDto.Add(model);
            }

            return this.Ok(lstPurchasedClientsDto);
        }

        /// <summary>
        /// Deletes the opportunity.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="interest">The interest</param>
        /// <returns>IHttpActionResult object.</returns>
        [Route("deleteclient")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteOpportunity(string userId, string interest)
        {
            string companyId = User.Identity.GetUserId();
            string crmOrgId = this.youfferContactService.GetOrgCRMId(companyId).CRMId;
            string crmLeadId = this.youfferContactService.GetMappingEntryByContactId(userId).LeadId;
            string crmOppId = this.youfferLeadService.GetMappingEntryByLeadAndOrgCRMIdAndInterest(crmLeadId, crmOrgId, interest).OpportunityId;

            bool isSuccess = this.crmManagerService.DeleteOpportunity(crmOppId);
            return this.Ok(isSuccess);
        }

        /// <summary>
        /// Searches the client.
        /// </summary>
        /// <param name="searchModel">The search model.</param>
        /// <returns>IHttpActionResult object.</returns>
        [Route("searchclient")]
        [HttpPost]
        public async Task<IHttpActionResult> SearchClient(SearchModelDto searchModel)
        {
            string companyId = this.User.Identity.GetUserId();
            if (searchModel.FetchCount == 0)
            {
                searchModel.FetchCount = AppSettings.Get<int>(ConfigConstants.DashboardLeadsDefaultCount);
            }

            if (!string.IsNullOrEmpty(searchModel.SubInterestName))
            {
                List<string> parentBusinessType = this.youfferInterestService.GetAllParentBusinessTypes();
                searchModel.InterestName = parentBusinessType.Single(x => x == searchModel.SubInterestName);
                searchModel.SubInterestName = string.Empty;
            }

            List<VTigerLead> lst = this.crmManagerService.SearchLeads(searchModel);

            OrganisationModel orgModel = this.crmManagerService.GetOrganisation(companyId);
            string orgCRMId = this.youfferContactService.GetOrgCRMId(companyId).CRMId;
            List<UserResultModel> lstUserResultModel = new List<UserResultModel>();
            foreach (var item in lst)
            {
                bool isUserBlocked = this.crmManagerService.CheckIfUserBlocked(item.cf_773, orgCRMId);
                if (!isUserBlocked)
                {
                    try
                    {
                        UserResultModel userResultModel = this.mapperFactory.GetMapper<VTigerLead, UserResultModel>().Map(item);
                        userResultModel.Id = this.youfferContactService.GetMappingEntryByCrmLeadId(item.id).ContactId;
                        string purchasedForInt = string.Empty;

                        string[] mainInt = item.cf_769[0].Split(new string[] { " |##| " }, StringSplitOptions.None).Where(x => !string.IsNullOrEmpty(x)).ToArray();
                        string[] subInt = item.cf_771[0].Split(new string[] { " |##| " }, StringSplitOptions.None).Where(x => !string.IsNullOrEmpty(x)).ToArray();

                        List<LeadOpportunityMappingDto> lstLeadOppMapping = this.youfferLeadService.GetMappingEntryByLeadAndOrgCRMId(item.id.ToString(), orgCRMId);

                        foreach (var itemLstLeadOppMapping in lstLeadOppMapping)
                        {
                            purchasedForInt = itemLstLeadOppMapping.Interest;
                            subInt = subInt.Where(val => val != purchasedForInt).ToArray();
                        }

                        string[] subIntArr = new string[] { };
                        if (!string.IsNullOrEmpty(searchModel.SubInterestName))
                        {
                            subIntArr = subInt.FirstOrDefault(x => x.ToLower().Contains(searchModel.SubInterestName.ToLower())) == null ? new string[] { } : subInt.FirstOrDefault(x => x.ToLower().Contains(searchModel.SubInterestName.ToLower())).Split(',').ToArray();
                        }

                        string[] mainIntArr = new string[] { };
                        if (!string.IsNullOrEmpty(searchModel.InterestName))
                        {
                            mainIntArr = mainInt.FirstOrDefault(x => x.ToLower().Contains(searchModel.InterestName.ToLower())) == null ? new string[] { } : mainInt.FirstOrDefault(x => x.ToLower().Contains(searchModel.InterestName.ToLower())).Split(',').ToArray();
                            if (subInt.Contains(mainIntArr[0]))
                            {
                                subIntArr = mainIntArr;
                            }
                            else
                            {
                                string[] children = this.youfferInterestService.GetSubBusinessTypeFromMain(mainIntArr[0]).Select(x => x.BusinessTypeName).Distinct().ToArray();
                                var common = children.Intersect(subInt);
                                subIntArr = common.ToArray();
                            }
                        }

                        if (subInt.Length > 0 && subIntArr.Length > 0)
                        {
                            userResultModel.CountryDetails = this.commonService.GetUserCountryDetailsFromName(item.country);
                            userResultModel.Rank = this.crmManagerService.GetUserRank(item.cf_773);

                            userResultModel.Availability = string.IsNullOrWhiteSpace(item.Availability.ToString()) ? Availability.Undefined : (Availability)Enum.Parse(typeof(Availability), item.Availability.ToString());
                            userResultModel.OSType = string.IsNullOrWhiteSpace(item.OSType.ToString()) ? OSType.Undefined : (OSType)Enum.Parse(typeof(OSType), item.OSType.ToString());
                            userResultModel.UserRole = Roles.Customer;
                            userResultModel.IsActive = item.cf_767;
                            userResultModel.MainInterest = new string[] { };
                            userResultModel.SubInterest = subIntArr;
                            userResultModel.PaymentDetails = new PaymentModelDto()
                            {
                                PayPalId = item.cf_851,
                                Mode = string.IsNullOrWhiteSpace(item.cf_849) ? PaymentMode.Undefined : (PaymentMode)Enum.Parse(typeof(PaymentMode), item.cf_849.ToString())
                            };

                            lstUserResultModel.Add(userResultModel);
                        }
                    }
                    catch (Exception ex)
                    {
                        this.LoggerService.LogException("Search Client Looping : " + ex.Message);
                    }
                }
            }

            this.LoggerService.LogException("Search Client:" + orgModel.AccountName + " -json-" + JsonConvert.SerializeObject(searchModel));
            return this.Ok(lstUserResultModel);
        }

        /// <summary>
        /// Searches the client.
        /// </summary>
        /// <param name="searchModel">The search model.</param>
        /// <returns>IHttpActionResult object.</returns>
        [Route("searchclient1")]
        [HttpPost]
        public async Task<IHttpActionResult> SearchClient1(SearchModelDto searchModel)
        {
            string companyId = this.User.Identity.GetUserId();
            string orgCRMId = this.youfferContactService.GetOrgCRMId(companyId).CRMId;
            if (searchModel.FetchCount == 0)
            {
                searchModel.FetchCount = AppSettings.Get<int>(ConfigConstants.DashboardLeadsDefaultCount);
            }

            OrganisationModel orgModel = this.crmManagerService.GetOrganisation(companyId);

            if (!string.IsNullOrEmpty(searchModel.SubInterestName))
            {
                List<string> parentBusinessType = this.youfferInterestService.GetAllParentBusinessTypes();
                try
                {
                    IEnumerable<string> interestNames = parentBusinessType.Where(x => x.ToLower() == searchModel.SubInterestName.ToLower());
                    if (!string.IsNullOrEmpty(interestNames.FirstOrDefault()))
                    {
                        searchModel.InterestName = interestNames.FirstOrDefault();
                        searchModel.SubInterestName = string.Empty;
                    }
                }
                catch
                {
                }
            }

            List<VTigerDashBoardData> lst = this.crmManagerService.GetSearchedLeads(orgModel.Id, searchModel);
            List<UserResultModel> lstUserResultModel = new List<UserResultModel>();
            string purchasedForInt = string.Empty;
            foreach (var item in lst)
            {
                try
                {
                    string[] mainInt = item.cf_769.Split(new string[] { " |##| " }, StringSplitOptions.None).Where(x => !string.IsNullOrEmpty(x)).ToArray();
                    string[] subInt = item.cf_771.Split(new string[] { " |##| " }, StringSplitOptions.None).Where(x => !string.IsNullOrEmpty(x)).ToArray();

                    string[] subIntArr = new string[] { };
                    if (!string.IsNullOrEmpty(searchModel.SubInterestName))
                    {
                        subIntArr = subInt.FirstOrDefault(x => x.ToLower().Contains(searchModel.SubInterestName.ToLower())) == null ? new string[] { } : subInt.FirstOrDefault(x => x.ToLower().Contains(searchModel.SubInterestName.ToLower())).Split(',').ToArray();
                    }

                    string[] mainIntArr = new string[] { };
                    if (!string.IsNullOrEmpty(searchModel.InterestName))
                    {
                        mainIntArr = mainInt.FirstOrDefault(x => x.ToLower().Contains(searchModel.InterestName.ToLower())) == null ? new string[] { } : mainInt.FirstOrDefault(x => x.ToLower().Contains(searchModel.InterestName.ToLower())).Split(',').ToArray();
                        if (subInt.Contains(mainIntArr[0]))
                        {
                            subIntArr = mainIntArr;
                        }
                        else
                        {
                            string[] children = this.youfferInterestService.GetSubBusinessTypeFromMain(mainIntArr[0]).Select(x => x.BusinessTypeName).Distinct().ToArray();
                            var common = children.Intersect(subInt);
                            subIntArr = common.ToArray();
                        }
                    }

                    if (subIntArr.Length > 0)
                    {
                        UserResultModel userResultModel = this.mapperFactory.GetMapper<VTigerDashBoardData, UserResultModel>().Map(item);
                        userResultModel.Id = this.youfferContactService.GetMappingEntryByCrmLeadId(AppSettings.Get<string>(ConfigConstants.LeadId) + item.leadid).ContactId;
                        userResultModel.CountryDetails = this.commonService.GetUserCountryDetailsFromName(item.country);
                        userResultModel.UserRole = Roles.Customer;
                        userResultModel.Phone = userResultModel.Phone ?? string.Empty;
                        userResultModel.MainInterest = new string[] { };
                        userResultModel.SubInterest = subIntArr;
                        userResultModel.PaymentDetails = new PaymentModelDto()
                        {
                            PayPalId = item.cf_851,
                            Mode =
                                string.IsNullOrWhiteSpace(item.cf_849)
                                    ? PaymentMode.Undefined
                                    : (PaymentMode)Enum.Parse(typeof(PaymentMode), item.cf_849)
                        };

                        lstUserResultModel.Add(userResultModel);
                    }
                }
                catch (Exception ex)
                {
                    this.LoggerService.LogException("Search Client Looping : " + ex.Message);
                }
            }

            this.LoggerService.LogException("Search Client:" + orgModel.AccountName + " -json-" + JsonConvert.SerializeObject(searchModel));
            return this.Ok(lstUserResultModel);
        }

        /// <summary>
        /// Gets the dashboard.
        /// </summary>
        /// <param name="lastPageId">The last page identifier.</param>
        /// <param name="fetchCnt">The fetch count.</param>
        /// <param name="interest"> The interest name. </param>
        /// <returns>IHttpActionResult object.</returns>
        [Route("getdashboard")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDashboard(int lastPageId = 0, int fetchCnt = 0, string interest = "")
        {
            if (fetchCnt == 0)
            {
                fetchCnt = AppSettings.Get<int>(ConfigConstants.DashboardLeadsDefaultCount);
            }

            string companyId = User.Identity.GetUserId();
            List<UserResultModel> lstUserResultModel = new List<UserResultModel>();

            OrganisationModel orgModel = this.crmManagerService.GetOrganisation(companyId);
            string orgCRMId = this.youfferContactService.GetOrgCRMId(companyId).CRMId;
            List<VTigerLead> lst = this.crmManagerService.GetDashboard(orgModel, lastPageId, fetchCnt, interest);

            foreach (var item in lst)
            {
                try
                {
                    bool isUserBlocked = this.crmManagerService.CheckIfUserBlocked(item.cf_773, orgCRMId);
                    if (!isUserBlocked)
                    {
                        UserResultModel userResultModel = this.mapperFactory.GetMapper<VTigerLead, UserResultModel>().Map(item);
                        userResultModel.Id = this.youfferContactService.GetMappingEntryByCrmLeadId(item.id).ContactId;
                        string purchasedForInt = string.Empty;
                        string[] subInt = item.cf_771[0].Split(new string[] { " |##| " }, StringSplitOptions.None).Where(x => !string.IsNullOrEmpty(x)).ToArray();

                        List<LeadOpportunityMappingDto> lstLeadOppMapping = this.youfferLeadService.GetMappingEntryByLeadAndOrgCRMId(item.id.ToString(), orgCRMId);

                        foreach (var itemLstLeadOppMapping in lstLeadOppMapping)
                        {
                            purchasedForInt = itemLstLeadOppMapping.Interest;
                            subInt = subInt.Where(val => val != purchasedForInt).ToArray();
                        }

                        bool isInterestExists = false;
                        foreach (string intName in subInt)
                        {
                            isInterestExists = orgModel.SubBusinessType.Contains(intName);
                            if (isInterestExists)
                            {
                                subInt = subInt.Intersect(orgModel.SubBusinessType).ToArray();
                                break;
                            }
                        }

                        if (subInt.Length > 0 && isInterestExists)
                        {
                            userResultModel.CountryDetails = this.commonService.GetUserCountryDetailsFromName(item.country);
                            userResultModel.Rank = this.crmManagerService.GetUserRank(item.cf_773);

                            userResultModel.Availability = string.IsNullOrWhiteSpace(item.cf_813.ToString()) ? Availability.Undefined : (Availability)Enum.Parse(typeof(Availability), item.cf_813.ToString());
                            userResultModel.OSType = string.IsNullOrWhiteSpace(item.cf_791.ToString()) ? OSType.Undefined : (OSType)Enum.Parse(typeof(OSType), item.cf_791.ToString());
                            userResultModel.UserRole = Roles.Customer;
                            userResultModel.IsActive = item.cf_767;
                            userResultModel.MainInterest = item.cf_769[0].Split(new string[] { " |##| " }, StringSplitOptions.None).Where(x => !string.IsNullOrEmpty(x)).ToArray();
                            userResultModel.SubInterest = subInt;
                            userResultModel.Price = AppSettings.Get<double>(ConfigConstants.UserPrice);
                            userResultModel.PaymentDetails = new PaymentModelDto()
                                {
                                    PayPalId = item.cf_851.ToString(),
                                    Mode = string.IsNullOrWhiteSpace(item.cf_849.ToString()) ? PaymentMode.Undefined : (PaymentMode)Enum.Parse(typeof(PaymentMode), item.cf_849.ToString())
                                };

                            lstUserResultModel.Add(userResultModel);
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.LoggerService.LogException("Get Dashboard Looping :- " + ex.Message);
                }
            }

            return this.Ok(lstUserResultModel);
        }

        /// <summary>
        /// Gets the dashboard.
        /// </summary>
        /// <param name="lastPageId">The last page identifier.</param>
        /// <param name="fetchCnt">The fetch count.</param>
        /// <param name="interest"> The interest name. </param>
        /// <returns>IHttpActionResult object.</returns>
        [Route("getdashboard1")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDashboard1(int lastPageId = 0, int fetchCnt = 0, string interest = "")
        {
            if (fetchCnt == 0)
            {
                fetchCnt = AppSettings.Get<int>(ConfigConstants.DashboardLeadsDefaultCount);
            }

            string companyId = User.Identity.GetUserId();
            List<UserResultModel> lstUserResultModel = new List<UserResultModel>();
            string orgCrmId = this.youfferContactService.GetOrgCRMId(companyId).CRMId;
            if (!string.IsNullOrWhiteSpace(orgCrmId))
            {
                List<VTigerDashBoardData> lst = this.crmManagerService.GetNewDashboard(orgCrmId, lastPageId, fetchCnt);
                foreach (var item in lst)
                {
                    try
                    {
                        UserResultModel userResultModel = this.mapperFactory.GetMapper<VTigerDashBoardData, UserResultModel>().Map(item);
                        userResultModel.CountryDetails = this.commonService.GetUserCountryDetailsFromName(item.country);
                        userResultModel.Id = this.youfferContactService.GetMappingEntryByCrmLeadId(AppSettings.Get<string>(ConfigConstants.LeadId) + item.id).ContactId;
                        userResultModel.UserRole = Roles.Customer;
                        userResultModel.Price = AppSettings.Get<double>(ConfigConstants.UserPrice);
                        userResultModel.PaymentDetails = new PaymentModelDto()
                        {
                            PayPalId = item.cf_851,
                            Mode = string.IsNullOrWhiteSpace(item.cf_849) ? PaymentMode.Undefined : (PaymentMode)Enum.Parse(typeof(PaymentMode), item.cf_849)
                        };

                        lstUserResultModel.Add(userResultModel);
                    }
                    catch (Exception ex)
                    {
                        var a = ex.Message;
                    }
                }
            }

            return this.Ok(lstUserResultModel);
        }

        /// <summary>
        /// Searches the client for review page.
        /// </summary>
        /// <param name="searchText">The search text.</param>
        /// <param name="lastpageId">The last page id. </param>
        /// <param name="fetchCount">The fetch count.</param>
        /// <returns>IHttpActionResult object.</returns>
        [Route("searchclientreviewpage")]
        [HttpGet]
        public async Task<IHttpActionResult> SearchClientForReviewPage(string searchText, int lastpageId = 0, int fetchCount = 0)
        {
            if (fetchCount == 0)
            {
                fetchCount = AppSettings.Get<int>(ConfigConstants.DashboardLeadsDefaultCount);
            }

            if (searchText != null)
            {
                searchText = HttpUtility.UrlDecode(searchText);
            }

            List<UserResultModel> lstUserResultModel = new List<UserResultModel>();
            string companyId = User.Identity.GetUserId();
            List<VTigerPotential> lst = this.crmManagerService.SearchClientForReviewPage(searchText, companyId, lastpageId, fetchCount);

            foreach (var item in lst)
            {
                string userId = this.userService.GetContactByCrmId(item.contact_id).Id;

                if (!string.IsNullOrWhiteSpace(userId))
                {
                    ContactModel contactModel = this.crmManagerService.GetContact(userId);
                    bool isContionTrue = false;
                    if (!string.IsNullOrWhiteSpace(searchText))
                    {
                        if (!string.IsNullOrWhiteSpace(contactModel.FirstName) && contactModel.FirstName.ToLowerString().Contains(searchText.ToLower()))
                        {
                            isContionTrue = true;
                        }
                    }
                    else
                    {
                        isContionTrue = true;
                    }

                    if (isContionTrue)
                    {
                        CountryModel countryDet = this.commonService.GetUserCountryDetailsFromName(contactModel.MailingCountry);
                        decimal rank = this.crmManagerService.GetUserRank(item.contact_id);

                        UserResultModel result = new UserResultModel();
                        result = this.mapperFactory.GetMapper<ContactModel, UserResultModel>().Map(contactModel);
                        result.Id = userId;
                        result.CreatedOn = item.createdtime;
                        result.SubInterest = item.cf_853.Split(',');
                        result.CountryDetails = countryDet;
                        result.Rank = rank;
                        result.CanCall = item.cf_843;
                        lstUserResultModel.Add(result);
                    }
                }
            }

            return this.Ok(lstUserResultModel);
        }

        /// <summary>
        /// Searches the client for review page.
        /// </summary>
        /// <param name="searchText">The search text.</param>
        /// <param name="lastpageId">The last page id. </param>
        /// <param name="fetchCount">The fetch count.</param>
        /// <returns>IHttpActionResult object.</returns>
        [Route("searchclientreviewpage1")]
        [HttpGet]
        public async Task<IHttpActionResult> SearchClientForReviewPage1(string searchText, int lastpageId = 0, int fetchCount = 0)
        {
            if (fetchCount == 0)
            {
                fetchCount = AppSettings.Get<int>(ConfigConstants.DashboardLeadsDefaultCount);
            }

            if (searchText != null)
            {
                searchText = HttpUtility.UrlDecode(searchText);
            }

            List<UserResultModel> lstUserResultModel = new List<UserResultModel>();
            string companyId = User.Identity.GetUserId();
            List<VtigerPotentialData> lst = this.crmManagerService.GetUnReviewdClients1(companyId, lastpageId, fetchCount, searchText);

            foreach (var item in lst)
            {
                UserResultModel userResultModel = new UserResultModel();
                string userId = this.userService.GetContactByCrmId(AppSettings.Get<string>(ConfigConstants.ContactId) + item.contactid).Id;

                if (userId != null)
                {
                    CountryModel countryDet = this.commonService.GetUserCountryDetailsFromName(item.mailingcountry);
                    userResultModel = this.mapperFactory.GetMapper<VtigerPotentialData, UserResultModel>().Map(item);
                    userResultModel.Id = userId;
                    userResultModel.Description = userResultModel.Description ?? string.Empty;
                    userResultModel.SubInterest = item.cf_853.Split(',');
                    userResultModel.CountryDetails = countryDet;
                    userResultModel.UserRole = Roles.Customer;
                    userResultModel.PaymentDetails = new PaymentModelDto
                    {
                        PayPalId = item.cf_847,
                        Mode = string.IsNullOrWhiteSpace(item.cf_847) ? PaymentMode.Undefined : PaymentMode.Paypal
                    };
                }

                lstUserResultModel.Add(userResultModel);
            }

            return this.Ok(lstUserResultModel);
        }

        /// <summary>
        /// Searches the client for my client page.
        /// </summary>
        /// <param name="searchText">The search text.</param>
        /// <param name="lastpageId">The last page id.</param>
        /// <param name="fetchCount">The fetch count.</param>
        /// <returns>IHttpActionResult object.</returns>
        [Route("searchclientmyclientpage")]
        [HttpGet]
        public async Task<IHttpActionResult> SearchClientForMyClientPage(string searchText, int lastpageId = 0, int fetchCount = 0)
        {
            if (fetchCount == 0)
            {
                fetchCount = AppSettings.Get<int>(ConfigConstants.DashboardLeadsDefaultCount);
            }

            if (searchText != null)
            {
                searchText = HttpUtility.UrlDecode(searchText);
            }

            List<UserResultModel> lstUserResultModel = new List<UserResultModel>();
            string companyId = User.Identity.GetUserId();
            List<VTigerPotential> lst = this.crmManagerService.SearchClientForMyClientPage(searchText, companyId, lastpageId, fetchCount);

            foreach (var item in lst)
            {
                string userId = this.userService.GetContactByCrmId(item.contact_id).Id;
                if (!string.IsNullOrWhiteSpace(userId))
                {
                    ContactModel contactModel = this.crmManagerService.GetContact(userId);
                    bool isContionTrue = false;
                    if (!string.IsNullOrWhiteSpace(searchText))
                    {
                        if (!string.IsNullOrWhiteSpace(contactModel.FirstName) && contactModel.FirstName.ToLowerString().Contains(searchText.ToLower()))
                        {
                            isContionTrue = true;
                        }

                        if (string.Compare(contactModel.Gender, searchText.Capitalize(), StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            isContionTrue = true;
                        }

                        if (!string.IsNullOrWhiteSpace(contactModel.Phone) && contactModel.Phone.Contains(searchText))
                        {
                            isContionTrue = true;
                        }
                    }
                    else
                    {
                        isContionTrue = true;
                    }

                    if (isContionTrue)
                    {
                        CountryModel countryDet = this.commonService.GetUserCountryDetailsFromName(contactModel.MailingCountry);
                        decimal rank = this.crmManagerService.GetUserRank(item.contact_id);

                        var result = this.mapperFactory.GetMapper<ContactModel, UserResultModel>().Map(contactModel);
                        result.Id = userId;
                        result.SubInterest = item.cf_853.Split(',');
                        result.CountryDetails = countryDet;
                        result.Rank = rank;
                        result.CanCall = item.cf_843;
                        result.MarkPurchased = item.cf_857;
                        lstUserResultModel.Add(result);
                    }
                }
            }

            return this.Ok(lstUserResultModel);
        }

        /// <summary>
        /// Searches the client for my client page.
        /// </summary>
        /// <param name="searchText">The search text.</param>
        /// <param name="lastpageId">The last page id.</param>
        /// <param name="fetchCount">The fetch count.</param>
        /// <returns>IHttpActionResult object.</returns>
        [Route("searchclientmyclientpage1")]
        [HttpGet]
        public async Task<IHttpActionResult> SearchClientForMyClientPage1(string searchText, int lastpageId = 0, int fetchCount = 0)
        {
            if (fetchCount == 0)
            {
                fetchCount = AppSettings.Get<int>(ConfigConstants.DashboardLeadsDefaultCount);
            }

            if (searchText != null)
            {
                searchText = HttpUtility.UrlDecode(searchText);
            }

            List<UserResultModel> lstUserResultModel = new List<UserResultModel>();
            string companyId = User.Identity.GetUserId();
            string crmOrgId = this.userService.GetContact(companyId).CRMId;
            List<VtigerPotentialData> lst = this.crmManagerService.GetMyClients(crmOrgId, searchText, lastpageId, fetchCount);

            foreach (var item in lst)
            {
                UserResultModel result = this.mapperFactory.GetMapper<VtigerPotentialData, UserResultModel>().Map(item);
                string userId = this.userService.GetContactByCrmId(AppSettings.Get<string>(ConfigConstants.ContactId) + item.contactid).Id;
                CountryModel countryDet = this.commonService.GetUserCountryDetailsFromName(item.mailingcountry);
                result.Id = userId;
                result.SubInterest = item.cf_853.Split(',');
                result.CountryDetails = countryDet;
                lstUserResultModel.Add(result);
            }

            return this.Ok(lstUserResultModel);
        }

        /// <summary>
        /// Gets the un reviewed clients.
        /// </summary>
        /// <param name="lastpageId">The last page id.</param>
        /// <param name="fetchCount">The fetch count.</param>
        /// <returns>>IHttpActionResult object.</returns>
        [Route("unreviewedclients")]
        [HttpGet]
        public async Task<IHttpActionResult> GetUnReviewedClients(int lastpageId = 0, int fetchCount = 0)
        {
            if (fetchCount == 0)
            {
                fetchCount = AppSettings.Get<int>(ConfigConstants.DashboardLeadsDefaultCount);
            }

            string companyId = User.Identity.GetUserId();
            List<UserResultModel> lstUserResultModel = new List<UserResultModel>();

            List<VTigerPotential> lst = this.crmManagerService.GetUnReviewdClients(companyId, lastpageId, fetchCount);

            foreach (var item in lst)
            {
                UserResultModel userResultModel = new UserResultModel();
                string userId = this.userService.GetContactByCrmId(item.contact_id).Id;

                if (userId != null)
                {
                    ContactModel contactModel = this.crmManagerService.GetContact(userId);
                    CountryModel countryDet = this.commonService.GetUserCountryDetailsFromName(contactModel.MailingCountry);
                    decimal rank = this.crmManagerService.GetUserRank(item.contact_id);

                    userResultModel = this.mapperFactory.GetMapper<ContactModel, UserResultModel>().Map(contactModel);
                    userResultModel.Id = userId;
                    userResultModel.Rank = rank;
                    userResultModel.SubInterest = item.cf_853.Split(',');
                    userResultModel.CountryDetails = countryDet;
                    userResultModel.UserRole = Roles.Customer;
                    userResultModel.CreatedOn = item.createdtime;
                    userResultModel.CanCall = item.cf_843;
                    userResultModel.PaymentDetails = new PaymentModelDto()
                        {
                            PayPalId = contactModel.PaypalId,
                            Mode = contactModel.PaymentMode
                        };
                }

                lstUserResultModel.Add(userResultModel);
            }

            return this.Ok(lstUserResultModel);
        }

        /// <summary>
        /// Gets the un reviewed clients.
        /// </summary>
        /// <param name="lastpageId">The last page id.</param>
        /// <param name="fetchCount">The fetch count.</param>
        /// <returns>>IHttpActionResult object.</returns>
        [Route("unreviewedclients1")]
        [HttpGet]
        public async Task<IHttpActionResult> GetUnReviewedClients1(int lastpageId = 0, int fetchCount = 0)
        {
            if (fetchCount == 0)
            {
                fetchCount = AppSettings.Get<int>(ConfigConstants.DashboardLeadsDefaultCount);
            }

            string companyId = User.Identity.GetUserId();
            List<UserResultModel> lstUserResultModel = new List<UserResultModel>();

            List<VtigerPotentialData> lst = this.crmManagerService.GetUnReviewdClients1(companyId, lastpageId, fetchCount);

            foreach (var item in lst)
            {
                UserResultModel userResultModel = new UserResultModel();
                string userId = this.userService.GetContactByCrmId(AppSettings.Get<string>(ConfigConstants.ContactId) + item.contactid).Id;

                if (userId != null)
                {
                    CountryModel countryDet = this.commonService.GetUserCountryDetailsFromName(item.mailingcountry);
                    userResultModel = this.mapperFactory.GetMapper<VtigerPotentialData, UserResultModel>().Map(item);
                    userResultModel.Id = userId;
                    userResultModel.Description = userResultModel.Description ?? string.Empty;
                    userResultModel.SubInterest = item.cf_853.Split(',');
                    userResultModel.CountryDetails = countryDet;
                    userResultModel.UserRole = Roles.Customer;
                    userResultModel.PaymentDetails = new PaymentModelDto
                    {
                        PayPalId = item.cf_847,
                        Mode = string.IsNullOrWhiteSpace(item.cf_847) ? PaymentMode.Undefined : PaymentMode.Paypal
                    };
                }

                lstUserResultModel.Add(userResultModel);
            }

            return this.Ok(lstUserResultModel);
        }

        /// <summary>
        /// Gets the un reviewed clients.
        /// </summary>
        /// <returns>>IHttpActionResult object.</returns>
        [Route("unreviewedclientscount")]
        [HttpGet]
        public async Task<IHttpActionResult> GetUnReviewedClientsCnt()
        {
            string companyId = User.Identity.GetUserId();
            List<UserResultModel> lstUserResultModel = new List<UserResultModel>();
            UnreviewedClientsCountDto unreviewedClientsCountDto = new UnreviewedClientsCountDto();
            List<VTigerPotential> lst = this.crmManagerService.GetUnReviewdClients(companyId, 1, int.MaxValue);
            int count = 0;

            foreach (var item in lst)
            {
                UserResultModel userResultModel = new UserResultModel();
                string userId = this.userService.GetContactByCrmId(item.contact_id).Id;

                if (userId != null)
                {
                    count++;
                }
            }

            unreviewedClientsCountDto.Count = count;
            return this.Ok(unreviewedClientsCountDto);
        }

        /// <summary>
        /// Gets the un reviewed clients.
        /// </summary>
        /// <param name="searchText"> THe search text </param>
        /// <returns>>IHttpActionResult object.</returns>
        [Route("unreviewedclientscount1")]
        [HttpGet]
        public async Task<IHttpActionResult> GetUnReviewedClientsCnt1(string searchText = "")
        {
            string companyId = User.Identity.GetUserId();
            UnreviewedClientsCountDto unreviewedClientsCountDto = new UnreviewedClientsCountDto
            {
                Count = this.crmManagerService.GetUnReviewdClientsCount(companyId, searchText)
            };

            return this.Ok(unreviewedClientsCountDto);
        }

        /// <summary>
        /// Gets the statistics.
        /// </summary>
        /// <returns>IHttpActionResult object.</returns>
        [Route("get-statistics")]
        [HttpGet]
        public async Task<IHttpActionResult> GetStatistics()
        {
            string companyId = User.Identity.GetUserId();
            List<StatisticsModelDto> lstStatsModel = new List<StatisticsModelDto>();
            string orgCRMId = this.youfferContactService.GetOrgCRMId(companyId).CRMId;

            lstStatsModel = this.crmManagerService.GetStatistics(orgCRMId);

            return this.Ok(lstStatsModel);
        }

        /// <summary>
        /// Sets the company feedback.
        /// </summary>
        /// <param name="feedback">The feedback.</param>
        /// <returns>IHttpActionResult object.</returns>
        [Route("reportuser")]
        [HttpPost]
        public async Task<IHttpActionResult> ReportUser(FeedbackDto feedback)
        {
            string companyId = User.Identity.GetUserId();

            List<ApplicationUserDto> appUsers = this.userService.GetUsers(feedback.ToId, companyId);
            string contactId = appUsers.Where(x => x.Id == feedback.ToId).Select(x => x.CRMId).First().ToString();
            string organisationId = appUsers.Where(x => x.Id == companyId).Select(x => x.CRMId).First().ToString();

            feedback.FromId = feedback.CreatedBy = companyId;
            feedback.FeedbackId = Convert.ToInt32(Feedback.ReportUser);
            feedback = this.youfferFeedbackService.SaveFeedback(feedback);

            StatusMessage status = new StatusMessage();
            status.IsSuccess = this.crmManagerService.ReportUser(contactId, organisationId);

            ContactModel contact = this.crmManagerService.GetContact(feedback.ToId);
            OrganisationModel org = this.crmManagerService.GetOrganisation(companyId);
            if (!string.IsNullOrEmpty(contact.GCMId))
            {
                this.pushMessageService.SendReportUserNotificationToAndroid(contact.GCMId, companyId, org.AccountName, "Report User", Notification.reportuser.ToString());
            }

            if (!string.IsNullOrEmpty(contact.UDId))
            {
                this.pushMessageService.SendReportUserNotificationToiOS(contact.UDId, companyId, org.AccountName, "Report User", Notification.reportuser.ToString());
            }

            CRMNotifications notification = new CRMNotifications()
            {
                OrganisationsId = organisationId,
                ContactId = contactId,
                NotificationType = ConfigConstants.OrganisationComplainedContact
            };

            this.crmManagerService.CreateNotification(notification);

            return this.Ok(feedback);
        }

        /// <summary>
        /// Gets all company reviews.
        /// </summary>  
        /// <param name="lastPageId">The lastPageId identifier.</param>
        /// <param name="fetchCount">The fetch count.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="direction">The direction.</param>
        /// <returns>
        /// List of UserReviewsDto object.
        /// </returns>
        [Route("company-reviews")]
        [HttpGet]
        public async Task<IHttpActionResult> GetReviews(int lastPageId = 0, int fetchCount = 0, string sortBy = "", string direction = "asc")
        {
            if (fetchCount == 0)
            {
                fetchCount = AppSettings.Get<int>(ConfigConstants.ReviewsDefaultCount);
            }

            string companyId = User.Identity.GetUserId();
            string crmOrgId = this.userService.GetContact(companyId).CRMId;

            var reviews = this.crmManagerService.GetReviewsForCompany(crmOrgId, lastPageId, fetchCount, sortBy, direction);
            List<UserReviewsDto> userReviews = this.mapperFactory.GetMapper<List<CRMUserReview>, List<UserReviewsDto>>().Map(reviews);
            return this.Ok(userReviews);
        }

        /// <summary>
        /// Deletes message thread.
        /// </summary>
        /// <param name="threadId">The thread identifier.</param>
        /// <returns>IHttpActionResult object.</returns>
        [Route("deletethread")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteMessageThread(int threadId)
        {
            bool isSuccess = true;
            string companyId = User.Identity.GetUserId();
            string orgCRMId = this.youfferContactService.GetOrgCRMId(companyId).CRMId;

            MessageThreadDto msgThread = this.youfferMessageService.GetMessageThread(companyId, "YoufferAdmin");
            if (msgThread != null)
            {
                this.crmManagerService.MarkContactUsMessagesDeleted(orgCRMId);
            }

            isSuccess = this.youfferMessageService.DeleteThread(threadId, companyId);
            return this.Ok(isSuccess);
        }

        /// <summary>
        /// Creates the contact us message.
        /// </summary>
        /// <param name="contactUs">The contact us.</param>
        /// <returns>IHttpActionResult object.</returns>
        [Route("contactusmessage")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateContactUsMessage(ContactUsDto contactUs)
        {
            contactUs.UserId = contactUs.CreatedBy = contactUs.ModifiedBy = User.Identity.GetUserId();
            string orgCRMId = this.youfferContactService.GetOrgCRMId(contactUs.UserId).CRMId;

            long threadId = 0;
            CRMContactUs crmContactUsOld = this.crmManagerService.ReadContactUsMessage(orgCRMId);
            if (crmContactUsOld == null)
            {
                MessageThreadDto msgThread = this.youfferMessageService.GetMessageThread("YoufferAdmin", contactUs.UserId);
                if (msgThread == null)
                {
                    msgThread = new MessageThreadDto { FromUser = contactUs.UserId, ToUser = "YoufferAdmin", CreatedBy = contactUs.UserId, ModifiedBy = contactUs.UserId };
                    msgThread = this.youfferMessageService.CreateMessageThread(msgThread);
                    threadId = msgThread.Id;
                }
                else
                {
                    threadId = msgThread.Id;
                }
            }
            else
            {
                threadId = crmContactUsOld.ThreadId;
            }

            CRMContactUs crmContactUs = new CRMContactUs()
            {
                Subject = contactUs.Subject,
                Description = contactUs.Description,
                Department = contactUs.DeptId.ToString(),
                IsIncomingMessage = true,
                IsDeleted = false,
                UserId = orgCRMId,
                ThreadId = threadId
            };

            if (contactUs.DeptId == ContactUsDept.Refund)
            {
                OrganisationModel org = this.crmManagerService.GetOrganisation(contactUs.UserId);
                crmContactUs.Assigned_User_Id = org.Assigned_User_Id;
            }

            crmContactUs = this.crmManagerService.CreateContactUsMessage(crmContactUs);
            if (!string.IsNullOrWhiteSpace(crmContactUs.Id))
            {
                contactUs.Id = Convert.ToInt64(crmContactUs.Id.Split('x')[1]);
            }

            StatusMessage res = new StatusMessage();
            res.IsSuccess = contactUs.Id > 0;

            return this.Ok(res);
        }

        /// <summary>
        /// Marks as purchased.
        /// </summary>
        /// <param name="markAsPurchased">The mark as purchased.</param>
        /// <returns>IHttpActionResult object.</returns>
        [Route("mark-purchased")]
        [HttpPost]
        public async Task<IHttpActionResult> MarkAsPurchased(MarkAsPurchasedDto markAsPurchased)
        {
            StatusMessage stat = new StatusMessage();

            string userId = markAsPurchased.UserId;
            string companyId = User.Identity.GetUserId();

            List<ApplicationUserDto> appUsers = this.userService.GetUsers(userId, companyId);
            string contactId = appUsers.Where(x => x.Id == userId).Select(x => x.CRMId).First().ToString();
            string organisationId = appUsers.Where(x => x.Id == companyId).Select(x => x.CRMId).First().ToString();
            string interestName = markAsPurchased.Interest;

            string leadId = this.youfferLeadService.GetMappingEntryByContactAndOrgCRMIdAndInterest(userId, organisationId, interestName).LeadId;

            ContactModel contact = this.crmManagerService.GetContact(userId);
            OrganisationModel org = this.crmManagerService.GetOrganisation(companyId);

            stat.IsSuccess = this.crmManagerService.MarkUserAsPurchased(userId, contactId, contact.FirstName, organisationId, interestName, leadId);

            if (!string.IsNullOrEmpty(contact.GCMId))
            {
                this.pushMessageService.SendMarkPurchasedNotificationToAndroid(contact.GCMId, org.AccountName, companyId, contact.FirstName, userId, interestName, "User Purchased", Notification.purchased.ToString());
            }

            if (!string.IsNullOrEmpty(contact.UDId))
            {
                this.pushMessageService.SendMarkPurchasedNotificationToiOS(contact.UDId, org.AccountName, companyId, string.Empty, userId, interestName, "User Purchased", Notification.purchased.ToString());
            }

            return this.Ok(stat);
        }

        /// <summary>
        /// Gets the payment configuration information.
        /// </summary>
        /// <param name="country">The country name</param>
        /// <returns>IHttpActionResult object.</returns>
        [Route("payment-config")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IHttpActionResult> GetPaymentConfigInfo(string country = "")
        {
            var list = this.commonService.GetPaymentConfigInfo(country);
            return this.Ok(list);
        }

        /// <summary>
        /// Updates the parent business types of organisations.
        /// </summary>
        /// <returns>IHttpActionResult object.</returns>
        [Route("updateparentbusinesstype")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IHttpActionResult> UpdateParentBusinessTypesOfOrganisations()
        {
            bool isSuccess = this.crmManagerService.UpdateParentBusinessTypesOfOrganisations();
            return this.Ok(isSuccess);
        }

        /// <summary>
        /// Gets the search options.
        /// </summary>
        /// <param name="searchText">The search text.</param>
        /// <returns>IHttpActionResult object.</returns>
        [Route("searchoptions")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IHttpActionResult> GetSearchOptions(string searchText)
        {
            if (string.IsNullOrEmpty(searchText))
            {
                searchText = string.Empty;
            }

            var list = this.youfferInterestService.GetSearchOptions(searchText);
            return this.Ok(list);
        }

        #region Private Methods

        /// <summary>
        /// Remove Extra Elements from The Search
        /// </summary>
        /// <param name="mainInterest"> MAin interest array</param>
        /// <param name="searchedInterest"> Searched interest array </param>
        /// <returns> string array </returns>
        private string[] RemoveExtraInterests(string[] mainInterest, string searchedInterest)
        {
            if (!string.IsNullOrWhiteSpace(searchedInterest))
            {
                string[] searchedList = searchedInterest.Split(',').ToArray();
                mainInterest = mainInterest.Intersect(searchedList).ToArray();
            }

            return mainInterest;
        }

        #endregion
    }
}