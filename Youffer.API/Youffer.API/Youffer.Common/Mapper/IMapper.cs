// ---------------------------------------------------------------------------------------------------
// <copyright file="IMapper.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-14</date>
// <summary>
//     The IMapper interface
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Common.Mapper
{
    /// <summary>
    /// The TypeMapper interface.
    /// </summary>
    public interface IMapper
    {
    }

    /// <summary>
    /// The TypeMapper interface.
    /// </summary>
    /// <typeparam name="TSource">The source type.</typeparam>
    /// <typeparam name="TDestination">The destination type.</typeparam>
    public interface IMapper<in TSource, TDestination> : IMapper
    {
        /// <summary>
        /// The map.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>
        /// The <see cref="TDestination" />.
        /// </returns>
        TDestination Map(TSource source);

        /// <summary>
        /// The map.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        void Map(TSource source, ref TDestination destination);
    }
}
