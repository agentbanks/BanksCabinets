namespace BanksCabinets.Forms
{
    partial class OpenJobForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.jobListBox = new System.Windows.Forms.ListBox();
            this.openJobButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.contractorComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Past Jobs:";
            // 
            // jobListBox
            // 
            this.jobListBox.FormattingEnabled = true;
            this.jobListBox.Location = new System.Drawing.Point(46, 113);
            this.jobListBox.Name = "jobListBox";
            this.jobListBox.Size = new System.Drawing.Size(725, 290);
            this.jobListBox.TabIndex = 1;
            this.jobListBox.SelectedIndexChanged += new System.EventHandler(this.jobSelected_Click);
            // 
            // openJobButton
            // 
            this.openJobButton.Location = new System.Drawing.Point(265, 429);
            this.openJobButton.Name = "openJobButton";
            this.openJobButton.Size = new System.Drawing.Size(75, 23);
            this.openJobButton.TabIndex = 2;
            this.openJobButton.Text = "Open";
            this.openJobButton.UseVisualStyleBackColor = true;
            this.openJobButton.Click += new System.EventHandler(this.openJobButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(384, 429);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Search:";
            // 
            // contractorComboBox
            // 
            this.contractorComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.contractorComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.contractorComboBox.FormattingEnabled = true;
            this.contractorComboBox.Location = new System.Drawing.Point(46, 42);
            this.contractorComboBox.Name = "contractorComboBox";
            this.contractorComboBox.Size = new System.Drawing.Size(413, 21);
            this.contractorComboBox.TabIndex = 5;
            this.contractorComboBox.SelectedIndexChanged += new System.EventHandler(this.contractorComboBox_SelectedIndexChanged);
            // 
            // OpenJobForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 495);
            this.Controls.Add(this.contractorComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.openJobButton);
            this.Controls.Add(this.jobListBox);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "OpenJobForm";
            this.Text = "Open a Job";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox jobListBox;
        private System.Windows.Forms.Button openJobButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox contractorComboBox;
    }
}