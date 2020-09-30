// <copyright file="AppDBConnection.cs" company="Cuyahoga Community College">
// Copyright (c) 2019 Cuyahoga Community College. All rights reserved.
// </copyright>
// <summary>
// The AppDBConnection class.
// </summary>
// <remarks>
//
// </remarks>
// <source_repository>
// Current Revision : $Revision: 2 $
// Last Check In : $Date: 2019-10-11 16:15:34-04:00 $
// Last Modification: $Modtime: 2019-10-11 15:41:29-04:00 $
// Last Change By : $Author: mwinter $
// $Log: /SoftwareQuiz/SoftwareQuiz/Data/AppDBConnection.cs $
// 
// Revision: 2   Date: 2019-10-11 20:15:34Z   User: mwinter 
// Stylecop changes 
// </source_repository>
namespace SoftwareQuiz.Data
{
    using System;

    /// <summary>
    /// Calls the DbConnection web service to get connection strings.
    /// </summary>
    public class AppDBConnection
    {
        /// <summary>
        /// Gets a unique identifier containing the Application Id.
        /// </summary>
        private static readonly Guid AppGuid = Properties.Settings.Default.ApplicationGUID;

        /// <summary>
        /// Gets a string containing the Kweb connection.
        /// </summary>
        public static string KwebConnection { get; private set; }

        /// <summary>
        /// Initializes the connections.
        /// </summary>
        public static void InitializeConnections()
        {
            KwebConnection = string.Empty;

            KwebConnection = DbConnection.Connection.GetSingleConnectionString(AppGuid);
        }
    }
}