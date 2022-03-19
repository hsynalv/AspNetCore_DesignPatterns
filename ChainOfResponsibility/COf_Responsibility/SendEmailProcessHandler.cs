using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace ChainOfResponsibility.COf_Responsibility
{
    public class SendEmailProcessHandler : ProcessHandler
    {
        private readonly string _fileName;
        private readonly string _toEmail;

        public SendEmailProcessHandler(string fileName, string toEmail)
        {
            _fileName = fileName;
            _toEmail = toEmail;
        }

        public override object Handler(object obj)
        {
            var zipMemoryStream = obj as MemoryStream;
            zipMemoryStream.Position = 0;
            var mailMessage = new MailMessage();

            var smptClient = new SmtpClient("smtp.google.com");

            mailMessage.From = new MailAddress("beyazskorsky@gmail.com");

            mailMessage.To.Add(new MailAddress(_toEmail));

            mailMessage.Subject = "Zip dosyası";

            mailMessage.Body = "<p>Zip dosyası ektedir.</p>";

            Attachment attachment = new Attachment(zipMemoryStream, _fileName, MediaTypeNames.Application.Zip);

            mailMessage.Attachments.Add(attachment);

            mailMessage.IsBodyHtml = true;
            smptClient.Port = 587;
            smptClient.Credentials = new NetworkCredential("alavhasan72892@gmail.com", "---------");

            smptClient.Send(mailMessage);

            return base.Handler(null);
        }
    }
}
