// ---------------------------------------------------------------------------------------------------
// <copyright file="ApplicationUserDto.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-14</date>
// <summary>
//     The ApplicationUserDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.ViewModel
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNet.Identity.EntityFramework;

    /// <summary>
    /// The application user dto.
    /// </summary>
    public class ApplicationUserDto : IdentityUser
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationUserDto"/> class.
        /// </summary>
        public ApplicationUserDto()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.ModifiedOn = DateTime.UtcNow;
            this.RoleName = string.Empty;
            this.AccountType = 1;
        }

        /// <summary>
        /// Gets or sets the role name.
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        public new ICollection<IdentityUserRole> Roles { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets the created on.
        /// </summary>
        public DateTime CreatedOn { get; private set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the modified by.
        /// </summary>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the modified on.
        /// </summary>
        public DateTime ModifiedOn { get; set; }

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
        /// Gets or sets the Reset Password Guid.
        /// </summary>
        public string ResetPwdGuid { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is deleted.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the type of Account (1 - > User/ 2- > Company)
        /// </summary>
        public short AccountType { get; set; }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name { get; set; }
    }
}