// <copyright file="Form.cs" company="Cuyahoga Community College">
// Copyright (c) 2019 Cuyahoga Community College. All rights reserved.
// </copyright>
// <summary>
// The Form model.
// </summary>
// <remarks>
//
// </remarks>
// <source_repository>
// Current Revision : $Revision: 3 $
// Last Check In : $Date: 2019-10-11 16:15:34-04:00 $
// Last Modification: $Modtime: 2019-10-11 15:49:07-04:00 $
// Last Change By : $Author: mwinter $
// $Log: /SoftwareQuiz/SoftwareQuiz/Models/Form.cs $
// 
// Revision: 3   Date: 2019-10-11 20:15:34Z   User: mwinter 
// Stylecop changes 
// </source_repository>
namespace SoftwareQuiz.Models
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Model containing user data.
    /// </summary>
    public class Form 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Form"/> class.
        /// </summary>
        public Form()
        {
            this.InterestQuestions = InterestQuestion.BuildQuestions();
        }

        /// <summary>
        /// Gets or sets a string containing the user's first name.
        /// </summary>
        [Required(ErrorMessage = "Required"), DisplayName("First Name"), MaxLength(100)]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets a string containing the user's last name.
        /// </summary>
        [Required(ErrorMessage = "Required"), DisplayName("Last Name"), MaxLength(100)]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets a string containing the user's email address.
        /// </summary>
        [Required(ErrorMessage = "Required"), FormHelper.Validators.ValidEmail]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets a boolean indicating whether the user is a potential student or not.
        /// </summary>
        public bool? IsPotentialStudent { get; set; }

        // Interests

        /// <summary>
        /// Gets or sets a list containing the interest questions.
        /// </summary>
        public List<InterestQuestion> InterestQuestions { get; set; }

        // Kind of Program

        /// <summary>
        /// Gets or sets a string containing the user's program of interest.
        /// </summary>
        [Required(ErrorMessage = "Required"), DisplayName("In what kind of program are you most interested?")]
        public string ProgramOfInterest { get; set; }

        /// <summary>
        /// Gets or sets a boolean indicating whether the user is switching from a non-tech field to a tech field.
        /// </summary>
        [Required(ErrorMessage = "Required"), DisplayName("I currently work as a technologist and want to switch technology fields.")]
        public bool? SwitchTechFields { get; set; }

        /// <summary>
        /// Gets or sets a string containing the user's education level.
        /// </summary>
        [Required(ErrorMessage = "Required"), DisplayName("What best describes your highest education level?")]
        public string HighestEdLevel { get; set; }

        // How Soon to Start

        /// <summary>
        /// Gets or sets a boolean indicating whether the user would like to begin a technology program now or in the future.
        /// </summary>
        [Required(ErrorMessage = "Required"), DisplayName("I would like to start training right away.")]
        public bool? StartNow { get; set; }
    }
}