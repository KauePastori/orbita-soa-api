using System.Net.Mail;
using Orbita.SoaApi.Domain.Exceptions;

namespace Orbita.SoaApi.Domain.ValueObjects
{
    public class Email
    {
        public string Address { get; }

        public Email(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                throw new ValidationException("Email não pode ser vazio.");
            try
            {
                var mail = new MailAddress(address);
                Address = mail.Address;
            }
            catch
            {
                throw new ValidationException("Email inválido.");
            }
        }

        public override string ToString() => Address;
    }
}
