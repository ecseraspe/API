// ---------------------------------------------------------------------------------------------------
// <copyright file="GenericService.cs" company="Rekurant">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2014-08-23</date>
// <summary>
//     The GenericService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Rekurant.Spreedly.Net.Service
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    using Rekurant.Spreedly.Net.Enum;
    using Rekurant.Spreedly.Net.Model;

    /// <summary>
    /// The generic service.
    /// </summary>
    /// <typeparam name="TClient">Type of object</typeparam>
    public class GenericService<TClient> where TClient : BaseAsyncClient, new()
    {
        #region Fields

        /// <summary>
        /// The root url.
        /// </summary>
        private readonly string rootUrl;

        /// <summary>
        /// The security keys.
        /// </summary>
        private readonly UsernamePasswordKeys securityKeys;

        /// <summary>
        /// The serializer.
        /// </summary>
        private readonly JsonSerializer serializer;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericService{TClient}" /> class.
        /// </summary>
        /// <param name="rootUrl">The root url.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="serializer">The serializer.</param>
        public GenericService(string rootUrl, string username, string password, JsonSerializer serializer)
        {
            this.securityKeys = new UsernamePasswordKeys(username, password);
            this.rootUrl = rootUrl.EndsWith("/") ? rootUrl : (rootUrl + "/");
            this.serializer = serializer;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The call.
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="innerCall">The inner call.</param>
        /// <returns>
        /// The <see><cref>AsyncCallResult</cref></see>
        ///     .
        /// </returns>
        public AsyncCallResult<T> Call<T>(Func<TClient, CancellationToken, Task<HttpResponseMessage>> innerCall)
            where T : class
        {
            var source = new CancellationTokenSource();
            CancellationToken token = source.Token;
            using (var client = new TClient())
            {
                client.Init(this.rootUrl, this.securityKeys.Credentials);
                using (Task<HttpResponseMessage> task = innerCall(client, token))
                {
                    try
                    {
                        if (task.Wait(10000, token) == false)
                        {
                            if (token.CanBeCanceled)
                            {
                                source.Cancel();
                            }

                            return new AsyncCallResult<T>(AsyncCallFailureReason.TimeOut);
                        }
                    }
                    catch (Exception)
                    {
                        return new AsyncCallResult<T>(AsyncCallFailureReason.FailedConnection);
                    }

                    if (task.Result.IsSuccessStatusCode == false)
                    {
                        return new AsyncCallResult<T>(AsyncCallFailureReason.FailedStatusCode);
                    }

                    Task<Stream> content = task.Result.Content.ReadAsStreamAsync();
                    if (content.Wait(250, token) == false)
                    {
                        if (token.CanBeCanceled)
                        {
                            source.Cancel();
                        }

                        return new AsyncCallResult<T>(AsyncCallFailureReason.TimeOut);
                    }

                    using (var streamReader = new StreamReader(content.Result))
                    {
                        using (var jsonTextReader = new JsonTextReader(streamReader))
                        {
                            var obj = this.serializer.Deserialize<T>(jsonTextReader);
                            return new AsyncCallResult<T>(AsyncCallFailureReason.None, obj);
                        }
                    }
                }
            }
        }

        #endregion
    }
}