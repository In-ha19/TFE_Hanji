using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace VotreProjet.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Configuration du client SMTP pour se connecter à Papercut
            var smtpClient = new SmtpClient(_configuration["EmailSettings:SmtpServer"])
            {
                Port = int.Parse(_configuration["EmailSettings:SmtpPort"]),  // Port 25 pour Papercut SMTP
                Credentials = new NetworkCredential(
                    _configuration["EmailSettings:SenderEmail"],
                    _configuration["EmailSettings:SenderPassword"]),  
                EnableSsl = false  
            };

            // Création du message email
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_configuration["EmailSettings:SenderEmail"], _configuration["EmailSettings:SenderName"]),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true,  // E-mail en format HTML
            };
            mailMessage.To.Add(email);

            // Envoi de l'e-mail (capturé par Papercut)
            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
