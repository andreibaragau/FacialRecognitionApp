using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Facial_Recognition_App
{
    public partial class Form3 : Form
    {
        NetworkCredential login;
        SmtpClient client;
        MailMessage msg;

        public Form3()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            login = new NetworkCredential(txtUser.Text, txtPass.Text);
            client = new SmtpClient(txtSmtp.Text);
            client.Port = Convert.ToInt32(txtPort.Text);
            client.EnableSsl = chkSSL.Checked;
            client.Credentials = login;
            msg = new MailMessage { From = new MailAddress(txtUser.Text + txtSmtp.Text.Replace("smtp.", "@"), "Andrei Baragau", Encoding.UTF8) };
            msg.To.Add(new MailAddress(txtTo.Text));
            if (!string.IsNullOrEmpty(txtCC.Text))
                msg.To.Add(new MailAddress(txtCC.Text));

            if (!string.IsNullOrEmpty(txtAttachament.Text))
                msg.Attachments.Add(new Attachment(txtAttachament.Text));

            msg.Subject = txtSubject.Text;
            msg.Body = txtMessage.Text;
            msg.BodyEncoding = Encoding.UTF8;
            msg.IsBodyHtml = true;
            msg.Priority = MailPriority.Normal;
            msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);

            string userState = "Sending...";
            client.SendAsync(msg, userState);
        }

        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            //14-21
            if (e.Cancelled)
                MessageBox.Show("Nu s-a trimis mesajul!");
            if (e.Error != null)
                MessageBox.Show("eroare!"+e.Error);
            else
                MessageBox.Show("Mesaj trimis!");
        }

        private void btnAttach_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string picPath = openFileDialog.FileName.ToString();
                txtAttachament.Text = picPath;
            }
        }

    }
}
