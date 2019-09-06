using DevExpress.XtraGrid.Views.Base;
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
    public partial class ProjectCard : Form
    {
        /// <summary>
        /// Показывает, что карточка открыта для редактирования.
        /// </summary>
        private bool IsEditMode { get; set; }

        /// <summary>
        /// Id выбранного проекта. 0 если IsEditMode=false.
        /// </summary>
        private int ProjectID { get; set; }

        /// <summary>
        /// Id нового проекта.
        /// </summary>
        public int IdAddedProject { get; set; }

        public ProjectCard()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            if (!String.IsNullOrEmpty(Properties.Settings.Default.Language))
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo(Properties.Settings.Default.Language);
            }

            InitializeComponent();

            ProjectID = 0;
            IsEditMode = false;
        }

        public ProjectCard(int projectId, string name, string description)
        {

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            if (!String.IsNullOrEmpty(Properties.Settings.Default.Language))
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo(Properties.Settings.Default.Language);
            }

            InitializeComponent();

            ProjectID = projectId;
            IsEditMode = true;

            this.txtName.Text = name;
            this.txtDescription.Text = description;
        }

        private object FitSum { get; set; }
        private object TransportSum { get; set; }
        private object LaborSum { get; set; }
        private object UnexcpectedSum { get; set; }
        private object TotalSum { get; set; }

        public ProjectCard(int projectId, string name, string description, object fitSum, object transSum, object labor, object unexcpect, object total,
            object goods, object dist, object hours, object peopleCount)
        {
            InitializeComponent();

            ProjectID = projectId;
            IsEditMode = true;

            this.txtName.Text = name;
            this.txtDescription.Text = description;

            this.FitSum = fitSum;
            TransportSum = transSum;
            LaborSum = labor;
            UnexcpectedSum = UnexcpectedSum;
            TotalSum = total;

            txtUnlaborExpenses.Text = unexcpect.ToString();
            txtItemCount.Text = goods.ToString();
            txtDistance.Text = dist.ToString();
            txtWorkHours.Text = hours.ToString();
            txtWorkerCount.Text = peopleCount.ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool flag = false;

            if (String.IsNullOrEmpty(txtName.Text))
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
                DialogResult = DialogResult.OK;
        }

        private bool Add()
        {
            Computing.Expenses expenses = new Computing.Expenses();
            double transportExpenses = expenses.TransportExpensesByDistance(int.Parse(txtDistance.Text), int.Parse(txtItemCount.Text));
            double laborExpen = expenses.LaborExpenditures(double.Parse(txtWorkHours.Text), int.Parse(txtWorkerCount.Text));
            double unlaborExpen = double.Parse(txtUnlaborExpenses.Text);
            double total = transportExpenses + laborExpen + unlaborExpen;
            double totalWithPercent = expenses.TotalProjectSumWithPercent(total);

            string sql = String.Format("exec AddProject N'{0}', N'{1}', 0.0, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}",
                txtName.Text,
                txtDescription.Text,
                transportExpenses,
                laborExpen,
                unlaborExpen,
                total,
                txtItemCount.Text,
            txtDistance.Text,
            txtWorkHours.Text,
            txtWorkerCount.Text,
            totalWithPercent);
            using (SqlConnection connection = new SqlConnection(Connection.MainSqlConnection))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);

                try
                {
                    IdAddedProject = int.Parse(command.ExecuteScalar().ToString());
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

        private bool Edit()
        {
            Computing.Expenses expenses = new Computing.Expenses();
            double transportExpenses = expenses.TransportExpensesByDistance(int.Parse(txtDistance.Text), int.Parse(txtItemCount.Text));
            double laborExpen = expenses.LaborExpenditures(double.Parse(txtWorkHours.Text), int.Parse(txtWorkerCount.Text));
            double unlaborExpen = double.Parse(txtUnlaborExpenses.Text);
            double total = transportExpenses + laborExpen + unlaborExpen + double.Parse(FitSum.ToString());
            double totalWithPercent = expenses.TotalProjectSumWithPercent(total);

            string sql = String.Format("exec EditProject {2}, N'{0}', N'{1}', '{3}', '{4}', '{5}', '{6}', '{7}', {8}, {9}, {10}, {11}, {12}",
                txtName.Text,
                txtDescription.Text,
                ProjectID,
                FitSum,
            transportExpenses,
            laborExpen,
            unlaborExpen,
            total,
            txtItemCount.Text,
            txtDistance.Text,
            txtWorkHours.Text,
            txtWorkerCount.Text,
            totalWithPercent);
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
    }
}
