// ---------------------------------------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-14</date>
// <summary>
//     The StringExtensions class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Framework.Extensions
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Globalization;

    /// <summary>
    /// The string extensions.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// The to GUID.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The <see cref="Guid" />.
        /// </returns>
        public static Guid ToGuid(this string value)
        {
            Guid retValue = Guid.Empty;

            if (!string.IsNullOrWhiteSpace(value))
            {
                Guid.TryParse(value, out retValue);
            }

            return retValue;
        }

        /// <summary>
        ///  The To Capitalize
        /// </summary>
        /// <param name="str"> the string value. </param>
        /// <returns> string value. </returns>
        public static string Capitalize(this string str)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }

        /// <summary>
        /// The to lower string.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns> string value. </returns>
        public static string ToLowerString(this string str)
        {
            string strLower = string.Empty;
            if (!string.IsNullOrEmpty(str))
            {
                strLower = CultureInfo.CurrentCulture.TextInfo.ToLower(str);
            }

            return strLower;
        }

        /// <summary>
        /// Removes the duplicate rows.
        /// </summary>
        /// <param name="dTable">The d table.</param>
        /// <param name="colName">Name of the col.</param>
        /// <returns>DataTable object.</returns>
        public static DataTable RemoveDuplicateRows(this DataTable dTable, string colName)
        {
            Hashtable hTable = new Hashtable();
            ArrayList duplicateList = new ArrayList();

            foreach (DataRow drow in dTable.Rows)
            {
                if (hTable.Contains(drow[colName]))
                {
                    duplicateList.Add(drow);
                }
                else
                {
                    hTable.Add(drow[colName], string.Empty);
                }
            }

            foreach (DataRow dRow in duplicateList)
            {
                dTable.Rows.Remove(dRow);
            }

            return dTable;
        }
    }
}
