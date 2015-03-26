// ---------------------------------------------------------------------------------------------------
// <copyright file="TransactionTypes.cs" company="Rekurant">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2014-10-08</date>
// <summary>
//     The TransactionTypes enum
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Rekurant.Spreedly.Net.Enum
{
    /// <summary>
    /// the transaction type enum
    /// </summary>
    public enum TransactionTypes
    {
        /// <summary>
        /// Not defined
        /// </summary>
        UnKnown,

        /// <summary>
        /// the add gateway
        /// </summary>
        AddGateway,

        /// <summary>
        /// the add creadit card
        /// </summary>
        AddCreditCard,

        /// <summary>
        /// the make purchase
        /// </summary>
        MakePurchase,

        /// <summary>
        /// the retain credit card
        /// </summary>
        RetainCreditCard
    }
}
