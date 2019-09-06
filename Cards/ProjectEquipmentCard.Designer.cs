namespace Fittings.Cards
{
    partial class ProjectEquipmentCard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectEquipmentCard));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.отменаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.провкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выбратьОборудованиеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboEquipmetns = new System.Windows.Forms.ComboBox();
            this.btnSearchEquipmetns = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUsePercent = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Gainsboro;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.провкаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(396, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сохранитьToolStripMenuItem,
            this.toolStripSeparator1,
            this.отменаToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("сохранитьToolStripMenuItem.Image")));
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.сохранитьToolStripMenuItem.Text = "Сохранить";
            this.сохранитьToolStripMenuItem.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // отменаToolStripMenuItem
            // 
            this.отменаToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("отменаToolStripMenuItem.Image")));
            this.отменаToolStripMenuItem.Name = "отменаToolStripMenuItem";
            this.отменаToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.отменаToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.отменаToolStripMenuItem.Text = "Отмена";
            this.отменаToolStripMenuItem.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // провкаToolStripMenuItem
            // 
            this.провкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.выбратьОборудованиеToolStripMenuItem});
            this.провкаToolStripMenuItem.Name = "провкаToolStripMenuItem";
            this.провкаToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.провкаToolStripMenuItem.Text = "Правка";
            // 
            // выбратьОборудованиеToolStripMenuItem
            // 
            this.выбратьОборудованиеToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("выбратьОборудованиеToolStripMenuItem.Image")));
            this.выбратьОборудованиеToolStripMenuItem.Name = "выбратьОборудованиеToolStripMenuItem";
            this.выбратьОборудованиеToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.выбратьОборудованиеToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.выбратьОборудованиеToolStripMenuItem.Text = "Найти оборудование";
            this.выбратьОборудованиеToolStripMenuItem.Click += new System.EventHandler(this.BtnSearchEquipmetns_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(13, 174);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 25);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Сохранить";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(109, 174);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 25);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 14);
            this.label1.TabIndex = 3;
            this.label1.Text = "*Выберите оборудование";
            // 
            // comboEquipmetns
            // 
            this.comboEquipmetns.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboEquipmetns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboEquipmetns.FormattingEnabled = true;
            this.comboEquipmetns.Location = new System.Drawing.Point(12, 59);
            this.comboEquipmetns.Name = "comboEquipmetns";
            this.comboEquipmetns.Size = new System.Drawing.Size(339, 21);
            this.comboEquipmetns.TabIndex = 4;
            // 
            // btnSearchEquipmetns
            // 
            this.btnSearchEquipmetns.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchEquipmetns.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchEquipmetns.Image")));
            this.btnSearchEquipmetns.Location = new System.Drawing.Point(360, 57);
            this.btnSearchEquipmetns.Name = "btnSearchEquipmetns";
            this.btnSearchEquipmetns.Size = new System.Drawing.Size(24, 24);
            this.btnSearchEquipmetns.TabIndex = 5;
            this.btnSearchEquipmetns.UseVisualStyleBackColor = true;
            this.btnSearchEquipmetns.Click += new System.EventHandler(this.BtnSearchEquipmetns_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(13, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(162, 14);
            this.label2.TabIndex = 6;
            this.label2.Text = "*Процент использования, %";
            // 
            // txtUsePercent
            // 
            this.txtUsePercent.Location = new System.Drawing.Point(177, 102);
            this.txtUsePercent.Name = "txtUsePercent";
            this.txtUsePercent.Size = new System.Drawing.Size(100, 20);
            this.txtUsePercent.TabIndex = 7;
            this.txtUsePercent.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtUsePercent_KeyPress);
            // 
            // ProjectEquipmentCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 209);
            this.Controls.Add(this.txtUsePercent);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSearchEquipmetns);
            this.Controls.Add(this.comboEquipmetns);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ProjectEquipmentCard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Карточка оборудования у проекта";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem отменаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem провкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выбратьОборудованиеToolStripMenuItem;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboEquipmetns;
        private System.Windows.Forms.Button btnSearchEquipmetns;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUsePercent;
    }
}