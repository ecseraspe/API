// ---------------------------------------------------------------------------------------------------
// <copyright file="RepositoryClientMock.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-14</date>
// <summary>
//     The RepositoryClientMock class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Test.Common.Mocks
{
    using System.Collections.Generic;

    using Rhino.Mocks;
    using Youffer.Common.DataService;
    using Youffer.DataService.DBSchema;

    /// <summary>
    /// The IRepositoryClientMock interface
    /// </summary>
    public class RepositoryClientMock : BaseMock
    {
        /// <summary>
        /// The get client repository.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <returns>
        /// The IRepository of Client
        /// </returns>
        public static IRepository<AuthClients> GetRepository(AuthClients client)
        {
            var repo = MockRepository.StrictMock<IRepository<AuthClients>>();
            repo.Expect(v => v.Find(x => x.Id == client.Id)).IgnoreArguments().Return(new List<AuthClients> { client });
            repo.Expect(v => v.Insert(Arg<AuthClients>.Is.Anything)).IgnoreArguments();
            repo.Expect(v => v.Delete(Arg<AuthClients>.Is.Anything)).IgnoreArguments();
            repo.Expect(v => v.Commit());

            return repo;
        }
    }
}
