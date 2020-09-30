// <copyright file="HomeController.cs" company="Cuyahoga Community College">
// Copyright (c) 2019 Cuyahoga Community College. All rights reserved.
// </copyright>
// <summary>
// This application builds a quiz and questionnaire, calculates the quiz score, generates an email response, and write to Kweb.
// </summary>
// <remarks>
//
// </remarks>
// <source_repository>
// Current Revision : $Revision: 5 $
// Last Check In : $Date: 2019-10-11 16:24:31-04:00 $
// Last Modification: $Modtime: 2019-10-11 16:24:28-04:00 $
// Last Change By : $Author: mwinter $
// $Log: /SoftwareQuiz/SoftwareQuiz/Controllers/HomeController.cs $
// 
// Revision: 5   Date: 2019-10-11 20:24:31Z   User: mwinter 
// remove antiforgerytoken 
// 
// Revision: 4   Date: 2019-10-11 20:15:34Z   User: mwinter 
// Stylecop changes 
// </source_repository>
namespace SoftwareQuiz.Controllers
{
    using System;
    using System.Web.Mvc;
    using Data;
    using FormHelper;
    using Models;
    using Recaptcha.Web;
    using Recaptcha.Web.Mvc;

    /// <summary>
    /// Collects and processes data, and displays either the error or thanks page.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Action method to display the Index view.
        /// </summary>
        /// <returns>Form model to the Index view.</returns>
        public ActionResult Index()
        {
            return this.View(new Form());
        }

        /// <summary>
        /// Validates and processes the Form model.
        /// </summary>
        /// <param name="data">User input from Form model.</param>
        /// <returns>Return the thanks page if successful and error page if unsuccessful.</returns>
        [HttpPost]
        public ActionResult Index(Form data)
        {
            RecaptchaVerificationHelper recaptchaHelper = this.GetRecaptchaVerificationHelper();

            if (string.IsNullOrEmpty(recaptchaHelper.Response))
            {
                this.ModelState.AddModelError(string.Empty, "Captcha answer cannot be empty.");

                return this.View(data);
            }

            RecaptchaVerificationResult recaptchaResult = recaptchaHelper.VerifyRecaptchaResponse();

            if (recaptchaResult != RecaptchaVerificationResult.Success)
            {
                this.ModelState.AddModelError(string.Empty, "Incorrect captcha answer.");

                return this.View(data);
            }

            FormValidator validator = new FormValidator();
            validator.Validate(data);

            if (!this.ModelState.IsValid)
            {
                return this.View(data);
            }

            string emailBody = string.Empty;

            // If at least half of the interest questions were "Agreed" or "Strongly Agreed".
            if (this.CalculateScore(data) >= 6)
            {
                data.IsPotentialStudent = true;

                emailBody = "Results to Software Development Quiz<br/><br/>";
                emailBody += "Congratulations! It sounds like training for a career in software development could be a good fit for you. We offer a few options that may work well for you.<br/><br/>";
                emailBody += "DEGREES (TWO YEARS)<br/>";
                emailBody += $"<a href=\"https://www.tri-c.edu/programs/information-technology/programming-and-development/programming-development-at-tri-c.html{Properties.Settings.Default.TrackingQueryString}\">Programming and Development</a><br/><br/>";
                emailBody += "SHORT-TERM AND POST DEGREE CERTIFICATES (JUST OVER A YEAR)<br/>";
                emailBody += $"<a href=\"https://www.tri-c.edu/programs/information-technology/programming-and-development/certificate-in-mobile-application-development.html{Properties.Settings.Default.TrackingQueryString}\">Mobile Application Development</a><br />";
                emailBody += $"<a href=\"https://www.tri-c.edu/programs/information-technology/programming-and-development/certificate-in-web-application-development.html{Properties.Settings.Default.TrackingQueryString}\">Web Application Development</a><br/>";
                emailBody += $"<a href=\"https://www.tri-c.edu/programs/information-technology/programming-and-development/net-programming.html{Properties.Settings.Default.TrackingQueryString}\">.NET Programming</a><br/>";
                emailBody += $"<a href=\"https://www.tri-c.edu/programs/information-technology/programming-and-development/post-degree-certificate-in-programming-and-development.html{Properties.Settings.Default.TrackingQueryString}\">Programming and Development</a><br/><br/>";
                emailBody += "FAST TRACK TRAINING (3-6 MONTHS)<br/>";
                emailBody += $"<a href=\"https://www.tri-c.edu/programs/information-technology/programming-and-development/clevelandcodes.html{Properties.Settings.Default.TrackingQueryString}\">Cleveland Codes Software Developers Academy</a><br/><br/>";

                if (Convert.ToBoolean(data.StartNow))
                {
                    emailBody += "One of our staff will be reaching out to you within 48 hours to discuss program options and enrollment procedures.";
                }
                else
                {
                    emailBody += "You indicated that you are not quite ready to begin a training program. We will keep your name on our email list and share program information about upcoming courses.<br/><br/>";
                    emailBody += "If you don’t want future training opportunities emailed to you, please <a href=\"mailto:ITCOE@tri-c.edu?subject=%20Unsubscribe%20from%20email%20list\">unsubscribe</a>.";
                }
            }
            else
            {
                data.IsPotentialStudent = false;

                emailBody = "Results to Software Development Quiz<br/><br/>";
                emailBody += "Based on the interests that you self-reported, it sounds like a career in software development might not be the best fit for you. Tri-C offers many different programs so you can find one that’s the right fit for you.<br/><br/>";
                emailBody += "If you like computers, take our <a href=\"https://forms.tri-c.edu/NetworkingQuiz\">computer networking quiz</a> to see if this area is a better match with your interests.<br/><br/>";
                emailBody += "We also offer programs that include technology applications as part of the coursework, such as business technology and entrepreneurial technology. In addition, the Gill and Tommy LiPuma Center for Creative Arts offers 3D design, digital video editing, game design and motion graphics, to name a few.<br /><br />";
                emailBody += $"<a href=\"https://www.tri-c.edu/programs/{Properties.Settings.Default.TrackingQueryString}\">View All Available Programs</a>";
            }
                
            EmailProcess ep = new EmailProcess(Properties.Settings.Default.EmailHost, Properties.Settings.Default.EmailPort, Properties.Settings.Default.EmailFrom);

            if (!ep.SendEmail(data.Email, "Tri-C Software Development Quiz Confirmation", emailBody))
            {
                return this.RedirectToAction("Error");
            }

            ListProcess lp = new ListProcess(
                Properties.Settings.Default.Site,
                Properties.Settings.Default.List,
                AppDBConnection.KwebConnection);

            if (!lp.Add(data))
            {
                return this.RedirectToAction("Error");
            }

            return this.RedirectToAction("Thanks");
        }

        /// <summary>
        /// Action method to display the Thanks view.
        /// </summary>
        /// <returns>The Thanks view.</returns>
        public ActionResult Thanks()
        {
            return this.View();
        }

        /// <summary>
        /// Action method to display the Error view.
        /// </summary>
        /// <returns>The Error view.</returns>
        public ActionResult Error()
        {
            return this.View();
        }

        /// <summary>
        /// Calculates the number of interest questions the user agreed with.
        /// </summary>
        /// <param name="form">User input from Form model.</param>
        /// <returns>The number of interest questions the user agreed with.</returns>
        public int CalculateScore(Form form)
        {
            int score = 0;

            foreach (var question in form.InterestQuestions)
            {
                if (question.Answer.Contains("Agree"))
                {
                    score++;
                }
            }

            return score;
        }
    }
}