using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using MySql.Data.MySqlClient;


namespace BanksCabinets.Forms
{
    public partial class CabinetBuilderForm : Form
    {

        #region Database
        static string connString = "Server=127.0.0.1;Port=3306;Database=bankscabinets;Uid=root;password=douche";
        static MySqlConnection conn = new MySqlConnection(connString);
        MySqlCommand command = conn.CreateCommand();
        #endregion

        #region Current Job Vars

        string currentJob = string.Empty;
        int numCabinets = 0; //Change this to get number from database
        string firstName = string.Empty;
        string lastName = string.Empty;
        string contractor = string.Empty;
        int phoneNumber = -1;
        string address = string.Empty;
        string city = string.Empty;
        string state = string.Empty;
        int zip = -1;
        #endregion

        #region Cabinet Button Vars

        int cabinetButtonSelected = -1;
        
        int cabinetButtonSize = 25;
        int cabinetButtonHorizontal = 225;
        int cabinetButtonVertical = 205;
        float cabinetButtonFontSize = 6.5f;
        List<Button> cabinetButtons;
        Color notSelectedCabinetButtonColor = Color.FromArgb(222,222,222);
        Color notSelectedCabinetButtonTextColor = Color.Black;
        Color selectedCabinetButtonColor = Color.FromArgb(97,68,48);
        Color selectedCabinetButtonTextColor = Color.White;
        Color deletedCabinetButtonColor = Color.FromArgb(222, 98, 91);
        ArrayList deletedCabinets = new ArrayList();

        #endregion

        #region Cabinet Type Button Vars
        string selectedCabinetType;
        List<Button> cabinetTypeButtonList = new List<Button>();
        int cabinetTypeButtonHorizontal = 225;
        int cabinetTypeButtonVertical = 317;
        int cabinetTypeButtonLength = 90;
        int cabinetTypeButtonHeight = 25;
        #endregion

        #region Material Button Vars

        List<Button> materialButtonList = new List<Button>();
        int materialButtonHorizontal = 225;
        int materialButtonVertical = 495;
        string selectedMaterialType;

        #endregion

        #region Good Sides Button Vars

        int goodSideButtonSelected = -1;
        int goodSideButtonSize = 20;
        int goodSideButtonHorizontal = 225;
        int goodSideButtonVertical = 713;
        int numGoodSideButtons = 3;
        List<Button> goodSideButtons;

        #endregion

        #region Good Side Thickness Button Vars

        int thicknessButtonSelected = -1;
        int thicknessButtonWidth = 28;
        int thicknessButtonHeight = 18;
        int thicknessButtonHorizontal = 225;
        int thicknessButtonVertical = 740;
        int numThicknessButtons = 2;
        List<Button> thicknessButtons;

        #endregion
        
        public CabinetBuilderForm()
        {
            InitializeComponent();

            //ReadJobInfo(some job number);
            
            CreateCabinetButtons();
            CreateCabinetTypeButtons();
            
            //Material dropdown box replaced material buttons
            ReadMaterial();
            //CreateMaterialButtons();
            
            CreateGoodSideButtons();
            CreateThicknessButtons();
            
        }
        
        private void CabinetBuilderForm_Load(object sender, EventArgs e)
        {           
        }
        
        private void CreateCabinetButtons()
        {
            //Need to read in job info first and then create amount of buttons based on job

            cabinetButtons = new List<Button>();
            Font cabinetButtonFont = ChangeFontSize(SystemFonts.DefaultFont, cabinetButtonFontSize);
            int horizontalLocation = cabinetButtonHorizontal;

            for (int i = 0; i < numCabinets; i++)
            {
                Button button = new Button();
                button.FlatStyle = FlatStyle.Popup;
                button.BackColor = notSelectedCabinetButtonColor;
                button.Font = cabinetButtonFont;
                button.Text = (i + 1).ToString();       
                button.Size = new Size(cabinetButtonSize, cabinetButtonSize);
                button.Location = new Point(cabinetButtonHorizontal, cabinetButtonVertical);
                if (i == 24)
                {
                    cabinetButtonVertical += cabinetButtonSize +1;
                    cabinetButtonHorizontal = horizontalLocation;
                }
                else
                {
                    cabinetButtonHorizontal += cabinetButtonSize -2;
                }                
                this.tabPage1.Controls.Add(button);
                button.Click += new EventHandler(cabinetButton_Click);
                cabinetButtons.Add(button);
            }

        }

        private void UpdateCabinetButtons(){}

        private void DeleteCabinetButtons()
        {
            cabinetButtons.Clear();
        }

        void cabinetButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int newCabinetButtonSelected = int.Parse(button.Text) - 1;
            cabinetButtons[newCabinetButtonSelected].BackColor = selectedCabinetButtonColor;
            cabinetButtons[newCabinetButtonSelected].ForeColor = selectedCabinetButtonTextColor;
            

