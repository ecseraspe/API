// ---------------------------------------------------------------------------------------------------
// <copyright file="IOrganisationService.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-19</date>
// <summary>
//     The IOrganisationService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Common.CRMService
{
    using Youffer.CRM;

    /// <summary>
    /// the IOrganisationService interface.
    /// </summary>
    public interface IOrganisationService
    {
        /// <summary>
        ///  Adding Organisation into CRM
        /// </summary>
        /// <param name="organisation"> The Organisation entity. </param>
        /// <returns> VTigerAccount entity </returns>
        VTigerAccount CreateOrganistaion(VTigerAccount organisation);

        /// <summary>
        ///  Retrieving Organisation from CRM
        /// </summary>
        /// <param name="organisationId"> The Organisation id. </param>
        /// <returns> VTigerAccount entity </returns>
        VTigerAccount ReadOrganisation(string organisationId);

        /// <summary>
        ///  Updating Organisation in CRM
        /// </summary>
        /// <param name="organisation"> The Organisation entity. </param>
        /// <returns> VTigerAccount entity </returns>
        VTigerAccount UpdateOrganisation(VTigerAccount organisation);

        /// <summary>
        ///  Deleting Organisation from CRM
        /// </summary>
        /// <param name="organisationId"> The Organisation id. </param>
        /// <returns> bool object </returns>
        bool DeleteOrganisation(string organisationId);
    }
}
