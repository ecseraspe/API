// ---------------------------------------------------------------------------------------------------
// <copyright file="StructuremapMvc.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-14</date>
// <summary>
//     The StructuremapMvc class
// </summary>
// ---------------------------------------------------------------------------------------------------

using WebActivatorEx;
using Youffer.API;

[assembly: PreApplicationStartMethod(typeof(StructuremapMvc), "Start")]
[assembly: ApplicationShutdownMethod(typeof(StructuremapMvc), "End")]

namespace Youffer.API
{
    using System.Web.Mvc;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using StructureMap;
    using Youffer.API.DependencyResolution;

    /// <summary>
    /// The StructuremapMvc class
    /// </summary>
    public static class StructuremapMvc
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the structure map dependency scope.
        /// </summary>
        /// <value>
        /// The structure map dependency scope.
        /// </value>
        public static StructureMapDependencyScope StructureMapDependencyScope { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Ends this instance.
        /// </summary>
        public static void End()
        {
            StructureMapDependencyScope.Dispose();
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public static void Start()
        {
            IContainer container = IoC.Initialize();
            StructureMapDependencyScope = new StructureMapDependencyScope(container);
            DependencyResolver.SetResolver(StructureMapDependencyScope); 
            DynamicModuleUtility.RegisterModule(typeof(StructureMapScopeModule)); 
        }

        #endregion
    }
}