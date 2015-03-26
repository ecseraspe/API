// ---------------------------------------------------------------------------------------------------
// <copyright file="ChallengeResult.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-19</date>
// <summary>
//     The ChallengeResult class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.API.Results
{
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;

    /// <summary>
    /// The ChallengeResult class
    /// </summary>
    public class ChallengeResult : IHttpActionResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChallengeResult"/> class.
        /// </summary>
        /// <param name="loginProvider">The login provider.</param>
        /// <param name="controller">The controller.</param>
        public ChallengeResult(string loginProvider, ApiController controller)
        {
            this.LoginProvider = loginProvider;
            this.Request = controller.Request;
        }

        /// <summary>
        /// Gets or sets the login provider.
        /// </summary>
        public string LoginProvider { get; set; }

        /// <summary>
        /// Gets or sets the request.
        /// </summary>
        public HttpRequestMessage Request { get; set; }

        /// <summary>
        /// Creates an <see cref="T:System.Net.Http.HttpResponseMessage" /> asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task that, when completed, contains the <see cref="T:System.Net.Http.HttpResponseMessage" />.
        /// </returns>
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            this.Request.GetOwinContext().Authentication.Challenge(this.LoginProvider);

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
            {
                RequestMessage = this.Request
            };

            return Task.FromResult(response);
        }
    }
}