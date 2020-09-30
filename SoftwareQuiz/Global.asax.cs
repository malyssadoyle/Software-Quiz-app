// <copyright file="Global.asax.cs" company="Cuyahoga Community College">
// Copyright (c) 2019 Cuyahoga Community College. All rights reserved.
// </copyright>
// <summary>
// The Global.asax class.
// </summary>
// <remarks>
//
// </remarks>
// <source_repository>
// Current Revision : $Revision: 3 $
// Last Check In : $Date: 2019-10-11 16:15:34-04:00 $
// Last Modification: $Modtime: 2019-10-11 16:12:37-04:00 $
// Last Change By : $Author: mwinter $
// $Log: /SoftwareQuiz/SoftwareQuiz/Global.asax.cs $
// 
// Revision: 3   Date: 2019-10-11 20:15:34Z   User: mwinter 
// Stylecop changes 
// </source_repository>
namespace SoftwareQuiz
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using SoftwareQuiz.Data;

    /// <summary>
    /// Handles events.
    /// </summary>
    public class MvcApplication : HttpApplication
    {
        /// <summary>
        /// Loads and initializes the data.
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AppDBConnection.InitializeConnections();
        }
    }
}
