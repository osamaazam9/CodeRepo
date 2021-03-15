using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SO.Utility.Helpers
{
    public class EmailHelper
    {
        private static bool sentSuccessfully = true; 
        public static bool SendEmail(MailAddress to, string subject, string body)
        {
            
            MailMessage message = new MailMessage();
            message.To.Add(to);
            message.Subject = subject;
            message.Body = body;
            SmtpClient client = new SmtpClient();
            client.Timeout = 3000;

            try
            {
                client.Send(message);
            }
            catch (Exception err)
            {
                System.Diagnostics.Debug.WriteLine(err.ToString());
                sentSuccessfully = false; 
            }
            finally
            {
                client.Dispose();
                message.Dispose();
            }
            return sentSuccessfully;
        }
    }
}
