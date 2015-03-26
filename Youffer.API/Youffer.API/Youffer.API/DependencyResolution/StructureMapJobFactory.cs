// ---------------------------------------------------------------------------------------------------
// <copyright file="StructureMapJobFactory.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-12-23</date>
// <summary>
//     The StructureMapJobFactory class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.API.DependencyResolution
{
    using System;
    using Quartz;
    using Quartz.Spi;
    using StructureMap;

    /// <summary>
    /// Class StructureMapJobFactory
    /// </summary>
    public class StructureMapJobFactory : IJobFactory
    {
        /// <summary>
        /// The container
        /// </summary>
        private readonly IContainer container;

        /// <summary>
        /// Initializes a new instance of the <see cref="StructureMapJobFactory"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public StructureMapJobFactory(IContainer container)
        {
            this.container = container;
        }

        /// <summary>
        /// News the job.
        /// </summary>
        /// <param name="bundle">The bundle.</param>
        /// <param name="scheduler">The scheduler.</param>
        /// <returns>IJob object.</returns>
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var jobDetail = bundle.JobDetail;
            var jobType = jobDetail.JobType;
            try
            {
                return (IJob)this.container.GetInstance(bundle.JobDetail.JobType);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Returns the job.
        /// </summary>
        /// <param name="job">The job.</param>
        public void ReturnJob(IJob job)
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            var disposable = job as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
    }
}