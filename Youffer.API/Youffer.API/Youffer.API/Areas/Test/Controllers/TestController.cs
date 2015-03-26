// ---------------------------------------------------------------------------------------------------
// <copyright file="TestController.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-14</date>
// <summary>
//     The TestController class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.API.Areas.Test.Controllers
{
    using System.Web.Http;

    /// <summary>
    /// The TestController for testing any issue
    /// </summary>
    [RoutePrefix("api/test")]
    public class TestController : ApiController
    {
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        [Route("~/api/test/get")]
        public string Get()
        {
            return "hell";
        }
    }
}
