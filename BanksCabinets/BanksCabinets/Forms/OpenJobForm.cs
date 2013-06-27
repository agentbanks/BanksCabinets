using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace BanksCabinets.Forms
{
    public partial class OpenJobForm : Form
    {

        #region Database
        string connString;
        MySqlConnection conn;
        MySqlCommand command;

        #endregion

        public string job = "";
        string selectedContractor = "";

        public OpenJobForm(string connString, MySqlConnection conn, MySqlCommand command)
        {
            this.connString = connString;
            this.conn = conn;
            this.command = command;

            InitializeComponent();
            openJobButton.Enabled = false;
            //query database for jobs and list in box
            //See if I really need another connection

            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            command.CommandText = "Select distinct contractor from customer order by contractor";

            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {

                contractorComboBox.Items.Add(reader["contractor"].ToString());
               

            }

            conn.Close();



        }

        private void contractorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            //do something
            openJobButton.Enabled = false;
            jobListBox.Items.Clear();

            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            selectedContractor = contractorComboBox.Items[contractorComboBox.SelectedIndex].ToString();
            command.CommandText = "SELECT * from customer where contractor = '" + selectedContractor + "' order by jobname";

            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {

                jobListBox.Items.Add(reader["jobname"].ToString());


            }

            conn.Close();
            

        }

        private void jobSelected_Click(object sender, EventArgs e)
        {
            openJobButton.Enabled = true;
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
