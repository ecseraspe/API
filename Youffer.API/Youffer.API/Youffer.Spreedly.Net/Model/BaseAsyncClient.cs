// ---------------------------------------------------------------------------------------------------
// <copyright file="BaseAsyncClient.cs" company="Rekurant">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2014-08-23</date>
// <summary>
//     The BaseAsyncClient class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Rekurant.Spreedly.Net.Model
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// The base async client.
    /// </summary>
    public class BaseAsyncClient : IDisposable
    {
        #region Fields

        /// <summary>
        /// The disposed.
        /// </summary>
        private bool disposed;

        /// <summary>
        /// The handler.
        /// </summary>
        private HttpClientHandler handler;

        /// <summary>
        /// The initialized.
        /// </summary>
        private bool initialized;

        /// <summary>
        /// The root url.
        /// </summary>
        private string rootUrl;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseAsyncClient"/> class.
        /// </summary>
        protected BaseAsyncClient()
        {
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Gets or sets the client.
        /// </summary>
        public HttpClient Client { get; set; }

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// The simple get.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="path">The path.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        public Task<HttpResponseMessage> SimpleGet(CancellationToken token, string path)
        {
            return this.Client.GetAsync(new Uri(this.rootUrl + path), HttpCompletionOption.ResponseContentRead, token);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The init.
        /// </summary>
        /// <param name="rootUrl">The root url.</param>
        /// <param name="credentials">The credentials.</param>
        /// <exception cref="System.InvalidOperationException">Already initialized</exception>
        protected internal void Init(string rootUrl, ICredentials credentials)
        {
            if (this.initialized)
            {
                throw new InvalidOperationException("Already initialized");
            }

            this.initialized = true;
            this.rootUrl = rootUrl;
            this.handler = new HttpClientHandler { Credentials = credentials };
            this.Client = new HttpClient(this.handler);
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        /// <param name="disposing">The disposing.</param>
        private void Dispose(bool disposing)
        {
            if (!this.disposed && disposing)
            {
                if (this.Client != null)
                {
                    HttpClient hc = this.Client;
                    this.Client = null;
                    hc.Dispose();
                }

                if (this.handler != null)
                {
                    HttpClientHandler hh = this.handler;
                    this.handler = null;
                    hh.Dispose();
                }

                this.disposed = true;
            }
        }

        #endregion
    }
}