            if (cabinetButtonSelected > -1)
            {
                cabinetButtons[cabinetButtonSelected].BackColor = notSelectedCabinetButtonColor;
                cabinetButtons[cabinetButtonSelected].ForeColor = notSelectedCabinetButtonTextColor;
                
            }

            ReadCabinetInfo(newCabinetButtonSelected);

            cabinetButtonSelected = newCabinetButtonSelected;

        }

        private void CreateCabinetTypeButtons()
        {
            Button kitchenButton = new Button();
            kitchenButton.Text = "Base";
            cabinetTypeButtonList.Add(kitchenButton);
            
            Button uppersButton = new Button();
            uppersButton.Text = "Uppers";
            cabinetTypeButtonList.Add(uppersButton);

            Button tallButton = new Button();
            tallButton.Text = "Tall";
            cabinetTypeButtonList.Add(tallButton);

            Button officeButton = new Button();
            officeButton.Text = "Office";
            cabinetTypeButtonList.Add(officeButton);

            Button vanityButton = new Button();
            vanityButton.Text = "Vanity";
            cabinetTypeButtonList.Add(vanityButton);

            foreach (Button button in cabinetTypeButtonList)
            {
                button.FlatStyle = FlatStyle.Popup;
                button.BackColor = notSelectedCabinetButtonColor; ;                              
                button.Size = new Size(cabinetTypeButtonLength, cabinetTypeButtonHeight);
                button.Location = new Point(cabinetTypeButtonHorizontal, cabinetTypeButtonVertical);
                this.tabPage1.Controls.Add(button);
                button.Click += new EventHandler(cabinetTypeButton_Click);

                //update location for next button
                cabinetTypeButtonHorizontal += cabinetTypeButtonLength;
            }
        }

        void cabinetTypeButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string newCabinetTypeSelected = button.Text;
            button.BackColor = selectedCabinetButtonColor;
            button.ForeColor = selectedCabinetButtonTextColor;

            if (!string.IsNullOrEmpty(selectedCabinetType))
            {
                foreach (Button b in cabinetTypeButtonList)
                {
                    if (b.Text == selectedCabinetType)
                    {
                        b.BackColor = notSelectedCabinetButtonColor;
                        b.ForeColor = notSelectedCabinetButtonTextColor;
                    }
                }
            }

