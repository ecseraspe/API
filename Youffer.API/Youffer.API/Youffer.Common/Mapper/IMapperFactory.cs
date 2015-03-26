// ---------------------------------------------------------------------------------------------------
// <copyright file="IMapperFactory.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-14</date>
// <summary>
//     The IMapperFactory interface
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Common.Mapper
{
    /// <summary>
    /// The MapperFactory interface.
    /// </summary>
    public interface IMapperFactory
    {
        /// <summary>
        /// The get mapper.
        /// </summary>
        /// <typeparam name="TSource">The source object.</typeparam>
        /// <typeparam name="TDestination">The destination object.</typeparam>
        /// <returns>
        /// The Mapper.
        /// </returns>
        IMapper<TSource, TDestination> GetMapper<TSource, TDestination>();
    }
}
