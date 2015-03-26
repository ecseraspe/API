// ---------------------------------------------------------------------------------------------------
// <copyright file="StructureMapWebApiDependencyScope.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-14</date>
// <summary>
//     The StructureMapWebApiDependencyScope class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.API.DependencyResolution
{
    using System.Web.Http.Dependencies;

    using StructureMap;

    /// <summary>
    /// The structure map web api dependency scope.
    /// </summary>
    public class StructureMapWebApiDependencyScope : StructureMapDependencyScope, IDependencyScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StructureMapWebApiDependencyScope" /> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public StructureMapWebApiDependencyScope(IContainer container)
            : base(container)
        {
        }
    }
}