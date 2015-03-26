// ---------------------------------------------------------------------------------------------------
// <copyright file="UserController.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-14</date>
// <summary>
//     The UserController class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Common.LogService;
    using Microsoft.AspNet.Identity;
    using Youffer.Common.CRMService;
    using Youffer.Common.DataService;
    using Youffer.Common.Helper;
    using Youffer.Common.Mapper;
    using Youffer.Common.MaxmindGeoIP2;
    using Youffer.Common.Notification;
    using Youffer.Common.SMS;
    using Youffer.CRM;
    using Youffer.Framework.Notification;
    using Youffer.Resources.Constants;
    using Youffer.Resources.CRMModel;
    using Youffer.Resources.Enum;
    using Youffer.Resources.Models;
    using Youffer.Resources.ViewModel;
    using Youffer.Resources.ViewModel.MaxmindGeoIP2;

    /// <summary>
    /// The user controller.
    /// </summary>
    [RoutePrefix("api/user")]
    [Authorize(Roles = RoleName.CustomerRole)]
    public class UserController : BaseApiController
    {
        /// <summary>
        /// The IContactService service instance
        /// </summary>
        private readonly ICRMManagerService crmManagerService;

        /// <summary>
        /// The IUserService service instance
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// The IYoufferContactService service instance
        /// </summary>
        private readonly IYoufferContactService youfferContactService;

        /// <summary>
        /// The common service instance
        /// </summary>
        private readonly ICommonService commonService;

        /// <summary>
        /// The youffer payment service
        /// </summary>
        private readonly IYoufferPaymentService youfferPaymentService;

        /// <summary>
        /// The IYoufferFeedbackService service instance
        /// </summary>
        private readonly IYoufferFeedbackService youfferFeedbackService;

        /// <summary>
        /// The mapper factory
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// The vtiger service
        /// </summary>
        private readonly IVTigerService vTigerService;

        /// <summary>
        /// The youffer message service
        /// </summary>
        private readonly IYoufferMessageService youfferMessageService;

        /// <summary>
        /// The push message service
        /// </summary>
        private readonly IPushMessageService pushMessageService;

        /// <summary>
        /// The ip2location service
        /// </summary>
        private readonly IIP2LocationService ip2LocationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController" /> class.
        /// </summary>
        /// <param name="loggerService">The Logger service.</param>
        /// <param name="crmManagerService">The CRMManager service.</param>
        /// <param name="userService">The User service.</param>
        /// <param name="youfferContactService">The YoufferContact service.</param>
        /// <param name="commonService">The Common service.</param>
        /// <param name="youfferMessageService">The YoufferMessage service.</param>
        /// <param name="youfferFeedbackService">The youffer feedback service.</param>
        /// <param name="youfferPaymentService">The youffer payment service.</param>
        /// <param name="mapperFactory">The mapper factory.</param>
        /// <param name="vTigerService">the vtiger service. </param>
        /// <param name="pushMessageService">The push service.</param> 
        /// <param name="ip2LocationService">The IP2Location service.</param>
        public UserController(ILoggerService loggerService, ICRMManagerService crmManagerService, IUserService userService, IYoufferContactService youfferContactService, ICommonService commonService, IYoufferMessageService youfferMessageService, IYoufferFeedbackService youfferFeedbackService, IYoufferPaymentService youfferPaymentService, IMapperFactory mapperFactory, IVTigerService vTigerService, IPushMessageService pushMessageService, IIP2LocationService ip2LocationService)
            : base(loggerService)
        {
            this.crmManagerService = crmManagerService;
            this.userService = userService;
            this.youfferContactService = youfferContactService;
            this.commonService = commonService;
            this.youfferMessageService = youfferMessageService;
            this.youfferFeedbackService = youfferFeedbackService;
            this.youfferPaymentService = youfferPaymentService;
            this.mapperFactory = mapperFactory;
            this.vTigerService = vTigerService;
            this.pushMessageService = pushMessageService;
            this.ip2LocationService = ip2LocationService;
        }

        /// <summary>
        /// test method
        /// </summary>
        /// <returns> IHttpActionResult obj </returns>
        [Route("t")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> Test()
        {
            CityDto cityData = this.ip2LocationService.GetCityData("203.110.85.172");
            return this.Ok(cityData);
        }

        /// <summary>
        /// Gets the user details.
        /// </summary>
        /// <returns>IHttpActionResult object</returns>
        [Route("user-details")]
        [HttpGet]
        public async Task<IHttpActionResult> GetUserDetails()
        {
            string userId = User.Identity.GetUserId();
            UserResultModel userResultModel = new UserResultModel();
            LeadModel leadModel = new LeadModel();
            ContactModel contactModel = new ContactModel();
            string crmContactId = this.userService.GetContact(userId).CRMId;
            string crmLeadId = string.Empty;

            if (!string.IsNullOrEmpty(crmContactId))
            {
                crmLeadId = this.youfferContactService.GetMappingEntryByContactId(userId).LeadId;
            }

            CountryModel countryDet = this.commonService.GetUserCountryDetails(userId);
            decimal rank = this.crmManagerService.GetUserRank(crmContactId);

            if (!string.IsNullOrEmpty(crmLeadId))
            {
                leadModel = this.crmManagerService.GetLead(crmLeadId);
                userResultModel = this.mapperFactory.GetMapper<LeadModel, UserResultModel>().Map(leadModel);
                userResultModel.Id = userId;
                userResultModel.Rank = rank;
                userResultModel.CountryDetails = countryDet;
                userResultModel.UserRole = Roles.Customer;
                userResultModel.PaymentDetails = new PaymentModelDto()
                {
                    PayPalId = leadModel.PaypalId,
                    Mode = (PaymentMode)leadModel.PaymentMode
                };

                return this.Ok(userResultModel);
            }
            else
            {
                contactModel = this.crmManagerService.GetContact(userId);
                userResultModel = this.mapperFactory.GetMapper<ContactModel, UserResultModel>().Map(contactModel);
                userResultModel.Id = userId;
                userResultModel.Rank = rank;
                userResultModel.CountryDetails = countryDet;
                userResultModel.UserRole = Roles.Customer;
                userResultModel.PaymentDetails = new PaymentModelDto()
                {
                    PayPalId = contactModel.PaypalId,
                    Mode = (PaymentMode)contactModel.PaymentMode
                };

                return this.Ok(userResultModel);
            }
        }

        /// <summary>
        /// Updates the user details.
        /// </summary>
        /// <param name="contactModel">The Contact model.</param>
        /// <returns>IHttpActionResult object.</returns>
        [Route("updateuser")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateUserDetails(ContactModel contactModel)
        {
            string userId = User.Identity.GetUserId();
            var contact = this.crmManagerService.GetContact(userId);

            if (!string.IsNullOrEmpty(contactModel.FirstName))
            {
                contact.FirstName = contactModel.FirstName;
                ApplicationUserDto user = this.userService.GetContact(userId);
                user.Name = contactModel.FirstName;
                this.userService.UpdateUser(user);
            }

            contact.GCMId = string.IsNullOrEmpty(contactModel.GCMId) ? contact.GCMId : contactModel.GCMId;
            contact.UDId = string.IsNullOrEmpty(contactModel.UDId) ? contact.UDId : contactModel.UDId;
            contact.LastName = string.IsNullOrEmpty(contactModel.LastName) ? contact.LastName : contactModel.LastName;
            contact.SubInterest = contactModel.SubInterest == null ? contact.SubInterest : contactModel.SubInterest;
            contact.IsAvailable = contactModel.IsAvailable == null ? contact.IsAvailable : contactModel.IsAvailable;
            contact.IsOnline = contactModel.IsOnline == null ? contact.IsOnline : contactModel.IsOnline;
            contact.Availability = (int)contactModel.Availability == 0 ? contact.Availability : contactModel.Availability;
            contact.AvailableFrom = string.IsNullOrEmpty(contactModel.AvailableFrom) ? contact.AvailableFrom : contactModel.AvailableFrom;
            contact.AvailableTo = string.IsNullOrEmpty(contactModel.AvailableTo) ? contact.AvailableTo : contactModel.AvailableTo;
            ////contact.CountryCode = string.IsNullOrEmpty(contactModel.CountryCode) ? contact.CountryCode : contactModel.CountryCode;
            ////contact.Phone = string.IsNullOrEmpty(contactModel.Phone) ? contact.Phone : contactModel.Phone;
            if (string.IsNullOrEmpty(contactModel.Phone))
            {
                contact.Phone = contact.Phone;
            }
            else
            {
                contact.Phone = contactModel.Phone;
                if (contactModel.CountryCode == "US" || contactModel.CountryCode == "1" || contact.CountryCode == "US")
                {
                    this.LoggerService.LogException("Entered AreaCode saving code - " + contactModel.Phone);
                    string areaCode = string.Empty;

                    if (contact.Phone.Split('-').Length > 0)
                    {
                        areaCode = contact.Phone.Split('-')[0].ToString();
                        this.LoggerService.LogException("AreaCode saved with new format - " + areaCode);
                    }
                    else
                    {
                        if (contact.Phone.StartsWith("+"))
                        {
                            areaCode = contact.Phone.Substring(2, 3);
                        }
                        else
                        {
                            areaCode = contact.Phone.Substring(0, 3);
                        }

                        this.LoggerService.LogException("AreaCode saved with old format - " + areaCode);
                    }

                    contact.MailingState = this.commonService.GetStateFromAreaCode(areaCode).StateName;
                    this.LoggerService.LogException("MailingState - " + contact.MailingState + " for - " + contact.FirstName);
                }
            }

            contact.Description = string.IsNullOrEmpty(contactModel.Description) ? contact.Description : contactModel.Description;
            contact.MailingCountry = contactModel.MailingCountry == null ? contact.MailingCountry : contactModel.MailingCountry;
            contact.Latitude = contactModel.Latitude <= 0 ? contact.Latitude : contactModel.Latitude;
            contact.Longitude = contactModel.Longitude <= 0 ? contact.Longitude : contactModel.Longitude;
            contact.CountryCode = string.IsNullOrWhiteSpace(contactModel.CountryCode) ? contact.CountryCode : contactModel.CountryCode;

            if (!string.IsNullOrWhiteSpace(contactModel.PaypalId))
            {
                contact.PaypalId = contactModel.PaypalId;
            }

            contactModel = this.crmManagerService.UpdateContact(userId, contact);

            if (contactModel == null)
            {
                return this.BadRequest();
            }

            CountryModel countryDet = this.commonService.GetUserCountryDetails(userId);
            decimal rank = this.crmManagerService.GetUserRank(contact.Id);

            UserResultModel userResultModel = new UserResultModel();
            userResultModel = this.mapperFactory.GetMapper<ContactModel, UserResultModel>().Map(contactModel);
            userResultModel.Rank = rank;
            userResultModel.CountryDetails = countryDet;
            userResultModel.UserRole = Roles.Customer;
            userResultModel.PaymentDetails = new PaymentModelDto()
            {
                PayPalId = contactModel.PaypalId,
                Mode = (PaymentMode)contactModel.PaymentMode
            };

            return this.Ok(userResultModel);
        }

        /// <summary>
        /// Gets user interests.
        /// </summary>
        /// <returns>IHttpActionResult object</returns>
        [Route("user-interests")]
        [HttpGet]
        public async Task<IHttpActionResult> GetUserInterests()
        {
            string userId = User.Identity.GetUserId();
            string[] interests = new string[] { };

            LeadModel leadModel = this.crmManagerService.GetLead(userId);
            interests = leadModel.SubInterest;

            return this.Ok(interests);
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
            message.IsReadByUser = true;
            message.CompanyId = message.ToUser;
            message.UserId = message.FromUser;

            message = this.youfferMessageService.CreateMessage(message);
            int unreadMsgCount = this.youfferMessageService.GetUnreadMsgCount(message.UserId, false);

            ContactModel fromUser = this.crmManagerService.GetContact(message.FromUser);
            OrganisationModel orgModel = this.crmManagerService.GetOrganisation(message.ToUser);

            if (!string.IsNullOrEmpty(orgModel.GCMId))
            {
                this.pushMessageService.SendMessageNotificationToAndroid(orgModel.GCMId, message.Id.ToString(), message.Message, fromUser.FirstName, Notification.usermsg.ToString());
            }

            if (!string.IsNullOrEmpty(orgModel.UDId))
            {
                this.pushMessageService.SendMessageNotificationToiOS(orgModel.UDId, message.Id.ToString(), message.Message, fromUser.FirstName, unreadMsgCount, Notification.usermsg.ToString());
            }

            SignalRHub hub = new SignalRHub();
            hub.SendMessage(message.CompanyId, message);

            return this.Ok(message);
        }

        /// <summary>
        /// Gets the messages.
        /// </summary>
        /// <param name="threadId">The thread identifier.</param>
        /// <param name="lastPageId">The lastPage identifier.</param>
        /// <param name="fetchCount">The fetch count.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="direction">The direction.</param>
        /// <returns>IHttpActionResult object.</returns>
        [Route("getmessages/{threadId}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetMessages(long threadId, int lastPageId = 0, int fetchCount = 0, string sortBy = "", string direction = "asc")
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

            string userId = User.Identity.GetUserId();
            string crmContactId = this.userService.GetContact(userId).CRMId;
            ContactModel contact = this.crmManagerService.GetContact(userId);

            List<MessagesDto> lstMessages = new List<MessagesDto>();
            lstMessages = this.youfferMessageService.GetAllMessages(userId, threadId, lastPageId, fetchCount, sortBy, direction);

            List<CRMContactUs> contactUsMsg = this.crmManagerService.ReadAllContactUsMessage(crmContactId, lastPageId, fetchCount, sortBy, direction);
            foreach (var contactUs in contactUsMsg)
            {
                MessagesDto msg = new MessagesDto()
                {
                    Id = Convert.ToInt32(contactUs.Id.Split('x')[1]),
                    IsRead = true,
                    CreatedOn = contactUs.CreatedOn,
                    ModifiedOn = contactUs.ModifiedOn,
                    FromUser = contactUs.IsIncomingMessage ? userId : "Youffer Admin",
                    CreatedBy = contactUs.IsIncomingMessage ? userId : "Youffer Admin",
                    ModifiedBy = contactUs.IsIncomingMessage ? userId : "Youffer Admin",
                    UserId = userId,
                    ToUser = contactUs.IsIncomingMessage ? "Youffer Admin" : userId,
                    Message = contactUs.Description,
                    Subject = contactUs.Subject,
                    DeptId = (ContactUsDept)Enum.Parse(typeof(ContactUsDept), contactUs.Department),
                    MediaId = 0,
                    Name = "Youffer Admin",
                    CompanyId = AppSettings.Get<string>(ConfigConstants.SuperUserId),
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
        /// <param name="companyId">The company identifier.</param>
        /// <param name="lastPageId">The lastPage identifier.</param>
        /// <param name="fetchCount">The fetch count.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="direction">The direction.</param>
        /// <returns>IHttpActionResult object.</returns>
        [Route("{companyId}/getmessages")]
        [HttpGet]
        public async Task<IHttpActionResult> GetMessages(string companyId, int lastPageId = 0, int fetchCount = 0, string sortBy = "", string direction = "asc")
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
            string userId = User.Identity.GetUserId();

            MessageThreadDto msgThreadDto = this.youfferMessageService.GetMessageThread(userId, companyId);
            if (msgThreadDto != null && msgThreadDto.Id > 0)
            {
                lstMessages = this.youfferMessageService.GetAllMessages(userId, msgThreadDto.Id, lastPageId, fetchCount, sortBy, direction);
            }

            return this.Ok(lstMessages);
        }

        /// <summary>
        /// Gets all user messages.
        /// </summary> 
        /// <param name="lastPageId">The lastPage identifier.</param>
        /// <param name="fetchCount">The fetch count.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="direction">The direction.</param>
        /// <returns>IHttpActionResult object.</returns>
        [Route("user-messages")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllUserMessages(int lastPageId = 0, int fetchCount = 0, string sortBy = "", string direction = "asc")
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

            string userId = User.Identity.GetUserId();
            string crmContactId = this.userService.GetContact(userId).CRMId;
            ContactModel contact = this.crmManagerService.GetContact(userId);

            List<MessagesDto> lstMessages = this.youfferMessageService.GetUserAllMessage(userId, lastPageId, fetchCount, sortBy, direction);

            if (lastPageId <= 1)
            {
                CRMContactUs contactUs = this.crmManagerService.ReadContactUsMessage(crmContactId);

                if (contactUs != null)
                {
                    MessageThreadDto msgThread = this.youfferMessageService.GetMessageThread(userId, "YoufferAdmin");
                    if (msgThread != null)
                    {
                        MessagesDto msg = new MessagesDto()
                        {
                            Id = Convert.ToInt32(contactUs.Id.Split('x')[1]),
                            IsRead = true,
                            CreatedOn = contactUs.CreatedOn,
                            ModifiedOn = contactUs.ModifiedOn,
                            FromUser = contactUs.IsIncomingMessage ? userId : "Youffer Admin",
                            CreatedBy = contactUs.IsIncomingMessage ? userId : "Youffer Admin",
                            ModifiedBy = contactUs.IsIncomingMessage ? userId : "Youffer Admin",
                            UserId = userId,
                            ToUser = contactUs.IsIncomingMessage ? "Youffer Admin" : userId,
                            Message = contactUs.Description,
                            Subject = contactUs.Subject,
                            DeptId = (ContactUsDept)Enum.Parse(typeof(ContactUsDept), contactUs.Department),
                            MediaId = 0,
                            Name = "Youffer Admin",
                            CompanyId = AppSettings.Get<string>(ConfigConstants.SuperUserId),
                            ThreadId = msgThread.Id
                        };

                        lstMessages.Add(msg);
                        lstMessages = lstMessages.Distinct().OrderByDescending(x => x.CreatedOn).GroupBy(x => x.ThreadId).Select(grp => grp.First()).ToList();
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
            string userId = User.Identity.GetUserId();
            StatusMessage status = new StatusMessage();

            bool isContactUs = this.crmManagerService.CheckIfContactUsMsg(AppSettings.Get<string>(ConfigConstants.ContactUsId) + msgId);
            if (isContactUs)
            {
                status.IsSuccess = this.crmManagerService.DeleteContactusMessage(AppSettings.Get<string>(ConfigConstants.ContactUsId) + msgId);
            }
            else
            {
                status.IsSuccess = this.youfferMessageService.DeleteMessage(msgId, userId);
            }

            return this.Ok(status);
        }

        /// <summary>
        /// Gets the company details.
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <returns>IHttpActionResult object.</returns>
        [Route("companyprofile")]
        [HttpGet]
        public async Task<IHttpActionResult> GetCompanyDetails(string companyId)
        {
            OrganisationModel orgModel = new OrganisationModel();

            orgModel = this.crmManagerService.GetOrganisation(companyId);
            CountryModel countryDet = this.commonService.GetCompanyCountryDetails(companyId);

            OrgResultModel orgResultModel = new OrgResultModel()
            {
                Id = companyId,
                Name = orgModel.AccountName,
                Email = orgModel.Email1,
                SecondaryEmail = orgModel.Email2,
                FacebookURL = orgModel.FacebookURL,
                GooglePlusURL = orgModel.GooglePlusURL,
                Website = orgModel.Website,
                Description = orgModel.Description,
                Phone = orgModel.Phone,
                OtherPhone = orgModel.OtherPhone,
                Fax = orgModel.Fax,
                AnnualRevenue = orgModel.AnnualRevenue,
                Employees = orgModel.Employees,
                CountryDetails = countryDet,
                Address = orgModel.BillAddress,
                City = orgModel.BillCity,
                State = orgModel.BillState,
                POBox = orgModel.BillPOBox,
                Timezone = orgModel.Timezone,
                Latitude = orgModel.Latitude,
                Longitude = orgModel.Longitude,
                ImageURL = orgModel.ImageURL,
                GCMId = orgModel.GCMId,
                UDId = orgModel.UDId,
                OSType = orgModel.OSType,
                UserRole = Roles.Company,
                Industry = orgModel.Industry,
                MainBusinessType = orgModel.MainBusinessType,
                SubBusinessType = orgModel.SubBusinessType,
                IsActive = orgModel.IsActive,
                PaymentDetails = string.Empty
            };

            return this.Ok(orgResultModel);
        }

        /// <summary>
        /// Sets the user feedback.
        /// </summary>
        /// <param name="feedback">The feedback.</param>
        /// <returns>IHttpActionResult object.</returns>
        [Route("reportcompany")]
        [HttpPost]
        public async Task<IHttpActionResult> ReportCompany(FeedbackDto feedback)
        {
            string userId = User.Identity.GetUserId();
            string crmContactId = this.userService.GetContact(userId).CRMId;
            string crmOrgId = this.userService.GetContact(feedback.ToId).CRMId;
            feedback.FromId = feedback.CreatedBy = userId;
            feedback.FeedbackId = Convert.ToInt32(Feedback.ReportCompany);

            feedback = this.youfferFeedbackService.SaveFeedback(feedback);

            StatusMessage status = new StatusMessage();
            status.IsSuccess = this.crmManagerService.ReportCompany(crmContactId, crmOrgId);

            OrganisationModel organisation = this.crmManagerService.GetOrganisation(feedback.ToId);
            ContactModel contact = this.crmManagerService.GetContact(userId);
            if (!string.IsNullOrEmpty(organisation.GCMId))
            {
                this.pushMessageService.SendReportCompanyNotificationToAndroid(organisation.GCMId, userId, contact.FirstName, "Fake Company", Notification.reportcompany.ToString());
            }

            if (!string.IsNullOrEmpty(organisation.UDId))
            {
                this.pushMessageService.SendReportCompanyNotificationToiOS(organisation.UDId, userId, contact.FirstName, "Fake Company", Notification.reportcompany.ToString());
            }

            CRMNotifications notification = new CRMNotifications()
            {
                OrganisationsId = crmOrgId,
                ContactId = crmContactId,
                NotificationType = ConfigConstants.ContactComplainedOrganisation
            };

            this.crmManagerService.CreateNotification(notification);

            return this.Ok(status);
        }

        /// <summary>
        /// Not call again.
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <returns>IHttpActionResult object.</returns>
        [Route("notcallagain")]
        [HttpGet]
        public async Task<IHttpActionResult> NotCallAgain(string companyId)
        {
            StatusMessage status = new StatusMessage();
            string userId = User.Identity.GetUserId();
            string crmContactId = this.userService.GetContact(userId).CRMId;
            string crmOrgId = this.userService.GetContact(companyId).CRMId;

            status.IsSuccess = this.crmManagerService.MarkNotCall(crmContactId, crmOrgId);

            OrganisationModel organisation = this.crmManagerService.GetOrganisation(companyId);
            ContactModel contact = this.crmManagerService.GetContact(userId);
            if (!string.IsNullOrEmpty(organisation.GCMId))
            {
                this.pushMessageService.SendNotCallAgainNotificationToAndroid(organisation.GCMId, userId, contact.FirstName, "Not call again", Notification.notcall.ToString());
            }

            if (!string.IsNullOrEmpty(organisation.UDId))
            {
                this.pushMessageService.SendNotCallAgainNotificationToiOS(organisation.UDId, userId, contact.FirstName, "Not call again", Notification.notcall.ToString());
            }

            return this.Ok(status);
        }

        /// <summary>
        /// Gets all user's reviews.
        /// </summary>  
        /// <param name="lastPageId">The lastPageId identifier.</param>
        /// <param name="fetchCount">The fetch count.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="direction">The direction.</param>
        /// <returns>
        /// IHttpActionResult object.
        /// </returns>
        [Route("user-reviews")]
        [HttpGet]
        public async Task<IHttpActionResult> GetReviews(int lastPageId = 0, int fetchCount = 0, string sortBy = "", string direction = "asc")
        {
            if (fetchCount == 0)
            {
                fetchCount = AppSettings.Get<int>(ConfigConstants.ReviewsDefaultCount);
            }

            string userId = User.Identity.GetUserId();
            string contactId = this.userService.GetContact(userId).CRMId;
            var reviewList = this.crmManagerService.ReadUserReviews(contactId, string.Empty, string.Empty, lastPageId, fetchCount);

            List<UserReviewsDto> userReviews = new List<UserReviewsDto>();
            foreach (var review in reviewList)
            {
                var organisationId = this.userService.GetContactByCrmId(review.OrganisationsId).Id;
                var org = this.crmManagerService.GetOrganisation(organisationId);
                UserReviewsDto userReview = this.mapperFactory.GetMapper<CRMUserReview, UserReviewsDto>().Map(review);
                userReview.Name = org.AccountName;
                userReview.CompanyImageUrl = org.ImageURL;
                userReviews.Add(userReview);
            }

            return this.Ok(userReviews);
        }

        /// <summary>
        /// Gets the user pricing details.
        /// </summary>
        /// <returns>IHttpActionResult object.</returns>
        [Route("user-purchasedetails")]
        [HttpGet]
        public async Task<IHttpActionResult> GetUserPurchaseDetails()
        {
            List<LeadOpportunityMappingDto> lstLeadOppMapping = new List<LeadOpportunityMappingDto>();
            string userId = User.Identity.GetUserId();
            string crmLeadId = string.Empty;
            string companyName = string.Empty;

            lstLeadOppMapping = this.youfferPaymentService.GetUserPurchaseDetails(userId);

            foreach (var item in lstLeadOppMapping)
            {
                companyName = this.userService.GetContactByCrmId(item.CompanyId).Name;
                item.Name = companyName;
            }

            return this.Ok(lstLeadOppMapping);
        }

        /// <summary>
        /// Gets the purchased user total amount.
        /// </summary>
        /// <returns>IHttpActionResult object.</returns>
        [Route("user-purchasedtotalamount")]
        [HttpGet]
        public async Task<IHttpActionResult> GetPurchasedUserTotalAmount()
        {
            AmountModel status = new AmountModel();
            status.Amount = 0;
            string userId = User.Identity.GetUserId();

            status.Amount = this.youfferPaymentService.GetPurchasedUserTotalAmount(userId);
            status.IsSuccess = true;
            return this.Ok(status);
        }

        /// <summary>
        /// Requests the payment.
        /// </summary>
        /// <param name="paymentModel">The payment model.</param>
        /// <returns>IHttpActionResult object.</returns>
        [Route("requestpayment")]
        [HttpPost]
        public async Task<IHttpActionResult> RequestPayment(PaymentModelDto paymentModel)
        {
            StatusMessage status = new StatusMessage();

            string userId = User.Identity.GetUserId();
            string crmContactId = this.userService.GetContact(userId).CRMId;

            decimal totalAmount = this.youfferPaymentService.GetPurchasedUserTotalAmount(userId);
            if (totalAmount < 50)
            {
                status.IsSuccess = false;
                status.ErrorMessage = "You need to have at least $50 in your account to request payment.";
                return this.Ok(status);
            }

            string crmLeadId = string.Empty;

            if (!string.IsNullOrEmpty(crmContactId))
            {
                crmLeadId = this.youfferContactService.GetMappingEntryByContactId(userId).LeadId;
            }

            CRMRequestPayment crmRequestPayment = new CRMRequestPayment()
            {
                ContactId = crmContactId,
                Note = string.Empty,
                IsApproved = false,
                Amount = totalAmount
            };

            this.crmManagerService.CreatePaymentRequest(crmRequestPayment);
            status.IsSuccess = this.youfferPaymentService.RequestPayment(crmLeadId);
            return this.Ok(status);
        }

        /// <summary>
        /// Logouts this instance.
        /// </summary>
        /// <returns>IHttpActionResult object.</returns>
        [Route("logout")]
        [HttpGet]
        public async Task<IHttpActionResult> Logout()
        {
            string userId = User.Identity.GetUserId();

            ContactModel contactModel = this.crmManagerService.GetContact(userId);

            contactModel.IsOnline = false;
            contactModel.UDId = string.Empty;
            contactModel.GCMId = string.Empty;
            this.crmManagerService.UpdateContact(userId, contactModel);
            StatusMessage msg = new StatusMessage()
            {
                IsSuccess = true
            };

            return this.Ok(msg);
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
            string contactCRMId = this.youfferContactService.GetMappingEntryByContactId(contactUs.UserId).ContactCRMId;

            long threadId = 0;
            CRMContactUs crmContactUsOld = this.crmManagerService.ReadContactUsMessage(contactCRMId);
            if (crmContactUsOld == null)
            {
                MessageThreadDto messageThread = new MessageThreadDto { FromUser = contactUs.UserId, ToUser = "YoufferAdmin", CreatedBy = contactUs.UserId, ModifiedBy = contactUs.UserId };
                messageThread = this.youfferMessageService.CreateMessageThread(messageThread);
                threadId = messageThread.Id;
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
                UserId = contactCRMId,
                ThreadId = threadId
            };

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
        /// Deletes message thread.
        /// </summary>
        /// <param name="threadId">The thread identifier.</param>
        /// <returns>IHttpActionResult object.</returns>
        [Route("deletethread")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeletemessageThread(int threadId)
        {
            StatusMessage status = new StatusMessage();
            string userId = User.Identity.GetUserId();
            string contactCRMId = this.youfferContactService.GetMappingEntryByContactId(userId).ContactCRMId;

            MessageThreadDto msgThread = this.youfferMessageService.GetMessageThread(userId, "YoufferAdmin");
            if (msgThread != null)
            {
                this.crmManagerService.MarkContactUsMessagesDeleted(contactCRMId);
            }

            status.IsSuccess = this.youfferMessageService.DeleteThread(threadId, userId);
            return this.Ok(status);
        }

        /// <summary>
        /// Owners the companies.
        /// </summary>
        /// <returns>IHttpActionResult object.</returns>
        [Route("ownercompanies")]
        [HttpGet]
        public async Task<IHttpActionResult> OwnerCompanies()
        {
            List<OwnerCompaniesDto> lstModel = new List<OwnerCompaniesDto>();
            string userId = User.Identity.GetUserId();

            lstModel = this.crmManagerService.GetOwnerCompanies(userId);
            return this.Ok(lstModel);
        }

        /// <summary>
        /// Send Verification Code.
        /// </summary>
        /// <param name="model">VerificationCode model</param>
        /// <returns>IHttpActionResult object.</returns>
        [Route("send-code")]
        [HttpPost]
        public async Task<IHttpActionResult> SendVerificationCode(VerificationCode model)
        {
            StatusMessage stat = new StatusMessage();
            if (!string.IsNullOrWhiteSpace(model.PhoneNumber))
            {
                if (model.PhoneNumber.StartsWith("0"))
                {
                    model.PhoneNumber = model.PhoneNumber.TrimStart('0');
                }

                model.PhoneNumber = "+" + model.CountryCode + model.PhoneNumber;
                string userId = User.Identity.GetUserId();
                Random generator = new Random();
                string code = generator.Next(0, 1000000).ToString("D6");
                string message = "Your Confirmation Code:{0}\n\nThank you for downloading Youffer.\n\nSupport:\n{1}\n{2}";
                message = string.Format(message, code, AppSettings.Get<string>(ConfigConstants.YoufferSupportEmail), AppSettings.Get<string>(ConfigConstants.YoufferWebsiteURL));
                stat.IsSuccess = this.crmManagerService.SendVerificationCode(userId, model.PhoneNumber, code, message);
            }

            return this.Ok(stat);
        }

        /// <summary>
        /// Send Verification Code.
        /// </summary>
        /// <param name="code"> The code</param>
        /// <returns>IHttpActionResult object.</returns>
        [Route("{code}/check-code")]
        [HttpGet]
        public async Task<IHttpActionResult> CheckVerificationCode(string code)
        {
            string userId = User.Identity.GetUserId();
            StatusMessage stat = new StatusMessage();
            string errorMessage;
            stat.IsSuccess = this.crmManagerService.UpdatePhoneNumberAfterVerification(userId, code, out errorMessage);
            stat.ErrorMessage = errorMessage;
            return this.Ok(stat);
        }

        /// <summary>
        /// Updates the parent interest of users.
        /// </summary>
        /// <returns>IHttpActionResult object.</returns>
        [Route("updateparentinterest")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IHttpActionResult> UpdateParentInterestOfUsers()
        {
            bool isSuccess = this.crmManagerService.UpdateParentInterestOfUsers();
            return this.Ok(isSuccess);
        }
    }
}