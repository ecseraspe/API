// ***********// ---------------------------------------------------------------------------------------------------
// <copyright file="InvoiceModel.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gaurav Barar</author>
// <date>2014-12-28</date>
// <summary>
//     The InvoiceModel class
// </summary>
// ---------------------------------------------------------------------------------------------------

/// <summary>
/// The Models namespace.
/// </summary>
namespace Youffer.Resources.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Youffer.Resources.Enum;

    /// <summary>
    /// Class InvoiceModel.
    /// </summary>
    public class InvoiceModel
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        /// <value>The identifier.</value>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the company identifier.
        /// </summary>
        /// <value>The company identifier.</value>
        public string CompanyId { get; set; }

        /// <summary>
        /// Gets or sets the client identifier.
        /// </summary>
        /// <value>The client identifier.</value>
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the client interest.
        /// </summary>
        /// <value>The client interest.</value>
        public string ClientInterest { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public InvoiceStatus EnumStatus { get; set; }

        /// <summary>
        /// Gets the status in string.
        /// </summary>
        /// <value>The status.</value>
        public string Status
        {
            get
            {
                return this.EnumStatus.ToString();
            }
        }

        /// <summary>
        /// Gets or sets the modified on.
        /// </summary>
        /// <value>The modified on.</value>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the modified on.
        /// </summary>
        /// <value>The modified on.</value>
        public DateTime ModifiedOn { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is deleted.
        /// </summary>
        /// <value><c>true</c> if this instance is deleted; otherwise, <c>false</c>.</value>
        public bool IsDeleted { get; set; }
    }
}
