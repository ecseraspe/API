// ---------------------------------------------------------------------------------------------------
// <copyright file="Startup.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-14</date>
// <summary>
//     The Startup class
// </summary>
// ---------------------------------------------------------------------------------------------------

using Microsoft.Owin;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Youffer.API;
using Youffer.Common.Helper;
using Youffer.Resources.Constants;

[assembly: OwinStartup(typeof(Startup))]

namespace Youffer.API
{
    using System;
    using System.Web.Http;
    using Microsoft.AspNet.SignalR;
    using Microsoft.Owin;
    using Microsoft.Owin.Cors;
    using Microsoft.Owin.Security.OAuth;
    using Owin;
    using Quartz;
    using Quartz.Impl;
    using Youffer.API.DependencyResolution;
    using Youffer.API.Providers;
    using Youffer.Common.CRMService;
    using Youffer.Common.DataService;
    using Youffer.Framework.Mapper;

    /// <summary>
    /// The Start-up class
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// The authentication repository
        /// </summary>
        private readonly IAuthRepository authRepository;

        /// <summary>
        /// The crm manager service
        /// </summary>
        private readonly ICRMManagerService crmManagerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        public Startup()
        {
            this.authRepository = StructuremapMvc.StructureMapDependencyScope.Container.GetInstance<IAuthRepository>();
            this.crmManagerService = StructuremapMvc.StructureMapDependencyScope.Container.GetInstance<ICRMManagerService>();
        }

        /// <summary>
        /// Gets the google authentication options.
        /// </summary>
        public static GoogleOAuth2AuthenticationOptions GoogleAuthOptions { get; private set; }

        /// <summary>
        /// Gets the facebook authentication options.
        /// </summary>
        public static FacebookAuthenticationOptions FacebookAuthOptions { get; private set; }

