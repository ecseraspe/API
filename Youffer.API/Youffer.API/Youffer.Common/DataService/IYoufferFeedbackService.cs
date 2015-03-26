// ---------------------------------------------------------------------------------------------------
// <copyright file="IYoufferFeedbackService.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2014-12-8</date>
// <summary>
//     The IYoufferFeedbackService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Common.DataService
{
    using System.Collections.Generic;
    using Youffer.Resources.ViewModel;

    /// <summary>
    /// Interface IYoufferNoteService
    /// </summary>
    public interface IYoufferFeedbackService
    {
        /// <summary>
        /// Saves the feedback.
        /// </summary>
        /// <param name="feedback">The feedback.</param>
        /// <returns>FeedbackDto object.</returns>
        FeedbackDto SaveFeedback(FeedbackDto feedback);
    }
}
