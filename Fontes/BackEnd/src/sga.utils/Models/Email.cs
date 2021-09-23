using System.ComponentModel.DataAnnotations;

namespace sga.utils.Models
{
    public class EmailModel
    {
        [Required, Display(Name = "Email de destino"), EmailAddress]
        public string RecipientEmail { get; set; }

        [Required, Display(Name = "Assunto")]
        public string Subject { get; set; }

        [Required, Display(Name = "Mensagem")]
        public string Body { get; set; }
    }
}