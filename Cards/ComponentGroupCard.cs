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

namespace Fittings.Cards
{
    public partial class ComponentGroupCard : Form
    {
        private bool IsEditMode { get; set; } = false;

        /// <summary>
        /// Id добавленной группы.
        /// </summary>
        public int IdGroup { get; set; }

        public Model.ComponentGroupModel ComponentGroupModel { get; set; }

        public ComponentGroupCard()
        {
            new UI.UserCulture().SetCultureOnForm();
            InitializeComponent();
            IsEditMode = false;

            ComponentGroupModel = new Model.ComponentGroupModel();

            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = ComponentGroupModel.Components;
            gridComponents.DataSource = bindingSource;

            SetGridControlStyle();
        }

        public ComponentGroupCard(object id, object name)
        {
            new UI.UserCulture().SetCultureOnForm();
            InitializeComponent();
            IsEditMode = true;

            ComponentGroupModel = new Model.ComponentGroupModel(int.Parse(id.ToString()), name.ToString());

            txtName.Text = ComponentGroupModel.Name;

            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = ComponentGroupModel.Components;
            gridComponents.DataSource = bindingSource;

            SetGridControlStyle();
        }

        private void SimpleButton1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show(ProjectStrings.ErrorOfFillFields, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ComponentGroupModel.Name = txtName.Text;

            if (!IsEditMode)
            {
                IdGroup = AddGroup(txtName.Text);                
            }
            else
            {
                IdGroup = EditGroup(ComponentGroupModel.Id, ComponentGroupModel.Name);
            }


            if (IdGroup > 0)
                DialogResult = DialogResult.OK;
        }

        private void SimpleButton2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        public int AddGroup(string name)
        {
            string sqlExpression = String.Format("exec AddComponentGroup N'{0}'", name);

            using (SqlConnection connection = new SqlConnection(Connection.MainSqlConnection))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                try
                {
                    int id = int.Parse(command.ExecuteScalar().ToString());
                    return id;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Sql error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
            }
        }

        public int EditGroup(int id, string name)
        {
            string sqlExpression = String.Format("exec EditComponentGroup {0}, N'{1}'", id, name);

            using (SqlConnection connection = new SqlConnection(Connection.MainSqlConnection))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                try
                {
                    command.ExecuteNonQuery();
                    return id;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Sql error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
            }
        }

        private void SetGridControlStyle()
        {
            // Выравнивание содержимого по левому краю.
            foreach (DevExpress.XtraGrid.Columns.GridColumn column in gridView1.Columns)
            {
                column.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                column.AppearanceCell.Options.UseTextOptions = true;
            }

            /// Скрыть ID.
            foreach (DevExpress.XtraGrid.Columns.GridColumn column in gridView1.Columns)
            {
                if (column.Name.Contains("Id") || column.Name.Contains("ID") || column.Name.Contains("Is"))
                {
                    column.Visible = false;
                }
            }

            /// Устанавливаем выделение всей строки целиком.
            gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
            gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;

            // Заголовки столбцов.
            gridView1.Columns["Name"].Caption = "Название";
            gridView1.Columns["Price"].Caption = "Цена за шт.";
            //gridView1.Columns["Price"].Visible = false;
        }
    }
}
