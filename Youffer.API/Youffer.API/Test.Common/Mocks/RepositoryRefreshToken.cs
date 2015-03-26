// ---------------------------------------------------------------------------------------------------
// <copyright file="RepositoryRefreshToken.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-14</date>
// <summary>
//     The RepositoryRefreshToken class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Test.Common.Mocks
{
    using System.Collections.Generic;

    using Rhino.Mocks;
    using Youffer.Common.DataService;
    using Youffer.DataService.DBSchema;

    /// <summary>
    /// The repository refresh token.
    /// </summary>
    public class RepositoryRefreshToken : BaseMock
    {
        /// <summary>
        /// The get repository.
        /// </summary>
        /// <param name="authTokens">The token.</param>
        /// <returns>
        /// The <see cref="IRepository" />.
        /// </returns>
        public static IRepository<RefreshAuthTokens> GetRepository(RefreshAuthTokens authTokens)
        {
            var repo = MockRepository.StrictMock<IRepository<RefreshAuthTokens>>();
            repo.Expect(v => v.Find(r => r.Subject == authTokens.Subject)).IgnoreArguments().Return(new List<RefreshAuthTokens> { authTokens });
            repo.Expect(v => v.Insert(Arg<RefreshAuthTokens>.Is.Anything)).IgnoreArguments();
            repo.Expect(v => v.Delete(Arg<RefreshAuthTokens>.Is.Anything)).IgnoreArguments();
            repo.Expect(v => v.Commit());

            return repo;
        }
    }
}
