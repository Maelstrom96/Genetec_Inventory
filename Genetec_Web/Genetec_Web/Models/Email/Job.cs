using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;

namespace Genetec_Web.Models.Email
{
    public class Job
    {
        private bool DoNothing = false;

        public Job()
        {
            Timer sendEmailTimer = new Timer(20 * 60 * 1000); // Verify each 20 minutes
            sendEmailTimer.Elapsed += new ElapsedEventHandler(VerifyTime);
            sendEmailTimer.Enabled = true;
        }

        private void VerifyTime(object sender, ElapsedEventArgs e)
        {
            TimeSpan currentTS = DateTime.Now.TimeOfDay;
            TimeSpan sendTime = TimeSpan.Parse(Properties.Settings.Default.Email_Time);
            double diff = currentTS.TotalMinutes - sendTime.TotalMinutes;

            if (diff > 0 && diff < 30 && !DoNothing)
            {
                SendEmails();
                DoNothing = false;
            }
            else
            {
                DoNothing = true;
            }
        }

        private void SendEmails()
        {
            /*
            MailMessage mail = new MailMessage("you@yourcompany.com", "user@hotmail.com");
            SmtpClient client = new SmtpClient();
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = "smtp.google.com";
            mail.Subject = "this is a test email.";
            mail.Body = "this is my test email body";
            client.Send(mail);
            */
        }
    }
}