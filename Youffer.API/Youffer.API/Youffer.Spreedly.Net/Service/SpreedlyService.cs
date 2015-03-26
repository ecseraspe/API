// ---------------------------------------------------------------------------------------------------
// <copyright file="SpreedlyService.cs" company="Rekurant">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2014-08-23</date>
// <summary>
//     The SpreedlyService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Rekurant.Spreedly.Net.Service
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    using Common.DataService;
    using Enum;
    using Extensions;
    using Model;
    using Resources.Models;
    using Spreedly;

    /// <summary>
    /// The spreedly service.
    /// </summary>
    public class SpreedlyService : ISpreedlyService
    {
        #region Fields

        /// <summary>
        /// The security keys.
        /// </summary>
        private readonly SecurityKeys securityKeys;

        /// <summary>
        /// The redirect uri.
        /// </summary>
        private readonly string redirectURI;

        /// <summary>
        /// The spreedly logs service instance.
        /// </summary>
        private readonly ISpreedlyLogService spreedlyLogsService;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SpreedlyService" /> class.
        /// </summary>
        /// <param name="applicationId">The application id.</param>
        /// <param name="masterKey">The master key.</param>
        /// <param name="gatewayToken">The gateway token.</param>
        /// <param name="redactedToken">The redacted token.</param>
        /// <param name="redirectURI">The redirect URI.</param>
        /// <param name="spreedlyLogsService">The spreedly logs service instance.</param>
        public SpreedlyService(string applicationId, string masterKey, string gatewayToken, string redactedToken, string redirectURI, ISpreedlyLogService spreedlyLogsService)
            : this(new SecurityKeys(applicationId, masterKey, gatewayToken, redactedToken))
        {
            this.redirectURI = redirectURI;
            this.spreedlyLogsService = spreedlyLogsService;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpreedlyService" /> class.
        /// </summary>
        /// <param name="securityKeys">The security keys.</param>
        private SpreedlyService(SecurityKeys securityKeys)
        {
            this.securityKeys = securityKeys;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The add gateway.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="userId">User id</param>
        /// <param name="gatewayXml">The gateway information's.</param>
        /// <returns>
        /// The <see cref="Gateway" />.
        /// </returns>
        public Gateway AddGateway(string type, string userId, string gatewayXml = "")
        {
            AsyncCallResult<XDocument> result =
                this.Call((client, token) => client.Gateways(token, type, gatewayXml), userId, gatewayXml);
            if (result.Failed())
            {
                return null;
            }

            Gateway gateway = Gateway.FromXml(result.Contents).FirstOrDefault();
            if (gateway != null)
            {
                this.securityKeys.LastGatewayToken = gateway.Token;
            }

            return gateway;
        }

        /// <summary>
        /// The adding receiver.
        /// </summary>
        /// <param name="receiverType">The receiver type.</param>
        /// <param name="hostName">The host name.</param>
        /// <returns>
        /// The <see cref="string" />.
        /// </returns>
        public string AddingReceiver(string receiverType, string hostName)
        {
            AsyncCallResult<XDocument> result =
                this.Call((client, token) => client.AddingReceiver(token, receiverType, hostName));
            if (result.Contents == null)
            {
                return null;
            }

            return result.Contents.ToString();
        }

        /// <summary>
        /// The delivering payment.
        /// </summary>
        /// <param name="receiverId">The receiver id.</param>
        /// <param name="paymentMethodId">The payment method id.</param>
        /// <param name="url">The url.</param>
        /// <returns>
        /// The <see cref="string" />.
        /// </returns>
        public string DeliveringPayment(string receiverId, string paymentMethodId, string url)
        {
            AsyncCallResult<XDocument> result =
                this.Call((client, token) => client.DeliveringPayment(token, receiverId, paymentMethodId, url));
            if (result.Contents == null)
            {
                return null;
            }

            return result.Contents.ToString();
        }

        /// <summary>
        /// The gateways.
        /// </summary>
        /// <returns>
        /// The <see cref="Gateway" />.
        /// </returns>
        public IEnumerable<Gateway> Gateways()
        {
            AsyncCallResult<XDocument> result = this.Call((client, token) => client.Gateways(token));
            if (result.Failed())
            {
                return null;
            }

            return Gateway.FromXml(result.Contents);
        }

        /// <summary>
        /// The get all payment method.
        /// </summary>
        /// <returns>
        /// The <see cref="string" />.
        /// </returns>
        public string GetAllPaymentMethod()
        {
            AsyncCallResult<XDocument> result = this.Call((client, token) => client.GetAllPaymentMethod(token));
            if (result.Failed())
            {
                return null;
            }

            return result.Contents.ToString();
        }

        /// <summary>
        /// The get all transaction for payment method.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="paymentMethodId">The payment method id.</param>
        /// <returns>
        /// The <see cref="string" />.
        /// </returns>
        public string GetAllTransactionForPaymentMethod(string type, string paymentMethodId)
        {
            AsyncCallResult<XDocument> result =
                this.Call((client, token) => client.GetAllTransactionForPaymentMethod(token, paymentMethodId));
            if (result.Contents == null)
            {
                return null;
            }

            return result.Contents.ToString();
        }

        /// <summary>
        /// The get all transactions.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// The <see cref="string" />.
        /// </returns>
        public string GetAllTransactions(string type)
        {
            AsyncCallResult<XDocument> result = this.Call((client, token) => client.GetAllTransaction(token));
            if (result.Contents == null)
            {
                return null;
            }

            return result.Contents.ToString();
        }

        /// <summary>
        /// The get enabled gateway.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// The <see cref="Gateway" />.
        /// </returns>
        public Gateway GetEnabledGateway(string type)
        {
            IEnumerable<Gateway> gateways = this.Gateways();
            return gateways.FirstOrDefault(g => g.Type == type && g.Enabled);
        }

        /// <summary>
        /// The get gateway transaction.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="gatewayId">The gateway id.</param>
        /// <returns>
        /// The <see cref="string" />.
        /// </returns>
        public string GetGatewayTransaction(string type, string gatewayId)
        {
            AsyncCallResult<XDocument> result =
                this.Call((client, token) => client.GetGatewayTransaction(token, gatewayId));
            if (result.Contents == null)
            {
                return null;
            }

            return result.Contents.ToString();
        }

        /// <summary>
        /// The get individual transaction.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="transactionId">The transaction id.</param>
        /// <returns>
        /// The <see cref="string" />.
        /// </returns>
        public string GetIndividualTransaction(string type, string transactionId)
        {
            AsyncCallResult<XDocument> result =
                this.Call((client, token) => client.GetIndividualTransaction(token, transactionId));
            if (result.Contents == null)
            {
                return null;
            }

            return result.Contents.ToString();
        }

        /// <summary>
        /// The get payment method.
        /// </summary>
        /// <param name="paymentMethodToken">The payment method token.</param>
        /// <returns>
        /// The <see cref="string" />.
        /// </returns>
        public string GetPaymentMethod(string paymentMethodToken)
        {
            AsyncCallResult<XDocument> result =
                this.Call((client, token) => client.GetPaymentMethod(token, paymentMethodToken));
            if (result.Failed())
            {
                return null;
            }

            return result.Contents.ToString();
        }

        /// <summary>
        /// The ping.
        /// </summary>
        /// <returns>
        /// The <see cref="AsyncCallFailureReason" />.
        /// </returns>
        public AsyncCallFailureReason Ping()
        {
            AsyncCallResult<XDocument> result = this.Call((client, token) => client.Gateways(token));
            if (result.FailureReason != AsyncCallFailureReason.None)
            {
                return result.FailureReason;
            }

            return result.Contents.Descendants("gateways").Any()
                       ? AsyncCallFailureReason.None
                       : AsyncCallFailureReason.ResultsNotFound;
        }

        /// <summary>
        /// The process payment.
        /// </summary>
        /// <param name="gatewayToken">The gateway token.</param>
        /// <param name="paymentMethodToken">The payment method token.</param>
        /// <param name="amount">The amount.</param>
        /// <param name="currency">The currency.</param>
        /// <param name="userId">user Id</param>
        /// <returns>
        /// The <see cref="Transaction" /> .
        /// </returns>
        public Transaction ProcessPayment(string gatewayToken, string paymentMethodToken, decimal amount, string currency, string userId)
        {
            string request =
               string.Format(
                   "<transaction><amount>{0}</amount><currency_code>{1}</currency_code><payment_method_token>{2}</payment_method_token></transaction>",
                   amount,
                   currency,
                   paymentMethodToken);
            string response = "No response from spreedly";
            TransactionTypes transactionType = TransactionTypes.MakePurchase;

            AsyncCallResult<XDocument> result =
                this.Call(
                    (client, token) => client.ProcessPayment(token, gatewayToken, request));
            if (result.Contents == null)
            {
                this.LogToDb(userId, request, response, transactionType);
                return new Transaction(false, new TransactionErrors(string.Empty, TransactionErrorType.CallFailed));
            }

            response = result.Contents.ToString();
            this.LogToDb(userId, request, response, transactionType);
            return Transaction.FromXml(result.Contents);
        }

        /// <summary>
        /// The redact gateway.
        /// </summary>
        /// <param name="gatewayToken">The gateway token.</param>
        /// <param name="userId">The user Id.</param>
        /// <returns>
        /// The <see cref="Gateway" />.
        /// </returns>
        public Gateway RedactGateway(string gatewayToken, string userId)
        {
            AsyncCallResult<XDocument> result = this.Call((client, token) => client.Redact(token, gatewayToken), userId);
            if (result.Failed())
            {
                return null;
            }

            return Gateway.FromXml(result.Contents).FirstOrDefault();
        }

        /// <summary>
        /// The redact payment method.
        /// </summary>
        /// <param name="paymentMethodToken">The payment method token.</param>
        /// <returns>
        /// The <see cref="string" />.
        /// </returns>
        public string RedactPaymentMethod(string paymentMethodToken)
        {
            AsyncCallResult<XDocument> result =
                this.Call((client, token) => client.RedactPaymentMethod(token, paymentMethodToken));
            if (result.Failed())
            {
                return null;
            }

            return result.Contents.ToString();
        }

        /// <summary>
        /// The redacted token.
        /// </summary>
        /// <returns>
        /// The <see cref="string" />.
        /// </returns>
        public string RedactedToken()
        {
            return this.securityKeys.RedactedTokens.FirstOrDefault();
        }

        /// <summary>
        /// The remove payment method from gateway.
        /// </summary>
        /// <param name="paymentMethodToken">The payment method token.</param>
        /// <param name="gatewayToken">The gateway token.</param>
        /// <returns>
        /// The <see cref="string" />.
        /// </returns>
        public string RemovePaymentMethodFromGateway(string paymentMethodToken, string gatewayToken)
        {
            AsyncCallResult<XDocument> result =
                this.Call(
                    (client, token) => client.RemovePaymentMethodFromGateway(token, paymentMethodToken, gatewayToken));
            if (result.Failed())
            {
                return null;
            }

            return result.Contents.ToString();
        }

        /// <summary>
        /// The retain credit card.
        /// </summary>
        /// <param name="payMentMethodId">The payment method id.</param>
        /// <param name="userId">the user id.</param>
        /// <returns>
        /// The <see cref="Transaction" />.
        /// </returns>
        public Transaction RetainCreditCard(string payMentMethodId, string userId)
        {
            string request = "Retain credit card for payment meyhod token :- " + payMentMethodId;
            AsyncCallResult<XDocument> result =
                this.Call((client, token) => client.RetainCreditCard(token, payMentMethodId));
            string response = result.Contents.ToString();
            if (result.Failed())
            {
                this.LogToDb(userId, request, response, TransactionTypes.RetainCreditCard);
                return null;
            }

            this.LogToDb(userId, request, response, TransactionTypes.RetainCreditCard);
            return Transaction.FromXml(result.Contents);
        }

        /// <summary>
        /// The save credit card info.
        /// </summary>
        /// <param name="creditCardInfo">The credit card info.</param>
        /// <param name="userId">the user id.</param>
        /// <returns>
        /// The <see>
        ///         <cref>AsyncCallResult</cref>
        ///     </see>
        ///     .
        /// </returns>
        public AsyncCallResult<string> SaveCreditCardInfo(CreditCardInfo creditCardInfo, string userId)
        {
            var source = new CancellationTokenSource();
            CancellationToken token = source.Token;
            string response = "Connection Timeout";
            TransactionTypes transactionType = TransactionTypes.AddCreditCard;
            using (
                var contents =
                    new FormUrlEncodedContent(
                        new List<KeyValuePair<string, string>>
                            {
                                new KeyValuePair<string, string>(
                                    "redirect_url", 
                                    this.redirectURI), 
                                new KeyValuePair<string, string>(
                                    "api_login", 
                                    this.securityKeys.Credentials.UserName), 
                                new KeyValuePair<string, string>(
                                    "credit_card[number]", 
                                    creditCardInfo.CreditCardNumber), 
                                new KeyValuePair<string, string>(
                                    "credit_card[month]", 
                                    creditCardInfo.CreditCardMonth.ToString()), 
                                new KeyValuePair<string, string>(
                                    "credit_card[year]", 
                                    creditCardInfo.CreditCardYear.ToString()), 
                                new KeyValuePair<string, string>(
                                    "credit_card[verification_value]", 
                                    creditCardInfo.CrediCardCVV.ToString()), 
                                new KeyValuePair<string, string>(
                                    "credit_card[first_name]", 
                                    creditCardInfo.FirstName), 
                                new KeyValuePair<string, string>(
                                    "credit_card[last_name]", 
                                    creditCardInfo.LastName)
                            }))
            using (var handler = new HttpClientHandler { AllowAutoRedirect = false, PreAuthenticate = false })
            using (var client = new HttpClient(handler))
            {
                const string URI = "https://spreedlycore.com/v1/payment_methods";
                string request = contents.ToString();
                using (Task<HttpResponseMessage> task = client.PostAsync(URI, contents, token))
                {
                    try
                    {
                        if (task.Wait(20000, token) == false)
                        {
                            if (token.CanBeCanceled)
                            {
                                source.Cancel();
                            }

                            this.LogToDb(userId, request, response, transactionType);
                            return new AsyncCallResult<string>(AsyncCallFailureReason.TimeOut);
                        }
                    }
                    catch (Exception ex)
                    {
                        request = "Connection Failure, error : - " + ex;
                        this.LogToDb(userId, request, response, transactionType);
                        return new AsyncCallResult<string>(AsyncCallFailureReason.FailedConnection);
                    }

                    if (task.Result.StatusCode != HttpStatusCode.Redirect)
                    {
                        request = "Connection Failure";
                        this.LogToDb(userId, request, response, transactionType);
                        return new AsyncCallResult<string>(
                            AsyncCallFailureReason.FailedStatusCode,
                            task.Result.Content.ReadAsStringAsync().Result);
                    }

                    response = task.Result.Content.ToString();
                    this.LogToDb(userId, request, response, transactionType);
                    return new AsyncCallResult<string>(
                        AsyncCallFailureReason.None,
                        task.Result.Headers.Location.Query.Substring(1));
                }
            }
        }

        /// <summary>
        /// The updating payment method.
        /// </summary>
        /// <param name="paymentMethodToken">The payment method token.</param>
        /// <param name="otherPaymentMethodInfos">The other payment method information's.</param>
        /// <returns>
        /// The <see cref="string" />.
        /// </returns>
        public string UpdatingPaymentMethod(string paymentMethodToken, Dictionary<string, string> otherPaymentMethodInfos = null)
        {
            AsyncCallResult<XDocument> result = this.Call((client, token) => client.UpdatingPaymentMethod(token, paymentMethodToken, otherPaymentMethodInfos));
            if (result.Failed())
            {
                return null;
            }

            return result.Contents.ToString();
        }

        /// <summary>
        /// Log all transaction into rekurant DB
        /// </summary>
        /// <param name="userId">the user id</param>
        /// <param name="request"> request sent to spreedly</param>
        /// <param name="response">response from spreedly</param>
        /// <param name="transactionType">type of transaction</param>
        public void LogToDb(string userId, string request, string response, TransactionTypes transactionType)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var logsDto = new SpreedlyLogsDto
                {
                    UserId = userId,
                    Request = request,
                    Response = response,
                    ModifiedBy = userId,
                    TransactionType = (int)transactionType
                };
                bool result = this.spreedlyLogsService.SaveSpreedlyLogs(logsDto);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The call no return.
        /// </summary>
        /// <param name="className">The class name.</param>
        /// <param name="criteria">The criteria.</param>
        /// <param name="innerCall">The inner call.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        internal bool CallNoReturn(string className, object criteria, Func<IAsyncClient, CancellationToken, Task<HttpResponseMessage>> innerCall)
        {
            var source = new CancellationTokenSource();
            CancellationToken token = source.Token;
            using (var client = new AsyncClient())
            {
                client.Init(this.securityKeys.Credentials);
                using (Task<HttpResponseMessage> task = innerCall(client, token))
                {
                    if (task.Wait(30000, token) == false)
                    {
                        if (token.CanBeCanceled)
                        {
                            source.Cancel();
                        }

                        return false;
                    }

                    return task.Result.IsSuccessStatusCode;
                }
            }
        }

        /// <summary>
        /// The call.
        /// </summary>
        /// <param name="innerCall">The inner call.</param>
        /// <param name="userId">The user Id.</param>
        /// <param name="request">The request.</param>
        /// <param name="transactionType">The transaction Type.</param>
        /// <returns>
        /// The <see cref="XDocument" />.
        /// </returns>
        private AsyncCallResult<XDocument> Call(Func<IAsyncClient, CancellationToken, Task<HttpResponseMessage>> innerCall, string userId = "", string request = "", TransactionTypes transactionType = TransactionTypes.UnKnown)
        {
            var source = new CancellationTokenSource();
            CancellationToken token = source.Token;
            string response = "Connection Timeout";
            using (var client = new AsyncClient())
            {
                client.Init(this.securityKeys.Credentials);
                using (Task<HttpResponseMessage> task = innerCall(client, token))
                {
                    try
                    {
                        if (task.Wait(20000, token) == false)
                        {
                            if (token.CanBeCanceled)
                            {
                                source.Cancel();
                            }

                            this.LogToDb(userId, request, response, transactionType);
                            return new AsyncCallResult<XDocument>(AsyncCallFailureReason.TimeOut);
                        }
                    }
                    catch (Exception ex)
                    {
                        response = "Connection failure ,error :- " + ex;
                        this.LogToDb(userId, request, response, transactionType);
                        return new AsyncCallResult<XDocument>(AsyncCallFailureReason.FailedConnection);
                    }

                    Task<Stream> content = task.Result.Content.ReadAsStreamAsync();
                    if (content.Wait(20000, token) == false)
                    {
                        if (token.CanBeCanceled)
                        {
                            source.Cancel();
                        }

                        this.LogToDb(userId, request, response, transactionType);
                        return new AsyncCallResult<XDocument>(AsyncCallFailureReason.TimeOut);
                    }

                    using (var streamReader = new StreamReader(content.Result))
                    {
                        XDocument doc = XDocument.Load(streamReader, LoadOptions.SetLineInfo);
                        response = doc.ToString();
                        if (task.Result.IsSuccessStatusCode == false)
                        {
                            this.LogToDb(userId, request, response, transactionType);
                            return new AsyncCallResult<XDocument>(AsyncCallFailureReason.FailedStatusCode, doc);
                        }

                        this.LogToDb(userId, request, response, transactionType);
                        return new AsyncCallResult<XDocument>(AsyncCallFailureReason.None, doc);
                    }
                }
            }
        }

        #endregion
    }
}