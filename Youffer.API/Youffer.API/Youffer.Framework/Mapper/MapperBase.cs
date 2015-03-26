// ---------------------------------------------------------------------------------------------------
// <copyright file="MapperBase.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-14</date>
// <summary>
//     The MapperBase class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Framework.Mapper
{
    using Youffer.Common.Mapper;

    /// <summary>
    /// The type mapper base.
    /// </summary>
    /// <typeparam name="TSource">The source type.</typeparam>
    /// <typeparam name="TDestination">The destination type.</typeparam>
    public abstract class MapperBase<TSource, TDestination> : IMapper<TSource, TDestination>
    {
        /// <summary>
        /// The map.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>
        /// The <see cref="TDestination" />.
        /// </returns>
        public TDestination Map(TSource source)
        {
            var destination = this.GetDestinationInstance();

            this.OnMap(source, ref destination);

            return destination;
        }

        /// <summary>
        /// The map.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        public void Map(TSource source, ref TDestination destination)
        {
            this.OnMap(source, ref destination);
        }

        /// <summary>
        /// The get destination instance.
        /// </summary>
        /// <returns>
        /// The <see cref="TDestination" />.
        /// </returns>
        protected abstract TDestination GetDestinationInstance();

        /// <summary>
        /// The on map.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        protected abstract void OnMap(TSource source, ref TDestination destination);
    }
}
