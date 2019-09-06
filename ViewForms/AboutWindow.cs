using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Fittings.ViewForms
{
    public partial class AboutWindow : Form
    {
        public AboutWindow()
        {
            new UI.UserCulture().SetCultureOnForm();
            InitializeComponent();

            lblVersion.Text += UI.Informations.GetFileVersion();
            txtNews.AppendText(GetNews());
        }

        /// <summary>
        /// Получить текст новостей из файла.
        /// </summary>
        private string GetNews()
        {
            //"pack://application:,,,/Fittings;component/Database/News.txt"
            string text = string.Empty;

            using (StreamReader reader = new StreamReader("Database/News.txt"))
            {
                text = reader.ReadToEnd();
            }
            return text;
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
