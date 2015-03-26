// ---------------------------------------------------------------------------------------------------
// <copyright file="CommonController.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2014-11-28</date>
// <summary>
//     The CommonController class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Common.CRMService;
    using Common.DataService;
    using Common.Helper;
    using Common.LogService;
    using Common.Mapper;
    using CRM;
    using Framework.Helper;
    using Microsoft.AspNet.Identity;
    using Newtonsoft.Json;
    using Resources.Constants;
    using Resources.CRMModel;
    using Resources.Models;
    using Resources.MySqlDbSchema;
    using Resources.ViewModel;
    using Youffer.Common.MaxmindGeoIP2;
    using Youffer.Common.Notification;
    using Youffer.Resources.Enum;
    using Youffer.Resources.ViewModel.MaxmindGeoIP2;

    /// <summary>
    /// The Common controller.
    /// </summary>
    [RoutePrefix("api/common")]
    public class CommonController : BaseApiController
    {
        /// <summary>
        /// The IUserService service instance
        /// </summary>
        private readonly ICommonService commonService;

        /// <summary>
        /// The IContactService service instance
        /// </summary>
        private readonly ICRMManagerService crmManagerService;

        /// <summary>
        /// The youffer message service
        /// </summary>
        private readonly IYoufferMessageService youfferMessageService;

        /// <summary>
        /// The youffer interest service
        /// </summary>
        private readonly IYoufferInterestService youfferInterestService;

        /// <summary>
        /// The mapper factory
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// The IYoufferContactService service instance
        /// </summary>
        private readonly IYoufferContactService youfferContactService;

        /// <summary>
        /// The push message service
        /// </summary>
        private readonly IPushMessageService pushMessageService;

        /// <summary>
        /// The ip2 location service
        /// </summary>
        private readonly IIP2LocationService ip2LocationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommonController"/> class.
        /// </summary>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="commonService">The common service.</param>
        /// <param name="crmManagerService">The CRM manager service.</param>
        /// <param name="youfferMessageService">The youffer message service.</param>
        /// <param name="youfferInterestService">the youffer interest service</param>
        /// <param name="mapperFactory"> The mapperFactory</param>
        /// <param name="youfferContactService"> The youffer contact service. </param>
        /// <param name="pushMessageService">The push message service.</param>
        /// <param name="ip2LocationService">The ip2Location service.</param>
        public CommonController(ILoggerService loggerService, ICommonService commonService, ICRMManagerService crmManagerService, IYoufferMessageService youfferMessageService, IYoufferInterestService youfferInterestService, IMapperFactory mapperFactory, IYoufferContactService youfferContactService, IPushMessageService pushMessageService, IIP2LocationService ip2LocationService)
            : base(loggerService)
        {
            this.mapperFactory = mapperFactory;
            this.commonService = commonService;
            this.crmManagerService = crmManagerService;
            this.youfferMessageService = youfferMessageService;
            this.youfferInterestService = youfferInterestService;
            this.youfferContactService = youfferContactService;
            this.pushMessageService = pushMessageService;
            this.ip2LocationService = ip2LocationService;
        }

        /// <summary>
        /// Gets the country meta data.
        /// </summary>
        /// <returns>DataTable object.</returns>
        [Route("countries")]
        [HttpGet]
        public async Task<IHttpActionResult> GetCountryMetaData()
        {
            List<CountryModel> lstCountryModel = this.commonService.GetCountryMetaData();
            return this.Ok(lstCountryModel);
        }

        /// <summary>
        /// Gets all interests.
        /// </summary>
        /// <returns>IHttpActionResult object.</returns>
        [Route("get-interests")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllInterests()
        {
            int fieldIndex = AppSettings.Get<int>(ConfigConstants.LeadSubInterestFieldIndex);
            List<CRM.VTigerPicklistItem> lst = this.crmManagerService.GetMetaData(CRM.VTigerType.Leads, fieldIndex).OrderBy(x => x.label).ToList();

            List<string> lstInt = lst.Select(x => x.label).ToList();
            return this.Ok(lstInt);
        }

        /// <summary>
        /// Gets all parent business types.
        /// </summary>
        /// <returns>IHttpActionResult object.</returns>
        [Route("get-businesstypes")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllParentBusinessTypes()
        {
            List<string> lst = new List<string>();
            lst = this.youfferInterestService.GetAllParentBusinessTypes();

            return this.Ok(lst);
        }

        /// <summary>
        /// Gets all interests.
        /// </summary>
        /// <returns>IHttpActionResult object.</returns>
        [Route("get-subbusinesstypes")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllBusinessTypes()
        {
            int fieldIndex = AppSettings.Get<int>(ConfigConstants.OrgSubBusinessTypesFieldIndex);
            List<CRM.VTigerPicklistItem> lst = this.crmManagerService.GetMetaData(CRM.VTigerType.Accounts, fieldIndex).OrderBy(x => x.label).ToList();

            List<string> lstInt = lst.Select(x => x.label).ToList();
            return this.Ok(lstInt);
        }

        /// <summary>
        /// Gets all main business types.
        /// </summary>
        /// <returns>IHttpActionResult object.</returns>
        [Route("get-mainbusinesstypes")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllMainBusinessTypes()
        {
            List<string> lst = new List<string>();
            lst = this.youfferInterestService.GetAllParentBusinessTypes();

            return this.Ok(lst);
        }

        /// <summary>
        /// Gets all business types.
        /// </summary>
        /// <param name="interest">The interest.</param>
        /// <returns>IHttpActionResult object.</returns>
        [Route("get-subbusinesstypes")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllSubBusinessTypes(string interest)
        {
            List<DictModel> lstDictModel = new List<DictModel>();
            if (!string.IsNullOrWhiteSpace(interest))
            {
                lstDictModel = this.youfferInterestService.GetAllSubInterests(interest);
            }

            return this.Ok(lstDictModel);
        }

        /// <summary>
        /// Trendings the search.
        /// </summary>
        /// <returns>IHttpActionResult object.</returns>
        [Route("trending-search")]
        [HttpGet]
        public async Task<IHttpActionResult> TrendingSearch()
        {
            List<string> lst = new List<string>();
            lst = this.crmManagerService.TrendingSearch();

            return this.Ok(lst);
        }

        /// <summary>
        /// Save the message attachemnts
        /// </summary>
        /// <param name="msgMedia"> The Message Media Details</param>
        /// <returns> IHttpActionResult object </returns>
        [Route("attach-media")]
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> SaveMessageAttachment(MessageMediaDto msgMedia)
        {
            msgMedia = ImageHelper.SaveMessageMediaFiles(msgMedia);
            msgMedia = this.youfferMessageService.MakeMessageMediaEntry(msgMedia);
            msgMedia.IsSuccess = msgMedia.Id > 0;

            return this.Ok(msgMedia);
        }

        /// <summary>
        /// Saves the message attachment.
        /// </summary>
        /// <returns>HttpResponseMessage object</returns>
        [Route("attachment")]
        [HttpPost]
        [Authorize]
        public async Task<HttpResponseMessage> SaveMessageAttachment()
        {
            string userId = User.Identity.GetUserId();
            string toUserId = string.Empty;
            bool isBulk = false;
            MediaStatusMessage stat = new MediaStatusMessage();

            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            MessagesDto msg = new MessagesDto();
            MessageMediaDto msgMedia = new MessageMediaDto();
            string defaultPath = AppSettings.Get<string>(ConfigConstants.DefaultMessageMediaSavePath);
            string defaultUrlPath = AppSettings.Get<string>(ConfigConstants.DefaultMessageMediaUrlPath);
            string root = System.Web.HttpContext.Current.Server.MapPath("~/" + defaultPath);

            var provider = new MultipartFormDataStreamProvider(root);
            string mediaUrl = string.Empty;

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                foreach (var key in provider.FormData.AllKeys)
                {
                    foreach (var val in provider.FormData.GetValues(key))
                    {
                        if (key == "ToUserId")
                        {
                            toUserId = val;
                        }
                        else if (key == "IsBulkMessage")
                        {
                            isBulk = val == "1" ? true : false;
                        }
                    }
                }

                foreach (MultipartFileData file in provider.FileData)
                {
                    string originalFileName = file.Headers.ContentDisposition.FileName.Replace("\"", string.Empty);
                    string fileExtension = Path.GetExtension(originalFileName);
                    string fname = Guid.NewGuid().ToString();
                    string fileName = fname + fileExtension;
                    string outputPath = Path.Combine(root, fileName);
                    System.IO.File.Move(file.LocalFileName, outputPath);
                    FileInfo fileInfo = new FileInfo(outputPath);
                    string size = Convert.ToDouble(fileInfo.Length / 1024) + string.Empty;

                    mediaUrl = AppSettings.Get<string>("ApiBaseUrl") + defaultUrlPath + "/" + fileName;

                    if (isBulk)
                    {
                        if (User.IsInRole(RoleName.CompanyRole))
                        {
                            this.youfferMessageService.SendBulkMessage(userId, string.Empty, originalFileName, fileExtension, size, fname);
                        }
                    }
                    else
                    {
                        if (User.IsInRole(RoleName.CompanyRole))
                        {
                            msg.UserId = toUserId;
                            msg.CompanyId = userId;
                        }
                        else if (User.IsInRole(RoleName.CustomerRole))
                        {
                            msg.CompanyId = toUserId;
                            msg.UserId = userId;
                        }

                        msg.FromUser = msg.CreatedBy = msg.ModifiedBy = userId;
                        msg.ToUser = toUserId;
                        msg.DoesContainMedia = true;
                        msg.Message = string.Empty;
                        msg.IsDeleted = false;

                        msg = this.youfferMessageService.CreateMessage(msg);
                        this.LoggerService.LogException("Message - json -" + JsonConvert.SerializeObject(msg));

                        msgMedia.MessageId = msg.Id;
                        msgMedia.Size = size;
                        msgMedia.Extension = fileExtension;
                        msgMedia.FileName = fileName;
                        msgMedia.OriginalFileName = file.Headers.ContentDisposition.FileName.Replace("\"", string.Empty);
                        msgMedia.IsDeleted = false;
                        msgMedia.IsSuccess = true;
                        msgMedia.CreatedBy = userId;
                        msgMedia.ModifiedBy = userId;

                        this.youfferMessageService.MakeMessageMediaEntry(msgMedia);
                        this.LoggerService.LogException("Message Media - json -" + JsonConvert.SerializeObject(msgMedia));
                        int unreadMsgCount = this.youfferMessageService.GetUnreadMsgCount(msg.UserId, false);

                        if (User.IsInRole(RoleName.CustomerRole))
                        {
                            OrganisationModel toUser = this.crmManagerService.GetOrganisation(toUserId);
                            ContactModel fromUser = this.crmManagerService.GetContact(userId);

                            if (!string.IsNullOrEmpty(toUser.GCMId))
                            {
                                this.pushMessageService.SendMessageNotificationToAndroid(toUser.GCMId, msg.Id.ToString(), msg.Message, fromUser.FirstName, Notification.companymsg.ToString());
                            }

                            if (!string.IsNullOrEmpty(toUser.UDId))
                            {
                                this.pushMessageService.SendMessageNotificationToiOS(toUser.UDId, msg.Id.ToString(), msg.Message, fromUser.FirstName, unreadMsgCount, Notification.companymsg.ToString());
                            }
                        }
                        else if (User.IsInRole(RoleName.CompanyRole))
                        {
                            ContactModel toUser = this.crmManagerService.GetContact(toUserId);
                            OrganisationModel fromUser = this.crmManagerService.GetOrganisation(userId);

                            if (!string.IsNullOrEmpty(toUser.GCMId))
                            {
                                this.pushMessageService.SendMessageNotificationToAndroid(toUser.GCMId, msg.Id.ToString(), msg.Message, fromUser.AccountName, Notification.usermsg.ToString());
                            }

                            if (!string.IsNullOrEmpty(toUser.UDId))
                            {
                                this.pushMessageService.SendMessageNotificationToiOS(toUser.UDId, msg.Id.ToString(), msg.Message, fromUser.AccountName, unreadMsgCount, Notification.usermsg.ToString());
                            }
                        }
                    }
                }

                stat.IsSuccess = true;
                stat.MediaUrl = mediaUrl;
                return Request.CreateResponse(HttpStatusCode.OK, stat);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("SaveMessageAttachment : " + ex.Message);
                stat.IsSuccess = false;
                stat.MediaUrl = string.Empty;
                return Request.CreateResponse(HttpStatusCode.BadRequest, stat);
            }
        }

        /// <summary>
        /// Save the Profile Image
        /// </summary>
        /// <param name="msgMedia"> The Message Media Details</param>
        /// <returns> IHttpActionResult object </returns>
        [Route("update-profileimage")]
        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> SaveProfileImage(MessageMediaDto msgMedia)
        {
            StatusMessage status = new StatusMessage();
            string userId = User.Identity.GetUserId();

            msgMedia.FileName = userId;
            msgMedia.Extension = ".jpg";

            string imageUrl = ImageHelper.SaveProfileImage(msgMedia);
            if (!string.IsNullOrEmpty(imageUrl))
            {
                if (User.IsInRole(RoleName.CompanyRole))
                {
                    var organisation = this.crmManagerService.GetOrganisation(userId);
                    organisation.ImageURL = imageUrl;
                }
                else if (User.IsInRole(RoleName.CustomerRole))
                {
                    var contact = this.crmManagerService.GetContact(userId);
                    contact.ImageURL = imageUrl;
                }

                status.IsSuccess = true;
            }

            return this.Ok(status);
        }

        /// <summary>
        /// Save the Profile Image
        /// </summary>
        /// <returns> HttpResponseMessage object </returns>
        [Route("profileimage")]
        [HttpPost]
        [Authorize]
        public Task<HttpResponseMessage> SaveProfileImage()
        {
            string userId = User.Identity.GetUserId();
            MediaStatusMessage stat = new MediaStatusMessage();

            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string defaultPath = AppSettings.Get<string>(ConfigConstants.DefaultImageSavePath);
            string defaultUrlPath = AppSettings.Get<string>(ConfigConstants.DefaultImageUrlPath);
            string root = System.Web.HttpContext.Current.Server.MapPath("~/" + defaultPath);

            var provider = new MultipartFormDataStreamProvider(root);
            string imageUrl = string.Empty;

            var task = Request.Content.ReadAsMultipartAsync(provider).ContinueWith(t =>
            {
                if (t.IsFaulted || t.IsCanceled)
                {
                    stat.ErrorMessage = t.Exception.Message;
                    Request.CreateResponse(HttpStatusCode.InternalServerError, stat);
                    this.LoggerService.LogException("SaveProfileImage : Id - " + userId + " Exception - " + t.Exception.Message);
                }

                foreach (MultipartFileData file in provider.FileData)
                {
                    string originalFileName = file.Headers.ContentDisposition.FileName.Replace("\"", string.Empty);
                    string fileName = userId + ".jpg";
                    string outputPath = Path.Combine(root, fileName);
                    if (File.Exists(outputPath))
                    {
                        File.Delete(outputPath);
                    }

                    System.IO.File.Move(file.LocalFileName, outputPath);

                    imageUrl = AppSettings.Get<string>("ApiBaseUrl") + defaultUrlPath + "/" + fileName;

                    if (!string.IsNullOrEmpty(imageUrl))
                    {
                        if (User.IsInRole(RoleName.CompanyRole))
                        {
                            var organisation = this.crmManagerService.GetOrganisation(userId);
                            organisation.ImageURL = imageUrl;
                            organisation.IsImageUploaded = true;
                            this.crmManagerService.UpdateOrganisation(userId, organisation);
                        }
                        else if (User.IsInRole(RoleName.CustomerRole))
                        {
                            var contact = this.crmManagerService.GetContact(userId);
                            contact.ImageURL = imageUrl;
                            this.crmManagerService.UpdateContact(userId, contact);
                        }
                    }
                }

                stat.IsSuccess = true;
                stat.MediaUrl = imageUrl;

                return Request.CreateResponse(HttpStatusCode.OK, stat);
            });

            return task;
        }

        /// <summary>
        /// Gets the home page leads.
        /// </summary>
        /// <param name="interest">The interest.</param>
        /// <param name="lastpageId">The lastpage identifier.</param>
        /// <param name="fetchCount">The fetch count.</param>
        /// <returns>IHttpActionResult object.</returns>
        [Route("top-leads")]
        [HttpGet]
        public async Task<IHttpActionResult> GetHomePageLeads(string interest = "", int lastpageId = 0, int fetchCount = 0)
        {
            if (fetchCount == 0)
            {
                fetchCount = AppSettings.Get<int>(ConfigConstants.HomePageLeadsDefaultCount);
            }

            List<UserResultModel> lst = new List<UserResultModel>();
            lastpageId = lastpageId < 1 ? 1 : lastpageId;
            var startVal = (lastpageId - 1) * fetchCount;

            List<VTigerLead> leadList = this.crmManagerService.GetHomePageLeads<VTigerLead>(interest, startVal, fetchCount).ToList();
            try
            {
                List<LeadModel> leads = this.mapperFactory.GetMapper<List<VTigerLead>, List<LeadModel>>().Map(leadList);
                foreach (var items in leads)
                {
                    string userId = this.youfferContactService.GetMappingEntryByCrmLeadId(items.Id.ToString()).ContactId;
                    CountryModel countryDet = this.commonService.GetUserCountryDetailsFromName(items.Country.ToString());
                    decimal rank = this.crmManagerService.GetUserRank(items.OwnerContactId);

                    lst.Add(new UserResultModel
                    {
                        Id = userId,
                        FirstName = items.FirstName,
                        LastName = items.LastName,
                        Gender = items.Gender,
                        Email = items.Email,
                        Birthday = items.Birthday,
                        Rank = rank,
                        CountryDetails = countryDet,
                        ImageURL = items.ImageURL,
                        Description = items.Description,
                        IsAvailable = items.IsAvailable,
                        IsOnline = items.IsOnline,
                        MainInterest = items.MainInterest[0].Split(new string[] { " |##| " }, StringSplitOptions.None).Where(x => !string.IsNullOrEmpty(x)).ToArray(),
                        SubInterest = items.SubInterest[0].Split(new string[] { " |##| " }, StringSplitOptions.None).Where(x => !string.IsNullOrEmpty(x)).ToArray(),
                        Price = AppSettings.Get<double>(ConfigConstants.UserPrice)
                    });
                }

                lst = lst.OrderByDescending(x => x.Rank).ToList();
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("get home page lead" + ex.Message);
            }

            return this.Ok(lst);
        }

        /// <summary>
        /// Gets the home page leads.
        /// </summary>
        /// <param name="topLeadsDto">The top leads Dto</param>
        /// <returns>IHttpActionResult object.</returns>
        [Route("top-leads1")]
        [HttpPost]
        public async Task<IHttpActionResult> GetHomePageLeads1(TopLeadsDto topLeadsDto)
        {
            if (topLeadsDto.FetchCount == 0)
            {
                topLeadsDto.FetchCount = AppSettings.Get<int>(ConfigConstants.HomePageLeadsDefaultCount);
            }

            List<UserResultModel> lst = new List<UserResultModel>();
            List<VTigerDashBoardData> leadList = this.crmManagerService.GetTopLeads(topLeadsDto);
            try
            {
                foreach (var lead in leadList)
                {
                    UserResultModel items = this.mapperFactory.GetMapper<VTigerDashBoardData, UserResultModel>().Map(lead);
                    string userId = this.youfferContactService.GetMappingEntryByCrmLeadId(lead.cf_773).ContactId;
                    CountryModel countryDet = this.commonService.GetUserCountryDetailsFromName(lead.country);
                    items.Id = userId;
                    items.CountryDetails = countryDet;
                    items.Price = AppSettings.Get<double>(ConfigConstants.UserPrice);

                    lst.Add(items);
                }
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("get home page lead" + ex.Message);
            }

            return this.Ok(lst);
        }

        /// <summary>
        /// Gets the contact us dept.
        /// </summary>
        /// <returns>IHttpActionResult object.</returns>
        [Route("department")]
        [HttpGet]
        public async Task<IHttpActionResult> GetContactUsDept()
        {
            List<DepartmentModel> lstDept = this.commonService.GetContactUsDeptMetaData();
            return this.Ok(lstDept);
        }

        /// <summary>
        /// Gets the state meta data.
        /// </summary>
        /// <returns>IHttpActionResult object.</returns>
        [Route("states")]
        [HttpGet]
        public async Task<IHttpActionResult> GetStateMetaData()
        {
            List<string> lstStates = this.commonService.GetStateMetaData();
            return this.Ok(lstStates);
        }

        /// <summary>
        /// Gets the mobile application version.
        /// </summary>
        /// <param name="appversion">The appversion.</param>
        /// <returns>IHttpActionResult object.</returns>
        [Route("appversion")]
        [HttpPost]
        public async Task<IHttpActionResult> GetMobileAppVersion(MobileAppVersionDto appversion)
        {
            var data = this.commonService.GetMobileAppVersion(appversion);
            return this.Ok(data);
        }

        /// <summary>
        /// Gets the current country details.
        /// </summary>
        /// <param name="ipAddress">The ip address.</param>
        /// <returns>IHttpActionResult object.</returns>
        [Route("currentcountry")]
        [HttpGet]
        public async Task<IHttpActionResult> GetCurrentCountryDetails(string ipAddress)
        {
            CountryDto countryData = this.ip2LocationService.GetCountryData(ipAddress);
            return this.Ok(countryData);
        }
    }
}