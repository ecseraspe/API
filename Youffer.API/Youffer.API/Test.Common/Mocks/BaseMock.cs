// ---------------------------------------------------------------------------------------------------
// <copyright file="BaseMock.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-14</date>
// <summary>
//     The BaseMock class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Test.Common.Mocks
{
    using Rhino.Mocks;

    /// <summary>
    /// The BaseMock class
    /// </summary>
    public class BaseMock
    {
        /// <summary>
        /// The mock repository.
        /// </summary>
        private static MockRepository mockRepository;

        /// <summary>
        /// Gets the mock repository.
        /// </summary>
        public static MockRepository MockRepository
        {
            get
            {
                return mockRepository ?? (mockRepository = new MockRepository());
            }
        }
    }
}