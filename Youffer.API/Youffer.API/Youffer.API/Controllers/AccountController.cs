// ---------------------------------------------------------------------------------------------------
// <copyright file="AccountController.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-14</date>
// <summary>
//     The AccountController class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Net.Http;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http;
    using Facebook;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.OAuth;
    using Newtonsoft.Json;
    using Youffer.API.Results;
    using Youffer.Common.CRMService;
    using Youffer.Common.DataService;
    using Youffer.Common.Helper;
    using Youffer.Common.LogService;
    using Youffer.Common.Mapper;
    using Youffer.Common.MaxmindGeoIP2;
    using Youffer.Common.Notification;
    using Youffer.Framework.Extensions;
    using Youffer.Framework.Helper;
    using Youffer.Resources.Constants;
    using Youffer.Resources.CRMModel;
    using Youffer.Resources.Enum;
    using Youffer.Resources.Models;
    using Youffer.Resources.ViewModel;
    using Youffer.Resources.ViewModel.MaxmindGeoIP2;

    /// <summary>
    /// The AccountController class
    /// </summary>
    [RoutePrefix("api/Account")]
    public class AccountController : BaseApiController
    {
        /// <summary>
        /// The authentication repository
        /// </summary>
        private readonly IAuthRepository authRepository;

        /// <summary>
        /// The crm manager service
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
        /// The ICommon service instance
        /// </summary>
        private readonly ICommonService commonService;

        /// <summary>
        /// The mapper factory
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// The Push Notification Service
        /// </summary>
        private readonly IPushMessageService pushService;

        /// <summary>
        /// The youffer interest service
        /// </summary>
        private readonly IYoufferInterestService youfferInterestService;

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
        ///  Initializes a new instance of the <see cref="AccountController" /> class.
        /// </summary>
        /// <param name="authRepository">The Authentication Repository.</param>
        /// <param name="loggerService">The Logger Service.</param>
        /// <param name="crmManagerService">The CRM Manager Service.</param>
        /// <param name="userService">The User Service.</param>
        /// <param name="youfferContactService">The YoufferContact Service.</param>
        /// <param name="commonService">The Common service. </param>
        /// <param name="mapperFactory">The Mapper Factory</param>
        /// <param name="pushService">The Push notification Service.</param>
        /// <param name="youfferInterestService">The youffer interest service.</param>
        /// <param name="youfferMessageService">The youffer message service.</param>
        /// <param name="pushMessageService">The push message service.</param>
        /// <param name="ip2LocationService">The IP2Location service.</param>
        public AccountController(IAuthRepository authRepository, ILoggerService loggerService, ICRMManagerService crmManagerService, IUserService userService, IYoufferContactService youfferContactService, ICommonService commonService, IMapperFactory mapperFactory, IPushMessageService pushService, IYoufferInterestService youfferInterestService, IYoufferMessageService youfferMessageService, IPushMessageService pushMessageService, IIP2LocationService ip2LocationService)
            : base(loggerService)
        {
            this.pushService = pushService;
            this.mapperFactory = mapperFactory;
            this.authRepository = authRepository;
            this.crmManagerService = crmManagerService;
            this.userService = userService;
            this.youfferContactService = youfferContactService;
            this.commonService = commonService;
            this.youfferInterestService = youfferInterestService;
            this.youfferMessageService = youfferMessageService;
            this.pushMessageService = pushMessageService;
            this.ip2LocationService = ip2LocationService;
        }

        /// <summary>
        /// Gets the authentication.
        /// </summary>
        private IAuthenticationManager Authentication
        {
            get
            {
                return Request.GetOwinContext().Authentication;
            }
        }

        /// <summary>
        /// Test Method
        /// </summary>
        /// <param name="email"> the email</param> 
        /// <returns>IHttpActionResult object</returns>
        [Route("test")]
        [HttpGet]
        public async Task<IHttpActionResult> Test(string email)
        {
            var stat = await this.authRepository.SendForgotPasswordLink(email);
            return this.Ok();
        }

        /// <summary>
        /// The activate.
        /// </summary>
        /// <param name="activationId">The activation id.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("activate/{activationId}")]
        public async Task<IHttpActionResult> Activate(string activationId)
        {
            Guid activationIdGuid = activationId.ToGuid();

            try
            {
                if (activationIdGuid != Guid.Empty)
                {
                    var result = await this.authRepository.ChangeActivationStatus(activationIdGuid);

                    if (result)
                    {
                        return this.Ok();
                    }
                }
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Activate: " + ex.Message);
            }

            this.ModelState.AddModelError(string.Empty, "Invalid activation request!");

            return this.BadRequest(this.ModelState);
        }

        /// <summary>
        /// The Change password.
        /// </summary>
        /// <param name="model">The activation id.</param>
        /// <returns>IHttpActionResult object.</returns>
        [Authorize]
        [HttpPost]
        [Route("changePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordModel model)
        {
            this.LoggerService.LogException("ChangePassword Request -json- " + JsonConvert.SerializeObject(model));
            StatusMessage msg = new StatusMessage();
            try
            {
                string userId = User.Identity.GetUserId();
                msg.ErrorMessage = await this.authRepository.ChangePassword(userId, model.OldPassword, model.NewPassword);
                if (string.IsNullOrWhiteSpace(msg.ErrorMessage))
                {
                    msg.IsSuccess = true;
                    return this.Ok(msg);
                }
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("ChangePassword: " + ex.Message);
                msg.ErrorMessage = ex.Message;
            }

            return this.Ok(msg);
        }

        /// <summary>
        /// The update password after reset.
        /// </summary>
        /// <param name="model">The activation id.</param>
        /// <returns>IHttpActionResult object.</returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("updatePassword")]
        public async Task<IHttpActionResult> UpdatePasswordAfterRest(ForgotPassword model)
        {
            this.LoggerService.LogException("UpdatePasswordAfterRest Request -json- " + JsonConvert.SerializeObject(model));
            StatusMessage msg = new StatusMessage();
            try
            {
                msg.ErrorMessage = await this.authRepository.UpdatePasswordAfterReset(model.ResetPwdGuid, model.NewPassword);
                if (string.IsNullOrWhiteSpace(msg.ErrorMessage))
                {
                    msg.IsSuccess = true;
                    return this.Ok(msg);
                }
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("ChangePassword: " + ex.Message);
                msg.ErrorMessage = ex.Message;
            }

            return this.Ok(msg);
        }

        /// <summary>
        /// The send password link.
        /// </summary>
        /// <param name="email">The email id.</param>
        /// <returns>IHttpActionResult object.</returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("SendResetPasswordLink")]
        public async Task<IHttpActionResult> SendResetPasswordLink(string email)
        {
            StatusMessage msg = new StatusMessage();
            try
            {
                msg.ErrorMessage = await this.authRepository.SendForgotPasswordLink(email);
                if (string.IsNullOrWhiteSpace(msg.ErrorMessage))
                {
                    msg.IsSuccess = true;
                    return this.Ok(msg);
                }
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("SendResetPasswordLink: " + ex.Message);
                msg.ErrorMessage = ex.Message;
            }

            return this.Ok(msg);
        }

        /// <summary>
        /// Registers the specified user model. POST api/Account/Register
        /// </summary>
        /// <param name="userModel">The user model.</param>
        /// <returns>Registration status</returns>
        [AllowAnonymous]
        [Route("register")]
        [HttpPost]
        public async Task<IHttpActionResult> Register(UserModel userModel)
        {
            try
            {
                this.LoggerService.LogException("Register Request -json- " + JsonConvert.SerializeObject(userModel));
            }
            catch (Exception ex1)
            {
            }

            if (string.IsNullOrEmpty(userModel.Country) && !string.IsNullOrEmpty(userModel.CountryCode))
            {
                userModel.Country = this.commonService.GetCountryFromISO2Code(userModel.CountryCode);
            }

            if (string.IsNullOrEmpty(userModel.Country))
            {
                userModel.Country = "United States";
            }

            ////string ipAddress = string.IsNullOrEmpty(userModel.IPAddress) ? HttpContext.Current.Request.UserHostAddress : userModel.IPAddress;
            ////CityDto cityData = this.ip2LocationService.GetCityData(ipAddress);
            ////userModel.Country = cityData.CountryInfo.CountryName;
            ////userModel.State = cityData.MostSpecificSubDivisionName == null ? string.Empty : cityData.MostSpecificSubDivisionName;
            ////userModel.Latitude = cityData.Latitude;
            ////userModel.Longitude = cityData.Longitude;

            ////Only for handling old Android apps
            List<string> lstCallingCodes = this.commonService.GetCountryMetaData().Select(x => x.CallingCode).ToList();
            try
            {
                IEnumerable<string> code = lstCallingCodes.Where(x => x == userModel.CountryCode);
                if (!string.IsNullOrEmpty(code.FirstOrDefault()))
                {
                    userModel.CountryCode = this.commonService.GetISO2CodeFromCallingCode(code.FirstOrDefault());
                }
            }
            catch
            {
            }

            StatusMessage status = new StatusMessage();
            Roles userRole;

            bool isRoleParsed = Enum.TryParse(userModel.Role.ToString(), true, out userRole);

            if (!Roles.IsDefined(typeof(Roles), userModel.Role) || userRole == Roles.Admin)
            {
                return this.BadRequest();
            }

            userModel.UserRole = userRole.ToString();

            if (!this.ModelState.IsValid)
            {
                if (userModel.OSType != OSType.Web)
                {
                    string errorMsg = this.ModelState.Values.First().Errors[0].ErrorMessage;
                    return this.BadRequest(errorMsg);
                }

                return this.BadRequest(this.ModelState);
            }

            IdentityResult result;
            try
            {
                result = await this.authRepository.RegisterUser(userModel);
                if (!result.Succeeded)
                {
                    string errorMessage = result.Errors.Any() ? result.Errors.First() : string.Empty;
                    return this.BadRequest(errorMessage);
                }

                var user = await this.authRepository.FindUser(userModel.EmailId, userModel.Password);

                string defaultImageUrl = AppSettings.Get<string>(ConfigConstants.ApiBaseUrl) + AppSettings.Get<string>(ConfigConstants.DefaultProfileImage);
                string imageUrl = ImageHelper.SaveImageFromUrl(defaultImageUrl, user.Id);
                if (userModel.UserRole == Roles.Company.ToString())
                {
                    OrganisationModel orgModel = new OrganisationModel();
                    orgModel = this.mapperFactory.GetMapper<UserModel, OrganisationModel>().Map(userModel);

                    orgModel.ImageURL = imageUrl;
                    orgModel.IsImageUploaded = false;
                    orgModel.CashBalance = orgModel.CreditBalance = 0;

                    ////Only for handling old Android apps
                    if (!string.IsNullOrEmpty(userModel.SubBusinessType) && userModel.SubBusinessType.Length > 0)
                    {
                        orgModel.SubBusinessType = userModel.SubBusinessType.Split(new string[] { "," }, StringSplitOptions.None).Where(x => !string.IsNullOrEmpty(x)).ToArray();
                        orgModel.MainBusinessType = this.youfferInterestService.GetMainBusinessTypeFromSub(userModel.SubBusinessType).Select(x => x.ParentBusinessTypeName).Distinct().ToArray();
                    }

                    if (!string.IsNullOrEmpty(userModel.MainBusinessType) && userModel.MainBusinessType.Length > 0)
                    {
                        orgModel.MainBusinessType = userModel.MainBusinessType.Split(',').ToArray();
                        string[] subInterest = this.youfferInterestService.GetSubBusinessTypeFromMain(userModel.MainBusinessType).Select(x => x.BusinessTypeName).Distinct().ToArray();
                        orgModel.SubBusinessType = subInterest;
                    }

                    orgModel = this.crmManagerService.AddOrganisation(orgModel, user);

                    ////Send username and password email
                    await this.SendUserNamePasswordEmail(orgModel.Email1, orgModel.Password);
                }
                else
                {
                    ContactModel contactModel = new ContactModel();
                    contactModel = this.mapperFactory.GetMapper<UserModel, ContactModel>().Map(userModel);
                    contactModel.ImageURL = imageUrl;

                    contactModel = this.crmManagerService.AddContact(contactModel, user);
                }
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Register: " + ex.Message);
                this.ModelState.AddModelError("Exception", ex.Message);

                if (userModel.OSType != OSType.Web)
                {
                    return this.BadRequest(ex.Message);
                }

                return this.BadRequest(this.ModelState);
            }

            IHttpActionResult errorResult = this.GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            status.IsSuccess = true;
            return this.Ok(status);
        }

        /// <summary>
        /// Gets the external login.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="error">The error.</param>
        /// <param name="role">The userRole.</param>
        /// <returns>IHttpActionResult object</returns>
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        [AllowAnonymous]
        [Route("ExternalLogin", Name = "ExternalLogin")]
        public async Task<IHttpActionResult> GetExternalLogin(string provider, string error = null, int role = 2)
        {
            string redirectUri = string.Empty;
            Roles userRole = (Roles)role;
            if (error != null)
            {
                return this.BadRequest(Uri.EscapeDataString(error));
            }

            if (!User.Identity.IsAuthenticated)
            {
                return new ChallengeResult(provider, this);
            }

            var redirectUriValidationResult = this.ValidateClientAndRedirectUri(ref redirectUri);

            if (!string.IsNullOrWhiteSpace(redirectUriValidationResult))
            {
                return this.BadRequest(redirectUriValidationResult);
            }

            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            if (externalLogin == null)
            {
                return this.InternalServerError();
            }

            if (externalLogin.LoginProvider != provider)
            {
                this.Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                return new ChallengeResult(provider, this);
            }

            var user = await this.authRepository.FindAsync(new UserLoginInfo(externalLogin.LoginProvider, externalLogin.ProviderKey));

            bool hasRegistered = user != null;
            if (!hasRegistered)
            {
                RegisterExternalBindingModel model = new RegisterExternalBindingModel()
                {
                    ExternalAccessToken = externalLogin.ExternalAccessToken,
                    Provider = externalLogin.LoginProvider,
                    UserName = externalLogin.UserName,
                    UserRole = userRole
                };
                return await this.RegisterExternal(model);
            }

            if (!string.IsNullOrEmpty(redirectUri))
            {
                redirectUri = string.Format(
                                                "{0}?external_access_token={1}&provider={2}&haslocalaccount={3}&external_user_name={4}",
                                                redirectUri,
                                                externalLogin.ExternalAccessToken,
                                                externalLogin.LoginProvider,
                                                hasRegistered.ToString(),
                                                externalLogin.UserName);
                return this.Redirect(redirectUri);
            }

            ExternalLoginResultModel externalLoginResultModel = new ExternalLoginResultModel()
            {
                ExternalAccessToken = externalLogin.ExternalAccessToken,
                Provider = externalLogin.LoginProvider,
                HasLocalAccount = hasRegistered,
                ExternalUserName = user.UserName
            };

            return this.Ok(externalLoginResultModel);
        }

        /// <summary>
        /// Gets the external login.
        /// </summary>
        /// <param name="model">The RegisterExternalBindingModel object.</param>
        /// <returns>IHttpActionResult object</returns>
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        [AllowAnonymous]
        [HttpPost]
        [Route("MExternalLoginiOS", Name = "MExternalLoginiOS")]
        public async Task<IHttpActionResult> GetMExternalLoginiOS(RegisterExternalBindingModel model)
        {
            try
            {
                try
                {
                    this.LoggerService.LogException("MExternalLoginiOS Request -json- " + JsonConvert.SerializeObject(model));
                }
                catch (Exception ex1)
                {
                }

                var verifiedAccessToken = await this.VerifyExternalAccessToken(model.Provider, model.ExternalAccessToken);
                if (verifiedAccessToken == null)
                {
                    return this.BadRequest("Invalid Provider or External Access Token");
                }

                var user = await this.authRepository.FindAsync(new UserLoginInfo(model.Provider, verifiedAccessToken.UserId));

                model.IsFromMobile = true;
                model.UserRole = (Roles)model.Role;
                model.OSType = (OSType)model.OS;

                if (!string.IsNullOrEmpty(model.CountryCode))
                {
                    model.Country = this.commonService.GetCountryFromISO2Code(model.CountryCode);
                }

                if (string.IsNullOrEmpty(model.Country))
                {
                    model.Country = "United States";
                }

                ////string ipAddress = string.IsNullOrEmpty(model.IPAddress) ? HttpContext.Current.Request.UserHostAddress : model.IPAddress;
                ////this.LoggerService.LogException("IP Address from Request - " + ipAddress);
                ////CityDto cityData = this.ip2LocationService.GetCityData(ipAddress);
                ////model.Country = cityData.CountryInfo.CountryName;
                ////model.State = cityData.MostSpecificSubDivisionName == null ? string.Empty : cityData.MostSpecificSubDivisionName;
                ////model.Latitude = cityData.Latitude;
                ////model.Longitude = cityData.Longitude;

                bool hasRegistered = user != null;

                if (!hasRegistered)
                {
                    return await this.RegisterExternal(model);
                }

                AccessTokenModel accessTokenResponse = this.GenerateLocalAccessTokenResponse(user);
                CountryModel countryDet = this.commonService.GetUserCountryDetails(user.Id);

                if (model.UserRole == Roles.Customer)
                {
                    string crmContactId = this.userService.GetContact(user.Id).CRMId;
                    decimal rank = this.crmManagerService.GetUserRank(crmContactId);
                    string crmLeadId = string.Empty;
                    if (!string.IsNullOrEmpty(crmContactId))
                    {
                        crmLeadId = this.youfferContactService.GetMappingEntryByContactId(user.Id).LeadId;
                    }

                    ContactModel contactModel = new ContactModel();
                    contactModel = this.crmManagerService.GetContact(user.Id);
                    contactModel.IsOnline = true;
                    if (!string.IsNullOrWhiteSpace(model.GCMId))
                    {
                        contactModel.GCMId = model.GCMId;
                    }

                    if (!string.IsNullOrWhiteSpace(model.UDId))
                    {
                        contactModel.UDId = model.UDId;
                    }

                    contactModel = this.crmManagerService.UpdateContact(user.Id, contactModel);

                    LeadModel leadModel = new LeadModel();
                    UserResultModel userResultModel = new UserResultModel();

                    if (!string.IsNullOrEmpty(crmLeadId))
                    {
                        leadModel = this.crmManagerService.GetLead(crmLeadId);

                        userResultModel = this.mapperFactory.GetMapper<LeadModel, UserResultModel>().Map(leadModel) ?? new UserResultModel();
                        userResultModel.Id = user.Id;
                        userResultModel.ExternalAccessToken = accessTokenResponse;
                        userResultModel.Rank = rank;
                        userResultModel.CountryDetails = countryDet;
                        userResultModel.UserRole = Roles.Customer;
                        userResultModel.PaymentDetails = new PaymentModelDto()
                        {
                            PayPalId = leadModel.PaypalId,
                            Mode = (PaymentMode)leadModel.PaymentMode
                        };
                    }
                    else
                    {
                        userResultModel = this.mapperFactory.GetMapper<ContactModel, UserResultModel>().Map(contactModel) ?? new UserResultModel();
                        userResultModel.Id = user.Id;
                        userResultModel.ExternalAccessToken = accessTokenResponse;
                        userResultModel.CountryDetails = countryDet;
                        userResultModel.UserRole = Roles.Customer;
                        userResultModel.PaymentDetails = new PaymentModelDto()
                        {
                            PayPalId = contactModel.PaypalId,
                            Mode = (PaymentMode)contactModel.PaymentMode
                        };
                    }

                    try
                    {
                        this.LoggerService.LogException("MExternalLoginiOS Response -json- " + JsonConvert.SerializeObject(userResultModel));
                    }
                    catch (Exception ex1)
                    {
                    }

                    return this.Ok(userResultModel);
                }
                else
                {
                    OrgResultModel orgResultModel = new OrgResultModel();
                    OrganisationModel orgModel = new OrganisationModel();

                    orgModel = this.crmManagerService.GetOrganisation(user.Id);
                    orgResultModel = this.mapperFactory.GetMapper<OrganisationModel, OrgResultModel>().Map(orgModel) ?? new OrgResultModel();
                    orgResultModel.Id = user.Id;
                    orgResultModel.ExternalAccessToken = accessTokenResponse.ToString();
                    orgResultModel.CountryDetails = countryDet;
                    orgResultModel.UserRole = Roles.Company;
                    orgResultModel.PaymentDetails = string.Empty;

                    try
                    {
                        this.LoggerService.LogException("MExternalLoginiOS Response -json- " + JsonConvert.SerializeObject(orgResultModel));
                    }
                    catch (Exception ex1)
                    {
                    }

                    return this.Ok(orgResultModel);
                }
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("MExternalLoginiOS : " + ex.Message);
                return this.BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Gets the external login.
        /// </summary>
        /// <param name="model">The RegisterExternalBindingModel object.</param>
        /// <returns>IHttpActionResult object</returns>
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        [AllowAnonymous]
        [HttpPost]
        [Route("MExternalLogin", Name = "MExternalLogin")]
        public async Task<IHttpActionResult> GetMExternalLogin(RegisterExternalBindingModel model)
        {
            try
            {
                try
                {
                    this.LoggerService.LogException("MExternalLogin Request -json- " + JsonConvert.SerializeObject(model));
                }
                catch (Exception ex1)
                {
                }

                var verifiedAccessToken = await this.VerifyExternalAccessToken(model.Provider, model.ExternalAccessToken);
                if (verifiedAccessToken == null)
                {
                    return this.BadRequest("Invalid Provider or External Access Token");
                }

                var user = await this.authRepository.FindAsync(new UserLoginInfo(model.Provider, verifiedAccessToken.UserId));

                model.IsFromMobile = true;
                model.UserRole = (Roles)model.Role;
                model.OSType = (OSType)model.OS;

                ////string ipAddress = string.IsNullOrEmpty(model.IPAddress) ? HttpContext.Current.Request.UserHostAddress : model.IPAddress;
                ////this.LoggerService.LogException("IP Address from Request - " + ipAddress);
                ////CityDto cityData = this.ip2LocationService.GetCityData(ipAddress);
                ////model.Country = cityData.CountryInfo.CountryName;
                ////model.State = cityData.MostSpecificSubDivisionName == null ? string.Empty : cityData.MostSpecificSubDivisionName;
                ////model.Latitude = cityData.Latitude;
                ////model.Longitude = cityData.Longitude;

                bool hasRegistered = user != null;

                if (!hasRegistered)
                {
                    return await this.RegisterExternal(model);
                }

                AccessTokenModel accessTokenResponse = this.GenerateLocalAccessTokenResponse(user);
                CountryModel countryDet = this.commonService.GetUserCountryDetails(user.Id);

                if (model.UserRole == Roles.Customer)
                {
                    string crmContactId = this.userService.GetContact(user.Id).CRMId;
                    decimal rank = this.crmManagerService.GetUserRank(crmContactId);
                    string crmLeadId = string.Empty;
                    if (!string.IsNullOrEmpty(crmContactId))
                    {
                        crmLeadId = this.youfferContactService.GetMappingEntryByContactId(user.Id).LeadId;
                    }

                    ContactModel contactModel = new ContactModel();
                    contactModel = this.crmManagerService.GetContact(user.Id);
                    contactModel.IsOnline = true;
                    if (!string.IsNullOrWhiteSpace(model.GCMId))
                    {
                        contactModel.GCMId = model.GCMId;
                    }

                    if (!string.IsNullOrWhiteSpace(model.UDId))
                    {
                        contactModel.UDId = model.UDId;
                    }

                    contactModel = this.crmManagerService.UpdateContact(user.Id, contactModel);

                    LeadModel leadModel = new LeadModel();
                    UserResultModel userResultModel = new UserResultModel();

                    if (!string.IsNullOrEmpty(crmLeadId))
                    {
                        leadModel = this.crmManagerService.GetLead(crmLeadId);

                        userResultModel = this.mapperFactory.GetMapper<LeadModel, UserResultModel>().Map(leadModel) ?? new UserResultModel();
                        userResultModel.Id = user.Id;
                        userResultModel.ExternalAccessToken = accessTokenResponse;
                        userResultModel.Rank = rank;
                        userResultModel.CountryDetails = countryDet;
                        userResultModel.UserRole = Roles.Customer;
                        userResultModel.PaymentDetails = new PaymentModelDto()
                        {
                            PayPalId = leadModel.PaypalId,
                            Mode = (PaymentMode)leadModel.PaymentMode
                        };
                    }
                    else
                    {
                        userResultModel = this.mapperFactory.GetMapper<ContactModel, UserResultModel>().Map(contactModel) ?? new UserResultModel();
                        userResultModel.Id = user.Id;
                        userResultModel.ExternalAccessToken = accessTokenResponse;
                        userResultModel.CountryDetails = countryDet;
                        userResultModel.UserRole = Roles.Customer;
                        userResultModel.PaymentDetails = new PaymentModelDto()
                        {
                            PayPalId = contactModel.PaypalId,
                            Mode = (PaymentMode)contactModel.PaymentMode
                        };
                    }

                    return this.Ok(userResultModel);
                }
                else
                {
                    OrgResultModel orgResultModel = new OrgResultModel();
                    OrganisationModel orgModel = new OrganisationModel();

                    orgModel = this.crmManagerService.GetOrganisation(user.Id);
                    orgResultModel = this.mapperFactory.GetMapper<OrganisationModel, OrgResultModel>().Map(orgModel) ?? new OrgResultModel();
                    orgResultModel.Id = user.Id;
                    orgResultModel.ExternalAccessToken = accessTokenResponse.ToString();
                    orgResultModel.CountryDetails = countryDet;
                    orgResultModel.UserRole = Roles.Company;
                    orgResultModel.PaymentDetails = string.Empty;

                    return this.Ok(orgResultModel);
                }
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetMExternalLogin : " + ex.Message);
                return this.BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Registers the external.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>IHttpActionResult object</returns>
        [AllowAnonymous]
        [Route("RegisterExternal")]
        public async Task<IHttpActionResult> RegisterExternal(RegisterExternalBindingModel model)
        {
            ExternalLoginResultModel externalLoginResultModel = new ExternalLoginResultModel();
            try
            {
                model.Country = string.IsNullOrWhiteSpace(model.Country) ? string.Empty : HttpUtility.UrlDecode(model.Country);
                ContactModel userModel = new ContactModel();
                OrganisationModel orgModel = new OrganisationModel();

                if (!this.ModelState.IsValid)
                {
                    return this.BadRequest(this.ModelState);
                }

                var verifiedAccessToken = await this.VerifyExternalAccessToken(model.Provider, model.ExternalAccessToken);
                if (verifiedAccessToken == null)
                {
                    return this.BadRequest("Invalid Provider or External Access Token");
                }

                var user = await this.authRepository.FindAsync(new UserLoginInfo(model.Provider, verifiedAccessToken.UserId));

                bool hasRegistered = user != null;

                if (!hasRegistered)
                {
                    userModel = this.GetExternalUserDetails(model.Provider, model.ExternalAccessToken);
                    if (string.IsNullOrEmpty(userModel.Email))
                    {
                        userModel.Email = model.Email;
                    }

                    userModel.GCMId = model.GCMId;
                    userModel.UDId = model.UDId;
                    userModel.OSType = model.OSType;
                    userModel.MailingCountry = model.Country;
                    userModel.MailingState = model.State;
                    userModel.CountryCode = model.CountryCode;
                    userModel.Timezone = model.Timezone;
                    userModel.Latitude = model.Latitude;
                    userModel.Longitude = model.Longitude;
                    userModel.IsAvailable = true;
                    userModel.Availability = Availability.AllDays;
                    userModel.AvailableFrom = "08:00:00";
                    userModel.AvailableTo = "20:00:00";
                    userModel.IsOnline = true;
                    userModel.IsActive = true;
                    string userName = userModel.Email;
                    if (string.IsNullOrWhiteSpace(userModel.Email))
                    {
                        userName = Guid.NewGuid().ToString();
                    }

                    user = new ApplicationUserDto
                    {
                        UserName = userName,
                        Email = userModel.Email,
                        Name = userModel.FirstName,
                        AccountType = (short)model.UserRole
                    };

                    if (model.Provider == "Facebook")
                    {
                        user.FacebookId = verifiedAccessToken.UserId;
                        user.FBAccessToken = model.ExternalAccessToken;
                    }
                    else
                    {
                        user.GoogleId = verifiedAccessToken.UserId;
                        user.GoogleAccessToken = model.ExternalAccessToken;
                    }

                    IdentityResult result = await this.authRepository.CreateAsync(user, model.UserRole.ToString());
                    if (!result.Succeeded)
                    {
                        string errorMessage = result.Errors.Any() ? result.Errors.First() : string.Empty;
                        return this.BadRequest(errorMessage);
                    }

                    var info = new ExternalLoginInfo
                    {
                        DefaultUserName = model.UserName,
                        Login = new UserLoginInfo(model.Provider, verifiedAccessToken.UserId)
                    };

                    result = await this.authRepository.AddLoginAsync(user.Id, info.Login);
                    if (!result.Succeeded)
                    {
                        return this.GetErrorResult(result);
                    }

                    userModel.ImageURL = ImageHelper.SaveImageFromUrl(userModel.ImageURL, user.Id);

                    if (model.UserRole == Roles.Company)
                    {
                        orgModel.AccountName = userModel.FirstName;
                        orgModel.Email1 = userModel.Email;
                        orgModel.FacebookURL = userModel.FBUrl;
                        orgModel.GooglePlusURL = userModel.GooglePlusURL;
                        orgModel.ImageURL = userModel.ImageURL;
                        orgModel = this.crmManagerService.AddOrganisation(orgModel, user);
                    }
                    else
                    {
                        userModel = this.crmManagerService.AddContact(userModel, user);
                    }
                }

                if (model.IsFromMobile)
                {
                    var accessTokenResponse = this.GenerateLocalAccessTokenResponse(user);
                    UserResultModel userResultModel = new UserResultModel();
                    OrgResultModel orgResultModel = new OrgResultModel();

                    CountryModel countryDet = this.commonService.GetUserCountryDetailsFromName(userModel.MailingCountry);

                    if (model.UserRole == Roles.Customer)
                    {
                        userResultModel = this.mapperFactory.GetMapper<ContactModel, UserResultModel>().Map(userModel);
                        userResultModel.ExternalAccessToken = accessTokenResponse;
                        userResultModel.Rank = 0.0M;
                        userResultModel.CountryDetails = countryDet;
                        userResultModel.UserRole = Roles.Customer;
                        userResultModel.IsActive = userModel.IsActive;
                        userResultModel.PaymentDetails = new PaymentModelDto()
                        {
                            PayPalId = userModel.PaypalId,
                            Mode = (PaymentMode)userModel.PaymentMode
                        };

                        MessagesDto message = new MessagesDto();
                        message.ModifiedBy = message.FromUser = message.CreatedBy = "YoufferAdmin";
                        message.IsDeleted = false;
                        message.CompanyId = AppSettings.Get<string>(ConfigConstants.SuperUserId);
                        message.UserId = message.ToUser = user.Id;
                        message.Name = "Youffer Admin";
                        message.MediaId = 0;

                        MessageTemplatesDto msgTemplate = this.commonService.GetMessageTemplate(MessageTemplateType.WelcomeMsg);
                        message.Message = msgTemplate.TemplateText;
                        this.youfferMessageService.CreateMessage(message);
                        int unreadMsgCount = this.youfferMessageService.GetUnreadMsgCount(message.UserId, false);

                        if (!string.IsNullOrEmpty(userResultModel.GCMId))
                        {
                            this.pushMessageService.SendMessageNotificationToAndroid(userResultModel.GCMId, message.Id.ToString(), message.Message, "Youffer", Notification.companymsg.ToString());
                        }

                        if (!string.IsNullOrEmpty(userResultModel.UDId))
                        {
                            this.pushMessageService.SendMessageNotificationToiOS(userResultModel.UDId, message.Id.ToString(), message.Message, "Youffer", unreadMsgCount, Notification.usermsg.ToString());
                        }

                        return this.Ok(userResultModel);
                    }
                    else
                    {
                        orgResultModel = this.mapperFactory.GetMapper<OrganisationModel, OrgResultModel>().Map(orgModel);
                        orgResultModel.ExternalAccessToken = accessTokenResponse.ToString();
                        orgResultModel.CountryDetails = countryDet;
                        orgResultModel.UserRole = Roles.Company;
                        orgResultModel.PaymentDetails = string.Empty;

                        return this.Ok(orgResultModel);
                    }
                }

                var redirectUri = string.Empty;
                var redirectUriValidationResult = this.ValidateClientAndRedirectUri(ref redirectUri);
                if (!string.IsNullOrEmpty(redirectUri))
                {
                    redirectUri = string.Format(
                                                  "{0}?external_access_token={1}&provider={2}&haslocalaccount={3}&external_user_name={4}",
                                                  redirectUri,
                                                  model.ExternalAccessToken,
                                                  model.Provider,
                                                  hasRegistered.ToString(),
                                                  model.UserName);
                    return this.Redirect(redirectUri);
                }

                externalLoginResultModel.ExternalAccessToken = model.ExternalAccessToken;
                externalLoginResultModel.Provider = model.Provider;
                externalLoginResultModel.HasLocalAccount = hasRegistered;
                externalLoginResultModel.ExternalUserName = model.UserName;
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("RegisterExternal : " + ex.Message);
            }

            return this.Ok(externalLoginResultModel);
        }

        /// <summary>
        /// Obtains the local access token.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="externalAccessToken">The external access token.</param>
        /// <returns>IHttpActionResult object</returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("ObtainLocalAccessToken")]
        public async Task<IHttpActionResult> ObtainLocalAccessToken(string provider, string externalAccessToken)
        {
            if (string.IsNullOrWhiteSpace(provider) || string.IsNullOrWhiteSpace(externalAccessToken))
            {
                return this.BadRequest("Provider or external access token is not sent");
            }

            var verifiedAccessToken = await this.VerifyExternalAccessToken(provider, externalAccessToken);
            if (verifiedAccessToken == null)
            {
                return this.BadRequest("Invalid Provider or External Access Token");
            }

            IdentityUser user = await this.authRepository.FindAsync(new UserLoginInfo(provider, verifiedAccessToken.UserId));
            ApplicationUserDto userDto = (ApplicationUserDto)user;
            bool hasRegistered = user != null;

            if (!hasRegistered)
            {
                return this.BadRequest("External user is not registered");
            }

            // generate access token response
            var accessTokenResponse = this.GenerateLocalAccessTokenResponse(userDto);

            return this.Ok(accessTokenResponse);
        }

        /// <summary>
        /// Releases the unmanaged resources that are used by the object and, optionally, releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.authRepository.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Helpers

        /// <summary>
        /// Gets the error result.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns>IHttpActionResult object</returns>
        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return this.InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        this.ModelState.AddModelError(string.Empty, error);
                    }
                }

                if (this.ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return this.BadRequest();
                }

                return this.BadRequest(this.ModelState);
            }

            return null;
        }

        /// <summary>
        /// Validates the client and redirect URI.
        /// </summary>
        /// <param name="redirectUriOutput">The redirect URI output.</param>
        /// <returns>string result</returns>
        private string ValidateClientAndRedirectUri(ref string redirectUriOutput)
        {
            Uri redirectUri;

            var redirectUriString = this.GetQueryString(Request, "redirect_uri");

            if (string.IsNullOrWhiteSpace(redirectUriString))
            {
                return "redirect_uri is required";
            }

            bool validUri = Uri.TryCreate(redirectUriString, UriKind.Absolute, out redirectUri);

            if (!validUri)
            {
                return "redirect_uri is invalid";
            }

            var clientId = this.GetQueryString(Request, "client_id");

            if (string.IsNullOrWhiteSpace(clientId))
            {
                return "client_Id is required";
            }

            var client = this.authRepository.FindClient(clientId);

            if (client == null)
            {
                return string.Format("Client_id '{0}' is not registered in the system.", clientId);
            }

            if (!string.Equals(client.AllowedOrigin, redirectUri.GetLeftPart(UriPartial.Authority), StringComparison.OrdinalIgnoreCase))
            {
                return string.Format("The given URL is not allowed by Client_id '{0}' configuration.", clientId);
            }

            redirectUriOutput = redirectUri.AbsoluteUri;

            return string.Empty;
        }

        /// <summary>
        /// Gets the query string.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="key">The key.</param>
        /// <returns>string object</returns>
        private string GetQueryString(HttpRequestMessage request, string key)
        {
            var queryStrings = request.GetQueryNameValuePairs();

            if (queryStrings == null)
            {
                return null;
            }

            var match = queryStrings.FirstOrDefault(keyValue => string.Compare(keyValue.Key, key, System.StringComparison.OrdinalIgnoreCase) == 0);

            if (string.IsNullOrEmpty(match.Value))
            {
                return null;
            }

            return match.Value;
        }

        /// <summary>
        /// Verifies the external access token.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="accessToken">The access token.</param>
        /// <returns>ParsedExternalAccessToken object</returns>
        private async Task<ParsedExternalAccessToken> VerifyExternalAccessToken(string provider, string accessToken)
        {
            ParsedExternalAccessToken parsedToken = null;

            string verifyTokenEndPoint;

            if (provider == "Facebook")
            {
                // You can get it from here: https://developers.facebook.com/tools/accesstoken/
                // More about debug_tokn here: http://stackoverflow.com/questions/16641083/how-does-one-get-the-app-access-token-for-debug-token-inspection-on-facebook
                string appToken = AppSettings.Get<string>(ConfigConstants.FaceBookAppToken);
                verifyTokenEndPoint = string.Format("https://graph.facebook.com/debug_token?input_token={0}&access_token={1}", accessToken, appToken);
            }
            else if (provider == "Google")
            {
                verifyTokenEndPoint = string.Format("https://www.googleapis.com/oauth2/v1/tokeninfo?access_token={0}", accessToken);
            }
            else
            {
                return null;
            }

            var client = new HttpClient();
            var uri = new Uri(verifyTokenEndPoint);
            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                dynamic jObj = Newtonsoft.Json.JsonConvert.DeserializeObject(content);

                parsedToken = new ParsedExternalAccessToken();

                if (provider == "Facebook")
                {
                    parsedToken.UserId = jObj["data"]["user_id"];
                    parsedToken.AppId = jObj["data"]["app_id"];

                    if (!string.Equals(Startup.FacebookAuthOptions.AppId, parsedToken.AppId, StringComparison.OrdinalIgnoreCase))
                    {
                        return null;
                    }
                }
                else if (provider == "Google")
                {
                    parsedToken.UserId = jObj["user_id"];
                    parsedToken.AppId = jObj["audience"];

                    if (!string.Equals(Startup.GoogleAuthOptions.ClientId, parsedToken.AppId, StringComparison.OrdinalIgnoreCase))
                    {
                        return null;
                    }
                }
            }

            return parsedToken;
        }

        /// <summary>
        /// Gets the external user details.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="accessToken">The access token.</param>
        /// <returns>ExtenalUserModel object</returns>
        private ContactModel GetExternalUserDetails(string provider, string accessToken)
        {
            ContactModel userModel = new ContactModel();

            string verifyTokenEndPoint;

            if (provider == "Facebook")
            {
                FacebookClient fbClient = new FacebookClient();
                fbClient.AccessToken = accessToken;

                try
                {
                    var content = fbClient.Get("me");
                    dynamic jObj = Newtonsoft.Json.JsonConvert.DeserializeObject(content.ToString());

                    userModel.FirstName = jObj["first_name"];
                    userModel.Gender = jObj["gender"] + string.Empty;
                    userModel.LastName = jObj["last_name"];
                    userModel.Birthday = jObj["birthday"];
                    userModel.Email = jObj["email"];
                    userModel.Timezone = jObj["timezone"];
                    userModel.ImageURL = "https://graph.facebook.com/" + jObj["id"] + string.Empty + "/picture?type=large";
                    userModel.FBUrl = jObj["link"];
                    userModel.Gender = userModel.Gender.Capitalize();

                    if (string.IsNullOrEmpty(userModel.Email))
                    {
                        this.LoggerService.LogException("GetExternalUserDetails Email : Email not found.");
                    }

                    try
                    {
                        DateTime bday = new DateTime();
                        var isConverted = DateTime.TryParse(userModel.Birthday, out bday);
                        if (isConverted)
                        {
                            userModel.Birthday = bday.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        this.LoggerService.LogException("GetExternalUserDetails BirthDay : " + ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    this.LoggerService.LogException("GetExternalUserDetails FBDetails : " + ex.Message);
                }
            }
            else if (provider == "Google")
            {
                Uri apiRequestUri = new Uri("https://www.googleapis.com/oauth2/v2/userinfo?access_token=" + accessToken);
                ////request profile image
                using (var webClient = new System.Net.WebClient())
                {
                    var json = webClient.DownloadString(apiRequestUri);
                    dynamic result = JsonConvert.DeserializeObject(json);
                    userModel.ImageURL = result.picture;
                    userModel.FirstName = result.given_name + string.Empty;
                    userModel.LastName = result.family_name + string.Empty;
                    userModel.Gender = result.gender + string.Empty;
                    userModel.Email = result.email;
                    if (!string.IsNullOrWhiteSpace(userModel.Gender))
                    {
                        userModel.Gender = userModel.Gender.Capitalize();
                    }
                }

                verifyTokenEndPoint = string.Format("https://www.googleapis.com/oauth2/v1/tokeninfo?access_token={0}", accessToken);
            }
            else
            {
                return null;
            }

            return userModel;
        }

        /// <summary>
        /// Generates the local access token response.
        /// </summary>
        /// <param name="user"> The Application user.</param>
        /// <returns>AccessTokenModel object</returns>
        private AccessTokenModel GenerateLocalAccessTokenResponse(ApplicationUserDto user)
        {
            var tokenExpiration = TimeSpan.FromDays(10);
            string roleName = user.RoleName;
            if (string.IsNullOrEmpty(roleName))
            {
                roleName = this.authRepository.GetRoleName(user.Roles.ToList()[0].RoleId);
            }

            ClaimsIdentity identity = new ClaimsIdentity(OAuthDefaults.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
            identity.AddClaim(new Claim(ClaimTypes.Role, roleName));
            identity.AddClaim(new Claim("sub", user.UserName));
            identity.AddClaim(new Claim("role", roleName));

            var props = new AuthenticationProperties
            {
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.Add(tokenExpiration),
            };

            var ticket = new AuthenticationTicket(identity, props);

            var accessToken = Startup.OAuthBearerOptions.AccessTokenFormat.Protect(ticket);

            ////JObject tokenResponse = new JObject(
            ////                            new JProperty("userName", user.UserName),
            ////                            new JProperty("access_token", accessToken),
            ////                            new JProperty("userId", user.Id),
            ////                            new JProperty("token_type", "bearer"),
            ////                            new JProperty("expires_in", tokenExpiration.TotalSeconds.ToString(CultureInfo.InvariantCulture)),
            ////                            new JProperty("issued", ticket.Properties.IssuedUtc.ToString()),
            ////                            new JProperty("expires", ticket.Properties.ExpiresUtc.ToString()));

            AccessTokenModel accessTokenModel = new AccessTokenModel();
            accessTokenModel.UserName = user.UserName;
            accessTokenModel.AccessToken = accessToken;
            accessTokenModel.UserId = user.Id;
            accessTokenModel.TokenType = "bearer";
            accessTokenModel.ExpiresIn = tokenExpiration.TotalSeconds.ToString(CultureInfo.InvariantCulture);
            accessTokenModel.Issued = ticket.Properties.IssuedUtc.ToString();
            accessTokenModel.Expires = ticket.Properties.ExpiresUtc.ToString();

            return accessTokenModel;
        }

        /// <summary>
        /// Sends the user name password email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns>IHttpActionResult object</returns>
        private async Task<IHttpActionResult> SendUserNamePasswordEmail(string email, string password)
        {
            StatusMessage msg = new StatusMessage();
            try
            {
                msg.ErrorMessage = await this.authRepository.SendUserNamePasswordEmail(email, password);
                if (string.IsNullOrWhiteSpace(msg.ErrorMessage))
                {
                    msg.IsSuccess = true;
                    return this.Ok(msg);
                }
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("SendUserNamePasswordEmail: " + ex.Message);
                msg.ErrorMessage = ex.Message;
            }

            return this.Ok(msg);
        }

        #endregion
    }
}