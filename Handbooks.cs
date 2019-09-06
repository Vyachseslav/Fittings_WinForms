using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTab;

namespace Fittings
{
    public partial class Handbooks : Form
    {
        private GridControl _focusedGrid;

        private SqlMethods MySqlMethods = new SqlMethods();
        
        /// <summary>
        /// Текущий грид в фокусе.
        /// </summary>
        public GridControl FocusedGrid
        {
            get { return _focusedGrid; }
            set
            {
                _focusedGrid = value;
                lblCount.Text = String.Format("Всего записей {0}", _focusedGrid.MainView.RowCount);
            }
        }

        public Handbooks()
        {
            new UI.UserCulture().SetCultureOnForm();
            InitializeComponent();

            LoadFittings();
            LoadBriefcases();
            LoadComponentGroup();
            LoadEquipments();

            SetGridControlStyle(gridView1);
            SetGridControlStyle(gridView2);
            SetGridControlStyle(gridView3);
            SetGridControlStyle(gridView4);

            FocusedGrid = gridFittings;
            gridFittings.GotFocus += GridControl_GotFocus; //GridFittings_GotFocus;
            gridBriefcases.GotFocus += GridControl_GotFocus;// GridBriefcases_GotFocus;
            gridComponentGroup.GotFocus += GridControl_GotFocus; // GridComponentGroup_GotFocus;
            gridEquipments.GotFocus += GridControl_GotFocus;
            gridFittings.Select();

            xtraTabControl1.MouseClick += XtraTabControl1_MouseClick;
        }

        private void GridControl_GotFocus(object sender, EventArgs e)
        {
            FocusedGrid = GetFocusedGrid((sender as GridControl));
        }

        private void XtraTabControl1_MouseClick(object sender, MouseEventArgs e)
        {
            FocusedGrid = GetFocusedGrid((sender as XtraTabControl).SelectedTabPage);
            FocusedGrid.Focus();
        }

        /// <summary>
        /// Загрузить список фиттингов из справочника.
        /// </summary>
        private void LoadFittings()
        {
            DataTable table = MySqlMethods.LoadProjects("exec ShowFittings");

            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = table;
            gridFittings.DataSource = bindingSource;

            gridView1.Columns["Name"].Caption = "Название";
            gridView1.Columns["Price"].Caption = "Цена за шт.";
            gridView1.Columns["ComponentGroup"].Caption = "Название группы комплектующих";
        }

        /// <summary>
        /// Загрузить список портфелей из справочника.
        /// </summary>
        private void LoadBriefcases()
        {
            DataTable table = MySqlMethods.LoadProjects("exec ShowBriefcases");

            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = table;
            gridBriefcases.DataSource = bindingSource;

            gridView2.Columns["Name"].Caption = "Название";
            gridView2.Columns["Description"].Caption = "Описание";
            gridView2.Columns["CreateDateTime"].Caption = "Дата создания";
            gridView2.Columns["CreateDateTime"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            gridView2.Columns["CreateDateTime"].DisplayFormat.FormatString = "dd.MM.yyyy";
        }

        /// <summary>
        /// Загрузить список групп комплектующих.
        /// </summary>
        private void LoadComponentGroup()
        {
            DataTable table = MySqlMethods.LoadProjects("exec ShowComponentGroups");

            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = table;
            gridComponentGroup.DataSource = bindingSource;

            gridView3.Columns["Name"].Caption = "Название";
        }

        /// <summary>
        /// Загрузить оборудование.
        /// </summary>
        private void LoadEquipments()
        {
            DataTable tableEquipments = MySqlMethods.LoadProjects("exec ShowEquipments");
            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = tableEquipments;
            gridEquipments.DataSource = bindingSource;

            try
            {
                gridView4.Columns["Name"].Caption = "Название";
                gridView4.Columns["Price"].Caption = "Цена за шт.";
                gridView4.Columns["Description"].Caption = "Описание";
            }
            catch
            {
                
            }
        }


        private void btnAddFitting_Click(object sender, EventArgs e)
        {
            AddMethod();
        }

        private void btnEditFitting_Click(object sender, EventArgs e)
        {
            EditMethod();
        }

        private void btnRemoveFitting_Click(object sender, EventArgs e)
        {
            DeleteMethods();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshMethods();
        }

        private void menuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void menuExportExcel_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Title = "Экспорт данных ...";
            saveFileDialog1.Filter = "Excel 2007 (*.xlsx)|*.xlsx";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FocusedGrid.ExportToXlsx(saveFileDialog1.FileName);
            }
        }

