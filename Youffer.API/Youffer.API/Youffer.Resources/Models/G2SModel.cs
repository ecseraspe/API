// ---------------------------------------------------------------------------------------------------
// <copyright file="G2SModel.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gaurav Barar</author>
// <date>2014-12-28</date>
// <summary>
//     The InterestModel class
// </summary>
// ---------------------------------------------------------------------------------------------------
namespace Youffer.Resources.Models
{
    /// <summary>
    /// Class G2SModel
    /// </summary>
    public class G2SModel
    {
        /// <summary>
        /// Gets or sets the id of the client.
        /// </summary>
        /// <value>
        /// The id of the client.
        /// </value>
        public string InvoiceId { get; set; }

        /// <summary>
        /// Gets or sets the id of the client.
        /// </summary>
        /// <value>
        /// The id of the client.
        /// </value>
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the intrest of the client.
        /// </summary>
        /// <value>
        /// The interest of the client.
        /// </value>
        public string ClientInterest { get; set; }

        /// <summary>
        /// Gets or sets the id of the company.
        /// </summary>
        /// <value>
        /// The id of the company.
        /// </value>
        public string CompanyId { get; set; }

        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        /// <value>
        /// The object of the product model.
        /// </value>
        public ProductModel Product { get; set; }

        /// <summary>
        /// Gets or sets the processing fee.
        /// </summary>
        /// <value>
        /// The processing fee.
        /// </value>
        public decimal ProcessingFee { get; set; }
    }
}
