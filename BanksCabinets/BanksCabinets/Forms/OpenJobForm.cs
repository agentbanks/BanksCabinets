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
        string searchString = "";

        TreeNode[] fullTree;
        

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
            command.CommandText = "Select contractor,jobname from customer order by contractor";

            MySqlDataReader reader = command.ExecuteReader();
            int testint = 0;
            String currentContractor = "";
            String currentJobname = "";
            TreeNode node = new TreeNode(currentContractor);
            while (reader.Read())
            {
                
               // contractorComboBox.Items.Add(reader["contractor"].ToString());
                currentContractor = reader["contractor"].ToString();
                currentJobname = reader["jobname"].ToString();
                
                
                if (ContractorTreeView.Nodes.Count == 0)
                {

                    node = new TreeNode(currentContractor);
                    node.Name = currentContractor;
                    ContractorTreeView.Nodes.Add(node);
                   
                    node.Nodes.Add(currentJobname);
                    
                }
                else
                {
                    
                    TreeNode[] nodesFound = ContractorTreeView.Nodes.Find(currentContractor, false);
                    if (nodesFound.GetLength(0) == 0)
                    {
                        
                        
                        node = new TreeNode(currentContractor);
                        node.Name = currentContractor;
                        node.Nodes.Add(currentJobname);
                        //node.Nodes.Add(node.Name.ToString());//test
                        ContractorTreeView.Nodes.Add(node);
                    }
                    else
                    {
                        nodesFound[nodesFound.GetLength(0) - 1].Nodes.Add(currentJobname);
                        //nodesFound[nodesFound.GetLength(0) - 1].Nodes.Add(currentContractor);//test
                    }

                    }                
            
                testint++;
             
            }

            conn.Close();

            /*foreach (TreeNode tn in ContractorTreeView.Nodes)
            {
                //tn.Nodes.Add("test");

                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                String contractorNodeName = tn.Text.ToString();
                command.CommandText = "Select jobname from customer where contractor='"+contractorNodeName+"' order by jobname";

                reader = command.ExecuteReader();
                while (reader.Read())
                {

                    // contractorComboBox.Items.Add(reader["contractor"].ToString());
                    tn.Nodes.Add(reader["jobname"].ToString());
                   // tn.Nodes.Add(contractorNodeName); FOR TESTING, adds the node name as a child

                }

                conn.Close();
            }*/
            fullTree = new TreeNode[ContractorTreeView.Nodes.Count];
            ContractorTreeView.Nodes.CopyTo(fullTree, 0);


            
        }

        private void searchStringOnTree(object sender, EventArgs e)
        {
            TreeNode[] searchTree;
            List<TreeNode> searchList = new List<TreeNode>();
            searchString = searchBox.Text.ToLower();
            
            if (searchString != "")
            {
               // int searchIndex = 0;
               // TreeNode[] nodesFound = ContractorTreeView.Nodes.Find(searchString, false);
                //TreeNode[] nodesFound = new TreeNode[fullTree.Length];
                //searchTree = new TreeNode[fullTree.Length];

                foreach(TreeNode tn in fullTree)
                {

                    if (contractorRadioButton.Checked == true)
                    {
                        if (tn.Name.ToLower().StartsWith(searchString))
                        {
                            //searchTree[searchIndex] = tn;
                            searchList.Add(tn);
                           // searchIndex++;
                        }
                    }
                    else
                    {
                        foreach (TreeNode tn2 in tn.Nodes)
                        {
                            if (tn2.Text.ToLower().StartsWith(searchString))
                            {
                                //searchTree[searchIndex] = tn;
                                if (searchList.Contains(tn2.Parent) == false)
                                {
                                    searchList.Add(tn2.Parent);
                                }
                               // searchIndex++;
                            }
                        }
                    }
                }

                searchTree = new TreeNode[searchList.Count];
                searchTree = searchList.ToArray();
               // searchTree = nodesFound;
                //MessageBox.Show(searchTree[searchIndex-1].Name.ToString());

                if (searchTree != null)
                {
                    
                    try
                    {
                        ContractorTreeView.Nodes.Clear();
                        ContractorTreeView.Nodes.AddRange(searchTree);
                        ContractorTreeView.ExpandAll();
                    }
                    catch (ArgumentNullException)
                    {

                    }
                }
            }
            else
            {
                ContractorTreeView.Nodes.Clear();
                ContractorTreeView.Nodes.AddRange(fullTree);
                ContractorTreeView.CollapseAll();
            }

        }

       /* private void contractorComboBox_SelectedIndexChanged(object sender, EventArgs e)
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
            

        }*/

         private void contractorTreeView_AfterSelect(object sender, EventArgs e)
        {
            openJobButton.Enabled = false;
            //do something
             if (ContractorTreeView.SelectedNode.Level == 1)
             {
            job = ContractorTreeView.SelectedNode.Text;
            openJobButton.Enabled = true;
            //MessageBox.Show(ContractorTreeView.SelectedNode.Level.ToString());
             }
           // jobListBox.Items.Clear();

            

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
            //string job = "";
            //job = ContractorTreeView.SelectedNode.Name;
           // job = "returned job";
            
            this.Close();
        }

        public string GetJob()
        {
            return job;
        }
    }
}
