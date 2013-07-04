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
    public partial class NewJobForm : Form
    {
        string job = "";
        string contractor = "";
        
        #region Database
        string connString;
        MySqlConnection conn;
        MySqlCommand command;

        #endregion

        public NewJobForm(string connString, MySqlConnection conn, MySqlCommand command)
        {
            this.connString = connString;
            this.conn = conn;
            this.command = command;
            InitializeComponent();


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

        private void saveJobButton_Click(object sender, EventArgs e)
        {            
            //query database to see if job name already exists
            //if it does, make user change name
            //else add job to database
            //command.CommandText = "INSERT INTO customer (contractor, jobname, numcabinets, firstname, lastname, address, city, state, zip, phone, email) "
            //    + "values (

            job = jobNameTextBox.Text;
            contractor = contractorComboBox.Text;

            bool isFormComplete = CheckInput();
            bool isUnique = CheckUniqueJobName();

            if (isFormComplete && isUnique)
            {
                job = jobNameTextBox.Text;
               // contractor = contractorComboBox.Text;
                if (string.IsNullOrEmpty(zipTextBox.Text))
                    zipTextBox.Text = "0";
                if (string.IsNullOrEmpty(phoneNumberTextBox.Text))
                    phoneNumberTextBox.Text = "0";

                //command.CommandText = "INSERT INTO customer (jobname) values ('"+ jobNameTextBox.Text +"' )";
                command.CommandText = "INSERT INTO customer (contractor, jobname, firstname, lastname, address, city, state, zip, phone, email) "
                    + "values ('" + contractorComboBox.Text + "', '" + jobNameTextBox.Text + "', '" + firstNameTextBox.Text + "', '" + lastNameTextBox.Text + "', '" + addressTextBox.Text + "', '"
                    + cityTextBox.Text + "', '" + stateTextBox.Text + "', '" + zipTextBox.Text + "', '" + phoneNumberTextBox.Text + "', '" + emailTextBox.Text + "')";
                
                try
                {

                    conn.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                command.ExecuteNonQuery();
                conn.Close();



                this.Close();
            }
            else
            {
                MessageBox.Show("Please fill out a unique Job Name");
                
            }
            

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            job = "";
            this.Close();
        }

        private bool CheckInput()
        {
            //Right now we are only checking to see if Job Name is filled out
            if (String.IsNullOrEmpty(jobNameTextBox.Text))
                return false;
            else
                return true;
        }

        private bool CheckUniqueJobName()
        {
            bool isUnique = false;
            string newJobName = "";
            command.CommandText = "Select jobname from customer where jobname = '" + job + "'";
           // command.CommandText = "Select jobname from customer where jobname = '" + job + "' and contractor = '" + contractor + "'";

            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                newJobName = reader["jobname"].ToString();
            }

            conn.Close();

            if (!string.Equals(job, newJobName))
                isUnique = true;

            return isUnique;

        }
        
        
        public string GetJob()
        {
            return job;
        }        

        private void phoneNumberTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "\\d+"))
                e.Handled = true;
        }

        private void zipTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "\\d+"))
                e.Handled = true;
        }

       

        

        
    }
}
