using System;
using System.Windows.Forms;

namespace Fittings.Cards
{
    public partial class ProjectEquipmentCard : Form
    {
        private bool IsEditMode { get; set; }

        public Model.ProjectEquipmentModel ProjectEquipmentModel { get; set; }

        public ProjectEquipmentCard(object idProject)
        {
            new UI.UserCulture().SetCultureOnForm();
            InitializeComponent();
            IsEditMode = false;

            ProjectEquipmentModel = new Model.ProjectEquipmentModel();
            ProjectEquipmentModel.ProjectID = (int)idProject;
            LoadEquipments();

            comboEquipmetns.SelectedValueChanged += ComboEquipmetns_SelectedValueChanged;
        }

        public ProjectEquipmentCard(object id, object idProject, object idEquipment, object percent)
        {
            new UI.UserCulture().SetCultureOnForm();
            InitializeComponent();
            IsEditMode = true;

            ProjectEquipmentModel = new Model.ProjectEquipmentModel();
            ProjectEquipmentModel.Id = (int)id;
            ProjectEquipmentModel.ProjectID = (int)idProject;
            ProjectEquipmentModel.EquipmentID = (int)idEquipment;
            ProjectEquipmentModel.EquipmetnUsingPercent = (double)percent;
            ProjectEquipmentModel.GetDataByEquipmentID();

            LoadEquipments();
            comboEquipmetns.SelectedValue = ProjectEquipmentModel.EquipmentID;
            txtUsePercent.Text = percent.ToString();

            comboEquipmetns.SelectedValueChanged += ComboEquipmetns_SelectedValueChanged;
        }

        private void ComboEquipmetns_SelectedValueChanged(object sender, EventArgs e)
        {
            ProjectEquipmentModel.EquipmentID = int.Parse(comboEquipmetns.SelectedValue.ToString());
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            bool flag = false;

            if (String.IsNullOrEmpty(txtUsePercent.Text) || (ProjectEquipmentModel.EquipmentID < 1))
            {
                MessageBox.Show(ProjectStrings.ErrorOfFillFields, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ProjectEquipmentModel.EquipmetnUsingPercent = double.Parse(txtUsePercent.Text);
            ProjectEquipmentModel.GetDataByEquipmentID();

            if (!IsEditMode)
            {
                ProjectEquipmentModel.Id = ProjectEquipmentModel.Add();
            }
            else
            {
                ProjectEquipmentModel.Edit();
            }

            if (ProjectEquipmentModel.Id > 0)
            {
                flag = true;
            }

            if (flag)
                this.DialogResult = DialogResult.OK;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void BtnSearchEquipmetns_Click(object sender, EventArgs e)
        {
            ViewForms.SearchEquipmentWindow window = new ViewForms.SearchEquipmentWindow();
            window.Owner = this;
            if (window.ShowDialog() == DialogResult.OK)
            {
                int id = window.SelectedEquipmentID[0];
                comboEquipmetns.SelectedValue = id;
            }
        }

        private void TxtUsePercent_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && number != 46)
            { 
                e.Handled = true;
                MessageBox.Show("В данном поле разрешается вводить только числа и точку (разделить дробной части)!",
                    "Ошибка ввода",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void LoadEquipments()
        {
            //DataTable groups = new SqlMethods().LoadProjects("exec ShowComponentGroups");
            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = ProjectEquipmentModel.EquipmentModels;
            comboEquipmetns.DataSource = bindingSource;
            comboEquipmetns.DisplayMember = "Name";
            comboEquipmetns.ValueMember = "Id";

            comboEquipmetns.SelectedIndex = -1;
            comboEquipmetns.Text = "выберите оборудование ...";
        }
    }
}
