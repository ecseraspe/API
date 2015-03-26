// ---------------------------------------------------------------------------------------------------
// <copyright file="IAsyncClient.cs" company="Rekurant">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2014-08-23</date>
// <summary>
//     The IAsyncClient class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Rekurant.Spreedly.Net.Model
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// The AsyncClient interface.
    /// </summary>
    internal interface IAsyncClient
    {
        #region Public Methods and Operators

        /// <summary>
        /// The adding receiver.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="receiverType">The receiver type.</param>
        /// <param name="hostName">The host name.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        Task<HttpResponseMessage> AddingReceiver(CancellationToken token, string receiverType, string hostName);

        /// <summary>
        /// The delivering payment.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="receiverId">The receiver id.</param>
        /// <param name="paymentMethodId">The payment method id.</param>
        /// <param name="url">The url.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        Task<HttpResponseMessage> DeliveringPayment(CancellationToken token, string receiverId, string paymentMethodId, string url);

        /// <summary>
        /// The gateways.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        Task<HttpResponseMessage> Gateways(CancellationToken token);

        /// <summary>
        /// The gateways.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="type">The type.</param>
        /// <param name="gatewayXml">The gateway information's.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        Task<HttpResponseMessage> Gateways(CancellationToken token, string type, string gatewayXml);

        /// <summary>
        /// The get all payment method.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        Task<HttpResponseMessage> GetAllPaymentMethod(CancellationToken token);

        /// <summary>
        /// The get all transaction.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        Task<HttpResponseMessage> GetAllTransaction(CancellationToken token);

        /// <summary>
        /// The get all transaction for payment method.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="paymentMethod">The payment method.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        Task<HttpResponseMessage> GetAllTransactionForPaymentMethod(CancellationToken token, string paymentMethod);

        /// <summary>
        /// The get gateway transaction.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="gatewayId">The gateway id.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        Task<HttpResponseMessage> GetGatewayTransaction(CancellationToken token, string gatewayId);

        /// <summary>
        /// The get individual transaction.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="transactionId">The transaction id.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        Task<HttpResponseMessage> GetIndividualTransaction(CancellationToken token, string transactionId);

        /// <summary>
        /// The get payment method.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="paymentMethodtoken">The payment method token.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        Task<HttpResponseMessage> GetPaymentMethod(CancellationToken token, string paymentMethodtoken);

        /// <summary>
        /// The process payment.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="gatewayToken">The gateway token.</param>
        /// <param name="xml">the reuest xml.</param>
        /// <returns> The <see cref="Task" />. </returns>
        Task<HttpResponseMessage> ProcessPayment(CancellationToken token, string gatewayToken, string xml);

        /// <summary>
        /// The redact.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="gatewayToken">The gateway token.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        Task<HttpResponseMessage> Redact(CancellationToken token, string gatewayToken);

        /// <summary>
        /// The redact payment method.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="paymentMethodToken">The payment method token.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        Task<HttpResponseMessage> RedactPaymentMethod(CancellationToken token, string paymentMethodToken);

        /// <summary>
        /// The remove payment method from gateway.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="paymentMethodToken">The payment method token.</param>
        /// <param name="gatewayToken">The gateway token.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        Task<HttpResponseMessage> RemovePaymentMethodFromGateway(CancellationToken token, string paymentMethodToken, string gatewayToken);

        /// <summary>
        /// The retain credit card.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="payMentMethodId">The payment method id.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        Task<HttpResponseMessage> RetainCreditCard(CancellationToken token, string payMentMethodId);

        /// <summary>
        /// The updating payment method.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="paymentMethodtoken">The payment method token.</param>
        /// <param name="otherPaymentMethodInfos">The other payment method information's.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        Task<HttpResponseMessage> UpdatingPaymentMethod(CancellationToken token, string paymentMethodtoken, Dictionary<string, string> otherPaymentMethodInfos = null);

        #endregion
    }
}