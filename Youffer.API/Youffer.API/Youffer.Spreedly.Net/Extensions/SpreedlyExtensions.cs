// ---------------------------------------------------------------------------------------------------
// <copyright file="SpreedlyExtensions.cs" company="Rekurant">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2014-08-18</date>
// <summary>
//     The SpreedlyExtensions class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Rekurant.Spreedly.Net.Extensions
{
    using System.Xml.Linq;

    using Rekurant.Spreedly.Net.Enum;
    using Rekurant.Spreedly.Net.Model;

    /// <summary>
    /// The SpreedlyExtensions class
    /// </summary>
    public static class SpreedlyExtensions
    {
        /// <summary>
        /// Failed the specified result.
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="result">The result.</param>
        /// <returns>Execution status</returns>
        public static bool Failed<T>(this AsyncCallResult<T> result) where T : class
        {
            return result == null || result.FailureReason != AsyncCallFailureReason.None;
        }

        /// <summary>
        /// Gets the string child.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="name">The name.</param>
        /// <returns>Child as String</returns>
        public static string GetStringChild(this XElement node, string name)
        {
            return GetStringChild(node, name, string.Empty);
        }

        /// <summary>
        /// Gets the string child.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="name">The name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>Child as String</returns>
        public static string GetStringChild(this XElement node, string name, string defaultValue)
        {
            if (node == null)
            {
                return defaultValue;
            }

            var token = node.Element(name);
            return token == null ? defaultValue : token.Value;
        }
    }
}