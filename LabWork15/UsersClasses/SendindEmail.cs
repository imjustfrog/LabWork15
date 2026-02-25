using System;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;
using WindowsFormsApp2.UsersClasses;

namespace WindowsFormsApp2.UsersClasses
{
    public class SendingEmail
    {
        private InfoEmailSending InfoEmailSending { get; set; }

        public SendingEmail(InfoEmailSending infoEmailSending)
        {
            InfoEmailSending = infoEmailSending
                ?? throw new ArgumentNullException(nameof(infoEmailSending));
        }

        public void Send()
        {
            try
            {
                using (MailMessage message = new MailMessage())
                {
                    message.From = new MailAddress(
                        InfoEmailSending.EmailAdressFrom.EmailAdress,
                        InfoEmailSending.EmailAdressFrom.Name);

                    message.To.Add(new MailAddress(
                        InfoEmailSending.EmailAdressTo.EmailAdress,
                        InfoEmailSending.EmailAdressTo.Name));

                    message.Subject = InfoEmailSending.Subject;

                    message.Body = InfoEmailSending.Body;

                    using (SmtpClient client = new SmtpClient(InfoEmailSending.SmtpClientAdress))
                    {
                        client.Port = 587; 
                        client.Credentials = new NetworkCredential(
                            InfoEmailSending.EmailAdressFrom.EmailAdress,
                            InfoEmailSending.EmailPassword);
                        client.EnableSsl = true;

                        client.Send(message);
                    }
                }

                MessageBox.Show("Письмо успешно отправлено!", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при отправке: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
