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
    public partial class LoadingForm : Form
    {
        public LoadingForm()
        {
            InitializeComponent();

            txtTitle.Text = "Пожалуйста, подождите ...";
        }

        public LoadingForm(string title)
        {
            InitializeComponent();

            txtTitle.Text = title;
        }
    }
}
