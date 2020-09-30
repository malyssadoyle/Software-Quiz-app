// <copyright file="ListProcess.cs" company="Cuyahoga Community College">
// Copyright (c) 2019 Cuyahoga Community College. All rights reserved.
// </copyright>
// <summary>
// The ListProcess class.
// </summary>
// <remarks>
//
// </remarks>
// <source_repository>
// Current Revision : $Revision: 3 $
// Last Check In : $Date: 2019-10-11 16:15:34-04:00 $
// Last Modification: $Modtime: 2019-10-11 16:11:36-04:00 $
// Last Change By : $Author: mwinter $
// $Log: /SoftwareQuiz/SoftwareQuiz/Models/ListProcess.cs $
// 
// Revision: 3   Date: 2019-10-11 20:15:34Z   User: mwinter 
// Stylecop changes 
// </source_repository>

namespace SoftwareQuiz.Models
{
    using System;
    using System.Linq;

    using CCC.KWeb.Gateway.DataAccess;
    using NLog;

    /// <summary>
    /// Writes Form model data to a Kweb list.
    /// </summary>
    public class ListProcess
    {
        /// <summary>
        /// The log file. 
        /// </summary>
        private readonly Logger log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The Kweb site location.
        /// </summary>
        private readonly string site;

        /// <summary>
        /// The Kweb list name.
        /// </summary>
        private readonly string listName;

        /// <summary>
        /// The Kweb connection string.
        /// </summary>
        private readonly string gatewayDbConnStr;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListProcess"/> class.
        /// </summary>
        /// <param name="site">The Kweb site location.</param>
        /// <param name="listName">The Kweb list name.</param>
        /// <param name="gatewayDbConnStr">The Kweb connection string.</param>
        public ListProcess(string site, string listName, string gatewayDbConnStr)
        {
            this.site = site ?? throw new ArgumentNullException(nameof(site));
            this.listName = listName ?? throw new ArgumentNullException(nameof(listName));
            this.gatewayDbConnStr = gatewayDbConnStr ?? throw new ArgumentNullException(nameof(gatewayDbConnStr));
        }

        /// <summary>
        /// Adds fields to the Kweb list.
        /// </summary>
        /// <param name="data">User input from Form model.</param>
        /// <returns>Returns either successful or unsuccessful depending on whether the data writes to Kweb.</returns>
        public bool Add(Form data)
        {
            var fields = new Fields
            {
                // Personal Information
                new Field("Title", data.LastName + ", " + data.FirstName),
                new Field("FirstName", data.FirstName),
                new Field("LastName", data.LastName),
                new Field("Email", data.Email),
                new Field("IsPotentialStudent", Convert.ToBoolean(data.IsPotentialStudent) ? "Yes" : "No"),

                // Kind of Program
                new Field("ProgramOfInterest", data.ProgramOfInterest),
                new Field("SwitchTechFields", Convert.ToBoolean(data.SwitchTechFields) ? "Yes" : "No"),
                new Field("HighestEdLevel", data.HighestEdLevel),

                // How Soon to Start
                new Field("StartNow", Convert.ToBoolean(data.StartNow) ? "Yes" : "No"),
            };

            // Interests
            foreach (var question in data.InterestQuestions)
            {
                fields.Add(new Field(question.Name, question.Answer));
            }

            var formRecord = new FormRecord
            {
                SiteUrl = this.site,
                ListName = this.listName,
                Fields = fields
            };

            try
            {
                IKWebGateway gateway = new KWebGateway(this.gatewayDbConnStr);
                gateway.AddItem(formRecord);
                return true;
            }
            catch (Exception ex)
            {
                this.log.Error(ex, "AddItem failed, site: {0}, list: {1}, fields: {2}", this.site, this.listName, string.Join(", ", fields.Select(m => m.Name + ":" + m.Value).ToArray()));
                return false;
            }
        }
    }
}