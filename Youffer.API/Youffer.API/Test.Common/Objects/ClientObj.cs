// ---------------------------------------------------------------------------------------------------
// <copyright file="ClientObj.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-14</date>
// <summary>
//     The ClientObj class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Test.Common.Objects
{
    using System;
    using Youffer.DataService.DBSchema;

    /// <summary>
    /// The client obj.
    /// </summary>
    public class ClientObj
    {
        /// <summary>
        /// The get client obj.
        /// </summary>
        /// <returns>
        /// The <see cref="AuthClients"/>.
        /// </returns>
        public static AuthClients GetClientObj()
        {
            return new AuthClients
                       {
                           Id = Guid.NewGuid().ToString(),
                           Name = "TestUser",
                           Active = true
                       };
        }
    }
}
