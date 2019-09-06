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
    public partial class OneFitting : Form
    {
        /// <summary>
        /// Показывает, что карточка открыта для редактирования.
        /// </summary>
        private bool IsEditMode { get; set; }

        /// <summary>
        /// Id выбранного фиттинга. 0 если IsEditMode=false.
        /// </summary>
        private int FittingID { get; set; }

        /// <summary>
        /// ID добавленного фиттинга.
        /// </summary>
        public int IdAddedFitting { get; set; }

        /// <summary>
        /// Название выбранной группы комплектующих.
        /// </summary>
        public string SelectedGroup { get; set; }

        public OneFitting()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            if (!String.IsNullOrEmpty(Properties.Settings.Default.Language))
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo(Properties.Settings.Default.Language);
            }

            InitializeComponent();

            FittingID = 0;
            IsEditMode = false;

            LoadGroups();
            comboGroups.SelectedValueChanged += ComboGroups_SelectedValueChanged;
        }

        public OneFitting(int fittingID, object name, object price, object groupName, bool isEdit = false)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            if (!String.IsNullOrEmpty(Properties.Settings.Default.Language))
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo(Properties.Settings.Default.Language);
            }

            InitializeComponent();

            FittingID = fittingID;
            IsEditMode = isEdit;

            txtName.Text = name.ToString();
            txtPrice.Text = price.ToString();

            LoadGroups();
            comboGroups.SelectedValueChanged += ComboGroups_SelectedValueChanged;
            comboGroups.SelectedValue = groupName;
        }

        /// <summary>
        /// Добавить новый фиттинг.
        /// </summary>
        private bool Add()
        {
            string sql = string.Empty;
            if (String.IsNullOrEmpty(SelectedGroup))
            {
                sql = String.Format("exec AddFitting N'{0}', {1}, NULL",
                txtName.Text,
                txtPrice.Text);
            }
            else
            {
                sql = String.Format("exec AddFitting N'{0}', {1}, N'{2}'",
                txtName.Text,
                txtPrice.Text,
                SelectedGroup);
            }

            
            using (SqlConnection connection = new SqlConnection(Connection.MainSqlConnection))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                
                try
                {
                    IdAddedFitting = int.Parse(command.ExecuteScalar().ToString());
                    return true;
                }
                catch(SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Sql error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        /// <summary>
        /// Изменить фиттинг.
        /// </summary>
        private bool Edit()
        {
            string sql = string.Empty;
            if (String.IsNullOrEmpty(SelectedGroup))
            {
                sql = String.Format("exec EditFitting {2}, N'{0}', {1}, NULL",
                txtName.Text,
                txtPrice.Text,
                FittingID);
            }
            else
            {
                sql = String.Format("exec EditFitting {2}, N'{0}', {1}, N'{3}'",
                txtName.Text,
                txtPrice.Text,
                FittingID,
                SelectedGroup);
            }

            
            using (SqlConnection connection = new SqlConnection(Connection.MainSqlConnection))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);

                try
                {
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Sql error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool flag = false;

            if (String.IsNullOrEmpty(txtName.Text) || String.IsNullOrEmpty(txtPrice.Text))
            {
                MessageBox.Show(ProjectStrings.ErrorOfFillFields, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (IsEditMode)
            {
                flag = Edit();
            }
            else
            {
                flag = Add();
            }

            if (flag)
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void LoadGroups()
        {
            //ShowComponentGroups
            DataTable groups = new SqlMethods().LoadProjects("exec ShowComponentGroups");
            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = groups;
            comboGroups.DataSource = bindingSource;
            comboGroups.DisplayMember = "Name";
            comboGroups.ValueMember = "Name";

            comboGroups.SelectedIndex = -1;
            comboGroups.Text = "выберите группу ...";
        }

        private void ComboGroups_SelectedValueChanged(object sender, EventArgs e)
        {
            SelectedGroup = comboGroups.SelectedValue.ToString();
        }

        private void BtnClearCombobox_Click(object sender, EventArgs e)
        {
            comboGroups.SelectedValueChanged -= ComboGroups_SelectedValueChanged;
            comboGroups.SelectedIndex = -1;
            comboGroups.Text = "выберите группу ...";
            SelectedGroup = string.Empty;
            comboGroups.SelectedValueChanged += ComboGroups_SelectedValueChanged;
        }
    }
}
