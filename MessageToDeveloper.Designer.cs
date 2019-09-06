namespace Fittings
{
    partial class MessageToDeveloper
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageToDeveloper));
            this.label2 = new System.Windows.Forms.Label();
            this.txtTitleMessage = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBobyMail = new System.Windows.Forms.RichTextBox();
            this.btnSend = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAuthorName = new System.Windows.Forms.TextBox();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.listAttaches = new System.Windows.Forms.ListBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnAddAttach = new System.Windows.Forms.ToolStripButton();
            this.btnEditAttach = new System.Windows.Forms.ToolStripButton();
            this.btnRemoveAttach = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txtTitleMessage
            // 
            resources.ApplyResources(this.txtTitleMessage, "txtTitleMessage");
            this.txtTitleMessage.Name = "txtTitleMessage";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // txtBobyMail
            // 
            resources.ApplyResources(this.txtBobyMail, "txtBobyMail");
            this.txtBobyMail.Name = "txtBobyMail";
            // 
            // btnSend
            // 
            resources.ApplyResources(this.btnSend, "btnSend");
            this.btnSend.ImageOptions.ImageIndex = ((int)(resources.GetObject("btnSend.ImageOptions.ImageIndex")));
            this.btnSend.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnSend.ImageOptions.SvgImage")));
            this.btnSend.LookAndFeel.SkinName = "Seven Classic";
            this.btnSend.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnSend.Name = "btnSend";
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.ImageOptions.ImageIndex = ((int)(resources.GetObject("btnCancel.ImageOptions.ImageIndex")));
            this.btnCancel.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnCancel.ImageOptions.SvgImage")));
            this.btnCancel.LookAndFeel.SkinName = "Seven Classic";
            this.btnCancel.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // txtAuthorName
            // 
            resources.ApplyResources(this.txtAuthorName, "txtAuthorName");
            this.txtAuthorName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAuthorName.Name = "txtAuthorName";
            // 
            // txtTitle
            // 
            resources.ApplyResources(this.txtTitle, "txtTitle");
            this.txtTitle.BackColor = System.Drawing.Color.White;
            this.txtTitle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTitle.Name = "txtTitle";
            // 
            // listAttaches
            // 
            resources.ApplyResources(this.listAttaches, "listAttaches");
            this.listAttaches.FormattingEnabled = true;
            this.listAttaches.Name = "listAttaches";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            resources.ApplyResources(this.openFileDialog1, "openFileDialog1");
            // 
            // toolStrip1
            // 
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddAttach,
            this.btnEditAttach,
            this.btnRemoveAttach});
            this.toolStrip1.Name = "toolStrip1";
            // 
            // btnAddAttach
            // 
            resources.ApplyResources(this.btnAddAttach, "btnAddAttach");
            this.btnAddAttach.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAddAttach.Name = "btnAddAttach";
            this.btnAddAttach.Click += new System.EventHandler(this.btnAddAttach_Click);
            // 
            // btnEditAttach
            // 
            resources.ApplyResources(this.btnEditAttach, "btnEditAttach");
            this.btnEditAttach.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEditAttach.Name = "btnEditAttach";
            this.btnEditAttach.Click += new System.EventHandler(this.btnEditAttach_Click);
            // 
            // btnRemoveAttach
            // 
            resources.ApplyResources(this.btnRemoveAttach, "btnRemoveAttach");
            this.btnRemoveAttach.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRemoveAttach.Name = "btnRemoveAttach";
            this.btnRemoveAttach.Click += new System.EventHandler(this.btnRemoveAttach_Click);
            // 
            // MessageToDeveloper
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.listAttaches);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.txtAuthorName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtBobyMail);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTitleMessage);
            this.Controls.Add(this.label2);
            this.Name = "MessageToDeveloper";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTitleMessage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox txtBobyMail;
        private DevExpress.XtraEditors.SimpleButton btnSend;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAuthorName;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.ListBox listAttaches;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnAddAttach;
        private System.Windows.Forms.ToolStripButton btnEditAttach;
        private System.Windows.Forms.ToolStripButton btnRemoveAttach;
    }
}