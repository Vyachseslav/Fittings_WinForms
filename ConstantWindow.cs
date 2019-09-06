using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fittings
{
    public partial class ConstantWindow : Form
    {
        /// <summary>
        /// Набор констант пользователя.
        /// </summary>
        private Constants UserConstants { get; set; }

        public ConstantWindow()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            InitializeComponent();

            UserConstants = new Constants();

            txtFuelCost.Text = UserConstants.GasolineCost.ToString();
            txtAverageFuel.Text = UserConstants.AverageFuelConsumption.ToString();
            txtWorkCost.Text = UserConstants.HourWorkCost.ToString();
            txtPercent.Text = UserConstants.PercentOfProject.ToString();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            UserConstants.GasolineCost = double.Parse(txtFuelCost.Text);
            UserConstants.AverageFuelConsumption = double.Parse(txtAverageFuel.Text);
            UserConstants.HourWorkCost = double.Parse(txtWorkCost.Text);
            UserConstants.PercentOfProject = double.Parse(txtPercent.Text);

            UserConstants.SaveConstants();

            this.Close();
        }
    }
}
