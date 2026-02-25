using System;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using WindowsFormsApp2.UsersClasses;

namespace WindowsFormsApp2
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            textBoxEmail.Text = "task_code_development@list.ru";
            textBoxName.Text = "Антон Игоревич";
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxEmail.Text) ||
          string.IsNullOrWhiteSpace(textBoxName.Text) ||
          string.IsNullOrWhiteSpace(textBoxSubject.Text) ||
          string.IsNullOrWhiteSpace(textBoxBody.Text))
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            string smtp = "smtp.mail.ru";

            StringPair fromInfo = new StringPair("zelibobik@mail.ru", "Исаков Михаил");

            string password = "K9xLm4QpT7vNs2ZrHc8";

            StringPair toInfo = new StringPair(textBoxEmail.Text, textBoxName.Text);
            string subject = textBoxSubject.Text;

            string body = $"{DateTime.Now} \n" +
                          $"@{Dns.GetHostName()} \n" +
                          $"@{Dns.GetHostAddresses(Dns.GetHostName()).First()} \n" +
                          $"@{textBoxBody.Text}";

            InfoEmailSending info = new InfoEmailSending(smtp, fromInfo, password, toInfo, subject, body);

            SendingEmail sendingEmail = new SendingEmail(info);
            sendingEmail.Send();

            MessageBox.Show("Письмо отправлено!");

            foreach (TextBox textBox in Controls.OfType<TextBox>())
                textBox.Text = "";
        }
    }
}
