// ---------------------------------------------------------------------------------------------------
// <copyright file="ISpreedlyService.cs" company="Rekurant">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2014-09-06</date>
// <summary>
//     The ISpreedlyService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Rekurant.Spreedly.Net.Service
{
    using System.Collections.Generic;

    using Rekurant.Spreedly.Net.Enum;
    using Rekurant.Spreedly.Net.Model;
    using Rekurant.Spreedly.Net.Spreedly;

    /// <summary>
    /// The SpreedlyService interface.
    /// </summary>
    public interface ISpreedlyService
    {
        /// <summary>
        /// The add gateway.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="userId">User id</param>
        /// <param name="gatewayXml">The gateway information's.</param>
        /// <returns>
        /// The <see cref="Gateway" />.
        /// </returns>
        Gateway AddGateway(string type, string userId, string gatewayXml = "");

        /// <summary>
        /// The adding receiver.
        /// </summary>
        /// <param name="receiverType">The receiver type.</param>
        /// <param name="hostName">The host name.</param>
        /// <returns>
        /// The <see cref="string" />.
        /// </returns>
        string AddingReceiver(string receiverType, string hostName);

        /// <summary>
        /// The delivering payment.
        /// </summary>
        /// <param name="receiverId">The receiver id.</param>
        /// <param name="paymentMethodId">The payment method id.</param>
        /// <param name="url">The url.</param>
        /// <returns>
        /// The <see cref="string" />.
        /// </returns>
        string DeliveringPayment(string receiverId, string paymentMethodId, string url);

        /// <summary>
        /// The gateways.
        /// </summary>
        /// <returns>
        /// The <see cref="Gateway" />.
        /// </returns>
        IEnumerable<Gateway> Gateways();

        /// <summary>
        /// The get all payment method.
        /// </summary>
        /// <returns>
        /// The <see cref="string" />.
        /// </returns>
        string GetAllPaymentMethod();

        /// <summary>
        /// The get all transaction for payment method.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="paymentMethodId">The payment method id.</param>
        /// <returns>
        /// The <see cref="string" />.
        /// </returns>
        string GetAllTransactionForPaymentMethod(string type, string paymentMethodId);

        /// <summary>
        /// The get all transactions.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// The <see cref="string" />.
        /// </returns>
        string GetAllTransactions(string type);

        /// <summary>
        /// The get enabled gateway.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// The <see cref="Gateway" />.
        /// </returns>
        Gateway GetEnabledGateway(string type);

        /// <summary>
        /// The get gateway transaction.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="gatewayId">The gateway id.</param>
        /// <returns>
        /// The <see cref="string" />.
        /// </returns>
        string GetGatewayTransaction(string type, string gatewayId);

        /// <summary>
        /// The get individual transaction.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="transactionId">The transaction id.</param>
        /// <returns>
        /// The <see cref="string" />.
        /// </returns>
        string GetIndividualTransaction(string type, string transactionId);

        /// <summary>
        /// The get payment method.
        /// </summary>
        /// <param name="paymentMethodToken">The payment method token.</param>
        /// <returns>
        /// The <see cref="string" />.
        /// </returns>
        string GetPaymentMethod(string paymentMethodToken);

        /// <summary>
        /// The ping.
        /// </summary>
        /// <returns>
        /// The <see cref="AsyncCallFailureReason" />.
        /// </returns>
        AsyncCallFailureReason Ping();

        /// <summary>
        /// The process payment.
        /// </summary>
        /// <param name="gatewayToken">The gateway token.</param>
        /// <param name="paymentMethodToken">The payment method token.</param>
        /// <param name="amount">The amount.</param>
        /// <param name="currency">The currency.</param>
        /// <param name="userId">the user id.</param>
        /// <returns>
        /// The <see cref="Transaction" />.
        /// </returns>
        Transaction ProcessPayment(string gatewayToken, string paymentMethodToken, decimal amount, string currency, string userId);

        /// <summary>
        /// The redact gateway.
        /// </summary>
        /// <param name="gatewayToken">The gateway token.</param>
        /// <param name="userId">User id</param>
        /// <returns>
        /// The <see cref="Gateway" />.
        /// </returns>
        Gateway RedactGateway(string gatewayToken, string userId);

        /// <summary>
        /// The redact payment method.
        /// </summary>
        /// <param name="paymentMethodToken">The payment method token.</param>
        /// <returns>
        /// The <see cref="string" />.
        /// </returns>
        string RedactPaymentMethod(string paymentMethodToken);

        /// <summary>
        /// The redacted token.
        /// </summary>
        /// <returns>
        /// The <see cref="string" />.
        /// </returns>
        string RedactedToken();

        /// <summary>
        /// The remove payment method from gateway.
        /// </summary>
        /// <param name="paymentMethodToken">The payment method token.</param>
        /// <param name="gatewayToken">The gateway token.</param>
        /// <returns>
        /// The <see cref="string" />.
        /// </returns>
        string RemovePaymentMethodFromGateway(string paymentMethodToken, string gatewayToken);

        /// <summary>
        /// The retain credit card.
        /// </summary>
        /// <param name="payMentMethodId">The payment method id.</param>
        /// <param name="userId">the user id.</param>
        /// <returns>
        /// The <see cref="Transaction" />.
        /// </returns>
        Transaction RetainCreditCard(string payMentMethodId, string userId);

        /// <summary>
        /// The save credit card info.
        /// </summary>
        /// <param name="creditCardInfo">The credit card info.</param>
        /// <param name="userId">the user id.</param>
        /// <returns>
        /// The <see> <cref>AsyncCallResult</cref></see>.</returns>
        AsyncCallResult<string> SaveCreditCardInfo(CreditCardInfo creditCardInfo, string userId);

        /// <summary>
        /// The updating payment method.
        /// </summary>
        /// <param name="paymentMethodToken">The payment method token.</param>
        /// <param name="otherPaymentMethodInfos">The other payment method information's.</param>
        /// <returns>
        /// The <see cref="string" />.
        /// </returns>
        string UpdatingPaymentMethod(string paymentMethodToken, Dictionary<string, string> otherPaymentMethodInfos = null);
    }
}