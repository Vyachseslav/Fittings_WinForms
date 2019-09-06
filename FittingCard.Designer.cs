namespace Fittings
{
    partial class FittingCard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FittingCard));
            this.label1 = new System.Windows.Forms.Label();
            this.comboFittings = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCount = new System.Windows.Forms.TextBox();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnShowChooser = new DevExpress.XtraEditors.SimpleButton();
            this.checkAddNew = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.checkAddNew.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // comboFittings
            // 
            this.comboFittings.FormattingEnabled = true;
            resources.ApplyResources(this.comboFittings, "comboFittings");
            this.comboFittings.Name = "comboFittings";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txtCount
            // 
            resources.ApplyResources(this.txtCount, "txtCount");
            this.txtCount.Name = "txtCount";
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.ImageOptions.ImageIndex = ((int)(resources.GetObject("btnSave.ImageOptions.ImageIndex")));
            this.btnSave.LookAndFeel.SkinName = "VS2010";
            this.btnSave.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnSave.Name = "btnSave";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.ImageOptions.ImageIndex = ((int)(resources.GetObject("btnCancel.ImageOptions.ImageIndex")));
            this.btnCancel.LookAndFeel.SkinName = "VS2010";
            this.btnCancel.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnShowChooser
            // 
            resources.ApplyResources(this.btnShowChooser, "btnShowChooser");
            this.btnShowChooser.LookAndFeel.SkinName = "VS2010";
            this.btnShowChooser.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnShowChooser.Name = "btnShowChooser";
            this.btnShowChooser.Click += new System.EventHandler(this.btnShowChooser_Click);
            // 
            // checkAddNew
            // 
            resources.ApplyResources(this.checkAddNew, "checkAddNew");
            this.checkAddNew.Name = "checkAddNew";
            this.checkAddNew.Properties.Caption = resources.GetString("checkEdit1.Properties.Caption");
            // 
            // FittingCard
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkAddNew);
            this.Controls.Add(this.btnShowChooser);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboFittings);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FittingCard";
            ((System.ComponentModel.ISupportInitialize)(this.checkAddNew.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboFittings;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCount;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnShowChooser;
        private DevExpress.XtraEditors.CheckEdit checkAddNew;
    }
}