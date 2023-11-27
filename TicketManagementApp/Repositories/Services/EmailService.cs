using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketManagementApp.Repositories.Services
{
    public class EmailService : IMessageRepository
    {
        private string message {  get; set; }
        private string subject { get; set; }
        private string recivierEmatilAddress { get; set; }
        private const string senderEmailAddress = "ticketing@kavehlogistic.com";
        private const string senderName = "Ticket app";

        #region Constructor
        public EmailService(string subject, string message, string recivier)
        {
            this.subject = subject;
            this.message = message;
            this.recivierEmatilAddress = recivier;
             
        }
        #endregion
        #region Implementation
        public void Send()
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(senderName, senderEmailAddress));
            email.To.Add(new MailboxAddress("admin", recivierEmatilAddress));
            email.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
            {
                Text = message
            };
            using (var smtp = new SmtpClient())
            {
                smtp.Connect("smtp.kavehlogistics.com", 587, false);

                // Note: only needed if the SMTP server requires authentication
                smtp.Authenticate("ticketing@kavehlogistics.com", "Km$1340");

                smtp.Send(email);
                smtp.Disconnect(true);
            }

        }
        #endregion

    }
}