using System.Threading.Tasks;
using Quartz;
using SendGrid;
using SendGrid.Helpers.Mail;
using ShowReminder.Web.Models;

namespace ShowReminder.Web.Mapper
{
    public class EmailManager
    {

        private readonly ApplicationConfiguration _applicationConfiguration;

        private readonly SendGridClient _sendGridClient;
        
        public EmailManager(ApplicationConfiguration applicationConfiguration)
        {
            _applicationConfiguration = applicationConfiguration;
            _sendGridClient = new SendGridClient(_applicationConfiguration.SendGridApiKey);
        }
        
        public async Task<Response> SendEmail(string subject, string body)
        {
            var from = new EmailAddress(_applicationConfiguration.FromEmailAddress,
                _applicationConfiguration.FromEmailAddressName);
            var to = new EmailAddress(_applicationConfiguration.ToEmailAddress, 
                _applicationConfiguration.ToEmailAddressName);

            var message = MailHelper.CreateSingleEmail(from, to, subject, null,body);
            return await _sendGridClient.SendEmailAsync(message);
        }
    }
}