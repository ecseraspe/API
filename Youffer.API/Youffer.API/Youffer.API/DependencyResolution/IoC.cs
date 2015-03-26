// ---------------------------------------------------------------------------------------------------
// <copyright file="IoC.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-14</date>
// <summary>
//     The IoC class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.API.DependencyResolution
{
    using StructureMap;

    /// <summary>
    /// The IoC class
    /// </summary>
    public static class IoC
    {
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>
        /// The <see cref="IContainer"/>.
        /// </returns>
        public static IContainer Initialize()
        {
            return new Container(c => c.AddRegistry<DefaultRegistry>());
        }
    }
}