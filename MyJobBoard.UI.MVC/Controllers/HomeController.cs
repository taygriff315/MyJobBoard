using MyJobBoard.UI.MVC.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using System.Configuration;

namespace MyJobBoard.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.testValue = "scroll";
            return View();
        }

        [HttpGet]
        public ActionResult UserProfile()
        {
            return View();
        }

  

        [HttpGet]
        [Authorize]
        public ActionResult About()
        {
            

            return View();
        }

        public ActionResult EmailConfirmation()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            

            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactViewModel cvm)
        {
           

            //First, check to see that the incoming ContactViewModel object meets validation requirements:
            if (!ModelState.IsValid)
            {
                //If the incoming object does not meet validation return the create view populated with the data the user just entered
                return View(cvm);
            }

            string message = $"You have recieved an email from {cvm.Name} with a subject of " +
                $"{cvm.Subject}. Respond to {cvm.EmailAddress}. Message: <br/>{cvm.Message}";

            MailMessage mm = new MailMessage("admin@tpgcode.com", "titleist315@gmail.com", cvm.Subject, message);

            //IsBodyHtml determines if the rendered message should be in HTML
            mm.IsBodyHtml = true;

            //Optional MailPriority that determines priority level
            //mm.Priority = MailPriority.High;

            //ReplyToList() updates the reply goto in the email to go to the end user instead of you admin account
            mm.ReplyToList.Add(cvm.EmailAddress);

            //Send the email:
            SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["EmailClient"].ToString());
            client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["EmailUser"].ToString(), ConfigurationManager.AppSettings["EmailPass"].ToString());
            

          

            //Attempt to send the email but handle if it cant be sent.
            try
            {
                client.Send(mm);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Your message could not be sent at this time. Please try again later." +
                    $"<br/> Error message: <br/>{ex.Message}.";
                return View(cvm);
            }

            //Everything went well lets sendthe user to a confirmation view
            return View("EmailConfirmation", cvm);
        }

    }
}
