// <copyright file="RouteConfig.cs" company="Cuyahoga Community College">
// Copyright (c) 2019 Cuyahoga Community College. All rights reserved.
// </copyright>
// <summary>
// The Route config file.
// </summary>
// <remarks>
//
// </remarks>
// <source_repository>
// Current Revision : $Revision: 2 $
// Last Check In : $Date: 2019-10-11 16:15:34-04:00 $
// Last Modification: $Modtime: 2019-10-11 15:32:38-04:00 $
// Last Change By : $Author: mwinter $
// $Log: /SoftwareQuiz/SoftwareQuiz/App_Start/RouteConfig.cs $
// 
// Revision: 2   Date: 2019-10-11 20:15:34Z   User: mwinter 
// Stylecop changes 
// </source_repository>
namespace SoftwareQuiz
{
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// Registers route patterns.
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// Registers the routes in the RouteCollection.
        /// </summary>
        /// <param name="routes">routes instance</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}
