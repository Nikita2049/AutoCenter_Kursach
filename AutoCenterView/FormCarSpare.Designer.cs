namespace AutoCenterView
{
    partial class FormCarSpare
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
            this.labelComp = new System.Windows.Forms.Label();
            this.comboBoxSpare = new System.Windows.Forms.ComboBox();
            this.labelCount = new System.Windows.Forms.Label();
            this.textBoxCount = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPlusSum = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelComp
            // 
            this.labelComp.AutoSize = true;
            this.labelComp.Location = new System.Drawing.Point(34, 36);
            this.labelComp.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelComp.Name = "labelComp";
            this.labelComp.Size = new System.Drawing.Size(48, 13);
            this.labelComp.TabIndex = 6;
            this.labelComp.Text = "Деталь:";
            // 
            // comboBoxSpare
            // 
            this.comboBoxSpare.FormattingEnabled = true;
            this.comboBoxSpare.Location = new System.Drawing.Point(107, 33);
            this.comboBoxSpare.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxSpare.Name = "comboBoxSpare";
            this.comboBoxSpare.Size = new System.Drawing.Size(247, 21);
            this.comboBoxSpare.TabIndex = 8;
            this.comboBoxSpare.SelectedIndexChanged += new System.EventHandler(this.ComboBoxSpare_SelectedIndexChanged);
            this.comboBoxSpare.Click += new System.EventHandler(this.ComboBoxSpare_SelectedIndexChanged);
            // 
            // 
            // labelCount
            // 
            this.labelCount.AutoSize = true;
            this.labelCount.Location = new System.Drawing.Point(34, 83);
            this.labelCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(69, 13);
            this.labelCount.TabIndex = 9;
            this.labelCount.Text = "Количество:";
            // 
            // textBoxCount
            // 
            this.textBoxCount.Location = new System.Drawing.Point(107, 80);
            this.textBoxCount.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxCount.Name = "textBoxCount";
            this.textBoxCount.Size = new System.Drawing.Size(247, 20);
            this.textBoxCount.TabIndex = 10;
            this.textBoxCount.TextChanged += new System.EventHandler(this.TextBoxCount_TextChanged);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(37, 174);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(124, 24);
            this.buttonSave.TabIndex = 11;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(207, 174);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(147, 24);
            this.buttonCancel.TabIndex = 12;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 132);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Добавка к стоимости машины:";
            // 
            // textBoxPlusSum
            // 
            this.textBoxPlusSum.Location = new System.Drawing.Point(204, 129);
            this.textBoxPlusSum.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxPlusSum.Name = "textBoxPlusSum";
            this.textBoxPlusSum.Size = new System.Drawing.Size(150, 20);
            this.textBoxPlusSum.TabIndex = 14;
            // 
            // FormCarSpare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 227);
            this.Controls.Add(this.textBoxPlusSum);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxCount);
            this.Controls.Add(this.labelCount);
            this.Controls.Add(this.comboBoxSpare);
            this.Controls.Add(this.labelComp);
            this.Name = "FormCarSpare";
            this.Text = "Деталь машины";
            this.Load += new System.EventHandler(this.FormCarSpare_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelComp;
        private System.Windows.Forms.ComboBox comboBoxSpare;
        private System.Windows.Forms.Label labelCount;
        private System.Windows.Forms.TextBox textBoxCount;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxPlusSum;
    }
}