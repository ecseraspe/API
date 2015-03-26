// ---------------------------------------------------------------------------------------------------
// <copyright file="IEmailQueueService.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-14</date>
// <summary>
//     The IEmailQueueService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Common.DataService
{
    using Youffer.Resources.ViewModel;

    /// <summary>
    /// The EmailQueueService interface.
    /// </summary>
    public interface IEmailQueueService
    {
        /// <summary>
        /// The send activation email.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        bool SendActivationEmail(ApplicationUserDto user);
    }
}
