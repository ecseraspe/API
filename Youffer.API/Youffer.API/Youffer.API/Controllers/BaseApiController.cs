// ---------------------------------------------------------------------------------------------------
// <copyright file="BaseApiController.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-14</date>
// <summary>
//     The BaseApiController class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.API.Controllers
{
    using System.Web.Http;

    using Youffer.Common.LogService;

    /// <summary>
    /// The base api controller.
    /// </summary>
    public class BaseApiController : ApiController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseApiController" /> class.
        /// </summary>
        /// <param name="loggerService">The logger service.</param>
        public BaseApiController(ILoggerService loggerService)
        {
            this.LoggerService = loggerService;
        }

        /// <summary>
        /// Gets the logger service.
        /// </summary>
        protected ILoggerService LoggerService { get; private set; }
    }
}