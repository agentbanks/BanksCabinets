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
            this.openJobButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.ContractorTreeView = new System.Windows.Forms.TreeView();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.contractorRadioButton = new System.Windows.Forms.RadioButton();
            this.jobRadioButton = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select a Job:";
            // 
            // openJobButton
            // 
            this.openJobButton.DialogResult = System.Windows.Forms.DialogResult.OK;
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
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
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
            this.label2.Location = new System.Drawing.Point(43, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Search:";
            // 
            // ContractorTreeView
            // 
            this.ContractorTreeView.Location = new System.Drawing.Point(46, 133);
            this.ContractorTreeView.Name = "ContractorTreeView";
            this.ContractorTreeView.Size = new System.Drawing.Size(697, 263);
            this.ContractorTreeView.TabIndex = 6;
            this.ContractorTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.contractorTreeView_AfterSelect);
            // 
            // searchBox
            // 
            this.searchBox.Location = new System.Drawing.Point(46, 57);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(149, 21);
            this.searchBox.TabIndex = 7;
            this.searchBox.TextChanged += new System.EventHandler(this.searchStringOnTree);
            // 
            // contractorRadioButton
            // 
            this.contractorRadioButton.AutoSize = true;
            this.contractorRadioButton.BackColor = System.Drawing.SystemColors.Control;
            this.contractorRadioButton.Checked = true;
            this.contractorRadioButton.Location = new System.Drawing.Point(6, 24);
            this.contractorRadioButton.Name = "contractorRadioButton";
            this.contractorRadioButton.Size = new System.Drawing.Size(86, 17);
            this.contractorRadioButton.TabIndex = 8;
            this.contractorRadioButton.TabStop = true;
            this.contractorRadioButton.Text = "Contractor";
            this.contractorRadioButton.UseVisualStyleBackColor = false;
            this.contractorRadioButton.EnabledChanged += new System.EventHandler(this.searchStringOnTree);
            // 
            // jobRadioButton
            // 
            this.jobRadioButton.AutoSize = true;
            this.jobRadioButton.Location = new System.Drawing.Point(134, 24);
            this.jobRadioButton.Name = "jobRadioButton";
            this.jobRadioButton.Size = new System.Drawing.Size(80, 17);
            this.jobRadioButton.TabIndex = 9;
            this.jobRadioButton.Text = "Job name";
            this.jobRadioButton.UseVisualStyleBackColor = true;
            this.jobRadioButton.EnabledChanged += new System.EventHandler(this.searchStringOnTree);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.contractorRadioButton);
            this.groupBox1.Controls.Add(this.jobRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(222, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(237, 44);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search by:";
            // 
            // OpenJobForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 495);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.searchBox);
            this.Controls.Add(this.ContractorTreeView);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.openJobButton);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "OpenJobForm";
            this.Text = "Open a Job";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button openJobButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TreeView ContractorTreeView;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.RadioButton contractorRadioButton;
        private System.Windows.Forms.RadioButton jobRadioButton;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}