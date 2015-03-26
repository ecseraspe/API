// ---------------------------------------------------------------------------------------------------
// <copyright file="IdentityUserObj.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-14</date>
// <summary>
//     The IdentityUserObj class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Test.Common.Objects
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity.EntityFramework;

    /// <summary>
    /// The identity user obj.
    /// </summary>
    public class IdentityUserObj
    {
        /// <summary>
        /// The get identity user.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public static Task<IdentityUser> GetTaskIdentityUser()
        {
            return new Task<IdentityUser>(GetIdentityUser);
        }

        /// <summary>
        /// The get identity user.
        /// </summary>
        /// <returns>
        /// The <see cref="IdentityUser"/>.
        /// </returns>
        public static IdentityUser GetIdentityUser()
        {
            return new IdentityUser
                       {
                           Id = Guid.NewGuid().ToString(),
                           UserName = "TestUser"
                       };
        }
    }
}
