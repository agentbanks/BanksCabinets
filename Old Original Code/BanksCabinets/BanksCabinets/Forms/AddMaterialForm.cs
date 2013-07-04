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
    public partial class AddMaterialForm : Form
    {
        MySqlConnection conn;

        public AddMaterialForm(MySqlConnection conn)
        {
            this.conn = conn;

            InitializeComponent();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(materialTextBox.Text))
            {
                try
                {
                    conn.Open();
                    MySqlCommand command = conn.CreateCommand();
                    
                    // Check to see if type already exists
                    command.CommandText = "Select type from material where type = '" + materialTextBox.Text + "'";
                    
                    List<string> typeList = new List<string>();

                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        typeList.Add(reader["type"].ToString());
                    }
                    conn.Close();

                    conn.Open();
                    // Add new type if not already present
                    if (typeList.Count == 0)
                    {
                        command.CommandText = "Insert into material (type) values ('" + materialTextBox.Text + "')";
                        command.ExecuteNonQuery();
                        MessageBox.Show(materialTextBox.Text + " has been added.");
                        conn.Close();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(materialTextBox.Text + " already exists.");
                    }                    
                    
                    conn.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Please enter a material name");
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
