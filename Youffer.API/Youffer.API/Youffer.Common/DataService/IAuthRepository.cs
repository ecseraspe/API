// ---------------------------------------------------------------------------------------------------
// <copyright file="IAuthRepository.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-14</date>
// <summary>
//     The IAuthRepository interface
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Common.DataService
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;

    using Youffer.Resources.Models;
    using Youffer.Resources.ViewModel;

    /// <summary>
    /// The IAuthRepository interface
    /// </summary>
    public interface IAuthRepository : IDisposable
    {
        /// <summary>
        /// The change activation status.
        /// </summary>
        /// <param name="activationId">The activation id.</param>
        /// <param name="status">The status.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        Task<bool> ChangeActivationStatus(Guid activationId, bool status = true);

        /// <summary>
        /// Registers the user.
        /// </summary>
        /// <param name="userModel">The user model.</param>
        /// <returns>Identity object</returns>
        Task<IdentityResult> RegisterUser(UserModel userModel);

        /// <summary>
        /// Finds the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns>ApplicationUserDto object</returns>
        Task<ApplicationUserDto> FindUser(string userName, string password);

        /// <summary>
        /// Finds the asynchronous.
        /// </summary>
        /// <param name="loginInfo">The login information.</param>
        /// <returns>ApplicationUserDto object</returns>
        Task<ApplicationUserDto> FindAsync(UserLoginInfo loginInfo);

        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="userRole">The User Role. </param>
        /// <returns>IdentityResult object</returns>
        Task<IdentityResult> CreateAsync(ApplicationUserDto user, string userRole);

        /// <summary>
        /// Adds the login asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="login">The login.</param>
        /// <returns>IdentityResult object</returns>
        Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login);

        /// <summary>
        /// Finds the client.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <returns>Identity object</returns>
        ClientDto FindClient(string clientId);

        /// <summary>
        /// Gets the name of the role.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <returns>Role name</returns>
        string GetRoleName(string roleId);

        /// <summary>
        /// Adds the refresh token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>Identity object</returns>
        Task<bool> AddRefreshToken(RefreshTokenDto token);

        /// <summary>
        /// Removes the refresh token.
        /// </summary>
        /// <param name="refreshTokenId">The refresh token identifier.</param>
        /// <returns>
        /// Identity object
        /// </returns>
        Task<bool> RemoveRefreshToken(string refreshTokenId);

        /// <summary>
        /// Removes the refresh token.
        /// </summary>
        /// <param name="refreshToken">The refresh token.</param>
        /// <returns>Identity object</returns>
        Task<bool> RemoveRefreshToken(RefreshTokenDto refreshToken);

        /// <summary>
        /// Finds the refresh token.
        /// </summary>
        /// <param name="refreshTokenId">The refresh token identifier.</param>
        /// <returns>Identity object</returns>
        Task<RefreshTokenDto> FindRefreshToken(string refreshTokenId);

        /// <summary>
        /// Gets all refresh tokens.
        /// </summary>
        /// <returns>Identity object</returns>
        List<RefreshTokenDto> GetAllRefreshTokens();

        /// <summary>
        /// The change activation status.
        /// </summary>
        /// <param name="email">The email id.</param> 
        /// <returns>
        /// The <see cref="string" />.
        /// </returns>
        Task<string> SendForgotPasswordLink(string email);

        /// <summary>
        /// Save New Password after Password reset
        /// </summary>
        /// <param name="passwordResetId">The password reset Guid</param>
        /// <param name="password">The password</param>
        /// <returns> The <see cref="string" />. </returns>
        Task<string> UpdatePasswordAfterReset(string passwordResetId, string password);

        /// <summary>
        /// Save New Password after Password reset
        /// </summary>
        /// <param name="userId">The user Id</param>
        /// <param name="oldPassword">The old password</param>
        /// <param name="newPassword">The new Password</param>
        /// <returns> The <see cref="string" />. </returns>
        Task<string> ChangePassword(string userId, string oldPassword, string newPassword);

        /// <summary>
        /// Sends the user name password email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns>The <see cref="string" />. </returns>
        Task<string> SendUserNamePasswordEmail(string email, string password);

        /// <summary>
        /// Sends the credit balance email.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="companyName">Name of the company.</param>
        /// <param name="amount">The amount.</param>
        /// <returns>The <see cref="string" />. </returns>
        Task<string> SendCreditBalanceEmail(string userId, string companyName, string amount);
    }
}