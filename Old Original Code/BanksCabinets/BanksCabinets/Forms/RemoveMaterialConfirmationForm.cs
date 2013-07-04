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
    public partial class RemoveMaterialConfirmationForm : Form
    {
        MySqlConnection conn;
        string material;

        public RemoveMaterialConfirmationForm(MySqlConnection conn, string material)
        {
            this.conn = conn;
            this.material = material;

            InitializeComponent();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            MySqlCommand command = conn.CreateCommand();
            command.CommandText = "delete from material where type = '" + material + "'";

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

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
