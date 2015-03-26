// ---------------------------------------------------------------------------------------------------
// <copyright file="YoufferFeedbackService.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2014-12-8</date>
// <summary>
//     The YoufferFeedbackService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Framework.Data
{
    using System;
    using Youffer.Common.DataService;
    using Youffer.Common.LogService;
    using Youffer.Common.Mapper;
    using Youffer.DataService.DBSchema;
    using Youffer.Resources.ViewModel;

    /// <summary>
    /// Class YoufferFeedbackService
    /// </summary>
    public class YoufferFeedbackService : IYoufferFeedbackService
    {
        /// <summary>
        /// The mapper factory.
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// The user feedback repository
        /// </summary>
        private readonly IRepository<Feedback> userFeedbackRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="YoufferFeedbackService"/> class.
        /// </summary>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="userFeedbackRepository">The user feedback repository.</param>
        /// <param name="mapperFactory">The mapper factory.</param>
        public YoufferFeedbackService(ILoggerService loggerService, IRepository<Feedback> userFeedbackRepository, IMapperFactory mapperFactory)
        {
            this.LoggerService = loggerService;
            this.userFeedbackRepository = userFeedbackRepository;
            this.mapperFactory = mapperFactory;
        }

        /// <summary>
        /// Gets the logger service.
        /// </summary>        
        protected ILoggerService LoggerService { get; private set; }

        /// <summary>
        /// Saves the feedback.
        /// </summary>
        /// <param name="feedbackDto">The feedback.</param>
        /// <returns>FeedbackDto object.</returns>
        public FeedbackDto SaveFeedback(FeedbackDto feedbackDto)
        {
            try
            {
                Feedback feedback = this.mapperFactory.GetMapper<FeedbackDto, Feedback>().Map(feedbackDto);
                this.userFeedbackRepository.Insert(feedback);
                this.userFeedbackRepository.Commit();

                feedbackDto.Id = feedback.Id;
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("SaveFeedback - " + ex.Message);
            }

            return feedbackDto;
        }
    }
}
