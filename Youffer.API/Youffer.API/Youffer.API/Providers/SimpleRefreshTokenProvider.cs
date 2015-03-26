// ---------------------------------------------------------------------------------------------------
// <copyright file="SimpleRefreshTokenProvider.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-14</date>
// <summary>
//     The SimpleRefreshTokenProvider class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.API.Providers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.Owin.Security.Infrastructure;

    using Youffer.Common.DataService;
    using Youffer.DataService;
    using Youffer.Resources.ViewModel;

    /// <summary>
    /// The SimpleRefreshTokenProvider class
    /// </summary>
    public class SimpleRefreshTokenProvider : IAuthenticationTokenProvider, IDisposable
    {
        /// <summary>
        /// The authentication repository
        /// </summary>
        private readonly IAuthRepository authRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleRefreshTokenProvider"/> class.
        /// </summary>
        /// <param name="authRepository">The authentication repository.</param>
        public SimpleRefreshTokenProvider(IAuthRepository authRepository)
        {
            this.authRepository = authRepository;
        }

        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var clientid = context.Ticket.Properties.Dictionary["as:client_id"];

            if (string.IsNullOrEmpty(clientid))
            {
                return;
            }

            var refreshTokenId = Guid.NewGuid().ToString("n");

            var refreshTokenLifeTime = context.OwinContext.Get<string>("as:clientRefreshTokenLifeTime");

            var token = new RefreshTokenDto
                            {
                                Id = DbHelper.GetHash(refreshTokenId),
                                ClientId = clientid,
                                Subject = context.Ticket.Identity.Name,
                                IssuedUtc = DateTime.UtcNow,
                                ExpiresUtc = DateTime.UtcNow.AddMinutes(Convert.ToDouble(refreshTokenLifeTime))
                            };

            context.Ticket.Properties.IssuedUtc = token.IssuedUtc;
            context.Ticket.Properties.ExpiresUtc = token.ExpiresUtc;

            token.ProtectedTicket = context.SerializeTicket();

            var result = await this.authRepository.AddRefreshToken(token);

            if (result)
            {
                context.SetToken(refreshTokenId);
            }
        }

        /// <summary>
        /// Receives the asynchronous.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            string hashedTokenId = DbHelper.GetHash(context.Token);

            var refreshToken = await this.authRepository.FindRefreshToken(hashedTokenId);

            if (refreshToken != null)
            {
                // Get protectedTicket from refreshToken class
                context.DeserializeTicket(refreshToken.ProtectedTicket);
                var result = await this.authRepository.RemoveRefreshToken(hashedTokenId);
            }
        }

        /// <summary>
        /// Creates the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Receives the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.authRepository.Dispose();
        }
    }
}