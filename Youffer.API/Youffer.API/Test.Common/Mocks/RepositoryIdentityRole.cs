// ---------------------------------------------------------------------------------------------------
// <copyright file="RepositoryIdentityRole.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-14</date>
// <summary>
//     The RepositoryIdentityRole class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Test.Common.Mocks
{
    using System.Collections.Generic;

    using Microsoft.AspNet.Identity.EntityFramework;
    using Rhino.Mocks;
    using Youffer.Common.DataService;

    /// <summary>
    /// The repository identity role.
    /// </summary>
    public class RepositoryIdentityRole : BaseMock
    {
        /// <summary>
        /// The get repository.
        /// </summary>
        /// <param name="identityRole">The identity role.</param>
        /// <returns>
        /// The <see cref="IRepository" />.
        /// </returns>
        public static IRepository<IdentityRole> GetRepository(IdentityRole identityRole)
        {
            var repo = MockRepository.StrictMock<IRepository<IdentityRole>>();
            repo.Expect(v => v.Find(x => x.Id == "id")).IgnoreArguments().Return(new List<IdentityRole> { identityRole });
            repo.Expect(v => v.Insert(Arg<IdentityRole>.Is.Anything));
            repo.Expect(v => v.Delete(Arg<IdentityRole>.Is.Anything));
            repo.Expect(v => v.Commit());
            
            return repo;
        }
    }
}
