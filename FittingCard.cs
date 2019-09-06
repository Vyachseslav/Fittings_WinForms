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
    public partial class FittingCard : Form
    {
        /// <summary>
        /// Показывает, что карточка открыта для редактирования.
        /// </summary>
        private bool IsEditMode { get; set; }
        
        /// <summary>
        /// ID проекта.
        /// </summary>
        public int ProjectID { get; set; }

        /// <summary>
        /// Выбранный фиттинг.
        /// </summary>
        private int SelectedFitting { get; set; }

        /// <summary>
        /// Кол-во фиттинга.
        /// </summary>
        private double FittingCount { get; set; }

        /// <summary>
        /// ID свзяки фиттинга и проекта.
        /// </summary>
        private int AccessoriesID { get; set; }

        /// <summary>
        /// Лист фиттингов.
        /// </summary>
        private DataTable FittingList { get; set; }

        /// <summary>
        /// Id довленной записи.
        /// </summary>
        public int IdAddedAccessories { get; set; }

        public FittingCard()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            if (!String.IsNullOrEmpty(Properties.Settings.Default.Language))
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo(Properties.Settings.Default.Language);
            }

            InitializeComponent();

            LoadFittings();

            txtCount.Text = "1.0";
            comboFittings.SelectedIndexChanged += ComboFittings_SelectedIndexChanged;            

            FittingCount = 1.0;
            //ProjectID = 1;

            IsEditMode = false;
            comboFittings.SelectedIndex = 1;
        }

        public FittingCard(int projectId, int idaccessories, int idFitting, double count, bool isEdit = false)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            if (!String.IsNullOrEmpty(Properties.Settings.Default.Language))
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo(Properties.Settings.Default.Language);
            }

            InitializeComponent();

            ProjectID = projectId;
            AccessoriesID = idaccessories;
            FittingCount = count;
            IsEditMode = isEdit;
            SelectedFitting = idFitting;

            LoadFittings();
            
            txtCount.Text = count.ToString();
            comboFittings.SelectedIndexChanged += ComboFittings_SelectedIndexChanged;

            comboFittings.SelectedValue = SelectedFitting;
        }

        private void ComboFittings_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedFitting = int.Parse(comboFittings.SelectedValue.ToString());
        }

        /// <summary>
        /// Загрузка фиттингов.
        /// </summary>
        private void LoadFittings()
        {
            using (SqlConnection conn = new SqlConnection(Connection.MainSqlConnection))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    string commandText = "exec ShowFittings";
                    cmd.CommandText = commandText;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds);

                    FittingList = ds.Tables[0];

                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = ds.Tables[0];
                    comboFittings.DataSource = bindingSource;
                    comboFittings.DisplayMember = "Name";
                    comboFittings.ValueMember = "Id";
                }
            }
        }

        /// <summary>
        /// Получить ID фиттитнга по ID Accessories.
        /// </summary>
        /// <returns></returns>
        //private int GetFittingID(int idAccessories)
        //{
        //    //select FittingID from Accessories where Id=3
        //    using (SQLiteConnection conn = new SQLiteConnection(Connection.MainSqlConnection))
        //    {
        //        conn.Open();
        //        using (SQLiteCommand cmd = conn.CreateCommand())
        //        {
        //            string commandText = String.Format("select FittingID from Accessories where Id={0}",
        //                idAccessories);
        //            cmd.CommandText = commandText;

        //            try
        //            {
        //                return int.Parse(cmd.ExecuteScalar().ToString());
        //            }
        //            catch(SQLiteException ex)
        //            {
        //                MessageBox.Show(ex.Message, "Ошибка sql", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                return -1;
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// Добавление фиттинга к проекту.
        /// </summary>
        private bool AddFitting()
        {
            //double pp = GetFittingSum(double.Parse(txtCount.Text.Replace('.',',')));

            string sql = String.Format("exec AddAccessory {0}, {1}, {2}",
                ProjectID,
                SelectedFitting,
                txtCount.Text);
            using (SqlConnection connection = new SqlConnection(Connection.MainSqlConnection))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);

                try
                {
                    IdAddedAccessories = int.Parse(command.ExecuteScalar().ToString());
                    //command.ExecuteNonQuery();
                    return true;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Error sql", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        /// <summary>
        /// Редактирование фиттинга.
        /// </summary>
        private bool EditFitting()
        {
            string sql = String.Format("exec EditAccessory {0}, {1}, {2}",
                AccessoriesID,
                SelectedFitting,
                txtCount.Text);
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
                    MessageBox.Show(ex.Message, "Error sql", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            //if (String.IsNullOrEmpty(txtName.Text) || String.IsNullOrEmpty(txtPrice.Text))
            //{
            //    MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            if (IsEditMode)
            {
                flag = EditFitting();
            }
            else
            {
                flag = AddFitting();
            }

            if (flag)
            {
                DialogResult = DialogResult.OK;
                if (checkAddNew.Checked)
                    new FittingCard() { ProjectID = ProjectID }.ShowDialog();
            }
        }

        //private void txtCount_TextChanged(object sender, EventArgs e)
        //{
        //    double count = double.Parse(txtCount.Text);
        //    //double priceByOne = double.Parse(comboFittings.SelectedValue.ToString());
        //    FittingList.DefaultView.Sort = "Id";
        //    int index = FittingList.DefaultView.Find(SelectedFitting);
        //    double priceByOne = double.Parse(FittingList.Rows[index]["PriceByOne"].ToString());
        //}

        private double GetFittingSum(double count)
        {
            double result = 0;
            
            //double count = double.Parse(txtCount.Text);
            FittingList.DefaultView.Sort = "Name";
            int index = FittingList.DefaultView.Find(comboFittings.Text);
            double priceByOne = double.Parse(FittingList.Rows[index]["PriceByOne"].ToString());

            result = count * priceByOne;

            return result;
        }

        private void btnShowChooser_Click(object sender, EventArgs e)
        {
            ViewForms.SelectItemWindow selectItem = new ViewForms.SelectItemWindow(SelectedFitting);
            selectItem.Owner = this;
            if (selectItem.ShowDialog() == DialogResult.OK)
            {
                int id = selectItem.SelectedFittingID;
                comboFittings.SelectedValue = id;
            }
        }
    }
}
