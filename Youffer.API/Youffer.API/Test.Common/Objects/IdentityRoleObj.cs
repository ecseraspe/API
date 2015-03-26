// ---------------------------------------------------------------------------------------------------
// <copyright file="IdentityRoleObj.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-14</date>
// <summary>
//     The IdentityRoleObj class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Test.Common.Objects
{
    using System;

    using Microsoft.AspNet.Identity.EntityFramework;

    /// <summary>
    /// The identity role obj.
    /// </summary>
    public class IdentityRoleObj
    {
        /// <summary>
        /// The get identity role.
        /// </summary>
        /// <returns>
        /// The <see cref="IdentityRole"/>.
        /// </returns>
        public static IdentityRole GetIdentityRole()
        {
            return new IdentityRole
                       {
                           Id = Guid.NewGuid().ToString(),
                           Name = "TestRole"
                       };
        }
    }
}