            selectedCabinetType = button.Text;

        }

        private void CreateMaterialButtons()
        {
            Button mapleButton = new Button();
            mapleButton.Text = "Maple Ply";
            materialButtonList.Add(mapleButton);

            Button millimeenButton = new Button();
            millimeenButton.Text = "Millimeen";
            materialButtonList.Add(millimeenButton);

            Button alderButton = new Button();
            alderButton.Text = "Alder";
            materialButtonList.Add(alderButton);

            foreach (Button button in materialButtonList)
            {
                button.FlatStyle = FlatStyle.Popup;
                button.BackColor = notSelectedCabinetButtonColor;
                button.Size = new Size(cabinetTypeButtonLength, cabinetTypeButtonHeight);
                button.Location = new Point(materialButtonHorizontal, materialButtonVertical);
                this.tabPage1.Controls.Add(button);
                button.Click += new EventHandler(materialTypeButton_Click);

                //update location for next button
                materialButtonHorizontal += cabinetTypeButtonLength;
            }

        }

        void materialTypeButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string newMaterialSelected = button.Text;
            button.BackColor = selectedCabinetButtonColor;
            button.ForeColor = selectedCabinetButtonTextColor;

            if (!string.IsNullOrEmpty(selectedMaterialType))
            {
                foreach (Button b in materialButtonList)
                {
                    if (b.Text == selectedMaterialType)
                    {
                        b.BackColor = notSelectedCabinetButtonColor;
                        b.ForeColor = notSelectedCabinetButtonTextColor;
                    }
                }
            }

            selectedMaterialType = button.Text;
        }

        private void CreateGoodSideButtons()
        {
            goodSideButtons = new List<Button>();
            Font cabinetButtonFont = ChangeFontSize(SystemFonts.DefaultFont, cabinetButtonFontSize);
            
            for (int i = 0; i < numGoodSideButtons; i++)
            {
                Button button = new Button();
                button.FlatStyle = FlatStyle.Popup;
                button.BackColor = notSelectedCabinetButtonColor;
                button.Font = cabinetButtonFont;
                button.Text = (i + 1).ToString();
                button.Size = new Size(goodSideButtonSize, goodSideButtonSize);
                button.Location = new Point(goodSideButtonHorizontal, goodSideButtonVertical);
                goodSideButtonHorizontal += goodSideButtonSize - 2;                
                this.tabPage1.Controls.Add(button);
                button.Click += new EventHandler(goodSideButton_Click);
                goodSideButtons.Add(button);
            }
        }

        void goodSideButton_Click(object sender, EventArgs e)
        {

        }

        private void CreateThicknessButtons()
        {
            thicknessButtons = new List<Button>();
            Font cabinetButtonFont = ChangeFontSize(SystemFonts.DefaultFont, cabinetButtonFontSize);
            
            Button button1 = new Button();
            button1.Text = ".25";
            thicknessButtons.Add(button1);

            Button button2 = new Button();
            button2.Text = ".75";
            thicknessButtons.Add(button2);

            foreach (Button button in thicknessButtons)
            {
                //Button button = new Button();
                button.FlatStyle = FlatStyle.Popup;
                button.BackColor = notSelectedCabinetButtonColor;
                button.Font = cabinetButtonFont;
                //button.Text = (i + 1).ToString();
                button.Size = new Size(thicknessButtonWidth, thicknessButtonHeight);
                button.Location = new Point(thicknessButtonHorizontal, thicknessButtonVertical);
                thicknessButtonHorizontal += thicknessButtonWidth - 2;
                this.tabPage1.Controls.Add(button);
                button.Click += new EventHandler(thicknessButton_Click);
                //thicknessButtons.Add(button);
            }
        }

        void thicknessButton_Click(object sender, EventArgs e)
        {

        }     
        
        private void ReadCabinetInfo(int newCabinetButtonSelected)
        {
            //get cabinet info from database
            //throw new NotImplementedException();
        }

        private Font ChangeFontSize(Font font, float fontSize)
        {
            font = new Font(font.Name, fontSize, font.Style, font.Unit, font.GdiCharSet, font.GdiVerticalFont);
            return font;
        }

        private void removeCabinetButton_Click(object sender, EventArgs e)
        {
            if (cabinetButtonSelected >= 0)
            {
                deletedCabinets.Add(cabinetButtonSelected);
                UpdateDeletedCabinets();
                cabinetButtonSelected = -1;
            }
        }

        private void UpdateDeletedCabinets()
        {
            foreach (int i in deletedCabinets)
            {                          
                cabinetButtons[i].ForeColor = selectedCabinetButtonTextColor;
                cabinetButtons[i].BackColor = deletedCabinetButtonColor;
                cabinetButtons[i].Enabled = false;                
            }
        }

        private void newJobButton_Click(object sender, EventArgs e)
        {
            NewJobForm form = new NewJobForm(connString, conn, command);
            form.ShowDialog();
            currentJob = form.GetJob();
            readJobData();
            
        }

        private void openJobButton_Click(object sender, EventArgs e)
        {
            OpenJobForm form = new OpenJobForm();
            form.ShowDialog();
            currentJob = form.GetJob();
            readJobData();
        }

        private void readJobData()
        {            
            jobNameTextBox.Text = currentJob;

            command.CommandText = "SELECT * from customer where jobname = '" + currentJob + "'";

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
                contractor = reader["contractor"].ToString();
                firstName = reader["firstname"].ToString();
                lastName = reader["lastname"].ToString();
                address = reader["address"].ToString();
                city = reader["city"].ToString();
                state = reader["state"].ToString();
                zip = Int32.Parse(reader["zip"].ToString());
                phoneNumber= Int32.Parse(reader["phone"].ToString());
                numCabinets= Int32.Parse(reader["numcabinets"].ToString());

            }

            conn.Close();
        }

        private void addMaterialButton_Click(object sender, EventArgs e)
        {
            AddMaterialForm form = new AddMaterialForm(conn);
            form.ShowDialog();
            ReadMaterial();

        }

        private void removeMaterialButton_Click(object sender, EventArgs e)
        {
            if (materialComboBox.SelectedIndex >= 0)
            {
                RemoveMaterialConfirmationForm form = new RemoveMaterialConfirmationForm(conn, materialComboBox.SelectedItem.ToString());
                form.ShowDialog();

                materialComboBox.ResetText();
                ReadMaterial();
            }
            else
            {
                MessageBox.Show("Please select the material type to remove.");
            }
        }
      
        private void ReadMaterial()
        {
            materialComboBox.Items.Clear();
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
            command.CommandText = "Select type from material";

            //List<string> typeList = new List<string>();

            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                materialComboBox.Items.Add(reader["type"].ToString());
                //typeList.Add(reader["type"].ToString());
            }
            conn.Close();
        }

        private void addTemplateButton_Click(object sender, EventArgs e)
        {

        }

        private bool CheckCabinetFields()
        {
            bool allFieldsCompleted = false;

            if (!string.IsNullOrEmpty(selectedCabinetType) && materialComboBox.SelectedIndex > -1 && 
        }

      
       
    }
}
