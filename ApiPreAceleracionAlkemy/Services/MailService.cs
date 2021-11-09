using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiPreAceleracionAlkemy.Entities;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace ApiPreAceleracionAlkemy.Services
{
    public class MailService : IMailService
    {
        private readonly ISendGridClient _sendGridClient;

        public MailService(ISendGridClient sendGridClient)
        {
            _sendGridClient = sendGridClient;
        }
        public async Task SendEmail(User user)
        {
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("santidota2012@gmail.com", "Pre-aceleracion Santiago Moreno"),
                Subject = "Se ha registrado correctamente.",
                PlainTextContent = $"Se ha creado el usuario {user.UserName} correcatamente."
            };

            msg.AddTo(new EmailAddress(user.Email, "Nuevo Usuario"));

            await _sendGridClient.SendEmailAsync(msg);
        }
    }

}
