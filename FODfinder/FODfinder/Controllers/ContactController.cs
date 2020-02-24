using System.Net;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using FODfinder.Models.Contact;
using reCAPTCHA.MVC;

namespace FODfinder.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CaptchaValidator]
        public async Task<ActionResult> Contact(ContactForm contactForm)
        {
            if (ModelState.IsValid)
            {
                var userName = System.Web.Configuration.WebConfigurationManager.AppSettings["senderEmail"];
                var password = System.Web.Configuration.WebConfigurationManager.AppSettings["senderPassword"];
                var body = "<h2>Email from: " +
                    contactForm.Name + " (" +
                    contactForm.Email + ")</h2><hr /><p><big><b>Message:</b>\n" +
                    contactForm.EmailContents + "</big></p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress("thealphabytes@gmail.com"));
                message.From = new MailAddress(userName);
                message.Subject = $"{contactForm.Subject} - {DateTime.Now}";
                message.Body = string.Format(body, contactForm.Name, contactForm.Email, contactForm.EmailContents);
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = userName,
                        Password = password
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    await smtp.SendMailAsync(message);
                    return RedirectToAction("Sent");
                }
            }
            return View(contactForm);
        }

        public ActionResult Sent()
        {
            return View();
        }
    }
}