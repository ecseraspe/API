// ---------------------------------------------------------------------------------------------------
// <copyright file="AuthRepositoryTests.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-14</date>
// <summary>
//     The AuthRepositoryTests class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Framework.Tests.Data
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Test.Common.Mocks;
    using Test.Common.Objects;
    using Youffer.Framework.Data;

    /// <summary>
    /// The AuthRepositoryTests class
    /// </summary>
    [TestClass]
    public class AuthRepositoryTests
    {
        /// <summary>
        /// Authentications the repository get role name test.
        /// </summary>
        [TestMethod]
        public void AuthRepositoryGetRoleNameTest()
        {
            var role = IdentityRoleObj.GetIdentityRole();
            var repo = new AuthRepository(
                RepositoryClientMock.GetRepository(ClientObj.GetClientObj()),
                null,
                RepositoryRefreshToken.GetRepository(RefreshTokenObj.GetRefreshToken()),
                null,
                RepositoryIdentityRole.GetRepository(role),
                null);

            BaseMock.MockRepository.ReplayAll();

            Assert.IsNotNull(repo);
            Assert.IsNotNull(repo.GetRoleName(role.Id));
        }
    }
}
