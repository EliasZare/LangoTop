using System;
using System.Net;
using System.Net.Mail;

namespace _0_Framework.Application.Email
{
    public class EmailService : IEmailService
    {
        public string SendEmail(string toAddress, string subject, string body)
        {
            var result = "Message Sent Successfully..!!";
            var senderID = "Contact@langotop.ir";
            const string senderPassword = "y29v4H$u9";
            try
            {
                var smtp = new SmtpClient
                {
                    Host = "langotop.ir",
                    Port = 25,
                    TargetName = "لنگوتاپ | LangoTop",
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(senderID, senderPassword),
                    Timeout = 30000
                };
                var message = new MailMessage(senderID, toAddress, subject, body);
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                result = "Error sending email.!!!";
                var a = ex.Message;
            }

            return result;
        }
    }
}