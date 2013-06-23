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

        string job = "";
        public OpenJobForm(string connString, MySqlConnection conn, MySqlCommand command)
        {
            this.connString = connString;
            this.conn = conn;
            this.command = command;

            InitializeComponent();
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
            command.CommandText = "Select jobname from customer";

            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {

                jobListBox.Items.Add(reader["jobname"].ToString());
               

            }

            conn.Close();



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
