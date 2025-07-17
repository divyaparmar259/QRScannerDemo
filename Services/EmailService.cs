using MimeKit;
using MailKit.Net.Smtp;
using System.Net.Mail;


namespace QRScanner.Services
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string name, string email, string message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("QR Connect", _config["EmailSettings:SenderEmail"]));
            emailMessage.To.Add(new MailboxAddress("Receiver", _config["EmailSettings:ReceiverEmail"]));
            emailMessage.Subject = "📬 New Contact Form Submission";

            string htmlBody = $@"
        <html>
            <body>
                <h2>New Contact Form Details</h2>
                <table style='width:100%; border-collapse: collapse;' border='1'>
                    <tr>
                        <th style='padding: 8px;'>Field</th>
                        <th style='padding: 8px;'>Value</th>
                    </tr>
                    <tr>
                        <td style='padding: 8px;'>Name</td>
                        <td style='padding: 8px;'>{name}</td>
                    </tr>
                    <tr>
                        <td style='padding: 8px;'>Email</td>
                        <td style='padding: 8px;'>{email}</td>
                    </tr>
                    <tr>
                        <td style='padding: 8px;'>Message</td>
                        <td style='padding: 8px;'>{message}</td>
                    </tr>
                </table>
                <p style='margin-top: 20px;'>Sent on: {DateTime.Now}</p>
            </body>
        </html>";

            emailMessage.Body = new TextPart("html")
            {
                Text = htmlBody
            };

            using var client = new MailKit.Net.Smtp.SmtpClient();
            await client.ConnectAsync(_config["EmailSettings:SmtpServer"], int.Parse(_config["EmailSettings:Port"]), false);
            await client.AuthenticateAsync(_config["EmailSettings:SenderEmail"], _config["EmailSettings:SenderPassword"]);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }

    }
}