        /// <summary>
        /// Gets the o authentication bearer options.
        /// </summary>
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }

        /// <summary>
        /// Configurations the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            this.ConfigureOAuth(app);

            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            app.Map(
               "/signalr",
               map =>
               {
                   map.UseCors(CorsOptions.AllowAll);
                   map.RunSignalR(new HubConfiguration() { EnableJSONP = true });
               });

            var container = StructuremapMvc.StructureMapDependencyScope.Container;
            config.DependencyResolver = new StructureMapWebApiDependencyResolver(container);

            app.UseWebApi(config);
            AutoMapperBootstraper.Initialize();
            this.ConfigureQuartzJobs();
            ////ISchedulerFactory schedFact = new StdSchedulerFactory();
            ////IScheduler scheduler = schedFact.GetScheduler();
            ////scheduler.Start();

            //// Database.SetInitializer(new MigrateDatabaseToLatestVersion<AuthContext, DataService.Migrations.Configuration>());
        }

        /// <summary>
        /// Configure Quartz Jobs
        /// </summary>
        public void ConfigureQuartzJobs()
        {
            // construct a scheduler factory
            ISchedulerFactory schedFact = new StdSchedulerFactory();

            // get a scheduler
            IScheduler sched = schedFact.GetScheduler();
            sched.Start();

            // construct job info
            IJobDetail job = JobBuilder.Create<SendNotification>()
                 .WithIdentity("myJobSendMsgFromCRM", "myJobSendMsgFromCRM")
                 .Build();
            ITrigger trigger = TriggerBuilder.Create()
               .WithDailyTimeIntervalSchedule(s => s.WithIntervalInSeconds(30).OnEveryDay().StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0)))
                .WithIdentity("myJobSendMsgFromCRM", "myJobSendMsgFromCRM")
                .ForJob(job)
                .StartNow().Build();

            // construct job info
            IJobDetail jobBroadcastMsg = JobBuilder.Create<SendNotification>()
                 .WithIdentity("myJobBroadcastMsgFromCRM", "myJobBroadcastMsgFromCRM")
                 .Build();
            ITrigger triggerBroadcastMsg = TriggerBuilder.Create()
               .WithDailyTimeIntervalSchedule(s => s.WithIntervalInSeconds(30).OnEveryDay().StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0)))
                .WithIdentity("myJobBroadcastMsgFromCRM", "myJobBroadcastMsgFromCRM")
                .ForJob(jobBroadcastMsg)
                .StartNow().Build();

            // construct job info
            IJobDetail jobEnterPhone = JobBuilder.Create<SendNotification>()
                 .WithIdentity("myJobEnterPhone", "myJobEnterPhone")
                 .Build();
            ITrigger triggerEnterPhone = TriggerBuilder.Create()
               .WithDailyTimeIntervalSchedule(s => s.WithIntervalInHours(6).OnEveryDay().StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0)))
                .WithIdentity("myJobEnterPhone", "myJobEnterPhone")
                .ForJob(jobEnterPhone)
                .StartNow().Build();

            // construct job info
            IJobDetail jobEnterNeeds = JobBuilder.Create<SendNotification>()
                 .WithIdentity("myJobEnterNeeds", "myJobEnterNeeds")
                 .Build();
            ITrigger triggerEnterNeeds = TriggerBuilder.Create()
               .WithDailyTimeIntervalSchedule(s => s.WithIntervalInHours(6).OnEveryDay().StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0)))
                .WithIdentity("myJobEnterNeeds", "myJobEnterNeeds")
                .ForJob(jobEnterNeeds)
                .StartNow().Build();

            // construct job info
            IJobDetail jobUpdateCreditBalance = JobBuilder.Create<SendNotification>()
                 .WithIdentity("myjobUpdateCreditBalance", "myjobUpdateCreditBalance")
                 .Build();
            ITrigger triggerUpdateCreditBalance = TriggerBuilder.Create()
               .WithDailyTimeIntervalSchedule(s => s.WithIntervalInSeconds(30).OnEveryDay().StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0)))
                .WithIdentity("myjobUpdateCreditBalance", "myjobUpdateCreditBalance")
                .ForJob(jobUpdateCreditBalance)
                .StartNow().Build();

            // construct job info
            IJobDetail jobUpdateCashBalance = JobBuilder.Create<SendNotification>()
                 .WithIdentity("myjobUpdateCashBalance", "myjobUpdateCashBalance")
                 .Build();
            ITrigger triggerUpdateCashBalance = TriggerBuilder.Create()
               .WithDailyTimeIntervalSchedule(s => s.WithIntervalInSeconds(20).OnEveryDay().StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0)))
                .WithIdentity("myjobUpdateCashBalance", "myjobUpdateCashBalance")
                .ForJob(jobUpdateCashBalance)
                .StartNow().Build();

            sched.ScheduleJob(job, trigger);
            sched.ScheduleJob(jobBroadcastMsg, triggerBroadcastMsg);
            sched.ScheduleJob(jobEnterPhone, triggerEnterPhone);
            sched.ScheduleJob(jobEnterNeeds, triggerEnterNeeds);
            sched.ScheduleJob(jobUpdateCreditBalance, triggerUpdateCreditBalance);
            sched.ScheduleJob(jobUpdateCashBalance, triggerUpdateCashBalance);
        }

        /// <summary>
        /// Configures the o authentication.
        /// </summary>
        /// <param name="app">The application.</param>
        public void ConfigureOAuth(IAppBuilder app)
        {
            // use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ExternalCookie);

            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();

            OAuthAuthorizationServerOptions authServerOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(10),
                Provider = new SimpleAuthorizationServerProvider(this.authRepository, this.crmManagerService),
                RefreshTokenProvider = new SimpleRefreshTokenProvider(this.authRepository)
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(authServerOptions);
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);

            // Configure Google External Login
            GoogleAuthOptions = new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = AppSettings.Get<string>(ConfigConstants.GoogleClientId),
                ClientSecret = AppSettings.Get<string>(ConfigConstants.GoogleClientSecret),
                Provider = new GoogleAuthProvider()
            };
            app.UseGoogleAuthentication(GoogleAuthOptions);

            // Configure Facebook External Login
            FacebookAuthOptions = new FacebookAuthenticationOptions()
            {
                AppId = AppSettings.Get<string>(ConfigConstants.FaceBookAppId),
                AppSecret = AppSettings.Get<string>(ConfigConstants.FaceBookAppSecret),
                Provider = new FacebookAuthProvider()
            };
            FacebookAuthOptions.Scope.Add("email");
            FacebookAuthOptions.Scope.Add("user_birthday");
            app.UseFacebookAuthentication(FacebookAuthOptions);
        }
    }
}