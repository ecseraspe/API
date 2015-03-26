// ---------------------------------------------------------------------------------------------------
// <copyright file="StructureMapScopeModule.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-14</date>
// <summary>
//     The StructureMapScopeModule class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.API.DependencyResolution
{
    using System.Web;

    using StructureMap.Web.Pipeline;

    /// <summary>
    /// The StructureMapScopeModule class
    /// </summary>
    public class StructureMapScopeModule : IHttpModule
    {
        #region Public Methods and Operators

        /// <summary>
        /// Disposes of the resources (other than memory) used by the module that implements <see cref="T:System.Web.IHttpModule" />.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// Initializes a module and prepares it to handle requests.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpApplication" /> that provides access to the methods, properties, and events common to all application objects within an ASP.NET application</param>
        public void Init(HttpApplication context)
        {
            context.BeginRequest += (sender, e) => StructuremapMvc.StructureMapDependencyScope.CreateNestedContainer();
            context.EndRequest += (sender, e) =>
                {
                    HttpContextLifecycle.DisposeAndClearAll();
                    StructuremapMvc.StructureMapDependencyScope.DisposeNestedContainer();
                };
        }

        #endregion
    }
}