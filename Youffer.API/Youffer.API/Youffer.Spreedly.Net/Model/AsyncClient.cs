// ---------------------------------------------------------------------------------------------------
// <copyright file="AsyncClient.cs" company="Rekurant">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2014-08-23</date>
// <summary>
//     The AsyncClient class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Rekurant.Spreedly.Net.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    ///     The AsyncClient class
    /// </summary>
    internal class AsyncClient : BaseAsyncClient, IAsyncClient
    {
        #region Constants

        /// <summary>
        ///     The root URL
        /// </summary>
        private const string RootUrl = "https://core.spreedly.com/v1";

        /// <summary>
        /// The default post url.
        /// </summary>
        private const string DefaultPostUrl = "http://posttestserver.com/post.php";

        #endregion

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
        public Task<HttpResponseMessage> AddingReceiver(CancellationToken token, string receiverType, string hostName)
        {
            string uri = string.Format(RootUrl + "/receivers.xml");
            string xml = string.Format("<receiver><receiver_type>{0}</receiver_type><hostnames>{1}</hostnames></receiver>", receiverType, hostName);

            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            var content = new StringContent(xml, null, "application/xml");
            request.Content = content;
            return this.Client.SendAsync(request, HttpCompletionOption.ResponseContentRead, token);
        }

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
        public Task<HttpResponseMessage> DeliveringPayment(CancellationToken token, string receiverId, string paymentMethodId, string url = DefaultPostUrl)
        {
            string uri = string.Format(RootUrl + "/receivers/{0}/deliver.xml", receiverId);
            string body = string.Empty;
            string xml = "<delivery><payment_method_token>" + paymentMethodId + "</payment_method_token><url>" + url
                         + "</url>"
                         + "<headers><![CDATA[Content-Type:application/json X-App-Id: {{ app-id }}  X-App-Secret: {{ app-secret }}]]></headers>"
                         + @"<body><![CDATA[{""product_id"":""1234"",""card_number"":""{{ credit_card_number }}"",""card_cvv"":""{{ credit_card_verification_value }}"",""first_name"":""{{ credit_card_first_name }}"",""last_name"":""{{ credit_card_last_name }}"",""year"":""{{ credit_card_year }}"",""month"":""{{ credit_card_month }}""}]]></body>"
                         + "</delivery>";
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            var content = new StringContent(xml, null, "application/xml");
            request.Content = content;
            return this.Client.SendAsync(request, HttpCompletionOption.ResponseContentRead, token);
        }

        /// <summary>
        /// Gateway the specified token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>
        /// XML result
        /// </returns>
        public Task<HttpResponseMessage> Gateways(CancellationToken token)
        {
            return this.Client.GetAsync(new Uri(RootUrl + "/gateways.xml"), HttpCompletionOption.ResponseContentRead, token);
        }

        /// <summary>
        /// Gateway the specified token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="type">The type.</param>
        /// <param name="gatewayXml">The other gateway information's</param>
        /// <returns>
        /// XML response
        /// </returns>
        public Task<HttpResponseMessage> Gateways(CancellationToken token, string type, string gatewayXml)
        {
            string xml = gatewayXml;

            var request = new HttpRequestMessage(HttpMethod.Post, new Uri(RootUrl + "/gateways.xml"));
            var content = new StringContent(xml, null, "application/xml");
            request.Content = content;
            return this.Client.SendAsync(request, HttpCompletionOption.ResponseContentRead, token);
        }

        /// <summary>
        /// The get all payment method.
        /// </summary>
        /// <param name="token">
        /// The token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<HttpResponseMessage> GetAllPaymentMethod(CancellationToken token)
        {
            string uri = string.Format(RootUrl + "/payment_methods.xml");
            return this.Client.GetAsync(uri, HttpCompletionOption.ResponseContentRead, token);
        }

        /// <summary>
        /// Gets all transaction.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>
        /// XML response
        /// </returns>
        public Task<HttpResponseMessage> GetAllTransaction(CancellationToken token)
        {
            return this.Client.GetAsync(new Uri(RootUrl + "/transactions.xml"), HttpCompletionOption.ResponseContentRead, token);
        }

        /// <summary>
        /// The get all transaction for payment method.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="paymentMethod">The payment method.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        public Task<HttpResponseMessage> GetAllTransactionForPaymentMethod(CancellationToken token, string paymentMethod)
        {
            string uri = string.Format(RootUrl + "/payment_methods/{0}/transactions.xml", paymentMethod);
            return this.Client.GetAsync(uri, HttpCompletionOption.ResponseContentRead, token);
        }

        /// <summary>
        /// The get gateway transaction.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="gatewayId">The gateway id.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        public Task<HttpResponseMessage> GetGatewayTransaction(CancellationToken token, string gatewayId)
        {
            string uri = string.Format(RootUrl + "/gateways/{0}/transactions.xml", gatewayId);
            return this.Client.GetAsync(uri, HttpCompletionOption.ResponseContentRead, token);
        }

        /// <summary>
        /// The get individual transaction.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="transactionId">The transaction id.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        public Task<HttpResponseMessage> GetIndividualTransaction(CancellationToken token, string transactionId)
        {
            string uri = string.Format(RootUrl + "/payment_methods/transactions/{0}.xml", transactionId);
            return this.Client.GetAsync(uri, HttpCompletionOption.ResponseContentRead, token);
        }

        /// <summary>
        /// The get payment method.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="paymentMethodToken">The payment method token.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        public Task<HttpResponseMessage> GetPaymentMethod(CancellationToken token, string paymentMethodToken)
        {
            string uri = string.Format(RootUrl + "/payment_methods/{0}.xml", paymentMethodToken);
            return this.Client.GetAsync(uri, HttpCompletionOption.ResponseContentRead, token);
        }

        /// <summary>
        /// The init.
        /// </summary>
        /// <param name="credentials">The credentials.</param>
        public void Init(NetworkCredential credentials)
        {
            this.Init(RootUrl, credentials);
        }

        /// <summary>
        /// Processes the payment.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="gatewayToken">The gateway token.</param>
        /// <param name="xml">The request xml.</param>
        /// <returns>
        /// XML response
        /// </returns>
        public Task<HttpResponseMessage> ProcessPayment(CancellationToken token, string gatewayToken, string xml)
        {
            string uri = string.Format(RootUrl + "/gateways/{0}/purchase.xml", gatewayToken);
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            var content = new StringContent(xml, null, "application/xml");
            request.Content = content;
            return this.Client.SendAsync(request, HttpCompletionOption.ResponseContentRead, token);
        }

        /// <summary>
        /// The redact.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="gatewaytoken">The gateway token.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        public Task<HttpResponseMessage> Redact(CancellationToken token, string gatewaytoken)
        {
            string uri = string.Format(RootUrl + "/gateways/{0}/redact.xml", gatewaytoken);
            var request = new HttpRequestMessage(HttpMethod.Put, new Uri(uri));
            var content = new StringContent(string.Empty);
            request.Content = content;
            return this.Client.SendAsync(request, HttpCompletionOption.ResponseContentRead, token);
        }

        /// <summary>
        /// The redact payment method.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="paymentMethodToken">The payment method token.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        public Task<HttpResponseMessage> RedactPaymentMethod(CancellationToken token, string paymentMethodToken)
        {
            string uri = string.Format(RootUrl + "/payment_methods/{0}/redact.xml", paymentMethodToken);
            var request = new HttpRequestMessage(HttpMethod.Put, new Uri(uri));
            var content = new StringContent(string.Empty);
            request.Content = content;
            return this.Client.SendAsync(request, HttpCompletionOption.ResponseContentRead, token);
        }

        /// <summary>
        /// The remove payment method from gateway.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="paymentMethodToken">The payment method token.</param>
        /// <param name="gatewaytoken">The gateway token.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        public Task<HttpResponseMessage> RemovePaymentMethodFromGateway(CancellationToken token, string paymentMethodToken, string gatewaytoken)
        {
            string uri = string.Format(RootUrl + "/payment_methods/{0}/redact.xml", paymentMethodToken);
            string xml = string.Format(
                "<transaction><remove_from_gateway>{0}</remove_from_gateway></transaction>", 
                gatewaytoken);
            var request = new HttpRequestMessage(HttpMethod.Put, new Uri(uri));
            var content = new StringContent(xml, null, "application/xml");
            request.Content = content;
            return this.Client.SendAsync(request, HttpCompletionOption.ResponseContentRead, token);
        }

        /// <summary>
        /// The retain credit card.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="payMentMethodId">The payment method id.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        public Task<HttpResponseMessage> RetainCreditCard(CancellationToken token, string payMentMethodId)
        {
            string uri = string.Format(RootUrl + "/payment_methods/{0}/retain.xml", payMentMethodId);
            var request = new HttpRequestMessage(HttpMethod.Put, new Uri(uri));
            var content = new StringContent(string.Empty, null, "application/xml");
            request.Content = content;
            return this.Client.SendAsync(request, HttpCompletionOption.ResponseContentRead, token);
        }

        /// <summary>
        /// The updating payment method.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="paymentMethodToken">The payment method token.</param>
        /// <param name="otherPaymentMethodInfos">The other payment method information's.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        public Task<HttpResponseMessage> UpdatingPaymentMethod(CancellationToken token, string paymentMethodToken, Dictionary<string, string> otherPaymentMethodInfos = null)
        {
            string uri = string.Format(RootUrl + "/payment_methods/{0}.xml", paymentMethodToken);
            string xml = string.Format("<payment_method>{0}</payment_method>", this.DicToXml(otherPaymentMethodInfos));
            var request = new HttpRequestMessage(HttpMethod.Put, uri);
            var content = new StringContent(xml, null, "application/xml");
            request.Content = content;
            return this.Client.SendAsync(request, HttpCompletionOption.ResponseContentRead, token);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The dictionary to xml.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        /// <returns>
        /// The <see cref="string" />.
        /// </returns>
        private string DicToXml(Dictionary<string, string> dictionary)
        {
            if (dictionary == null || dictionary.Any() == false)
            {
                return string.Empty;
            }

            var sb = new StringBuilder();
            foreach (string key in dictionary.Keys)
            {
                sb.AppendFormat("<{0}>{1}</{0}>", key, dictionary[key]);
            }

            return sb.ToString();
        }

        #endregion
    }
}