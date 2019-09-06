using Fittings.Connections;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fittings
{
    public partial class ConnectionWindow : Form
    {
        private delegate void someItem();
        private someItem sm;
        private string server;

        public ConnectionWindow()
        {
            if (!String.IsNullOrEmpty(Properties.Settings.Default.Language))
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo(Properties.Settings.Default.Language);
            }

            InitializeComponent();

            backgroundWorker1.DoWork += BackgroundWorker1_DoWork;
            backgroundWorker1.RunWorkerCompleted += BackgroundWorker1_RunWorkerCompleted;
            backgroundWorker1.RunWorkerAsync();
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            Connection.DataBaseName = comboDatabases.Text;
            Connection.SQLServerName = comboServers.Text;
            Connection.IntegratedSecurity = checkInteratedSecurity.Checked;
            Connection.Login = txtLogin.Text;
            Connection.Password = txtPassword.Text;

            Connection.IsConnectionString = checkConnectionString.Checked;
            Connection.ConString = txtConStr.Text;


            Properties.Settings.Default.serverName = comboServers.Text;
            Properties.Settings.Default.databaseName = comboDatabases.Text;
            Properties.Settings.Default.integratedSecurity = checkInteratedSecurity.Checked;
            Properties.Settings.Default.login = txtLogin.Text;
            Properties.Settings.Default.password = txtPassword.Text;
            Properties.Settings.Default.IsConnectionString = checkConnectionString.Checked;
            Properties.Settings.Default.UserConnectionString = txtConStr.Text;
            Properties.Settings.Default.Save();

            this.Close();
        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            checkInteratedSecurity.Checked = Connection.IntegratedSecurity;
            comboServers.SelectedIndex = comboServers.Items.IndexOf(Connection.SQLServerName);
            if (comboServers.SelectedIndex < 0)
            {
                comboServers.Text = Connection.SQLServerName;
            }
            comboDatabases.SelectedIndex = comboDatabases.Items.IndexOf(Connection.DataBaseName);
            if (comboDatabases.SelectedIndex < 0)
            {
                comboDatabases.Text = Connection.DataBaseName;
            }

            loadPicture.Visible = false;

            checkInteratedSecurity.Checked = true;
            if (checkInteratedSecurity.Checked)
            {
                txtLogin.Enabled = false;
                txtPassword.Enabled = false;
            }
            else
            {
                txtLogin.Enabled = true;
                txtPassword.Enabled = true;
            }
            txtLogin.Text = Connection.Login;
            txtPassword.Text = Connection.Password;

            checkConnectionString.Checked = Connection.IsConnectionString;
            if (Connection.IsConnectionString)
            {
                txtConStr.Enabled = true;
                txtConStr.Text = Connection.ConString;
            }
            else
            {
                txtConStr.Enabled = false;
            }
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            LoadServers();
            comboServers.SelectedValueChanged += ComboServers_SelectedValueChanged;
            comboServers.SelectedIndexChanged += ComboServers_SelectedValueChanged;
            comboServers.TextChanged += ComboServers_TextChanged;
        }

        private void ComboServers_TextChanged(object sender, EventArgs e)
        {
            Connection.SQLServerName = comboServers.Text;
            LoadDatabases();
        }

        private void ComboServers_SelectedValueChanged(object sender, EventArgs e)
        {
            Connection.SQLServerName = comboServers.SelectedItem.ToString();
            LoadDatabases();
        }

        private void LoadServers()
        {
            sm = new someItem(AddItem);
            DataTable dt = System.Data.Sql.SqlDataSourceEnumerator.Instance.GetDataSources();

            foreach (DataRow dr in dt.Rows)
            {
                //comboServers.Items.Add(string.Concat(dr["ServerName"], "\\", dr["InstanceName"]));
                server = string.Concat(dr["ServerName"], "\\", dr["InstanceName"]);
                comboServers.Invoke(sm);
            }
        }

        private void AddItem()
        {
            comboServers.Items.Add(server);
        }

        private void LoadDatabases()
        {
            string connectionString = Connection.GetSQLConnection;
            string request = String.Format("EXEC sp_databases");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(request, connection);
                    DataTable ds = new DataTable();
                    adapter.Fill(ds);

                    foreach (DataRow dr in ds.Rows)
                    {
                        comboDatabases.Items.Add(string.Concat(dr["DATABASE_NAME"]));
                    }
                }
                catch (Exception ex)
                {
                    comboServers.Items.Clear();
                    MessageBox.Show(ex.Message, "Error sql", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void checkInteratedSecurity_CheckedChanged(object sender, EventArgs e)
        {
            bool flag = checkInteratedSecurity.Checked;
            txtLogin.Enabled = !flag;
            txtPassword.Enabled = !flag;
            

            //if (flag)
            //{
            //    txtLogin.Enabled = !flag;
            //    txtPassword.Enabled = !flag;
            //}
            //else
            //{
            //    txtLogin.Enabled = true;
            //    txtPassword.Enabled = true;
            //}
        }

        private void CheckConnectionString_CheckedChanged(object sender, EventArgs e)
        {
            bool flag = checkConnectionString.Checked;
            if (flag)
            {
                txtConStr.Text = Connection.ConString;
                txtConStr.Enabled = true;
                comboDatabases.Enabled = false;
                comboServers.Enabled = false;
            }
            else
            {
                //txtConStr.Text = "";
                txtConStr.Enabled = false;
                comboDatabases.Enabled = true;
                comboServers.Enabled = true;
            }
        }
    }
}
