// ---------------------------------------------------------------------------------------------------
// <copyright file="OrganisationService.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-19</date>
// <summary>
//     The OrganisationService class
// </summary>
// ---------------------------------------------------------------------------------------------------
 
namespace Youffer.Framework.Data.CRMService
{
    using System;
    using System.Linq;
    using Youffer.Common.CRMService;
    using Youffer.Common.LogService;
    using Youffer.CRM;

    /// <summary>
    /// The OrganisationService class.
    /// </summary>
    public class OrganisationService : IOrganisationService
    {
        /// <summary>
        /// VTiger service instance
        /// </summary>
        private readonly IVTigerService vTigerService;

        /// <summary>
        ///  Initializes a new instance of the <see cref="OrganisationService" /> class.
        /// </summary>
        /// <param name="vTigerService">the vtiger service</param>
        /// <param name="loggerService">the logger service</param>
        public OrganisationService(IVTigerService vTigerService, ILoggerService loggerService)
        {
            this.vTigerService = vTigerService;
            this.LoggerService = loggerService;
        }

        /// <summary>
        /// Gets the logger service.
        /// </summary>        
        protected ILoggerService LoggerService { get; private set; }

        /// <summary>
        ///  Adding Organisation into CRM
        /// </summary>
        /// <param name="organisation"> The Organisation entity. </param>
        /// <returns> VTigerAccount entity </returns>
        public VTigerAccount CreateOrganistaion(VTigerAccount organisation)
        {
            try
            {
                organisation = this.vTigerService.Create<VTigerAccount>(organisation);
                organisation.cf_1024 = organisation.cf_1024 != null ? organisation.cf_1024[0].Split(new string[] { " |##| " }, StringSplitOptions.None).Where(x => !string.IsNullOrEmpty(x)).ToArray() : new string[] { };
                organisation.cf_777 = organisation.cf_777 != null ? organisation.cf_777[0].Split(new string[] { " |##| " }, StringSplitOptions.None).Where(x => !string.IsNullOrEmpty(x)).ToArray() : new string[] { };
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Adding Organisation :- " + ex.Message);
                organisation = new VTigerAccount();
            }

            return organisation;
        }

        /// <summary>
        ///  Retrieving Organisation from CRM
        /// </summary>
        /// <param name="organisationId"> The Organisation id. </param>
        /// <returns> VTigerAccount entity </returns>
        public VTigerAccount ReadOrganisation(string organisationId)
        {
            VTigerAccount organisation = new VTigerAccount();
            try
            {
                organisation = this.vTigerService.Retrieve<VTigerAccount>(organisationId);
                organisation.cf_1024 = organisation.cf_1024 != null ? organisation.cf_1024[0].Split(new string[] { " |##| " }, StringSplitOptions.None).Where(x => !string.IsNullOrEmpty(x)).ToArray() : new string[] { };
                organisation.cf_777 = organisation.cf_777 != null ? organisation.cf_777[0].Split(new string[] { " |##| " }, StringSplitOptions.None).Where(x => !string.IsNullOrEmpty(x)).ToArray() : new string[] { };
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Retrieving Organisation :- " + ex.Message);
            }

            return organisation;
        }

        /// <summary>
        ///  Updating Organisation in CRM
        /// </summary>
        /// <param name="organisation"> The Organisation entity. </param>
        /// <returns> VTigerAccount entity </returns>
        public VTigerAccount UpdateOrganisation(VTigerAccount organisation)
        {
            try
            {
                organisation = this.vTigerService.Update<VTigerAccount>(organisation);
                organisation.cf_1024 = organisation.cf_1024 != null ? organisation.cf_1024[0].Split(new string[] { " |##| " }, StringSplitOptions.None).Where(x => !string.IsNullOrEmpty(x)).ToArray() : new string[] { };
                organisation.cf_777 = organisation.cf_777 != null ? organisation.cf_777[0].Split(new string[] { " |##| " }, StringSplitOptions.None).Where(x => !string.IsNullOrEmpty(x)).ToArray() : new string[] { };
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Updating Organisation :- " + ex.Message);
                return null;
            }

            return organisation;
        }

        /// <summary>
        ///  Deleting Organisation from CRM
        /// </summary>
        /// <param name="organisationId"> The Organisation id. </param>
        /// <returns> bool object </returns>
        public bool DeleteOrganisation(string organisationId)
        {
            try
            {
                this.vTigerService.Delete(organisationId);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Deleteing Organisation :- " + ex.Message);
                return false;
            }

            return true;
        }
    }
}