        private void menuExportPDF_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Title = "Экспорт данных ...";
            saveFileDialog1.Filter = "PDF file (*.pdf)|*.pdf";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FocusedGrid.ExportToPdf(saveFileDialog1.FileName);
            }
        }

        private void menuExportCSV_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Title = "Экспорт данных ...";
            saveFileDialog1.Filter = "csv file (*.csv)|*.csv";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FocusedGrid.ExportToCsv(saveFileDialog1.FileName);
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            ImportWindow importWindow = new ImportWindow();
            importWindow.StartPosition = FormStartPosition.CenterParent;
            importWindow.Owner = this;
            importWindow.ShowDialog();
        }

        private void MyGridControl_DoubleClick(object sender, EventArgs e)
        {
            EditMethod();
        }

        private void AddMethod()
        {
            if (FocusedGrid.Name == "gridFittings")
            {
                OneFitting oneFitting = new OneFitting();
                oneFitting.Owner = this;
                oneFitting.ShowDialog();

                if (oneFitting.DialogResult == DialogResult.OK)
                {
                    int newId = oneFitting.IdAddedFitting;
                    LoadFittings();
                    gridView1.FocusedRowHandle = UI.GridControlUIModifications.GetIndexRowOfItem(newId, gridView1);
                }
            }
            else if (FocusedGrid.Name == "gridBriefcases")
            {
                ViewForms.BriefcaseCard card = new ViewForms.BriefcaseCard();
                card.Owner = this;
                card.ShowDialog();

                if (card.DialogResult == DialogResult.OK)
                {
                    int newId = card.IdAddedBriefcase;
                    LoadBriefcases();
                    gridView2.FocusedRowHandle = UI.GridControlUIModifications.GetIndexRowOfItem(newId, gridView2);
                }
            }
            else if (FocusedGrid.Name == "gridComponentGroup")
            {
                Cards.ComponentGroupCard card = new Cards.ComponentGroupCard();
                card.Owner = this;
                card.ShowDialog();

                if (card.DialogResult == DialogResult.OK)
                {
                    int id = card.IdGroup;
                    LoadComponentGroup();
                    gridView3.FocusedRowHandle = UI.GridControlUIModifications.GetIndexRowOfItem(id, gridView3);
                }
            }
            else if (FocusedGrid.Name == "gridEquipments")
            {
                Cards.EquipmentCard card = new Cards.EquipmentCard();
                card.Owner = this;
                card.ShowDialog();

                if (card.DialogResult == DialogResult.OK)
                {
                    int id = card.EquipmentModel.Id;
                    LoadEquipments();
                    gridView4.FocusedRowHandle = UI.GridControlUIModifications.GetIndexRowOfItem(id, gridView4);
                }
            }
        }

        private void EditMethod()
        {
            GridView focusedView = (FocusedGrid.Views[0] as GridView);
            int index = focusedView.FocusedRowHandle;
            int selectedID = int.Parse(focusedView.GetRowCellValue(index, "Id").ToString());
            Form card = null;

            if (index < 0)
                return;

            if (FocusedGrid.Name == "gridFittings")
            {
                object name = focusedView.GetRowCellValue(index, "Name");
                object price = focusedView.GetRowCellValue(index, "Price");
                object groupName = focusedView.GetRowCellValue(index, "ComponentGroup");

                card = new OneFitting(selectedID, name, price, groupName, true);
            }
            else if (FocusedGrid.Name == "gridBriefcases")
            {
                object name = focusedView.GetRowCellValue(index, "Name");
                object description = focusedView.GetRowCellValue(index, "Description");
                object date = focusedView.GetRowCellValue(index, "CreateDateTime");

                card = new ViewForms.BriefcaseCard(selectedID, name, description, date);
            }
            else if (FocusedGrid.Name == "gridComponentGroup")
            {
                object name = focusedView.GetRowCellValue(index, "Name");

                card = new Cards.ComponentGroupCard(selectedID, name);
            }
            else if (FocusedGrid.Name == "gridEquipments")
            {
                object name = focusedView.GetRowCellValue(index, "Name");
                object description = focusedView.GetRowCellValue(index, "Description");
                object price = focusedView.GetRowCellValue(index, "Price");

                Model.EquipmentModel model = new Model.EquipmentModel()
                {
                    Id = selectedID,
                    Name = name.ToString(),
                    Description = description.ToString(),
                    Price = double.Parse(price.ToString())
                };

                card = new Cards.EquipmentCard(model);
            }

            card.Owner = this;
            if (card.ShowDialog() == DialogResult.OK)
            {
                RefreshMethods();
            }

            focusedView.FocusedRowHandle = UI.GridControlUIModifications.GetIndexRowOfItem(selectedID, focusedView);
        }

        private void DeleteMethods()
        {
            GridView focusedView = (FocusedGrid.Views[0] as GridView);
            int index = focusedView.FocusedRowHandle;
            int selectedID = int.Parse(focusedView.GetRowCellValue(index, "Id").ToString());
            bool flag = false;

            if (index < 0)
                return;

            if (FocusedGrid.Name == "gridFittings")
            {
                if (MessageBox.Show(ProjectStrings.RemoveFittingText, ProjectStrings.RemoveTitle, 
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Question) ==
                    DialogResult.No)
                {
                    return;
                }

                string sql = String.Format("exec DropFitting {0}", selectedID);
                flag = MySqlMethods.Delete(sql);
            }
            else if (FocusedGrid.Name == "gridBriefcases")
            {
                if (MessageBox.Show(ProjectStrings.RemoveBriefcastText,
                    ProjectStrings.RemoveBriefcastTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                    DialogResult.No)
                {
                    return;
                }

                string sql = String.Format("exec DropBriefcase {0}", selectedID);
                flag = MySqlMethods.Delete(sql);
            }
            else if (FocusedGrid.Name == "gridComponentGroup")
            {
                if (MessageBox.Show(ProjectStrings.RemoveComponentGroupText,
                    ProjectStrings.RemoveComponentGroupTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                    DialogResult.No)
                {
                    return;
                }

                string sql = String.Format("exec DropComponentGroup {0}", selectedID);
                flag = MySqlMethods.Delete(sql);
            }
            else if (FocusedGrid.Name == "gridEquipments")
            {
                if (MessageBox.Show(ProjectStrings.RemoveEquipmentText,
                    ProjectStrings.RemoveEquipmentTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                    DialogResult.No)
                {
                    return;
                }

                string sql = String.Format("exec DropEquipment {0}", selectedID);
                flag = MySqlMethods.Delete(sql);
            }

            if (flag)
            {
                RefreshMethods();
                focusedView.FocusedRowHandle = (index > 0) ? (index - 1) : index;
            }
        }

        private void RefreshMethods()
        {
            GridView focusedView = (FocusedGrid.Views[0] as GridView);
            int index = focusedView.FocusedRowHandle;

            if (FocusedGrid.Name == "gridFittings")
            {
                LoadFittings();
            }
            else if (FocusedGrid.Name == "gridBriefcases")
            {
                LoadBriefcases();
            }
            else if (FocusedGrid.Name == "gridComponentGroup")
            {
                LoadComponentGroup();
            }
            else if (FocusedGrid.Name == "gridEquipments")
            {
                LoadEquipments();
            }

            focusedView.FocusedRowHandle = index;
        }

        private void SetGridControlStyle(GridView gridView)
        {
            foreach (DevExpress.XtraGrid.Columns.GridColumn column in gridView.Columns)
            {
                column.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                column.AppearanceCell.Options.UseTextOptions = true;
                if (column.Name.Contains("Id") || column.Name.Contains("ID"))
                {
                    column.Visible = false;
                }
            }
            gridView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
            gridView.OptionsSelection.EnableAppearanceFocusedCell = false;
        }

        private GridControl GetFocusedGrid(XtraTabPage page)
        {
            string tabName = page.Name;
            GridControl grid = null;

            switch (tabName)
            {
                case "xtraTabPage1":
                    grid = gridFittings;
                    break;
                case "xtraTabPage2":
                    grid = gridBriefcases;
                    break;
                case "xtraTabPage3":
                    grid = gridComponentGroup;
                    break;
                case "tabPageEquipments":
                    grid = gridEquipments;
                    break;
                default:
                    grid = gridFittings;
                    break;
            }

            return grid;
        }

        private GridControl GetFocusedGrid(GridControl gridControl)
        {
            string name = gridControl.Name;
            GridControl grid = null;

            switch (name)
            {
                case "gridFittings":
                    grid = gridFittings;
                    break;
                case "gridBriefcases":
                    grid = gridBriefcases;
                    break;
                case "gridComponentGroup":
                    grid = gridComponentGroup;
                    break;
                case "gridEquipments":
                    grid = gridEquipments;
                    break;
                default:
                    grid = gridFittings;
                    break;
            }
            return grid;
        }
    }
}
