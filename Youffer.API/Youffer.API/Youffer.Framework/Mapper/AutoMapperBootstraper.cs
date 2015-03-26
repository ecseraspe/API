// ---------------------------------------------------------------------------------------------------
// <copyright file="AutoMapperBootstraper.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-14</date>
// <summary>
//     The AutoMapperBootstraper class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Framework.Mapper
{
    using Youffer.CRM;
    using Youffer.DataService.DBSchema;
    using Youffer.Resources.CRMModel;
    using Youffer.Resources.Models;
    using Youffer.Resources.MySqlDbSchema;
    using Youffer.Resources.ViewModel;

    /// <summary>
    /// The auto mapper boot straper.
    /// </summary>
    public class AutoMapperBootstraper
    {
        /// <summary>
        /// Gets a value indicating whether is initialize.
        /// </summary>
        public static bool IsInitialize { get; private set; }

        /// <summary>
        /// The initialize.
        /// </summary>
        public static void Initialize()
        {
            if (IsInitialize)
            {
                return;
            }

            IsInitialize = true;

            MappingLookupDbToDto();

            MappingVTigerToDto();
        }

        /// <summary>
        /// The mapping lookup Db to dto.
        /// </summary>
        private static void MappingLookupDbToDto()
        {
            AutoMapper.Mapper.CreateMap<ApplicationUser, ApplicationUserDto>().ReverseMap();
            AutoMapper.Mapper.CreateMap<AuthClients, ClientDto>().ReverseMap();
            AutoMapper.Mapper.CreateMap<RefreshAuthTokens, RefreshTokenDto>().ReverseMap();
            AutoMapper.Mapper.CreateMap<ContactLeadMapping, ContactLeadMappingDto>().ReverseMap();
            AutoMapper.Mapper.CreateMap<LeadOpportunityMapping, LeadOpportunityMappingDto>().ReverseMap();
            AutoMapper.Mapper.CreateMap<CompanyNotes, CompanyNotesDto>().ReverseMap();
            AutoMapper.Mapper.CreateMap<Department, DepartmentModel>().ReverseMap();
            AutoMapper.Mapper.CreateMap<Country, CountryModel>().ReverseMap();
            AutoMapper.Mapper.CreateMap<MessageMedia, MessageMediaDto>().ReverseMap();
            AutoMapper.Mapper.CreateMap<Messages, MessagesDto>().ReverseMap();
            AutoMapper.Mapper.CreateMap<MessageThread, MessageThreadDto>().ReverseMap();
            AutoMapper.Mapper.CreateMap<ContactUs, ContactUsDto>().ReverseMap();
            AutoMapper.Mapper.CreateMap<PayPalDetails, PayPalDetailsDto>().ReverseMap();
            AutoMapper.Mapper.CreateMap<EmailTemplates, EmailTemplatesDto>().ReverseMap();
            AutoMapper.Mapper.CreateMap<ContactModel, UserResultModel>().ReverseMap();
            AutoMapper.Mapper.CreateMap<States, StateModel>().ReverseMap();
            AutoMapper.Mapper.CreateMap<MessageTemplates, MessageTemplatesDto>().ReverseMap();
            AutoMapper.Mapper.CreateMap<MobileAppVersion, MobileAppVersionDto>().ReverseMap();

            AutoMapper.Mapper.CreateMap<LeadModel, UserResultModel>().ReverseMap();
            AutoMapper.Mapper.CreateMap<Feedback, FeedbackDto>().ReverseMap();
            AutoMapper.Mapper.CreateMap<NotificationLog, NotificationLogDto>().ReverseMap();

            AutoMapper.Mapper.CreateMap<PaymentConfigInfo, PaymentConfigInfoDto>()
                .ForMember(dest => dest.PaymentGatewayInfo, opt => opt.MapFrom(origin => origin.PaymentGateway)).ReverseMap()
                 .ForMember(dest => dest.PaymentGateway, opt => opt.MapFrom(origin => origin.PaymentGatewayInfo));

            AutoMapper.Mapper.CreateMap<PaymentGateway, PaymentGatewayDto>()
                .ForMember(dest => dest.PaymentGatewayDetailsInfo, opt => opt.MapFrom(origin => origin.PaymentGatewayDetails)).ReverseMap()
                 .ForMember(dest => dest.PaymentGatewayDetails, opt => opt.MapFrom(origin => origin.PaymentGatewayDetailsInfo));

            AutoMapper.Mapper.CreateMap<PaymentGatewayDetails, PaymentGatewayDetailsDto>().ReverseMap();

            AutoMapper.Mapper.CreateMap<SmsVerification, SmsVerificationDto>().ReverseMap();

            AutoMapper.Mapper.CreateMap<OrganisationModel, OrgResultModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(origin => origin.AccountName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(origin => origin.Email1))
                .ForMember(dest => dest.SecondaryEmail, opt => opt.MapFrom(origin => origin.Email2))
                .ForMember(dest => dest.State, opt => opt.MapFrom(origin => origin.BillState))
                .ForMember(dest => dest.City, opt => opt.MapFrom(origin => origin.BillCity))
                .ForMember(dest => dest.POBox, opt => opt.MapFrom(origin => origin.BillPOBox))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(origin => origin.BillAddress))
            .ReverseMap()
                .ForMember(dest => dest.AccountName, opt => opt.MapFrom(origin => origin.Name))
                .ForMember(dest => dest.Email1, opt => opt.MapFrom(origin => origin.Email))
                .ForMember(dest => dest.Email2, opt => opt.MapFrom(origin => origin.SecondaryEmail))
                .ForMember(dest => dest.BillState, opt => opt.MapFrom(origin => origin.State))
                .ForMember(dest => dest.BillCity, opt => opt.MapFrom(origin => origin.City))
                .ForMember(dest => dest.BillPOBox, opt => opt.MapFrom(origin => origin.POBox))
                .ForMember(dest => dest.BillAddress, opt => opt.MapFrom(origin => origin.Address));

            AutoMapper.Mapper.CreateMap<UserModel, OrganisationModel>()
               .ForMember(dest => dest.AccountName, opt => opt.MapFrom(origin => origin.Name))
               .ForMember(dest => dest.Email1, opt => opt.MapFrom(origin => origin.EmailId))
               .ForMember(dest => dest.Website, opt => opt.MapFrom(origin => origin.WebsiteURL))
               .ForMember(dest => dest.BillCountry, opt => opt.MapFrom(origin => origin.Country))
               .ForMember(dest => dest.BillState, opt => opt.MapFrom(origin => origin.State))
           .ReverseMap()
               .ForMember(dest => dest.Name, opt => opt.MapFrom(origin => origin.AccountName))
               .ForMember(dest => dest.EmailId, opt => opt.MapFrom(origin => origin.Email1))
               .ForMember(dest => dest.WebsiteURL, opt => opt.MapFrom(origin => origin.Website))
               .ForMember(dest => dest.Country, opt => opt.MapFrom(origin => origin.BillCountry))
               .ForMember(dest => dest.State, opt => opt.MapFrom(origin => origin.BillState));

            AutoMapper.Mapper.CreateMap<UserModel, ContactModel>()
               .ForMember(dest => dest.FirstName, opt => opt.MapFrom(origin => origin.Name))
               .ForMember(dest => dest.Email, opt => opt.MapFrom(origin => origin.EmailId))
               .ForMember(dest => dest.FBUrl, opt => opt.MapFrom(origin => origin.FacebookURL))
               .ForMember(dest => dest.MailingCountry, opt => opt.MapFrom(origin => origin.Country))
           .ReverseMap()
               .ForMember(dest => dest.Name, opt => opt.MapFrom(origin => origin.FirstName))
               .ForMember(dest => dest.EmailId, opt => opt.MapFrom(origin => origin.Email))
               .ForMember(dest => dest.FacebookURL, opt => opt.MapFrom(origin => origin.FBUrl))
               .ForMember(dest => dest.Country, opt => opt.MapFrom(origin => origin.MailingCountry));

            AutoMapper.Mapper.CreateMap<UserReviewsDto, CRMUserReview>()
              .ForMember(dest => dest.FeedbackText, opt => opt.MapFrom(origin => origin.Feedback))
          .ReverseMap()
              .ForMember(dest => dest.Feedback, opt => opt.MapFrom(origin => origin.FeedbackText));

            AutoMapper.Mapper.CreateMap<InvoiceModel, Invoice>().ReverseMap();
            AutoMapper.Mapper.CreateMap<G2SDMNModel, G2SDMN>().ReverseMap();
            AutoMapper.Mapper.CreateMap<G2SRequestModel, G2SRequest>().ReverseMap();
            AutoMapper.Mapper.CreateMap<G2SResponseModel, G2SResponse>().ReverseMap();
            AutoMapper.Mapper.CreateMap<PaypalPayTransactionModel, PaypalPayTransaction>().ReverseMap();
            AutoMapper.Mapper.CreateMap<PaypalPaymentDetailsModel, PaypalPaymentDetails>().ReverseMap();
        }

        /// <summary>
        /// Mappings the vtiger to dto.
        /// </summary>
        private static void MappingVTigerToDto()
        {
            AutoMapper.Mapper.CreateMap<VtigerPotentialData, UserResultModel>()
               .ForMember(dest => dest.Gender, opt => opt.MapFrom(origin => origin.cf_757))
               .ForMember(dest => dest.ImageURL, opt => opt.MapFrom(origin => origin.cf_759))
               .ForMember(dest => dest.GCMId, opt => opt.MapFrom(origin => origin.cf_761))
               .ForMember(dest => dest.UDId, opt => opt.MapFrom(origin => origin.cf_859))
               .ForMember(dest => dest.AvailableFrom, opt => opt.MapFrom(origin => origin.cf_797))
               .ForMember(dest => dest.AvailableTo, opt => opt.MapFrom(origin => origin.cf_799))
               .ForMember(dest => dest.Latitude, opt => opt.MapFrom(origin => origin.cf_821))
               .ForMember(dest => dest.Longitude, opt => opt.MapFrom(origin => origin.cf_823))
               .ForMember(dest => dest.Timezone, opt => opt.MapFrom(origin => origin.cf_765))
               .ReverseMap()
               .ForMember(dest => dest.cf_757, opt => opt.MapFrom(origin => origin.Gender))
               .ForMember(dest => dest.cf_759, opt => opt.MapFrom(origin => origin.ImageURL))
               .ForMember(dest => dest.cf_761, opt => opt.MapFrom(origin => origin.GCMId))
               .ForMember(dest => dest.cf_859, opt => opt.MapFrom(origin => origin.UDId))
               .ForMember(dest => dest.cf_797, opt => opt.MapFrom(origin => origin.AvailableFrom))
               .ForMember(dest => dest.cf_799, opt => opt.MapFrom(origin => origin.AvailableTo))
               .ForMember(dest => dest.cf_821, opt => opt.MapFrom(origin => origin.Latitude))
               .ForMember(dest => dest.cf_823, opt => opt.MapFrom(origin => origin.Longitude))
               .ForMember(dest => dest.cf_765, opt => opt.MapFrom(origin => origin.Timezone));

            AutoMapper.Mapper.CreateMap<VtigerPotentialData, PurchasedClientsDto>()
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(origin => origin.cf_757))
                .ForMember(dest => dest.ImageURL, opt => opt.MapFrom(origin => origin.cf_759))
                .ForMember(dest => dest.Latitude, opt => opt.MapFrom(origin => origin.cf_821))
                .ForMember(dest => dest.Longitude, opt => opt.MapFrom(origin => origin.cf_823))
                .ForMember(dest => dest.Interest, opt => opt.MapFrom(origin => origin.cf_853))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(origin => origin.phone))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(origin => origin.potentialname))
                 .ReverseMap()
                .ForMember(dest => dest.cf_757, opt => opt.MapFrom(origin => origin.Gender))
                .ForMember(dest => dest.cf_759, opt => opt.MapFrom(origin => origin.ImageURL))
                .ForMember(dest => dest.cf_821, opt => opt.MapFrom(origin => origin.Latitude))
                .ForMember(dest => dest.cf_823, opt => opt.MapFrom(origin => origin.Longitude))
                .ForMember(dest => dest.cf_853, opt => opt.MapFrom(origin => origin.Interest))
                .ForMember(dest => dest.phone, opt => opt.MapFrom(origin => origin.PhoneNumber))
                .ForMember(dest => dest.potentialname, opt => opt.MapFrom(origin => origin.Name));

            AutoMapper.Mapper.CreateMap<CRMCompanyNotes, VTigerCompanyNotes>()
                .ForMember(dest => dest.companynotes_tks_contact, opt => opt.MapFrom(origin => origin.ContactId))
                .ForMember(dest => dest.companynotes_tks_organisation, opt => opt.MapFrom(origin => origin.OrganisationId))
                .ForMember(dest => dest.companynotesno, opt => opt.MapFrom(origin => origin.CompanyNotesNo))
                .ForMember(dest => dest.createdtime, opt => opt.MapFrom(origin => origin.CreatedOn))
                .ForMember(dest => dest.modifiedtime, opt => opt.MapFrom(origin => origin.ModifiedOn))
                .ForMember(dest => dest.companynotes_tks_isdeleted, opt => opt.MapFrom(origin => origin.IsDeleted))
                .ForMember(dest => dest.companynotes_tks_note, opt => opt.MapFrom(origin => origin.Note))
                    .ReverseMap()
                .ForMember(dest => dest.ContactId, opt => opt.MapFrom(origin => origin.companynotes_tks_contact))
                .ForMember(dest => dest.OrganisationId, opt => opt.MapFrom(origin => origin.companynotes_tks_organisation))
                .ForMember(dest => dest.CompanyNotesNo, opt => opt.MapFrom(origin => origin.companynotesno))
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(origin => origin.createdtime))
                .ForMember(dest => dest.ModifiedOn, opt => opt.MapFrom(origin => origin.modifiedtime))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(origin => origin.companynotes_tks_isdeleted))
                .ForMember(dest => dest.Note, opt => opt.MapFrom(origin => origin.companynotes_tks_note));

            AutoMapper.Mapper.CreateMap<ContactModel, VTigerContact>()
                .ForMember(dest => dest.cf_757, opt => opt.MapFrom(origin => origin.Gender))
                .ForMember(dest => dest.cf_759, opt => opt.MapFrom(origin => origin.ImageURL))
                .ForMember(dest => dest.cf_761, opt => opt.MapFrom(origin => origin.GCMId))
                .ForMember(dest => dest.cf_859, opt => opt.MapFrom(origin => origin.UDId))
                .ForMember(dest => dest.cf_765, opt => opt.MapFrom(origin => origin.Timezone))
                .ForMember(dest => dest.cf_795, opt => opt.MapFrom(origin => origin.IsAvailable))
                .ForMember(dest => dest.cf_889, opt => opt.MapFrom(origin => origin.IsOnline))
                .ForMember(dest => dest.cf_797, opt => opt.MapFrom(origin => origin.AvailableFrom))
                .ForMember(dest => dest.cf_799, opt => opt.MapFrom(origin => origin.AvailableTo))
                .ForMember(dest => dest.cf_821, opt => opt.MapFrom(origin => origin.Latitude))
                .ForMember(dest => dest.cf_823, opt => opt.MapFrom(origin => origin.Longitude))
                .ForMember(dest => dest.cf_835, opt => opt.MapFrom(origin => origin.IsActive))
                //// .ForMember(dest => dest.cf_845, opt => opt.MapFrom(origin => origin.PaymentMode))
                .ForMember(dest => dest.cf_847, opt => opt.MapFrom(origin => origin.PaypalId))
                .ForMember(dest => dest.cf_944, opt => opt.MapFrom(origin => origin.CountryCode))
                .ReverseMap()
                 .ForMember(dest => dest.Gender, opt => opt.MapFrom(origin => origin.cf_757))
                 .ForMember(dest => dest.ImageURL, opt => opt.MapFrom(origin => origin.cf_759))
                 .ForMember(dest => dest.GCMId, opt => opt.MapFrom(origin => origin.cf_761))
                 .ForMember(dest => dest.UDId, opt => opt.MapFrom(origin => origin.cf_859))
                 .ForMember(dest => dest.Timezone, opt => opt.MapFrom(origin => origin.cf_765))
                 .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(origin => origin.cf_795))
                 .ForMember(dest => dest.IsOnline, opt => opt.MapFrom(origin => origin.cf_889))
                 .ForMember(dest => dest.AvailableFrom, opt => opt.MapFrom(origin => origin.cf_797))
                 .ForMember(dest => dest.AvailableTo, opt => opt.MapFrom(origin => origin.cf_799))
                 .ForMember(dest => dest.Latitude, opt => opt.MapFrom(origin => origin.cf_821))
                 .ForMember(dest => dest.Longitude, opt => opt.MapFrom(origin => origin.cf_823))
                 .ForMember(dest => dest.IsActive, opt => opt.MapFrom(origin => origin.cf_835))
                //// .ForMember(dest => dest.PaymentMode, opt => opt.MapFrom(origin => origin.cf_845))
                 .ForMember(dest => dest.PaypalId, opt => opt.MapFrom(origin => origin.cf_847))
                 .ForMember(dest => dest.CountryCode, opt => opt.MapFrom(origin => origin.cf_944));

            AutoMapper.Mapper.CreateMap<OrganisationModel, VTigerAccount>()
                 .ForMember(dest => dest.cf_1024, opt => opt.MapFrom(origin => origin.MainBusinessType))
                 .ForMember(dest => dest.cf_777, opt => opt.MapFrom(origin => origin.SubBusinessType))
                 .ForMember(dest => dest.cf_779, opt => opt.MapFrom(origin => origin.GCMId))
                 .ForMember(dest => dest.cf_863, opt => opt.MapFrom(origin => origin.UDId))
                 .ForMember(dest => dest.cf_781, opt => opt.MapFrom(origin => origin.FacebookURL))
                 .ForMember(dest => dest.cf_887, opt => opt.MapFrom(origin => origin.GooglePlusURL))
                 .ForMember(dest => dest.cf_817, opt => opt.MapFrom(origin => origin.ImageURL))
                 .ForMember(dest => dest.cf_829, opt => opt.MapFrom(origin => origin.Latitude))
                 .ForMember(dest => dest.cf_831, opt => opt.MapFrom(origin => origin.Longitude))
                 .ForMember(dest => dest.cf_833, opt => opt.MapFrom(origin => origin.Timezone))
                 .ForMember(dest => dest.cf_837, opt => opt.MapFrom(origin => origin.IsActive))
                 .ForMember(dest => dest.bill_country, opt => opt.MapFrom(origin => origin.BillCountry))
                 .ForMember(dest => dest.bill_city, opt => opt.MapFrom(origin => origin.BillCity))
                 .ForMember(dest => dest.bill_state, opt => opt.MapFrom(origin => origin.BillState))
                 .ForMember(dest => dest.bill_street, opt => opt.MapFrom(origin => origin.BillStreet))
                 .ForMember(dest => dest.bill_code, opt => opt.MapFrom(origin => origin.BillCode))
                 .ForMember(dest => dest.cf_952, opt => opt.MapFrom(origin => origin.CountryCode))
                 .ForMember(dest => dest.cf_950, opt => opt.MapFrom(origin => origin.Password))
                 .ForMember(dest => dest.cf_954, opt => opt.MapFrom(origin => origin.IsImageUploaded))
                 .ForMember(dest => dest.cf_1044, opt => opt.MapFrom(origin => origin.CashBalance))
                 .ForMember(dest => dest.cf_1046, opt => opt.MapFrom(origin => origin.CreditBalance))
                 .ForMember(dest => dest.cf_1054, opt => opt.MapFrom(origin => origin.Browser))
                 .ForMember(dest => dest.cf_1056, opt => opt.MapFrom(origin => origin.Version))
                .ReverseMap()
                 .ForMember(dest => dest.MainBusinessType, opt => opt.MapFrom(origin => origin.cf_1024))
                 .ForMember(dest => dest.SubBusinessType, opt => opt.MapFrom(origin => origin.cf_777))
                 .ForMember(dest => dest.GCMId, opt => opt.MapFrom(origin => origin.cf_779))
                 .ForMember(dest => dest.UDId, opt => opt.MapFrom(origin => origin.cf_863))
                 .ForMember(dest => dest.FacebookURL, opt => opt.MapFrom(origin => origin.cf_781))
                 .ForMember(dest => dest.GooglePlusURL, opt => opt.MapFrom(origin => origin.cf_887))
                 .ForMember(dest => dest.ImageURL, opt => opt.MapFrom(origin => origin.cf_817))
                 .ForMember(dest => dest.Latitude, opt => opt.MapFrom(origin => origin.cf_829))
                 .ForMember(dest => dest.Longitude, opt => opt.MapFrom(origin => origin.cf_831))
                 .ForMember(dest => dest.Timezone, opt => opt.MapFrom(origin => origin.cf_833))
                 .ForMember(dest => dest.IsActive, opt => opt.MapFrom(origin => origin.cf_837))
                 .ForMember(dest => dest.BillCountry, opt => opt.MapFrom(origin => origin.bill_country))
                 .ForMember(dest => dest.BillCity, opt => opt.MapFrom(origin => origin.bill_city))
                 .ForMember(dest => dest.BillState, opt => opt.MapFrom(origin => origin.bill_state))
                 .ForMember(dest => dest.BillStreet, opt => opt.MapFrom(origin => origin.bill_street))
                 .ForMember(dest => dest.BillCode, opt => opt.MapFrom(origin => origin.bill_code))
                 .ForMember(dest => dest.CountryCode, opt => opt.MapFrom(origin => origin.cf_952))
                 .ForMember(dest => dest.Password, opt => opt.MapFrom(origin => origin.cf_950))
                 .ForMember(dest => dest.IsImageUploaded, opt => opt.MapFrom(origin => origin.cf_954))
                 .ForMember(dest => dest.CashBalance, opt => opt.MapFrom(origin => origin.cf_1044))
                 .ForMember(dest => dest.CreditBalance, opt => opt.MapFrom(origin => origin.cf_1046))
                 .ForMember(dest => dest.Browser, opt => opt.MapFrom(origin => origin.cf_1054))
                 .ForMember(dest => dest.Version, opt => opt.MapFrom(origin => origin.cf_1056));

            AutoMapper.Mapper.CreateMap<ContactModel, VTigerLead>()
                .ForMember(dest => dest.cf_783, opt => opt.MapFrom(origin => origin.Birthday))
                .ForMember(dest => dest.cf_785, opt => opt.MapFrom(origin => origin.Gender))
                .ForMember(dest => dest.cf_787, opt => opt.MapFrom(origin => origin.ImageURL))
                .ForMember(dest => dest.cf_789, opt => opt.MapFrom(origin => origin.GCMId))
                .ForMember(dest => dest.cf_861, opt => opt.MapFrom(origin => origin.UDId))
                .ForMember(dest => dest.cf_793, opt => opt.MapFrom(origin => origin.Timezone))
                .ForMember(dest => dest.cf_769, opt => opt.MapFrom(origin => origin.MainInterest))
                .ForMember(dest => dest.cf_771, opt => opt.MapFrom(origin => origin.SubInterest))
                .ForMember(dest => dest.cf_773, opt => opt.MapFrom(origin => origin.ContactId))
                .ForMember(dest => dest.cf_803, opt => opt.MapFrom(origin => origin.IsAvailable))
                .ForMember(dest => dest.cf_891, opt => opt.MapFrom(origin => origin.IsOnline))
                .ForMember(dest => dest.cf_807, opt => opt.MapFrom(origin => origin.AvailableFrom))
                .ForMember(dest => dest.cf_809, opt => opt.MapFrom(origin => origin.AvailableTo))
                .ForMember(dest => dest.cf_825, opt => opt.MapFrom(origin => origin.Latitude))
                .ForMember(dest => dest.cf_827, opt => opt.MapFrom(origin => origin.Longitude))
                .ForMember(dest => dest.cf_767, opt => opt.MapFrom(origin => origin.IsActive))
                .ForMember(dest => dest.country, opt => opt.MapFrom(origin => origin.MailingCountry))
                .ForMember(dest => dest.state, opt => opt.MapFrom(origin => origin.MailingState))
                ////.ForMember(dest => dest.cf_849, opt => opt.MapFrom(origin => origin.PaymentMode))
                .ForMember(dest => dest.cf_851, opt => opt.MapFrom(origin => origin.PaypalId))
                .ForMember(dest => dest.cf_942, opt => opt.MapFrom(origin => origin.CountryCode))
                .ReverseMap()
                    .ForMember(dest => dest.Birthday, opt => opt.MapFrom(origin => origin.cf_783))
                    .ForMember(dest => dest.Gender, opt => opt.MapFrom(origin => origin.cf_785))
                    .ForMember(dest => dest.ImageURL, opt => opt.MapFrom(origin => origin.cf_787))
                    .ForMember(dest => dest.GCMId, opt => opt.MapFrom(origin => origin.cf_789))
                    .ForMember(dest => dest.UDId, opt => opt.MapFrom(origin => origin.cf_861))
                    .ForMember(dest => dest.Timezone, opt => opt.MapFrom(origin => origin.cf_793))
                    .ForMember(dest => dest.MainInterest, opt => opt.MapFrom(origin => origin.cf_769))
                    .ForMember(dest => dest.SubInterest, opt => opt.MapFrom(origin => origin.cf_771))
                    .ForMember(dest => dest.ContactId, opt => opt.MapFrom(origin => origin.cf_773))
                    .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(origin => origin.cf_803))
                    .ForMember(dest => dest.IsOnline, opt => opt.MapFrom(origin => origin.cf_891))
                    .ForMember(dest => dest.AvailableFrom, opt => opt.MapFrom(origin => origin.cf_807))
                    .ForMember(dest => dest.AvailableTo, opt => opt.MapFrom(origin => origin.cf_809))
                    .ForMember(dest => dest.Latitude, opt => opt.MapFrom(origin => origin.cf_825))
                    .ForMember(dest => dest.Longitude, opt => opt.MapFrom(origin => origin.cf_827))
                    .ForMember(dest => dest.IsActive, opt => opt.MapFrom(origin => origin.cf_767))
                    .ForMember(dest => dest.MailingCountry, opt => opt.MapFrom(origin => origin.country))
                    .ForMember(dest => dest.MailingState, opt => opt.MapFrom(origin => origin.state))
                ////.ForMember(dest => dest.PaymentMode, opt => opt.MapFrom(origin => origin.cf_849))
                    .ForMember(dest => dest.PaypalId, opt => opt.MapFrom(origin => origin.cf_851))
                    .ForMember(dest => dest.CountryCode, opt => opt.MapFrom(origin => origin.cf_942));

            AutoMapper.Mapper.CreateMap<LeadModel, VTigerLead>()
                .ForMember(dest => dest.cf_803, opt => opt.MapFrom(origin => origin.IsAvailable))
                 .ForMember(dest => dest.cf_891, opt => opt.MapFrom(origin => origin.IsOnline))
                .ForMember(dest => dest.cf_807, opt => opt.MapFrom(origin => origin.AvailableFrom))
                .ForMember(dest => dest.cf_809, opt => opt.MapFrom(origin => origin.AvailableTo))
                .ForMember(dest => dest.cf_769, opt => opt.MapFrom(origin => origin.MainInterest))
                .ForMember(dest => dest.cf_771, opt => opt.MapFrom(origin => origin.SubInterest))
                .ForMember(dest => dest.cf_785, opt => opt.MapFrom(origin => origin.Gender))
                .ForMember(dest => dest.cf_789, opt => opt.MapFrom(origin => origin.GCMId))
                .ForMember(dest => dest.cf_861, opt => opt.MapFrom(origin => origin.UDId))
                .ForMember(dest => dest.cf_773, opt => opt.MapFrom(origin => origin.OwnerContactId))
                .ForMember(dest => dest.cf_787, opt => opt.MapFrom(origin => origin.ImageURL))
                .ForMember(dest => dest.cf_783, opt => opt.MapFrom(origin => origin.Birthday))
                .ForMember(dest => dest.cf_825, opt => opt.MapFrom(origin => origin.Latitude))
                .ForMember(dest => dest.cf_827, opt => opt.MapFrom(origin => origin.Longitude))
                .ForMember(dest => dest.cf_767, opt => opt.MapFrom(origin => origin.IsActive))
                .ForMember(dest => dest.country, opt => opt.MapFrom(origin => origin.Country))
                //// .ForMember(dest => dest.cf_849, opt => opt.MapFrom(origin => origin.PaymentMode))
                .ForMember(dest => dest.cf_851, opt => opt.MapFrom(origin => origin.PaypalId))
                .ForMember(dest => dest.cf_885, opt => opt.MapFrom(origin => origin.IsUserBlocked))
                .ForMember(dest => dest.cf_942, opt => opt.MapFrom(origin => origin.CountryCode))
                .ForMember(dest => dest.cf_1032, opt => opt.MapFrom(origin => origin.PurchasedWithCreditCount))
                .ReverseMap()
                    .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(origin => origin.cf_803))
                    .ForMember(dest => dest.IsOnline, opt => opt.MapFrom(origin => origin.cf_891))
                    .ForMember(dest => dest.AvailableFrom, opt => opt.MapFrom(origin => origin.cf_807))
                    .ForMember(dest => dest.AvailableTo, opt => opt.MapFrom(origin => origin.cf_809))
                    .ForMember(dest => dest.MainInterest, opt => opt.MapFrom(origin => origin.cf_769))
                    .ForMember(dest => dest.SubInterest, opt => opt.MapFrom(origin => origin.cf_771))
                    .ForMember(dest => dest.Gender, opt => opt.MapFrom(origin => origin.cf_785))
                    .ForMember(dest => dest.GCMId, opt => opt.MapFrom(origin => origin.cf_789))
                    .ForMember(dest => dest.UDId, opt => opt.MapFrom(origin => origin.cf_861))
                    .ForMember(dest => dest.OwnerContactId, opt => opt.MapFrom(origin => origin.cf_773))
                    .ForMember(dest => dest.ImageURL, opt => opt.MapFrom(origin => origin.cf_787))
                    .ForMember(dest => dest.Birthday, opt => opt.MapFrom(origin => origin.cf_783))
                    .ForMember(dest => dest.Latitude, opt => opt.MapFrom(origin => origin.cf_825))
                    .ForMember(dest => dest.Longitude, opt => opt.MapFrom(origin => origin.cf_827))
                    .ForMember(dest => dest.IsActive, opt => opt.MapFrom(origin => origin.cf_767))
                    .ForMember(dest => dest.Country, opt => opt.MapFrom(origin => origin.country))
                ////  .ForMember(dest => dest.PaymentMode, opt => opt.MapFrom(origin => origin.cf_849))
                    .ForMember(dest => dest.PaypalId, opt => opt.MapFrom(origin => origin.cf_851))
                    .ForMember(dest => dest.IsUserBlocked, opt => opt.MapFrom(origin => origin.cf_885))
                    .ForMember(dest => dest.CountryCode, opt => opt.MapFrom(origin => origin.cf_942))
                    .ForMember(dest => dest.PurchasedWithCreditCount, opt => opt.MapFrom(origin => origin.cf_1032));

            AutoMapper.Mapper.CreateMap<OpportunityModel, VTigerPotential>()
                .ForMember(dest => dest.cf_841, opt => opt.MapFrom(origin => origin.IsActive))
                 .ForMember(dest => dest.cf_843, opt => opt.MapFrom(origin => origin.Call))
                 .ForMember(dest => dest.cf_853, opt => opt.MapFrom(origin => origin.Interest))
                 .ForMember(dest => dest.cf_855, opt => opt.MapFrom(origin => origin.CompanyReported))
                 .ForMember(dest => dest.cf_883, opt => opt.MapFrom(origin => origin.UserReported))
                 .ForMember(dest => dest.cf_857, opt => opt.MapFrom(origin => origin.DealClosed))
                .ReverseMap()
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(origin => origin.cf_841))
                .ForMember(dest => dest.Call, opt => opt.MapFrom(origin => origin.cf_843))
                .ForMember(dest => dest.Interest, opt => opt.MapFrom(origin => origin.cf_853))
                .ForMember(dest => dest.CompanyReported, opt => opt.MapFrom(origin => origin.cf_855))
                .ForMember(dest => dest.UserReported, opt => opt.MapFrom(origin => origin.cf_883))
                .ForMember(dest => dest.DealClosed, opt => opt.MapFrom(origin => origin.cf_857));

            AutoMapper.Mapper.CreateMap<LeadModel, VTigerPotential>()
                .ForMember(dest => dest.potentialname, opt => opt.MapFrom(origin => origin.FirstName))
                .ReverseMap()
                    .ForMember(dest => dest.FirstName, opt => opt.MapFrom(origin => origin.potentialname));

            AutoMapper.Mapper.CreateMap<VTigerLead, UserResultModel>()
               .ForMember(dest => dest.FirstName, opt => opt.MapFrom(origin => origin.firstname))
               .ForMember(dest => dest.LastName, opt => opt.MapFrom(origin => origin.lastname))
               .ForMember(dest => dest.Gender, opt => opt.MapFrom(origin => origin.cf_785))
               .ForMember(dest => dest.Email, opt => opt.MapFrom(origin => origin.email))
               .ForMember(dest => dest.Birthday, opt => opt.MapFrom(origin => origin.cf_783))
               .ForMember(dest => dest.ImageURL, opt => opt.MapFrom(origin => origin.cf_787))
               .ForMember(dest => dest.GCMId, opt => opt.MapFrom(origin => origin.cf_789))
               .ForMember(dest => dest.UDId, opt => opt.MapFrom(origin => origin.cf_861))
               .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(origin => origin.cf_803))
               .ForMember(dest => dest.IsOnline, opt => opt.MapFrom(origin => origin.cf_891))
               .ForMember(dest => dest.AvailableFrom, opt => opt.MapFrom(origin => origin.cf_807))
               .ForMember(dest => dest.AvailableTo, opt => opt.MapFrom(origin => origin.cf_809))
               .ForMember(dest => dest.Latitude, opt => opt.MapFrom(origin => origin.cf_825))
               .ForMember(dest => dest.Longitude, opt => opt.MapFrom(origin => origin.cf_827))
               .ForMember(dest => dest.Description, opt => opt.MapFrom(origin => origin.description))
               .ForMember(dest => dest.Timezone, opt => opt.MapFrom(origin => origin.cf_793))
               .ForMember(dest => dest.Phone, opt => opt.MapFrom(origin => origin.phone))
               .ForMember(dest => dest.CountryCode, opt => opt.MapFrom(origin => origin.cf_942))
               .ReverseMap()
                   .ForMember(dest => dest.firstname, opt => opt.MapFrom(origin => origin.FirstName))
                    .ForMember(dest => dest.lastname, opt => opt.MapFrom(origin => origin.LastName))
                    .ForMember(dest => dest.cf_785, opt => opt.MapFrom(origin => origin.Gender))
                    .ForMember(dest => dest.email, opt => opt.MapFrom(origin => origin.Email))
                    .ForMember(dest => dest.cf_783, opt => opt.MapFrom(origin => origin.Birthday))
                    .ForMember(dest => dest.cf_787, opt => opt.MapFrom(origin => origin.ImageURL))
                    .ForMember(dest => dest.cf_789, opt => opt.MapFrom(origin => origin.GCMId))
                    .ForMember(dest => dest.cf_861, opt => opt.MapFrom(origin => origin.UDId))
                    .ForMember(dest => dest.cf_803, opt => opt.MapFrom(origin => origin.IsAvailable))
                    .ForMember(dest => dest.cf_891, opt => opt.MapFrom(origin => origin.IsOnline))
                    .ForMember(dest => dest.cf_807, opt => opt.MapFrom(origin => origin.AvailableFrom))
                    .ForMember(dest => dest.cf_809, opt => opt.MapFrom(origin => origin.AvailableTo))
                    .ForMember(dest => dest.cf_825, opt => opt.MapFrom(origin => origin.Latitude))
                    .ForMember(dest => dest.cf_827, opt => opt.MapFrom(origin => origin.Longitude))
                    .ForMember(dest => dest.description, opt => opt.MapFrom(origin => origin.Description))
                    .ForMember(dest => dest.cf_793, opt => opt.MapFrom(origin => origin.Timezone))
                    .ForMember(dest => dest.phone, opt => opt.MapFrom(origin => origin.Phone))
                    .ForMember(dest => dest.cf_942, opt => opt.MapFrom(origin => origin.CountryCode));

            AutoMapper.Mapper.CreateMap<VTigerDashBoardData, UserResultModel>()
               .ForMember(dest => dest.FirstName, opt => opt.MapFrom(origin => origin.firstname))
               .ForMember(dest => dest.LastName, opt => opt.MapFrom(origin => origin.lastname))
               .ForMember(dest => dest.Gender, opt => opt.MapFrom(origin => origin.cf_785))
               .ForMember(dest => dest.Email, opt => opt.MapFrom(origin => origin.email))
               .ForMember(dest => dest.ImageURL, opt => opt.MapFrom(origin => origin.cf_787))
               .ForMember(dest => dest.GCMId, opt => opt.MapFrom(origin => origin.cf_789))
               .ForMember(dest => dest.UDId, opt => opt.MapFrom(origin => origin.cf_861))
               .ForMember(dest => dest.AvailableFrom, opt => opt.MapFrom(origin => origin.cf_807))
               .ForMember(dest => dest.AvailableTo, opt => opt.MapFrom(origin => origin.cf_809))
               .ForMember(dest => dest.Latitude, opt => opt.MapFrom(origin => origin.cf_825))
               .ForMember(dest => dest.Longitude, opt => opt.MapFrom(origin => origin.cf_827))
               .ForMember(dest => dest.Timezone, opt => opt.MapFrom(origin => origin.cf_793))
               .ForMember(dest => dest.Phone, opt => opt.MapFrom(origin => origin.phone))
               .ForMember(dest => dest.CountryCode, opt => opt.MapFrom(origin => origin.cf_942))
               .ReverseMap()
                   .ForMember(dest => dest.firstname, opt => opt.MapFrom(origin => origin.FirstName))
                    .ForMember(dest => dest.lastname, opt => opt.MapFrom(origin => origin.LastName))
                    .ForMember(dest => dest.cf_785, opt => opt.MapFrom(origin => origin.Gender))
                    .ForMember(dest => dest.email, opt => opt.MapFrom(origin => origin.Email))
                    .ForMember(dest => dest.cf_787, opt => opt.MapFrom(origin => origin.ImageURL))
                    .ForMember(dest => dest.cf_789, opt => opt.MapFrom(origin => origin.GCMId))
                    .ForMember(dest => dest.cf_861, opt => opt.MapFrom(origin => origin.UDId))
                    .ForMember(dest => dest.cf_807, opt => opt.MapFrom(origin => origin.AvailableFrom))
                    .ForMember(dest => dest.cf_809, opt => opt.MapFrom(origin => origin.AvailableTo))
                    .ForMember(dest => dest.cf_825, opt => opt.MapFrom(origin => origin.Latitude))
                    .ForMember(dest => dest.cf_827, opt => opt.MapFrom(origin => origin.Longitude))
                    .ForMember(dest => dest.cf_793, opt => opt.MapFrom(origin => origin.Timezone))
                    .ForMember(dest => dest.phone, opt => opt.MapFrom(origin => origin.Phone))
                    .ForMember(dest => dest.cf_942, opt => opt.MapFrom(origin => origin.CountryCode));

            AutoMapper.Mapper.CreateMap<CRMUserReview, VTigerUserReviews>()
               .ForMember(dest => dest.reviewforuser_tks_contact, opt => opt.MapFrom(origin => origin.ContactId))
               .ForMember(dest => dest.reviewforuser_tks_organisation, opt => opt.MapFrom(origin => origin.OrganisationsId))
               .ForMember(dest => dest.reviewforuser_tks_feedbacktext, opt => opt.MapFrom(origin => origin.FeedbackText))
               .ForMember(dest => dest.reviewforuser_tks_interestname, opt => opt.MapFrom(origin => origin.InterestName))
               .ForMember(dest => dest.reviewforuser_tks_rating, opt => opt.MapFrom(origin => origin.Rating))
               .ForMember(dest => dest.assigned_user_id, opt => opt.MapFrom(origin => origin.Assigned_User_Id))
               .ForMember(dest => dest.reviewforuser_tks_deleted, opt => opt.MapFrom(origin => origin.IsDeleted))
               .ForMember(dest => dest.modifiedtime, opt => opt.MapFrom(origin => origin.ModifiedOn))
               .ForMember(dest => dest.createdtime, opt => opt.MapFrom(origin => origin.CreatedOn))
               .ForMember(dest => dest.reviewforuserno, opt => opt.MapFrom(origin => origin.UserReviewsNo))
                .ReverseMap()
               .ForMember(dest => dest.ContactId, opt => opt.MapFrom(origin => origin.reviewforuser_tks_contact))
               .ForMember(dest => dest.OrganisationsId, opt => opt.MapFrom(origin => origin.reviewforuser_tks_organisation))
               .ForMember(dest => dest.FeedbackText, opt => opt.MapFrom(origin => origin.reviewforuser_tks_feedbacktext))
               .ForMember(dest => dest.InterestName, opt => opt.MapFrom(origin => origin.reviewforuser_tks_interestname))
               .ForMember(dest => dest.Rating, opt => opt.MapFrom(origin => origin.reviewforuser_tks_rating))
               .ForMember(dest => dest.Assigned_User_Id, opt => opt.MapFrom(origin => origin.assigned_user_id))
               .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(origin => origin.reviewforuser_tks_deleted))
               .ForMember(dest => dest.ModifiedOn, opt => opt.MapFrom(origin => origin.modifiedtime))
               .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(origin => origin.createdtime))
               .ForMember(dest => dest.UserReviewsNo, opt => opt.MapFrom(origin => origin.reviewforuserno));

            AutoMapper.Mapper.CreateMap<CRMNotifications, VTigerNotifications>()
               .ForMember(dest => dest.notifications_tks_contact, opt => opt.MapFrom(origin => origin.ContactId))
               .ForMember(dest => dest.notifications_tks_organisation, opt => opt.MapFrom(origin => origin.OrganisationsId))
               .ForMember(dest => dest.notifications_tks_notification, opt => opt.MapFrom(origin => origin.NotificationType))
               .ForMember(dest => dest.assigned_user_id, opt => opt.MapFrom(origin => origin.Assigned_User_Id))
               .ForMember(dest => dest.modifiedtime, opt => opt.MapFrom(origin => origin.ModifiedOn))
               .ForMember(dest => dest.createdtime, opt => opt.MapFrom(origin => origin.CreatedOn))
               .ForMember(dest => dest.notificationsno, opt => opt.MapFrom(origin => origin.NotificationNo))
                .ReverseMap()
               .ForMember(dest => dest.ContactId, opt => opt.MapFrom(origin => origin.notifications_tks_contact))
               .ForMember(dest => dest.OrganisationsId, opt => opt.MapFrom(origin => origin.notifications_tks_organisation))
               .ForMember(dest => dest.NotificationType, opt => opt.MapFrom(origin => origin.notifications_tks_notification))
               .ForMember(dest => dest.Assigned_User_Id, opt => opt.MapFrom(origin => origin.assigned_user_id))
               .ForMember(dest => dest.ModifiedOn, opt => opt.MapFrom(origin => origin.modifiedtime))
               .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(origin => origin.createdtime))
               .ForMember(dest => dest.NotificationNo, opt => opt.MapFrom(origin => origin.notificationsno));

            AutoMapper.Mapper.CreateMap<CRMContactUs, VTigerContactUs>()
               .ForMember(dest => dest.contactus_tks_user, opt => opt.MapFrom(origin => origin.UserId))
               .ForMember(dest => dest.contactus_tks_subject, opt => opt.MapFrom(origin => origin.Subject))
               .ForMember(dest => dest.contactus_tks_description, opt => opt.MapFrom(origin => origin.Description))
               .ForMember(dest => dest.contactus_tks_department, opt => opt.MapFrom(origin => origin.Department))
               .ForMember(dest => dest.assigned_user_id, opt => opt.MapFrom(origin => origin.Assigned_User_Id))
               .ForMember(dest => dest.modifiedtime, opt => opt.MapFrom(origin => origin.ModifiedOn))
               .ForMember(dest => dest.createdtime, opt => opt.MapFrom(origin => origin.CreatedOn))
               .ForMember(dest => dest.contactusno, opt => opt.MapFrom(origin => origin.ContactUsNo))
               .ForMember(dest => dest.cf_966, opt => opt.MapFrom(origin => origin.IsIncomingMessage))
               .ForMember(dest => dest.contactus_tks_isdeleted, opt => opt.MapFrom(origin => origin.IsDeleted))
                .ReverseMap()
               .ForMember(dest => dest.UserId, opt => opt.MapFrom(origin => origin.contactus_tks_user))
               .ForMember(dest => dest.Subject, opt => opt.MapFrom(origin => origin.contactus_tks_subject))
               .ForMember(dest => dest.Description, opt => opt.MapFrom(origin => origin.contactus_tks_description))
               .ForMember(dest => dest.Department, opt => opt.MapFrom(origin => origin.contactus_tks_department))
               .ForMember(dest => dest.Assigned_User_Id, opt => opt.MapFrom(origin => origin.assigned_user_id))
               .ForMember(dest => dest.ModifiedOn, opt => opt.MapFrom(origin => origin.modifiedtime))
               .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(origin => origin.createdtime))
               .ForMember(dest => dest.ContactUsNo, opt => opt.MapFrom(origin => origin.contactusno))
               .ForMember(dest => dest.IsIncomingMessage, opt => opt.MapFrom(origin => origin.cf_966))
               .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(origin => origin.contactus_tks_isdeleted));

            AutoMapper.Mapper.CreateMap<CRMRequestPayment, VTigerRequestPayment>()
              .ForMember(dest => dest.requestpayments_tks_contact, opt => opt.MapFrom(origin => origin.ContactId))
              .ForMember(dest => dest.requestpayments_tks_amount, opt => opt.MapFrom(origin => origin.Amount))
              .ForMember(dest => dest.requestpayments_tks_note, opt => opt.MapFrom(origin => origin.Note))
              .ForMember(dest => dest.requestpayments_tks_isapproved, opt => opt.MapFrom(origin => origin.IsApproved))
              .ForMember(dest => dest.assigned_user_id, opt => opt.MapFrom(origin => origin.Assigned_User_Id))
              .ForMember(dest => dest.modifiedtime, opt => opt.MapFrom(origin => origin.ModifiedOn))
              .ForMember(dest => dest.createdtime, opt => opt.MapFrom(origin => origin.CreatedOn))
              .ForMember(dest => dest.requestpaymentsno, opt => opt.MapFrom(origin => origin.RequestPaymentNo))
               .ReverseMap()
              .ForMember(dest => dest.ContactId, opt => opt.MapFrom(origin => origin.requestpayments_tks_contact))
              .ForMember(dest => dest.Amount, opt => opt.MapFrom(origin => origin.requestpayments_tks_amount))
              .ForMember(dest => dest.Note, opt => opt.MapFrom(origin => origin.requestpayments_tks_note))
              .ForMember(dest => dest.IsApproved, opt => opt.MapFrom(origin => origin.requestpayments_tks_isapproved))
              .ForMember(dest => dest.Assigned_User_Id, opt => opt.MapFrom(origin => origin.assigned_user_id))
              .ForMember(dest => dest.ModifiedOn, opt => opt.MapFrom(origin => origin.modifiedtime))
              .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(origin => origin.createdtime))
              .ForMember(dest => dest.RequestPaymentNo, opt => opt.MapFrom(origin => origin.requestpaymentsno));
        }
    }
}
