// ---------------------------------------------------------------------------------------------------
// <copyright file="BaseTestClass.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-14</date>
// <summary>
//     The BaseTestClass class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Common.Tests
{
    using Youffer.Framework.Mapper;

    /// <summary>
    /// The base test class.
    /// </summary>
    public class BaseTestClass
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTestClass"/> class.
        /// </summary>
        public BaseTestClass()
        {
            AutoMapperBootstraper.Initialize();
        }
    }
}
