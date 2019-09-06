using Fittings.Connections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Fittings.ViewForms
{
    public partial class BriefcaseCard : Form
    {
        /// <summary>
        /// Имеются не сохраненные изменения.
        /// </summary>
        private bool IsNotSaveChanges { get; set; } = false;
        
        /// <summary>
        /// Показывает, что карточка открыта для редактирования.
        /// </summary>
        private bool IsEditMode { get; set; }

        /// <summary>
        /// Id добавленного портфеля.
        /// </summary>
        public int IdAddedBriefcase { get; set; }

        /// <summary>
        /// Id редактируемого портфеля.
        /// </summary>
        private int IdBriefcase { get; set; }

        private List<Model.FittingToBriefcaseModel> Accessories { get; set; }
        private List<Model.FittingToBriefcaseModel> AccessoriesToRemove { get; set; }

        public BriefcaseCard()
        {
            new UI.UserCulture().SetCultureOnForm();
            InitializeComponent();

            IdBriefcase = -1;
            IsEditMode = false;
            dateCreation.Value = DateTime.Now;

            Accessories = new List<Model.FittingToBriefcaseModel>();
            AccessoriesToRemove = new List<Model.FittingToBriefcaseModel>();
        }

        public BriefcaseCard(int id, object name, object description, object date)
        {
            new UI.UserCulture().SetCultureOnForm();
            InitializeComponent();

            //FittingID = 0;
            IsEditMode = true;
            IdBriefcase = id;
            txtName.Text = name.ToString();
            txtDescription.Text = description.ToString();
            dateCreation.Value = (DateTime)date;

            LoadFittingToBriefcase(id);

            AccessoriesToRemove = new List<Model.FittingToBriefcaseModel>();
        }

        public BriefcaseCard(int id, object name, object description, object date, bool isReadOnly)
        {
            new UI.UserCulture().SetCultureOnForm();
            InitializeComponent();

            //FittingID = 0;
            IsEditMode = true;
            IdBriefcase = id;
            txtName.Text = name.ToString();
            txtDescription.Text = description.ToString();
            dateCreation.Value = (DateTime)date;

            LoadFittingToBriefcase(id);

            AccessoriesToRemove = new List<Model.FittingToBriefcaseModel>();
            if (isReadOnly == true)
            {
                btnSave.Enabled = false;
                btnAddFitting.Enabled = false;
                btnRemoveFitting.Enabled = false;
                сохранитьToolStripMenuItem.Enabled = false;
                новыйToolStripMenuItem.Enabled = false;
                удалитьToolStripMenuItem.Enabled = false;
                txtDescription.Enabled = false;
                txtName.Enabled = false;
                gridBriefcases.DoubleClick -= GridBriefcases_DoubleClick;
            }
        }

        /// <summary>
        /// Загрузить комплектующие портфеля.
        /// </summary>
        /// <param name="idBriefcase">Id портфеля.</param>
        private void LoadFittingToBriefcase(int idBriefcase)
        {
            Accessories = new List<Model.FittingToBriefcaseModel>();
            string sql = String.Format("exec ShowFittingToBriefcase {0}", idBriefcase);

            using (SqlConnection conn = new SqlConnection(Connection.MainSqlConnection))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds);

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Accessories.Add(new Model.FittingToBriefcaseModel()
                        {
                            Id = int.Parse(row["Id"].ToString()),
                            IdBriefcase = IdBriefcase,
                            IdFitting = int.Parse(row["IDFitting"].ToString()),
                            FittingName = row["Name"].ToString(),
                            FittingCount = double.Parse(row["FittingCount"].ToString())
                        });
                    }
                }
            }

            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = Accessories;
            gridBriefcases.DataSource = bindingSource;

            SetGridControlStyle();

        }

        /// <summary>
        /// Добавить новый портфель.
        /// </summary>
        private int Add(string name, string description, string date)
        {
            //string sql = String.Format("exec AddBriefcase N'{0}', N'{1}', '{2}'",
            //    name,
            //    description,
            //    date);

            string sql = String.Format("exec AddBriefcase N'{0}', N'{1}'",
                name,
                description);

            using (SqlConnection connection = new SqlConnection(Connection.MainSqlConnection))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);

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

        /// <summary>
        /// Редактировать существующий портфель.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        private bool Edit(int id, string name, string description)
        {
            string sql = String.Format("exec EditBriefcase {0}, N'{1}', N'{2}'",
                id,
                name,
                description);
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

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            bool flag = false;

            string name = txtName.Text;
            string description = txtDescription.Text;
            //string date = dateCreation.Value.ToShortDateString();
            int day = dateCreation.Value.Day;
            int month = dateCreation.Value.Month;
            int year = dateCreation.Value.Year;

            string date = String.Format("{0}.{1}.{2}",
                UI.UserCulture.GetDateLikeString(day),
                UI.UserCulture.GetDateLikeString(month),
                year);

            if (String.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show(ProjectStrings.ErrorOfFillFields, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (IsEditMode)
            {
                flag = Edit(IdBriefcase, name, description);
                if (flag)
                {
                    IdAddedBriefcase = IdBriefcase;
                }
            }
            else
            {
                IdAddedBriefcase = Add(name, description, date);
            }

            if (IdAddedBriefcase > 0)
            {
                SaveFittingsToBriefcases();
                IsNotSaveChanges = false;
                DialogResult = DialogResult.OK;
            }
        }

        private void BtnAddFitting_Click(object sender, EventArgs e)
        {
            ViewForms.SelectItemWindow selectItem = new ViewForms.SelectItemWindow();
            selectItem.Owner = this;
            if (selectItem.ShowDialog() == DialogResult.OK)
            {
                int id = selectItem.SelectedFittingID;
                string name = selectItem.SelectedFittingName;

                Accessories.Add(new Model.FittingToBriefcaseModel()
                {
                    IdBriefcase = IdBriefcase,
                    IdFitting = id,
                    FittingName = name,
                    FittingCount = 1,
                    IsAdded = true
                });

                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = Accessories;
                gridBriefcases.DataSource = bindingSource;

                SetGridControlStyle();

                IsNotSaveChanges = true;
            }

            new UI.UserCulture().SetCultureOnForm();
        }

        private void BtnEditFitting_Click(object sender, EventArgs e)
        {

        }

        private void BtnRemoveFitting_Click(object sender, EventArgs e)
        {
            int index = gridView1.FocusedRowHandle;
            if (index < 0)
                return;
            int selectedFittingID = int.Parse(gridView1.GetRowCellValue(index, "IdFitting").ToString());

            AccessoriesToRemove.Add(Accessories[index]);
            Accessories.RemoveAt(index);

            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = Accessories;
            gridBriefcases.DataSource = bindingSource;

            IsNotSaveChanges = true;
        }

        /// <summary>
        /// Сохранить комплектующее у портфеля.
        /// </summary>
        /// <param name="idBriefcase"></param>
        /// <param name="idFitting"></param>
        /// <param name="fittingCount"></param>
        /// <returns></returns>
        public int SaveFittingToBriefcase(int idBriefcase, int idFitting, double fittingCount)
        {
            string sql = String.Format("exec AddFittingToBriefcase {0}, {1}, {2}",
                idBriefcase,
                idFitting,
                fittingCount.ToString().Replace(',','.'));

            using (SqlConnection connection = new SqlConnection(Connection.MainSqlConnection))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);

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

        public void EditFittingToBriefcase(int id, int idBriefcase, int idFitting, double fittingCount)
        {
            string sql = String.Format("exec EditFittingToBriefcase {0}, {1}, {2}, {3}",
                id,
                idBriefcase,
                idFitting,
                fittingCount.ToString().Replace(',', '.'));
            using (SqlConnection connection = new SqlConnection(Connection.MainSqlConnection))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Sql error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void RemoveFittingToBriefcase(int id)
        {
            string sql = String.Format("exec DropFittingToBriefcase {0}",
                id);
            using (SqlConnection connection = new SqlConnection(Connection.MainSqlConnection))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Sql error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Сохранить в БД комплектующие у портфеля.
        /// </summary>
        private void SaveFittingsToBriefcases()
        {
            foreach (Model.FittingToBriefcaseModel model in Accessories)
            {
                if (model.IsAdded == true)
                {
                    int id = SaveFittingToBriefcase(IdAddedBriefcase, model.IdFitting, model.FittingCount);
                    model.IsEdited = false;
                }
            }

            foreach (Model.FittingToBriefcaseModel model in AccessoriesToRemove)
            {
                if (model.Id > 0)
                {
                    RemoveFittingToBriefcase(model.Id);
                }
            }

            foreach (Model.FittingToBriefcaseModel model in Accessories)
            {
                if (model.IsEdited == true)
                {
                    EditFittingToBriefcase(model.Id, IdAddedBriefcase, model.IdFitting, model.FittingCount);
                }
            }
        }

        /// <summary>
        /// Установить стиль GridControl на форме.
        /// </summary>
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
            gridView1.Columns["FittingName"].Caption = "Название";
            gridView1.Columns["FittingCount"].Caption = "Количество";

            
        }

        private void BriefcaseCard_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void GridBriefcases_DoubleClick(object sender, EventArgs e)
        {
            int index = gridView1.FocusedRowHandle;
            if (index < 0)
                return;
            int selectedFittingID = int.Parse(gridView1.GetRowCellValue(index, "IdFitting").ToString());
            double selectedFittingCount = double.Parse(gridView1.GetRowCellValue(index, "FittingCount").ToString());

            Cards.EditFittingCountCard editFittingCountCard = new Cards.EditFittingCountCard(Accessories[index]);
            editFittingCountCard.Owner = this;
            editFittingCountCard.ShowDialog();

            if (editFittingCountCard.DialogResult == DialogResult.OK)
            {
                Accessories[index] = editFittingCountCard.SelectedModel;
                Accessories[index].IsEdited = true;
                IsNotSaveChanges = true;
            }

            new UI.UserCulture().SetCultureOnForm();
        }
    }
}
