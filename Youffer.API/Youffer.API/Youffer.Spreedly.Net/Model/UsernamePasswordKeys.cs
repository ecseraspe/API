// ---------------------------------------------------------------------------------------------------
// <copyright file="UsernamePasswordKeys.cs" company="Rekurant">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2014-08-23</date>
// <summary>
//     The UsernamePasswordKeys class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Rekurant.Spreedly.Net.Model
{
    using System;
    using System.Net;
    using System.Text;

    /// <summary>
    /// The username password keys.
    /// </summary>
    public class UsernamePasswordKeys
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UsernamePasswordKeys" /> class.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        public UsernamePasswordKeys(string username, string password)
        {
            this.Credentials = new NetworkCredential(username, password);
            this.Authorizationheader = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", username, password)));
        }

        /// <summary>
        /// Gets the credentials.
        /// </summary>
        internal NetworkCredential Credentials { get; private set; }

        /// <summary>
        /// Gets the authorization header.
        /// </summary>
        internal string Authorizationheader { get; private set; }
    }
}