// <copyright file="FilterConfig.cs" company="Cuyahoga Community College">
// Copyright (c) 2019 Cuyahoga Community College. All rights reserved.
// </copyright>
// <summary>
// The Filter config file.
// </summary>
// <remarks>
//
// </remarks>
// <source_repository>
// Current Revision : $Revision: 2 $
// Last Check In : $Date: 2019-10-11 16:15:34-04:00 $
// Last Modification: $Modtime: 2019-10-11 15:32:39-04:00 $
// Last Change By : $Author: mwinter $
// $Log: /SoftwareQuiz/SoftwareQuiz/App_Start/FilterConfig.cs $
// 
// Revision: 2   Date: 2019-10-11 20:15:34Z   User: mwinter 
// Stylecop changes 
// </source_repository>
namespace SoftwareQuiz
{
    using System.Web.Mvc;

    /// <summary>
    /// Registers global MVC filters.
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// Renders error page when an unhandled exception occurs.
        /// </summary>
        /// <param name="filters">filter instance</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
