// ---------------------------------------------------------------------------------------------------
// <copyright file="AutoMapperMapper.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-14</date>
// <summary>
//     The AutoMapperMapper class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Framework.Mapper
{
    /// <summary>
    /// The auto mapper mapper.
    /// </summary>
    /// <typeparam name="TSource">The source type.</typeparam>
    /// <typeparam name="TDestination">The destination type.</typeparam>
    public class AutoMapperMapper<TSource, TDestination> : MapperBase<TSource, TDestination>
    {
        /// <summary>
        /// The get destination instance.
        /// </summary>
        /// <returns>
        /// The <see cref="TDestination"/>.
        /// </returns>
        protected override TDestination GetDestinationInstance()
        {
            return default(TDestination);
        }

        /// <summary>
        /// The on map.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        protected override void OnMap(TSource source, ref TDestination destination)
        {
            destination = AutoMapper.Mapper.Map<TSource, TDestination>(source);
        }
    }
}
