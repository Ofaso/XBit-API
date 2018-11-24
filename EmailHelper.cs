using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace XBitApi
{
    public class EmailHelper
    {
        public bool SendMail(string toAddress,string toAddressTitle,string subject,string BodyHTML)
        {
            try
            {
                //From Address
                string FromAddress = "test@mail.com";
                string FromAdressTitle = "Test Mail";
                //To Address
                string ToAddress = toAddress;// "";
                string ToAdressTitle = toAddressTitle;// "";
                string Subject = subject;// "Confirm Registration";
                //string BodyHTML = "";
                //FileStream fileStream = new FileStream(templatePath, FileMode.Open); //"MailTemplate/EmailConfirmation.html"//
                //using (StreamReader reader = new StreamReader(fileStream))
                //{
                //    BodyHTML = reader.ReadToEnd();
                //}

                //BodyHTML = BodyHTML.Replace("@Name@", "Dharmesh");

                //Smtp Server
                string SmtpServer = "Host";
                //Smtp Port Number
                int SmtpPortNumber = 587;

                MimeMessage mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress(FromAdressTitle, FromAddress));
                mimeMessage.To.Add(new MailboxAddress(ToAdressTitle, ToAddress));
                mimeMessage.Subject = Subject;
                mimeMessage.Body = new TextPart("html")
                {
                    Text = BodyHTML
                };

                using (SmtpClient client = new SmtpClient())
                {
                    client.Connect(SmtpServer, SmtpPortNumber, false);
                    client.Authenticate("", "");
                    client.Send(mimeMessage);
                    client.Disconnect(true);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
