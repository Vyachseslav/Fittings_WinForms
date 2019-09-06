using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Fittings.ViewForms
{
    public partial class SearchEquipmentWindow : Form
    {
        /// <summary>
        /// Набор ID выбранных оборудований.
        /// </summary>
        public List<int> SelectedEquipmentID { get; set; }

        public SearchEquipmentWindow()
        {
            new UI.UserCulture().SetCultureOnForm();
            InitializeComponent();

            LoadEquipments();
            SetGridControlStyle();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            UserSelectOK();
        }

        private void MainGrid_DoubleClick(object sender, EventArgs e)
        {
            UserSelectOK();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void UserSelectOK()
        {
            SelectedEquipmentID = new List<int>();
            int[] selectedBriefcases = gridView1.GetSelectedRows(); // Получаем выделенные строки.

            foreach (int row in selectedBriefcases)
            {
                SelectedEquipmentID.Add(int.Parse(gridView1.GetRowCellValue(row, "Id").ToString()));
            }

            DialogResult = DialogResult.OK;
        }

        private void LoadEquipments()
        {
            Model.EquipmentModel equipmentModel = new Model.EquipmentModel();            

            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = equipmentModel.Show();
            mainGrid.DataSource = bindingSource;

            gridView1.Columns["Id"].Visible = false;
            gridView1.Columns["Name"].Caption = "Название";
            gridView1.Columns["Price"].Caption = "Цена за шт.";
            gridView1.Columns["Description"].Caption = "Описание";
        }

        private void SetGridControlStyle()
        {
            foreach (DevExpress.XtraGrid.Columns.GridColumn column in gridView1.Columns)
            {
                column.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                column.AppearanceCell.Options.UseTextOptions = true;
            }

            gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
            gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridView1.OptionsSelection.MultiSelect = false;
        }
    }
}
