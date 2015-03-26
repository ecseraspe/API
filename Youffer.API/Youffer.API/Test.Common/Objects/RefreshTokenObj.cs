// ---------------------------------------------------------------------------------------------------
// <copyright file="RefreshTokenObj.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-14</date>
// <summary>
//     The RefreshTokenObj class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Test.Common.Objects
{
    using System;

    using Youffer.DataService.DBSchema;

    /// <summary>
    /// The refresh token obj.
    /// </summary>
    public class RefreshTokenObj
    {
        /// <summary>
        /// The get refresh token.
        /// </summary>
        /// <returns>
        /// The <see cref="RefreshAuthTokens"/>.
        /// </returns>
        public static RefreshAuthTokens GetRefreshToken()
        {
            return new RefreshAuthTokens
                       {
                           Id = Guid.NewGuid().ToString(),
                           Subject = "TestSub"
                       };
        }
    }
}
