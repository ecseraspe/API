// ---------------------------------------------------------------------------------------------------
// <copyright file="AuthRepository.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-14</date>
// <summary>
//     The AuthRepository class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Framework.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Youffer.Common.CRMService;
    using Youffer.Common.DataService;
    using Youffer.Common.Helper;
    using Youffer.Common.Mapper;
    using Youffer.DataService.DBSchema;
    using Youffer.Resources.Constants;
    using Youffer.Resources.CRMModel;
    using Youffer.Resources.Enum;
    using Youffer.Resources.Models;
    using Youffer.Resources.ViewModel;

    /// <summary>
    /// The AuthRepository class
    /// </summary>
    public class AuthRepository : IAuthRepository
    {
        /// <summary>
        /// The client
        /// </summary>
        private readonly IRepository<AuthClients> client;

        /// <summary>
        /// The email queue service.
        /// </summary>
        private readonly IEmailQueueService emailQueueService;

        /// <summary>
        /// The refresh token
        /// </summary>
        private readonly IRepository<RefreshAuthTokens> refreshToken;

        /// <summary>
        /// The user manager
        /// </summary>
        private readonly UserManager<ApplicationUser> userManager;

        /// <summary>
        /// The mapper factory.
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// The roles
        /// </summary>
        private readonly IRepository<IdentityRole> roles;

        /// <summary>
        /// The crm manager service
        /// </summary>
        private readonly ICRMManagerService crmManagerService;

        /// <summary>
        /// The common service
        /// </summary>
        private readonly ICommonService commonService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthRepository" /> class.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="emailQueueService">The email queue service.</param>
        /// <param name="refreshToken">The refresh token.</param>
        /// <param name="userManager">The user manager.</param>
        /// <param name="roles">The roles.</param>
        /// <param name="mapperFactory">The mapper factory.</param>
        /// <param name="crmManagerService">The CRMManager service.</param>
        /// <param name="commonService"> the commonService</param>
        public AuthRepository(
            IRepository<AuthClients> client,
            IEmailQueueService emailQueueService,
            IRepository<RefreshAuthTokens> refreshToken,
            UserManager<ApplicationUser> userManager,
            IRepository<IdentityRole> roles,
            IMapperFactory mapperFactory,
            ICRMManagerService crmManagerService,
            ICommonService commonService)
        {
            this.client = client;
            this.refreshToken = refreshToken;
            this.userManager = userManager;
            this.roles = roles;
            this.emailQueueService = emailQueueService;
            this.mapperFactory = mapperFactory;
            this.crmManagerService = crmManagerService;
            this.commonService = commonService;
            userManager.UserValidator = new UserValidator<ApplicationUser>(userManager) { AllowOnlyAlphanumericUserNames = false };
            userManager.EmailService = new EmailService();
        }

        /// <summary>
        /// Gets the name of the role.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <returns>Role name</returns>
        public string GetRoleName(string roleId)
        {
            var result = this.roles.Find(x => x.Id == roleId.ToString()).FirstOrDefault();

            return result == null ? null : result.Name;
        }

        /// <summary>
        /// The change activation status.
        /// </summary>
        /// <param name="activationId">The activation id.</param>
        /// <param name="status">The status.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        public async Task<bool> ChangeActivationStatus(Guid activationId, bool status = true)
        {
            var existingUser = this.userManager.Users.Single(x => x.ActivationId == activationId.ToString());

            if (existingUser != null && existingUser.IsActive != status)
            {
                existingUser.IsActive = status;
                if (status)
                {
                    existingUser.ActivatedOn = DateTime.UtcNow;
                }

                return this.userManager.Update(existingUser) != null;
            }

            return false;
        }

        /// <summary>
        /// The change activation status.
        /// </summary>
        /// <param name="email">The email id.</param> 
        /// <returns>
        /// The <see cref="string" />.
        /// </returns>
        public async Task<string> SendForgotPasswordLink(string email)
        {
            string errorMessage = string.Empty;
            var existingUser = this.userManager.Users.Where(x => x.Email == email && x.AccountType == 3).FirstOrDefault();
            if (existingUser != null)
            {
                try
                {
                    var code = existingUser.ResetPwdGuid;
                    string callbackUrl = string.Format(AppSettings.Get(ConfigConstants.ResetPasswordUrl, "{0}"), code);
                    EmailTemplatesDto template = this.commonService.GetEmailTemlate(TemplateType.ForgotPasswordLink);
                    if (template.TemplateHtml != null)
                    {
                        template.TemplateHtml = template.TemplateHtml.Replace("{{RestPwdLink}}", callbackUrl);
                        await this.userManager.SendEmailAsync(existingUser.Id, template.Subject, template.TemplateHtml);
                    }
                    else
                    {
                        await this.userManager.SendEmailAsync(existingUser.Id, "Reset Password", "Hi <br></br>Please reset your password by clicking here: <a href='" + callbackUrl + "'>link</a><br><br>");
                    }
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                }
            }
            else
            {
                errorMessage = AppSettings.Get(ConfigConstants.InvaliEmailForForgotPwd, "No User found with this email address.");
            }

            return errorMessage;
        }

        /// <summary>
        /// Save New Password after Password reset
        /// </summary>
        /// <param name="passwordResetId">The password reset Guid</param>
        /// <param name="password">The password</param>
        /// <returns> The <see cref="string" />. </returns>
        public async Task<string> UpdatePasswordAfterReset(string passwordResetId, string password)
        {
            string errorMessage = string.Empty;
            try
            {
                var user = this.userManager.Users.Where(x => x.ResetPwdGuid == passwordResetId).FirstOrDefault();
                if (user != null)
                {
                    await this.userManager.RemovePasswordAsync(user.Id);
                    var result = await this.userManager.AddPasswordAsync(user.Id, password);
                    if (result.Succeeded)
                    {
                        var newuser = await this.userManager.FindAsync(user.UserName, password);

                        if (!string.IsNullOrWhiteSpace(newuser.PasswordHash))
                        {
                            user.PasswordHash = newuser.PasswordHash;
                            user.ResetPwdGuid = Guid.NewGuid().ToString();
                            this.userManager.Update(user);

                            ////Update password in CRM
                            OrganisationModel orgModel = this.crmManagerService.GetOrganisation(newuser.Id);
                            orgModel.Password = password;
                            orgModel = this.crmManagerService.UpdateOrganisation(newuser.Id, orgModel);
                        }
                    }
                    else
                    {
                        errorMessage = result.Errors.FirstOrDefault();
                    }
                }
                else
                {
                    errorMessage = AppSettings.Get(ConfigConstants.ExpiredLink, "Invalid Link or link has expired");
                }
            }
            catch (Exception ex)
            {
                var a = ex.Message;
                errorMessage = ex.Message;
            }

            return errorMessage;
        }

        /// <summary>
        /// Save New Password after Password reset
        /// </summary>
        /// <param name="userId">The user Id</param>
        /// <param name="oldPassword">The old password</param>
        /// <param name="newPassword">The new Password</param>
        /// <returns> The <see cref="string" />. </returns>
        public async Task<string> ChangePassword(string userId, string oldPassword, string newPassword)
        {
            IdentityResult result = await this.userManager.ChangePasswordAsync(userId, oldPassword, newPassword);
            if (result.Succeeded)
            {
                return string.Empty;
            }

            return result.Errors.FirstOrDefault();
        }

        /// <summary>
        /// Sends the user name password email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns>The <see cref="string" />. </returns>
        public async Task<string> SendUserNamePasswordEmail(string email, string password)
        {
            string errorMessage = string.Empty;
            var existingUser = this.userManager.Users.Where(x => x.Email == email && x.AccountType == 3).FirstOrDefault();
            if (existingUser != null)
            {
                try
                {
                    EmailTemplatesDto template = this.commonService.GetEmailTemlate(TemplateType.SendUsernameAndPwd);
                    if (template.TemplateHtml != null)
                    {
                        template.TemplateHtml = template.TemplateHtml.Replace("{{Email}}", email);
                        template.TemplateHtml = template.TemplateHtml.Replace("{{Password}}", password);
                        await this.userManager.SendEmailAsync(existingUser.Id, template.Subject, template.TemplateHtml);
                    }
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                }
            }
            else
            {
                errorMessage = AppSettings.Get(ConfigConstants.InvaliEmailForForgotPwd, "No User found with this email address.");
            }

            return errorMessage;
        }

        /// <summary>
        /// Sends the credit balance email.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="companyName">Name of the company.</param>
        /// <param name="amount">The amount.</param>
        /// <returns>The <see cref="string" />. </returns>
        public async Task<string> SendCreditBalanceEmail(string userId, string companyName, string amount)
        {
            string errorMessage = string.Empty;

            try
            {
                EmailTemplatesDto template = this.commonService.GetEmailTemlate(TemplateType.UpdateCreditBalance);
                if (template.TemplateHtml != null)
                {
                    template.TemplateHtml = template.TemplateHtml.Replace("{{CompanyName}}", companyName);
                    template.TemplateHtml = template.TemplateHtml.Replace("{{Amount}}", amount);
                    await this.userManager.SendEmailAsync(userId, template.Subject, template.TemplateHtml);
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            return errorMessage;
        }

        /// <summary>
        /// Registers the user.
        /// </summary>
        /// <param name="userModel">The user model.</param>
        /// <returns>Identity object</returns>
        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            ApplicationUser user = new ApplicationUser { Email = userModel.EmailId, UserName = userModel.EmailId, Name = userModel.Name };
            var existingUser = this.userManager.FindByName(userModel.EmailId);

            if (existingUser != null)
            {
                var errorResult = new IdentityResult(new[] { string.Format("Email {0} already exists!", userModel.EmailId) });
                return errorResult;
            }

            user.IsActive = true;
            if (userModel.UserRole == Roles.Company.ToString())
            {
                user.AccountType = 3;
            }
            else if (userModel.UserRole == Roles.Customer.ToString())
            {
                user.AccountType = 2;
            }

            var result = await this.userManager.CreateAsync(user, userModel.Password);

            if (result != null && result.Succeeded)
            {
                this.userManager.AddToRole(user.Id, userModel.UserRole);
                var newUser = this.mapperFactory.GetMapper<ApplicationUser, ApplicationUserDto>().Map(user);
                newUser.RoleName = userModel.UserRole;

                this.emailQueueService.SendActivationEmail(newUser);
            }

            return result;
        }

        /// <summary>
        /// Finds the user.
        /// </summary>
        /// <param name="email">Email id of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns>ApplicationUserDto object</returns>
        public async Task<ApplicationUserDto> FindUser(string email, string password)
        {
            var user = await this.userManager.FindAsync(email, password);

            if (user == null)
            {
                return null;
            }

            return this.mapperFactory.GetMapper<ApplicationUser, ApplicationUserDto>().Map(user);
        }

        /// <summary>
        /// Finds the asynchronous.
        /// </summary>
        /// <param name="loginInfo">The login information.</param>
        /// <returns>ApplicationUserDto object</returns>
        public async Task<ApplicationUserDto> FindAsync(UserLoginInfo loginInfo)
        {
            var user = await this.userManager.FindAsync(loginInfo);
            return this.mapperFactory.GetMapper<ApplicationUser, ApplicationUserDto>().Map(user);
        }

        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="userRole">The User Role. </param>
        /// <returns>IdentityResult object</returns>
        public async Task<IdentityResult> CreateAsync(ApplicationUserDto user, string userRole)
        {
            var result = new IdentityResult();
            try
            {
                var u = this.mapperFactory.GetMapper<ApplicationUserDto, ApplicationUser>().Map(user);
                result = await this.userManager.CreateAsync(u);

                if (result != null && result.Succeeded)
                {
                    this.userManager.AddToRole(user.Id, userRole);
                    var newUser = this.mapperFactory.GetMapper<ApplicationUser, ApplicationUserDto>().Map(u);
                    newUser.RoleName = userRole;
                    user.RoleName = userRole;
                    this.emailQueueService.SendActivationEmail(newUser);
                }
            }
            catch (Exception ex)
            {
                var a = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// Adds the login asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="login">The login.</param>
        /// <returns>IdentityResult object</returns>
        public async Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login)
        {
            var result = await this.userManager.AddLoginAsync(userId, login);

            return result;
        }

        /// <summary>
        /// Finds the client.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <returns>Identity object</returns>
        public ClientDto FindClient(string clientId)
        {
            var client = this.client.Find(x => x.Id == clientId).FirstOrDefault();

            if (client != null)
            {
                return this.mapperFactory.GetMapper<AuthClients, ClientDto>().Map(client);
            }

            return null;
        }

        /// <summary>
        /// Adds the refresh token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>
        /// Identity object
        /// </returns>
        public async Task<bool> AddRefreshToken(RefreshTokenDto token)
        {
            var existingToken = this.refreshToken.Find(r => r.Subject == token.Subject && r.ClientId == token.ClientId).SingleOrDefault();

            if (existingToken != null)
            {
                var result = await this.RemoveRefreshToken(existingToken.Id);
            }

            var newToken = this.mapperFactory.GetMapper<RefreshTokenDto, RefreshAuthTokens>().Map(token);

            this.refreshToken.Insert(newToken);

            return this.refreshToken.Commit();
        }

        /// <summary>
        /// Removes the refresh token.
        /// </summary>
        /// <param name="refreshTokenId">The refresh token identifier.</param>
        /// <returns>
        /// Identity object
        /// </returns>
        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
            var refreshToken = this.refreshToken.Find(x => x.Id == refreshTokenId).FirstOrDefault();

            if (refreshToken != null)
            {
                this.refreshToken.Delete(refreshToken);
                return this.refreshToken.Commit();
            }

            return false;
        }

        /// <summary>
        /// Removes the refresh token.
        /// </summary>
        /// <param name="refreshToken">The refresh token.</param>
        /// <returns>
        /// Identity object
        /// </returns>
        public async Task<bool> RemoveRefreshToken(RefreshTokenDto refreshToken)
        {
            this.refreshToken.Delete(x => x.Id == refreshToken.Id);
            return this.refreshToken.Commit();
        }

        /// <summary>
        /// Finds the refresh token.
        /// </summary>
        /// <param name="refreshTokenId">The refresh token identifier.</param>
        /// <returns>
        /// Identity object
        /// </returns>
        public async Task<RefreshTokenDto> FindRefreshToken(string refreshTokenId)
        {
            var refreshToken = this.refreshToken.Find(x => x.Id == refreshTokenId).FirstOrDefault();

            if (refreshToken != null)
            {
                return this.mapperFactory.GetMapper<RefreshAuthTokens, RefreshTokenDto>().Map(refreshToken);
            }

            return null;
        }

        /// <summary>
        /// Gets all refresh tokens.
        /// </summary>
        /// <returns>
        /// Identity object
        /// </returns>
        public List<RefreshTokenDto> GetAllRefreshTokens()
        {
            var list = this.refreshToken.GetAll().ToList();
            var mapper = this.mapperFactory.GetMapper<RefreshAuthTokens, RefreshTokenDto>();

            return list.Select(mapper.Map).ToList();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.refreshToken.Dispose();
            this.client.Dispose();
        }
    }
}