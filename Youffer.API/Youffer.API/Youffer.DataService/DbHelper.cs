// ---------------------------------------------------------------------------------------------------
// <copyright file="DbHelper.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-14</date>
// <summary>
//     The DbHelper class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.DataService
{
    using System;
    using System.Security.Cryptography;

    /// <summary>
    /// The DbHelper class
    /// </summary>
    public class DbHelper
    {
        /// <summary>
        /// Gets the hash.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>Hased value as string</returns>
        public static string GetHash(string input)
        {
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();
       
            byte[] byteValue = System.Text.Encoding.UTF8.GetBytes(input);

            byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);

            return Convert.ToBase64String(byteHash);
        }
    }
}