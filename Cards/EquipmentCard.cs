using System;
using System.Windows.Forms;

namespace Fittings.Cards
{
    public partial class EquipmentCard : Form
    {
        private bool IsEditMode { get; set; } = false;

        /// <summary>
        /// Id добавленного оборудования.
        /// </summary>
        private int IdEquipment { get; set; }

        public Model.EquipmentModel EquipmentModel { get; set; }

        public EquipmentCard()
        {
            new UI.UserCulture().SetCultureOnForm();
            InitializeComponent();

            EquipmentModel = new Model.EquipmentModel();
            IsEditMode = false;
            txtPrice.Text = "0.0";
        }

        public EquipmentCard(Model.EquipmentModel equipmentModel)
        {
            new UI.UserCulture().SetCultureOnForm();
            InitializeComponent();

            EquipmentModel = equipmentModel;

            txtName.Text = EquipmentModel.Name;
            txtDescription.Text = EquipmentModel.Description;
            txtPrice.Text = EquipmentModel.Price.ToString();

            IsEditMode = true;
        }

        private void TxtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            int length = txtName.Text.Length;
            if (length >= 250)
            {
                MessageBox.Show("Длина данного поля не должна превышать 250 символов!",
                    "Ошибка ввода",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void TxtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && number != 46)
            {
                e.Handled = true;
                MessageBox.Show("В данном поле разрешается вводить только числа и точку (разделитель дробной части)!", 
                    "Ошибка ввода", 
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            bool flag = false;

            if (String.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show(ProjectStrings.ErrorOfFillFields, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            EquipmentModel.Name = txtName.Text;
            EquipmentModel.Description = txtDescription.Text;
            EquipmentModel.Price = double.Parse(txtPrice.Text);

            if (!IsEditMode) // Добавление.
            {
                EquipmentModel.Id = EquipmentModel.Add();
            }
            else
            {
                EquipmentModel.Id = EquipmentModel.Edit();
            }

            if (EquipmentModel.Id > 0)
            {
                flag = true;
            }

            if (flag)
            {
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
