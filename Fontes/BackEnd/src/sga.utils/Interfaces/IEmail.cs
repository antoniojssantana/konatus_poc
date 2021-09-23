using sga.utils.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace sga.utils.Interfaces
{
    public interface IEmailSender
    {
        string PrepareEmail(string _pathHtml, List<FieldEmail> fieldsEmail);

        Task SendEmailAsync(string subject, string recipientName,
                string recipientEmail, string body);
    }
}