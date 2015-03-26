// ---------------------------------------------------------------------------------------------------
// <copyright file="ProductModel.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gaurav Barar</author>
// <date>2014-12-28</date>
// <summary>
//     The ProductModel class
// </summary>
// ---------------------------------------------------------------------------------------------------
namespace Youffer.Resources.Models
{
    /// <summary>
    /// Class G2SModel
    /// </summary>
    public class ProductModel
    {
        /// <summary>
        /// Gets or sets the id of the product.
        /// </summary>
        /// <value>
        /// The id of the product.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the title of the product.
        /// </summary>
        /// <value>
        /// The title of the product.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        /// <value>
        /// The price of the product.
        /// </value>
        public decimal Price { get; set; }
    }
}
