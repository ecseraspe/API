// ---------------------------------------------------------------------------------------------------
// <copyright file="HelpPageAreaRegistration.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-14</date>
// <summary>
//     The HelpPageAreaRegistration class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.API.Areas.HelpPage
{
    using System.Web.Http;
    using System.Web.Mvc;

    using Youffer.API.Areas.HelpPage.App_Start;

    /// <summary>
    /// The help page area registration.
    /// </summary>
    public class HelpPageAreaRegistration : AreaRegistration
    {
        #region Public Properties

        /// <summary>
        /// Gets the area name.
        /// </summary>
        public override string AreaName
        {
            get
            {
                return "HelpPage";
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The register area.
        /// </summary>
        /// <param name="context">The context.</param>
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "HelpPage_Default", 
                "Help/{action}/{apiId}", 
                new { controller = "Help", action = "Index", apiId = UrlParameter.Optional });

            HelpPageConfig.Register(GlobalConfiguration.Configuration);
        }

        #endregion
    }
}