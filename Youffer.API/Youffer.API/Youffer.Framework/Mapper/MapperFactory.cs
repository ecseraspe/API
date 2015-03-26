// ---------------------------------------------------------------------------------------------------
// <copyright file="MapperFactory.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-14</date>
// <summary>
//     The MapperFactory class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Framework.Mapper
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Management.Instrumentation;

    using Youffer.Common.Mapper;

    /// <summary>
    /// The type mapper factory.
    /// </summary>
    public class MapperFactory : IMapperFactory
    {
        /// <summary>
        /// The type mappers.
        /// </summary>
        private readonly IEnumerable<IMapper> typeMappers;

        /// <summary>
        /// Initializes a new instance of the <see cref="MapperFactory" /> class.
        /// </summary>
        /// <param name="typeMappers">The type mappers.</param>
        public MapperFactory(IEnumerable<IMapper> typeMappers)
        {
            this.typeMappers = typeMappers;
        }

        /// <summary>
        /// The get mapper.
        /// </summary>
        /// <typeparam name="TSource">The source object.</typeparam>
        /// <typeparam name="TDestination">The destination object.</typeparam>
        /// <returns>
        /// The Mapper.
        /// </returns>
        public IMapper<TSource, TDestination> GetMapper<TSource, TDestination>()
        {
            return new AutoMapperMapper<TSource, TDestination>();
        }
    }
}
