using System.Net;
using System.Net.Mail;

namespace MvcCoreUtilidades.Helpers
{
    public class HelperMails
    {
        private IConfiguration config;
        
        public HelperMails(IConfiguration config)
        {
            this.config = config;
        }

        private MailMessage ConfigureMailMessage
            (string para, string asunto, string mensaje)
        {
            MailMessage mail = new MailMessage();
            //esta clase neesita indicar from, es decir,
            //desde que correo se envian los mails
            //tenemos que reuperar los settings

            string user =
                this.config.GetValue<string>("MailSettings:Credentials:User");
            mail.From = new MailAddress(user);

            //si fuesen varios se hace un bucle con un split para separar con comas
            mail.To.Add(para);
            mail.Subject = asunto;
            mail.Body = mensaje;
            mail.Priority = MailPriority.Normal;

            return mail;
        }

        private SmtpClient ConfigureSmtpClient()
        {
            string user =
                this.config.GetValue<string>("MailSettings:Credentials:User");

            //configuramos nuestro smtp server con config
            string password =
                this.config.GetValue<string>("MailSettings:Credentials:Password");
            string hostname =
                this.config.GetValue<string>("MailSettings:ServerSmtp:Host");
            int port =
                this.config.GetValue<int>("MailSettings:ServerSmtp:Port");
            bool enableSSL =
                this.config.GetValue<bool>("MailSettings:ServerSmtp:EnableSsl");
            bool defaultCredentials =
                this.config.GetValue<bool>("MailSettings:ServerSmtp:DefaultCredentials");

            //creamos el servidor smtp para enviar los mails
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = hostname;
            smtpClient.Port = port;
            smtpClient.EnableSsl = enableSSL;
            smtpClient.UseDefaultCredentials = defaultCredentials;

            //creamos las credenciales de red para enviar el mail
            NetworkCredential credentials =
                new NetworkCredential(user, password);
            smtpClient.Credentials = credentials;

            return smtpClient;
        }

        public async Task SendMailAsync(string para, string asunto, string mensaje)
        {
            MailMessage mail = this.ConfigureMailMessage(para, asunto, mensaje);

            SmtpClient smtpClient = this.ConfigureSmtpClient();

            await smtpClient.SendMailAsync(mail);

        }

        public async Task SendMailAsync(string para, string asunto, string mensaje, string path)
        {
            MailMessage mail = this.ConfigureMailMessage(para, asunto, mensaje);

            // creamos un adjunto
            Attachment attachment = new Attachment(path);
            mail.Attachments.Add(attachment);

            SmtpClient smtpClient = this.ConfigureSmtpClient();

            await smtpClient.SendMailAsync(mail);

        }
    }
}


/*
            
                
                
                   
*/