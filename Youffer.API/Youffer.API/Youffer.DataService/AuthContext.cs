// ---------------------------------------------------------------------------------------------------
// <copyright file="AuthContext.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-14</date>
// <summary>
//     The AuthContext class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.DataService
{
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;

    using Youffer.DataService.DBSchema;
    using Youffer.Resources.Constants;

    /// <summary>
    /// The AuthContext class
    /// </summary>
    public class AuthContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthContext"/> class.
        /// </summary>
        public AuthContext()
            : base(ConfigConstants.ConnectionStringKey)
        {
        }

        /// <summary>
        /// Gets or sets the clients.
        /// </summary>
        public DbSet<AuthClients> Clients { get; set; }

        /// <summary>
        /// Gets or sets the EmailTemplates.
        /// </summary>
        public DbSet<EmailTemplates> EmailTemplates { get; set; }

        /// <summary>
        /// Gets or sets the refresh tokens.
        /// </summary>
        public DbSet<RefreshAuthTokens> RefreshTokens { get; set; }

        /// <summary>
        /// Gets or sets the email queue.
        /// </summary>
        public DbSet<EmailQueue> EmailQueue { get; set; }

        /// <summary>
        /// Gets or sets the Contact Lead Mapping
        /// </summary>
        public DbSet<ContactLeadMapping> ContactLeadMapping { get; set; }

        /// <summary>
        /// Gets or sets the Lead Opportuntity Mapping
        /// </summary>
        public DbSet<LeadOpportunityMapping> LeadOpportunityMapping { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>The country.</value>
        public DbSet<Country> Country { get; set; }

        /// <summary>
        /// Gets or sets the company notes.
        /// </summary>
        /// <value>
        /// The company notes.
        /// </value>
        public DbSet<CompanyNotes> CompanyNotes { get; set; }

        /// <summary>
        /// Gets or sets the messages.
        /// </summary>
        /// <value>The messages.</value>
        public DbSet<Messages> Messages { get; set; }

        /// <summary>
        /// Gets or sets the message thread.
        /// </summary>
        /// <value>The message thread.</value>
        public DbSet<MessageThread> MessageThread { get; set; }

        /// <summary>
        /// Gets or sets the message media.
        /// </summary>
        /// <value>The message media.</value>
        public DbSet<MessageMedia> MessageMedia { get; set; }

        /// <summary>
        /// Gets or sets the feedback.
        /// </summary>
        /// <value>
        /// The feedback.
        /// </value>
        public DbSet<Feedback> Feedback { get; set; }

        /// <summary>
        /// Gets or sets the contact us.
        /// </summary>
        /// <value>
        /// The contact us.
        /// </value>
        public DbSet<ContactUs> ContactUs { get; set; }

        /// <summary>
        /// Gets or sets the main interest.
        /// </summary>
        /// <value>
        /// The main interest.
        /// </value>
        public DbSet<MainInterest> MainInterest { get; set; }

        /// <summary>
        /// Gets or sets the sub interest.
        /// </summary>
        /// <value>
        /// The sub interest.
        /// </value>
        public DbSet<SubInterest> SubInterest { get; set; }

        /// <summary>
        /// Gets or sets the department.
        /// </summary>
        /// <value>
        /// The department.
        /// </value>
        public DbSet<Department> Department { get; set; }

        /// <summary>
        ///  Gets or sets the Sms verification
        /// </summary>
        public DbSet<SmsVerification> SmsVerification { get; set; }

        /// <summary>
        /// Gets or sets the pay pal details.
        /// </summary>
        public DbSet<PayPalDetails> PayPalDetails { get; set; }

        /// <summary>
        /// Gets or sets the notification log.
        /// </summary>
        /// <value>
        /// The notification log.
        /// </value>
        public DbSet<NotificationLog> NotificationLog { get; set; }

        /// <summary>
        /// Gets or sets the contact us.
        /// </summary>
        /// <value>
        /// The contact us.
        /// </value>
        public DbSet<Invoice> Invoice { get; set; }

        /// <summary>
        /// Gets or sets the contact us.
        /// </summary>
        /// <value>
        /// The contact us.
        /// </value>
        public DbSet<G2SRequest> G2SRequest { get; set; }

        /// <summary>
        /// Gets or sets the contact us.
        /// </summary>
        /// <value>
        /// The contact us.
        /// </value>
        public DbSet<G2SResponse> G2SResponse { get; set; }

        /// <summary>
        /// Gets or sets the contact us.
        /// </summary>
        /// <value>
        /// The contact us.
        /// </value>
        public DbSet<G2SDMN> G2SDMN { get; set; }

        /// <summary>
        /// Gets or sets PaypalPay Transaction
        /// </summary>
        /// <value>
        /// The PaypalPay Transaction
        /// </value>
        public DbSet<PaypalPayTransaction> PaypalPayTransaction { get; set; }

        /// <summary>
        /// Gets or sets PaypalPay Transaction
        /// </summary>
        /// <value>
        /// The PaypalPay Transaction
        /// </value>
        public DbSet<PaypalPaymentDetails> PaypalPaymentDetails { get; set; }

        /// <summary>
        /// Gets or sets the states.
        /// </summary>
        public DbSet<States> States { get; set; }

        /// <summary>
        /// Gets or sets the message templates.
        /// </summary>
        public DbSet<MessageTemplates> MessageTemplates { get; set; }

        /// <summary>
        /// Gets or sets the payment configuration information.
        /// </summary> 
        public DbSet<PaymentConfigInfo> PaymentConfigInfo { get; set; }

        /// <summary>
        /// Gets or sets the payment gateway.
        /// </summary>
        /// <value>
        /// The payment gateway.
        /// </value>
        public DbSet<PaymentGateway> PaymentGateway { get; set; }

        /// <summary>
        /// Gets or sets the payment gateway details.
        /// </summary>
        /// <value>
        /// The payment gateway details.
        /// </value>
        public DbSet<PaymentGatewayDetails> PaymentGatewayDetails { get; set; }

        /// <summary>
        /// Gets or sets the type of the parent business.
        /// </summary>
        public DbSet<ParentBusinessType> ParentBusinessType { get; set; }

        /// <summary>
        /// Gets or sets the type of the business.
        /// </summary>
        public DbSet<BusinessType> BusinessType { get; set; }

        /// <summary>
        /// Gets or sets the mobile application version.
        /// </summary>
        public DbSet<MobileAppVersion> MobileAppVersion { get; set; }

        /// <summary>
        /// The on model creating.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUser>().ToTable("Users");

            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
        }
    }
}