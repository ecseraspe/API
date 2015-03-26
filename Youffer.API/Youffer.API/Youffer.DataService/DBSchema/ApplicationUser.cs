// ---------------------------------------------------------------------------------------------------
// <copyright file="ApplicationUser.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-14</date>
// <summary>
//     The ApplicationUser class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.DataService.DBSchema
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    /// <summary>
    /// The application user.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationUser"/> class.
        /// </summary>
        public ApplicationUser()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.ModifiedOn = DateTime.UtcNow;
            this.ActivationId = Guid.NewGuid().ToString();
            this.ResetPwdGuid = Guid.NewGuid().ToString();
            this.AccountType = 2;
        }

        /// <summary>
        /// Gets the repository Id.
        /// </summary>
        public object RepositoryId
        {
            get
            {
                return this.Id;
            }
        }

        /// <summary>
        /// Gets or sets the type of Account (1 - > User/ 2- > Company)
        /// </summary>
        public short AccountType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is deleted.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the activation id.
        /// </summary>
        public string ActivationId { get; set; }

        /// <summary>
        /// Gets or sets the Reset Password Guid.
        /// </summary>
        public string ResetPwdGuid { get; set; }

        /// <summary>
        /// Gets or sets the activated on.
        /// </summary>
        public DateTime? ActivatedOn { get; set; }

        /// <summary>
        /// Gets or sets the Facebook Id.
        /// </summary>
        public string FacebookId { get; set; }

        /// <summary>
        /// Gets or sets the Google+ Id.
        /// </summary>
        public string GoogleId { get; set; }

        /// <summary>
        /// Gets or sets the activation id.
        /// </summary>
        public string FBAccessToken { get; set; }

        /// <summary>
        /// Gets or sets the Google+ Access Token.
        /// </summary>
        public string GoogleAccessToken { get; set; }

        /// <summary>
        /// Gets or sets the CRM Id.
        /// </summary>
        public string CRMId { get; set; }

        /// <summary>
        /// Gets the created on.
        /// </summary>
        public DateTime CreatedOn { get; private set; }

        /// <summary>
        /// Gets or sets the modified on.
        /// </summary>
        public DateTime ModifiedOn { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the modified by.
        /// </summary>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The generate user identity async.
        /// </summary>
        /// <param name="manager">The manager.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            //// Add custom user claims here
            return userIdentity;
        }
    }
}