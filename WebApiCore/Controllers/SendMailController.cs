using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace WebApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendMailController : ControllerBase
    {

        //[HttpPost]
        ////public async Task<IActionResult> MailSend(Email email)
        //public async Task<IActionResult> MailSend()
        //{
        //var client = new System.Net.Mail.SmtpClient("smtp.gmail.com", 465);//587 
        //client.UseDefaultCredentials = false;
        //client.EnableSsl = true;

        //client.Credentials = new System.Net.NetworkCredential("project.harendra@gmail.com", "albadya@2019");

        //var mailMessage = new System.Net.Mail.MailMessage();
        //mailMessage.From = new System.Net.Mail.MailAddress("project.harendra@gmail.com");

        //mailMessage.To.Add(email.To);

        //if (!string.IsNullOrEmpty(email.Cc))
        //{
        //    mailMessage.CC.Add(email.Cc);
        //}

        //mailMessage.Body = email.Text;

        //mailMessage.Subject = email.Subject;

        //mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
        //mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;

        //await client.SendMailAsync(mailMessage);

        //    return Ok();
        //}
        [HttpPost]
        public void Post([FromBody] Email email)
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("project.harendra@gmail.com");
                mail.To.Add("project.harendra@gmail.com");
                mail.Subject = "Hello World";
                mail.Body = "<h1>Hello</h1>";
                mail.IsBodyHtml = true;
                //mail.Attachments.Add(new Attachment("C:\\file.zip"));

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("project.harendra@gmail.com", "albadya@2019");
                    smtp.UseDefaultCredentials = false;
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }


           
            //var client = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);//465 ,587 
            //client.UseDefaultCredentials = false;
            //client.EnableSsl = true;

            //client.Credentials = new System.Net.NetworkCredential("project.harendra@gmail.com", "albadya@2019");

            //var mailMessage = new System.Net.Mail.MailMessage();
            //mailMessage.From = new System.Net.Mail.MailAddress("project.harendra@gmail.com");

            //mailMessage.To.Add(email.To);

            //if (!string.IsNullOrEmpty(email.Cc))
            //{
            //    mailMessage.CC.Add(email.Cc);
            //}
            ////mailMessage.IsBodyHtml = true;
            ////mailMessage.Body = "<html><head><body>Hi</body></head><html>";
           
            //mailMessage.Body = email.Text;

            //mailMessage.Subject = email.Subject;

            //mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            //mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;

            //client.Send(mailMessage);

            //return Ok();
        }

        public class Email
        {
            public string To { get; set; }
            public string Cc { get; set; }
            public string Subject { get; set; }
            public string Text { get; set; }
        }
    }
}
