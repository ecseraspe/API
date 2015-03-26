// ---------------------------------------------------------------------------------------------------
// <copyright file="StructuremapWebApi.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-14</date>
// <summary>
//     The StructuremapWebApi class
// </summary>
// ---------------------------------------------------------------------------------------------------

// [assembly: WebActivatorEx.PostApplicationStartMethod(typeof(Youffer.API.App_Start.StructuremapWebApi), "Start")]

namespace Youffer.API
{
    using System.Web.Http;

    using Youffer.API.DependencyResolution;

    /// <summary>
    /// The StructuremapWebApi class
    /// </summary>
    public static class StructuremapWebApi
    {
        /// <summary>
        /// The start.
        /// </summary>
        public static void Start()
        {
            var container = StructuremapMvc.StructureMapDependencyScope.Container;
            GlobalConfiguration.Configuration.DependencyResolver = new StructureMapWebApiDependencyResolver(container);
        }
    }
}