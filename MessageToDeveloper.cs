using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;
using Fittings.Model;
using System.Net;

namespace Fittings
{
    public partial class MessageToDeveloper : Form
    {
        /// <summary>
        /// Модель письма разработчику.
        /// </summary>
        private EmailModel EmailToDeveloper { get; set; }

        /// <summary>
        /// Лист прикрепляемых файлов.
        /// </summary>
        private List<string> EmailAttaches { get; set; }

        public MessageToDeveloper()
        {
            if (!String.IsNullOrEmpty(Properties.Settings.Default.Language))
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo(Properties.Settings.Default.Language);
            }

            InitializeComponent();

            txtBobyMail.AcceptsTab = true;
            txtBobyMail.WordWrap = true;
            txtBobyMail.ShortcutsEnabled = true;

            EmailAttaches = new List<string>();

            //txtTitle.Text = "Здесь Вы можете отправить сообщение разработчику.\n Пожалуйста, оставьте свои контактые данные для связи в тексте сообщения.";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            // Проверка на заполненность обязательных полей.
            if (String.IsNullOrEmpty(txtAuthorName.Text) ||
                String.IsNullOrEmpty(txtTitleMessage.Text) ||
                String.IsNullOrEmpty(txtBobyMail.Text))
            {
                MessageBox.Show(ProjectStrings.ErrorOfFillFields,
                    ProjectStrings.ErrorFillDataTitle, 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            InitEmailModel();


            if (MessageBox.Show(ProjectStrings.SendMessageToDeveloperText,
                ProjectStrings.SendMessageToDeveloperTitle, 
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question)==
                DialogResult.Cancel)
            {
                return;
            }

            btnSend.Enabled = false;
            btnCancel.Enabled = false;

            //LoadingForm loading = new LoadingForm("Идет отправка сообщения...\nподождите немного");
            //loading.Owner = this;
            //loading.Show();

            bool flag = SendEmailToDeveloper();

            //loading.Close();
            btnSend.Enabled = true;
            btnCancel.Enabled = true;
            if (flag)
            {
                MessageBox.Show(ProjectStrings.MessageSendedText,
                    ProjectStrings.MessageSendedTitle,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Инициализация модели сообщения.
        /// </summary>
        private void InitEmailModel()
        {
            EmailToDeveloper = new EmailModel()
            {
                AuthorName = txtAuthorName.Text,
                Title = txtTitleMessage.Text,
                Body = txtBobyMail.Text
            };
        }


        /// <summary>
        /// Отправить сообщение разработчику.
        /// </summary>
        private bool SendEmailToDeveloper()
        {
            // отправитель - устанавливаем адрес и отображаемое в письме имя
            MailAddress from = new MailAddress("v.selyakov@yandex.ru", EmailToDeveloper.AuthorName);
            // кому отправляем
            MailAddress to = new MailAddress("v.selyakov@yandex.ru");
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to);
            // тема письма
            m.Subject = EmailToDeveloper.Title;
            // текст письма
            m.Body = EmailToDeveloper.Body;
            // письмо представляет код html
            m.IsBodyHtml = EmailToDeveloper.IsHtmlBody;

            // Добавляем прикрепления.
            foreach (string name in EmailAttaches)
            {
                m.Attachments.Add(new Attachment(name));
            }

            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            SmtpClient smtp = new SmtpClient("smtp.yandex.ru", 587);
            // логин и пароль
            smtp.Credentials = new NetworkCredential("v.selyakov", "telefonMTS321");
            smtp.EnableSsl = true;
            
            try
            {
                smtp.Send(m);
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(String.Format(ProjectStrings.ErrorSendMessageText + "\n{0}", ex.Message),
                    ProjectStrings.ErrorSendMessageTitle,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
        }

        private void btnAddAttach_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = ProjectStrings.SelectFileTitle;
            openFileDialog1.Multiselect = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (string fileName in openFileDialog1.FileNames)
                {
                    EmailAttaches.Add(fileName);
                }
            }

            listAttaches.DataSource = null;
            listAttaches.DataSource = EmailAttaches;
        }

        private void btnRemoveAttach_Click(object sender, EventArgs e)
        {
            if (listAttaches.SelectedItem == null)
            {
                return;
            }

            string selectedAttach = listAttaches.SelectedItem.ToString(); // Получаем выбранный элемент.
            EmailAttaches.Remove(selectedAttach);
            listAttaches.DataSource = null;
            listAttaches.DataSource = EmailAttaches;
        }

        private void btnEditAttach_Click(object sender, EventArgs e)
        {
            if (listAttaches.SelectedItem == null)
            {
                return;
            }

            openFileDialog1.Title = ProjectStrings.SelectFileTitle;
            openFileDialog1.Multiselect = false;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (IsFileIntoList(openFileDialog1.FileName))
                {
                    MessageBox.Show(ProjectStrings.ErrorEditAttachFileText,
                        ProjectStrings.ErrorEditAttachFileTitle, 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string selectedAttach = listAttaches.SelectedItem.ToString(); // Получаем выбранный элемент.
                EmailAttaches.Remove(selectedAttach);

                EmailAttaches.Add(openFileDialog1.FileName);
            }

            listAttaches.DataSource = null;
            listAttaches.DataSource = EmailAttaches;
        }

        /// <summary>
        /// Проверка, что файл уже есть в списке.
        /// </summary>
        /// <returns></returns>
        private bool IsFileIntoList(string fileName)
        {
            if (EmailAttaches.Contains(fileName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
