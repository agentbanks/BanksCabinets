using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BanksCabinets.Forms
{
    public partial class OpenJobForm : Form
    {
        string job = "";
        public OpenJobForm()
        {
            InitializeComponent();
            //query database for jobs and list in box
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void openJobButton_Click(object sender, EventArgs e)
        {
            string job = "";
            job = jobListBox.SelectedItem.ToString();
            this.Close();
        }

        public string GetJob()
        {
            return job;
        }
    }
}
