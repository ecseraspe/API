// ---------------------------------------------------------------------------------------------------
// <copyright file="SampleDirection.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-14</date>
// <summary>
//     The SampleDirection class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.API.Areas.HelpPage.SampleGeneration
{
    /// <summary>
    /// Indicates whether the sample is used for request or response
    /// </summary>
    public enum SampleDirection
    {
        /// <summary>
        /// The request
        /// </summary>
        Request = 0,

        /// <summary>
        /// The response
        /// </summary>
        Response
    }
}