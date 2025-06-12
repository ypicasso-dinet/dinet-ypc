namespace Toshi.Backend.Domain.DTO.Common
{
    public class EmailData
    {
        public EmailData()
        {
            destinies = new List<string>();
            copies = new List<string>();
            hiddens = new List<string>();
            files = new List<string>();
        }

        public EmailSettings? settings { get; set; }
        public string? title { get; set; }
        public string? body { get; set; }

        public List<string> destinies { get; set; }
        public List<string> copies { get; set; }
        public List<string> hiddens { get; set; }
        public List<string> files { get; set; }

        public List<string> GetDestinatarios() => destinies ?? [];
        public List<string> GetCopias() => copies ?? [];
        public List<string> GetOcultos() => hiddens ?? [];
        public List<string> GetArchivos() => files ?? [];

        public void SetEmails(List<Destinatario> emails)
        {
            foreach (var email in emails ?? [])
            {
                if (IsValidEmail(email!.correo))
                {
                    if (email.IsTO && !destinies.Contains(email!.correo!)) destinies.Add(email.correo!);
                    if (email.IsCC && !copies.Contains(email!.correo!)) copies.Add(email.correo!);
                    if (email.IsCO && !hiddens.Contains(email!.correo!)) hiddens.Add(email.correo!);
                }
            }
        }

        private bool IsValidEmail(string? me)
        {
            try
            {
                var email = new System.Net.Mail.MailAddress(me ?? "");

                return email != null;
            }
            catch // (Exception ex)
            {
                return false;
            }
        }
    }
}
