// ---------------------------------------------------------------------------------------------------
// <copyright file="HomeController.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-14</date>
// <summary>
//     The HomeController class
// </summary>
// ---------------------------------------------------------------------------------------------------
 
namespace Youffer.API.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using System.Web.Mvc;
    using MySql.Data.MySqlClient;
    using Youffer.Common.DataService;
    using Youffer.DataService;
    using Youffer.Resources.MySqlDbSchema;

    /// <summary>
    /// The HomeController controller
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>Index page</returns>
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            ////using (MySqlContext context = new MySqlContext())
            ////{
            ////    IRepository<vtiger_contactdetails> tmp = new Repository<vtiger_contactdetails>(context);
            ////    var tt = tmp.GetAll().ToList();
            ////    object[] sqlCol = { new MySqlParameter("@OrganisationId", 614) };

            ////    var att = tmp.SqlQuery<VTigerDashBoardData>("CALL GetDashboardData({0});", 614).ToList();
            ////    Debug.Write(tt);
            ////}

            return this.View();
        }
    }
}