// ---------------------------------------------------------------------------------------------------
// <copyright file="DefaultRegistry.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-14</date>
// <summary>
//     The DefaultRegistry class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.API.DependencyResolution
{
    using System;
    using System.Data.Entity;
    using System.IO;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Quartz;
    using Quartz.Impl;
    using Quartz.Spi;
    using StructureMap;
    using StructureMap.Configuration.DSL;
    using StructureMap.Graph;
    using Youffer.Common.CRMService;
    using Youffer.Common.DataService;
    using Youffer.Common.Helper;
    using Youffer.Common.LogService;
    using Youffer.Common.Mapper;
    using Youffer.Common.MaxmindGeoIP2;
    using Youffer.Common.Notification;
    using Youffer.Common.SMS;
    using Youffer.CRM;
    using Youffer.DataService;
    using Youffer.Framework.CRMService;
    using Youffer.Framework.Data;
    using Youffer.Framework.Data.CRMService;
    using Youffer.Framework.Mapper;
    using Youffer.Framework.MaxmindGeoIP2;
    using Youffer.Framework.Notification;
    using Youffer.Resources.Constants;
    using Youffer.Sms;

    /// <summary>
    /// The DefaultRegistry class
    /// </summary>
    public class DefaultRegistry : Registry
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultRegistry"/> class.
        /// </summary>
        public DefaultRegistry()
        {
            var apiUserName = AppSettings.Get<string>("VTiger_UserName");
            var apiAccessKey = AppSettings.Get<string>("VTiger_AccessKey");
            var apiserviceUrl = AppSettings.Get<string>("VTiger_ServiceUrl");
            var googleGcmApiKey = AppSettings.Get<string>("GoogleGCMNotification");
            var appleCertPwd = AppSettings.Get("AppleCertPwd", string.Empty);

            var plivoAuthToken = AppSettings.Get<string>(ConfigConstants.PlivoAuthToken);
            var plivoAuthId = AppSettings.Get<string>(ConfigConstants.PlivoAuthId);
            var plivoFromNumber = AppSettings.Get<string>(ConfigConstants.PlivoFromNumber);

            var ip2LocationUserId = AppSettings.Get<string>(ConfigConstants.IP2LocationUserId);
            var ip2LocationLicenseKey = AppSettings.Get<string>(ConfigConstants.IP2LocationLicenseKey);

            string defaultPath = AppSettings.Get<string>(ConfigConstants.DefaultAppleCertPath);
            string outputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, defaultPath, "PushSharp.Apns.Sandbox.p12");

            this.Scan(
                            scan =>
                            {
                                scan.TheCallingAssembly();
                                scan.AssembliesFromApplicationBaseDirectory(assembly => assembly.FullName.StartsWith("Youffer"));
                                scan.WithDefaultConventions();
                            });

            this.For(typeof(DbContext)).Use(typeof(AuthContext));
            this.For(typeof(IRepository<>)).Use(typeof(Repository<>));

            this.For(typeof(IUserStore<>)).Use(typeof(UserStore<>));
            this.For(typeof(UserManager<>)).Singleton().Use(typeof(UserManager<>));

            this.For<IAuthRepository>().Use<AuthRepository>();

            this.For<ILoggerService>().Singleton().Use<LoggerService>();

            this.For<IContactService>().Use<ContactService>();
            this.For<ILeadService>().Use<LeadService>();
            this.For<IOrganisationService>().Use<OrganisationService>();
            this.For<IOpportunityService>().Use<OpportunityService>();
            this.For<IUserService>().Use<UserService>();
            this.For<ICRMManagerService>().Use<CRMManagerService>();
            this.For<ICommonService>().Use<CommonService>();
            this.For<IYoufferContactService>().Use<YoufferContactService>();
            this.For<IYoufferLeadService>().Use<YoufferLeadService>();
            this.For<IYoufferNoteService>().Use<YoufferNoteService>();
            this.For<IYoufferMessageService>().Use<YoufferMessageService>();
            this.For<INoteService>().Use<NoteService>();
            this.For<IYoufferFeedbackService>().Use<YoufferFeedbackService>();
            this.For<IYoufferPaymentService>().Use<YoufferPaymentService>();
            this.For<IMapperFactory>().Singleton().Use<MapperFactory>();
            this.For<IYoufferNoteService>().Use<YoufferNoteService>();
            this.For<IUserReviewService>().Use<UserReviewService>();
            this.For<IContactUsService>().Use<ContactUsService>();

            this.For<IJobFactory>().Use<StructureMapJobFactory>();
            this.For<StdSchedulerFactory>().Use(() => new StdSchedulerFactory());
            this.For<ISchedulerFactory>().Use<StdSchedulerFactory>();
            this.For<IScheduler>().Use(() => ObjectFactory.GetInstance<ISchedulerFactory>().GetScheduler());

            this.For<ISmsService>().Singleton().Use<SmsService>()
                .Ctor<string>("accountSid").Is(plivoAuthId)
                .Ctor<string>("authToken").Is(plivoAuthToken)
                .Ctor<string>("fromNumber").Is(plivoFromNumber);

            this.For<IPushMessageService>().Singleton().Use<PushService>()
                .Ctor<string>("googleGcmApiKey").Is(googleGcmApiKey)
                .Ctor<string>("appleCertPwd").Is(appleCertPwd)
                .Ctor<string>("outputPath").Is(outputPath);

            this.For<IIP2LocationService>().Singleton().Use<IP2LocationService>()
                .Ctor<string>("userId").Is(ip2LocationUserId)
                .Ctor<string>("licenseKey").Is(ip2LocationLicenseKey);

            this.For<IVTigerService>().Singleton().Use<VTiger>()
                .Ctor<string>("apiUserName").Is(apiUserName)
                .Ctor<string>("apiAccessKey").Is(apiAccessKey)
                .Ctor<string>("apiServiceUrl").Is(apiserviceUrl);
        }

        #endregion
    }
}