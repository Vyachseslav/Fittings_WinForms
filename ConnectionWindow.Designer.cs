namespace Fittings
{
    partial class ConnectionWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectionWindow));
            this.label1 = new System.Windows.Forms.Label();
            this.comboServers = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboDatabases = new System.Windows.Forms.ComboBox();
            this.checkInteratedSecurity = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnApply = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.loadPicture = new System.Windows.Forms.PictureBox();
            this.checkConnectionString = new System.Windows.Forms.CheckBox();
            this.txtConStr = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.loadPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // comboServers
            // 
            resources.ApplyResources(this.comboServers, "comboServers");
            this.comboServers.FormattingEnabled = true;
            this.comboServers.Name = "comboServers";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // comboDatabases
            // 
            resources.ApplyResources(this.comboDatabases, "comboDatabases");
            this.comboDatabases.FormattingEnabled = true;
            this.comboDatabases.Name = "comboDatabases";
            // 
            // checkInteratedSecurity
            // 
            resources.ApplyResources(this.checkInteratedSecurity, "checkInteratedSecurity");
            this.checkInteratedSecurity.Name = "checkInteratedSecurity";
            this.checkInteratedSecurity.UseVisualStyleBackColor = true;
            this.checkInteratedSecurity.CheckedChanged += new System.EventHandler(this.checkInteratedSecurity_CheckedChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // txtLogin
            // 
            resources.ApplyResources(this.txtLogin, "txtLogin");
            this.txtLogin.Name = "txtLogin";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // txtPassword
            // 
            resources.ApplyResources(this.txtPassword, "txtPassword");
            this.txtPassword.Name = "txtPassword";
            // 
            // btnApply
            // 
            resources.ApplyResources(this.btnApply, "btnApply");
            this.btnApply.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003;
            this.btnApply.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnApply.Name = "btnApply";
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003;
            this.btnCancel.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // loadPicture
            // 
            resources.ApplyResources(this.loadPicture, "loadPicture");
            this.loadPicture.Name = "loadPicture";
            this.loadPicture.TabStop = false;
            // 
            // checkConnectionString
            // 
            resources.ApplyResources(this.checkConnectionString, "checkConnectionString");
            this.checkConnectionString.Name = "checkConnectionString";
            this.checkConnectionString.UseVisualStyleBackColor = true;
            this.checkConnectionString.CheckedChanged += new System.EventHandler(this.CheckConnectionString_CheckedChanged);
            // 
            // txtConStr
            // 
            resources.ApplyResources(this.txtConStr, "txtConStr");
            this.txtConStr.Name = "txtConStr";
            // 
            // ConnectionWindow
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.loadPicture);
            this.Controls.Add(this.txtConStr);
            this.Controls.Add(this.checkConnectionString);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtLogin);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.checkInteratedSecurity);
            this.Controls.Add(this.comboDatabases);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboServers);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ConnectionWindow";
            ((System.ComponentModel.ISupportInitialize)(this.loadPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboServers;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboDatabases;
        private System.Windows.Forms.CheckBox checkInteratedSecurity;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPassword;
        private DevExpress.XtraEditors.SimpleButton btnApply;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.PictureBox loadPicture;
        private System.Windows.Forms.CheckBox checkConnectionString;
        private System.Windows.Forms.TextBox txtConStr;
    }
}