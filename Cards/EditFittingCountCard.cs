using System;
using System.Windows.Forms;

namespace Fittings.Cards
{
    public partial class EditFittingCountCard : Form
    {
        /// <summary>
        /// Модель фиттинга, входящего в состав портфеля.
        /// </summary>
        public Model.FittingToBriefcaseModel SelectedModel { get; set; }

        public EditFittingCountCard()
        {
            new UI.UserCulture().SetCultureOnForm();

            InitializeComponent();
        }

        public EditFittingCountCard(Model.FittingToBriefcaseModel model)
        {
            new UI.UserCulture().SetCultureOnForm();

            InitializeComponent();

            SelectedModel = model;
            txtCount.Text = SelectedModel.FittingCount.ToString();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            SelectedModel.FittingCount = double.Parse(txtCount.Text);
            DialogResult = DialogResult.OK;
        }
    }
}
