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
        //static string connString = "Server=50.87.150.162;Port=3306;Database=BanksCabinets;Uid=peynadov_agent;password=codybanks";
        //static string connString = "Server=127.0.0.1;Port=3306;Database=BanksCabinets;Uid=agent;password=codybanks";
        static string connString = "Server=bankscabinets.cmm7xidttue2.us-west-2.rds.amazonaws.com;Port=3306;Database=bankscabinets;Uid=agent;password=codybanks";
        static MySqlConnection conn = new MySqlConnection(connString);
        MySqlCommand command = conn.CreateCommand();
        #endregion

        #region Current Job Vars

        string currentJob = string.Empty;
        int numCabinets = 0; //Change this to get number from database
        string firstName = string.Empty;
        string lastName = string.Empty;
        string contractor = string.Empty;
        long phoneNumber = -1;
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

        List<Template> templatesList = new List<Template>();
        
        List<Template> baseTemplatesList = new List<Template>();
        List<Template> uppersTemplatesList = new List<Template>();
        List<Template> tallTemplatesList = new List<Template>();
        List<Template> officeTemplatesList = new List<Template>();
        List<Template> vanityTemplatesList = new List<Template>();
        List<Template> toPopulate = new List<Template>();
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
           // this.WindowState = FormWindowState.Maximized;
            //ReadJobInfo(some job number);
            
            //CreateCabinetButtons();
            CreateCabinetTypeButtons();
            
            //Material dropdown box replaced material buttons
            ReadMaterial();
            //CreateMaterialButtons();
            readTemplates();
            
            CreateGoodSideButtons();
            CreateThicknessButtons();
            
        }
        
        private void CabinetBuilderForm_Load(object sender, EventArgs e)
        {           
        }
        
        private void CreateCabinetButtons()
        {
            //Need to read in job info first and then create amount of buttons based on job

            clearCabinetButtons();

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

        private void clearCabinetButtons()
        {
            if (cabinetButtons != null)
            {
                cabinetButtonHorizontal = 225;
                cabinetButtonVertical = 205;
                
                foreach (Button btn in cabinetButtons)
                {
                    this.tabPage1.Controls.Remove(btn);
                }
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

            populateTemplateListBox(selectedCabinetType);
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
            DialogResult newJobResult = form.ShowDialog();
            //if (newJobResult == DialogResult.OK)
            
               // form.ShowDialog();
               //  MessageBox.Show("User clicked OK button");
                currentJob = form.GetJob();
                readJobData();
            
        }

        private void openJobButton_Click(object sender, EventArgs e)
        {
            //OpenJobForm form = new OpenJobForm();
            
            OpenJobForm form = new OpenJobForm(connString, conn, command);
            DialogResult openJobResult = form.ShowDialog();
            if (openJobResult == DialogResult.OK)
            {
                currentJob = form.GetJob();
                readJobData();
               // MessageBox.Show("User clicked OK button");
                //jobNameLabel.Text = currentJob;
            }
        }

        private void readJobData()
        {            
            jobNameLabel.Text = currentJob;

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
                phoneNumber= Int64.Parse(reader["phone"].ToString());
                numCabinets= Int32.Parse(reader["numcabinets"].ToString());

            }

            conn.Close();
           // MessageBox.Show(numCabinets.ToString());
            CreateCabinetButtons();
        }

       /* private void newJobForm_closed(object sender, EventArgs e)
        {

        }*/

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


        private void readTemplates()
        {

            string cabinetType, template;
            bool hasGlassDoors, isBoxDrawer, hasToeKick;
            int numGoodSides, adjustableShelves, fixedShelves, stainGradeShelves, rollouts, numOpenings;
            double goodSideThickness, width, height, depth, topRail,middleRail1, middleRail2, middleRail3, middleRail4, bottomRail,
                rightStyle, leftStyle, middleStyle, middleShelfWidth, middleShelfHeight, middleShelfDepth;
          //  double[] middleRails = new double[4];

            command.CommandText = "SELECT * from templates";

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
                

                template = reader["template"].ToString();
                cabinetType = reader["cabinetType"].ToString();
               // MessageBox.Show(reader["hasGlassDoors"].ToString());
                hasGlassDoors = reader.GetBoolean(reader.GetOrdinal("hasglassdoors"));
                //MessageBox.Show(hasGlassDoors.ToString());
                isBoxDrawer = reader.GetBoolean(reader.GetOrdinal("isboxdrawer"));
                hasToeKick = reader.GetBoolean(reader.GetOrdinal("hastoekick"));
               
                numGoodSides = Int32.Parse(reader["numgoodsides"].ToString());
                adjustableShelves = Int32.Parse(reader["adjustableshelves"].ToString());
                fixedShelves = Int32.Parse(reader["fixedshelves"].ToString());
                stainGradeShelves = Int32.Parse(reader["staingradeshelves"].ToString());
                rollouts = Int32.Parse(reader["rollouts"].ToString());

                goodSideThickness = Double.Parse(reader["goodsidethickness"].ToString());
                width = Double.Parse(reader["width"].ToString());
                height = Double.Parse(reader["height"].ToString());
                depth = Double.Parse(reader["depth"].ToString());

                

                topRail = Double.Parse(reader["toprail"].ToString());                
                bottomRail = Double.Parse(reader["bottomrail"].ToString());

               // middleRails[0] = Double.Parse(reader["middlerail1"].ToString());
                middleRail1 = reader.GetDouble(reader.GetOrdinal("middlerail1"));
                middleRail2 = Double.Parse(reader["middlerail2"].ToString());
                middleRail3 = Double.Parse(reader["middlerail3"].ToString());
                middleRail4 = Double.Parse(reader["middlerail4"].ToString());
               // MessageBox.Show(middleRail2.ToString());  
                rightStyle = Double.Parse(reader["rightstyle"].ToString());
                leftStyle = Double.Parse(reader["leftstyle"].ToString());
                middleStyle = Double.Parse(reader["middlestyle"].ToString());
                middleShelfWidth = Double.Parse(reader["middleshelfwidth"].ToString());
                middleShelfHeight = Double.Parse(reader["middleshelfheight"].ToString());
                middleShelfDepth = Double.Parse(reader["middleshelfdepth"].ToString());

                numOpenings = Int32.Parse(reader["numopenings"].ToString());
                
                   

                Template myTemp = new Template(cabinetType, template, hasGlassDoors, isBoxDrawer, hasToeKick, numGoodSides, adjustableShelves, fixedShelves, stainGradeShelves, rollouts, numOpenings, goodSideThickness, width, height, depth, topRail, middleRail1, middleRail2, middleRail3, middleRail4, bottomRail,
                rightStyle, leftStyle, middleStyle, middleShelfWidth, middleShelfHeight, middleShelfDepth);
                
                switch (myTemp.cabinetType.ToLower())
                {
                    case "base":
                        baseTemplatesList.Add(myTemp);
                       
                        break;
                    case "uppers":
                        uppersTemplatesList.Add(myTemp);
                        
                        break;
                    case "tall":
                        tallTemplatesList.Add(myTemp);
                       
                        break;
                    case "office":
                        officeTemplatesList.Add(myTemp);
                        
                        break;
                    default:
                        vanityTemplatesList.Add(myTemp);
                        
                        break;
                }
               // templatesList.Add(myTemp);
              //  MessageBox.Show(myTemp.middleRail2.ToString());  

            }

            conn.Close();
        }

        private void populateTemplateListBox(string cabinetType)
        {

            templateListBox.Items.Clear();

            /*foreach (Template temp in templatesList)
            {
                if (temp.cabinetType.ToLower() == cabinetType.ToLower())
                {
                    templateListBox.Items.Add(temp.template);
                }
            }*/

            

            switch (cabinetType.ToLower())
            {            
            
                case "base":
                    toPopulate = baseTemplatesList;
                     
                    break;
                case "uppers":
                    toPopulate = uppersTemplatesList;
                    break;
                case "tall":
                    toPopulate = tallTemplatesList;
                    break;
                case "office":
                    toPopulate = officeTemplatesList;
                    break;
                default:
                    toPopulate = vanityTemplatesList;
                    break;
            }

            foreach (Template tmp in toPopulate)
            {
                templateListBox.Items.Add(tmp);
                //MessageBox.Show(tmp.numOpenings.ToString() + " " +tmp.middleRail3.ToString());                 
            }

        }

        private void selectedTemplateChanged(object sender, EventArgs e)
        {
            Template aja = (Template) templateListBox.SelectedItem;
            //MessageBox.Show(aja.middleRail2.ToString());
            
            try
            {
                cabinetDimensionWidthTextBox.Text = aja.width.ToString();
                cabinetDimensionHeightTextBox.Text = aja.height.ToString();
                cabinetDimensionDepthTextBox.Text = aja.width.ToString();

                boxDrawerCheckBox.Checked = aja.isBoxDrawer;
                toeKickCheckBox.Checked = aja.hasToeKick;

                customGoodSideTextBox.Text = aja.goodSideThickness.ToString();
                topRailTextBox.Text = aja.topRail.ToString();
                //middleRailTextBox.Text = aja.middleRail.ToString(); //remove
                bottomRailTextBox.Text = aja.bottomRail.ToString();

                middleRail1TextBox.Text = aja.middleRail1.ToString();
                middleRail2TextBox.Text = aja.middleRail2.ToString();
                middleRail3TextBox.Text = aja.middleRail3.ToString();
                middleRail4TextBox.Text = aja.middleRail4.ToString();
                
                adjustableShelvesTextBox.Text = aja.adjustableShelves.ToString();
                fixedShelvesTextBox.Text = aja.fixedShelves.ToString();
                stainGradeShelvesTextBox.Text = aja.stainGradeShelves.ToString();
                rolloutsTextBox.Text = aja.rollouts.ToString();

                numOpeningsComboBox.Text = aja.numOpenings.ToString();

                rightStyleTextBox.Text = aja.rightStyle.ToString();
                leftStyleTextBox.Text = aja.leftStyle.ToString();
                middleStyleTextBox.Text = aja.middleStyle.ToString(); 

                widthMiddleShelfTextBox.Text = aja.middleShelfWidth.ToString();
                heightMiddleShelfTextBox.Text = aja.middleShelfHeight.ToString();
                depthMiddleShelfTextBox.Text = aja.middleShelfDepth.ToString();
                
                
            }
            catch (Exception ex)
            {
            }
            updateOpeningGroups(null, null);
        }

        private void updateOpeningGroups(object sender, EventArgs e)
        {
            int secondOpeningGroupPosition = 267;
            int distBetPanels = 137;

            List<Panel> openingPanels = new List<Panel>();
            openingPanels.Add(panel1);
            openingPanels.Add(panel2);
            openingPanels.Add(panel3);
            openingPanels.Add(panel4);
            openingPanels.Add(panel5);

            foreach (Panel myPanel in openingPanels)
            {
                if (openingPanels.IndexOf(myPanel) < Int32.Parse(numOpeningsComboBox.Text))
                {
                    myPanel.Visible = true;
                }
                else
                {
                    myPanel.Visible = false;
                }

                
            }

            Point pt = panel2.Location;

            pt.Offset(0, distBetPanels * (Int32.Parse(numOpeningsComboBox.Text) - 1));

            panel6.Location = pt;

           // bottomrailLabel.Location.Offset(0, secondOpeningGroupPosition * (Int32.Parse(numOpeningsComboBox.Text) - 1));
        }


       
        private void addTemplateButton_Click(object sender, EventArgs e)
        {

        }

        private bool CheckCabinetFields()
        {
            bool allFieldsCompleted = false;

            if (!string.IsNullOrEmpty(selectedCabinetType) && materialComboBox.SelectedIndex > -1)
                return true;
            else
                return false;
        }

        private void checkHeightTotal(object sender, EventArgs e)
        {
            double calculatedHeight = 0;

            List <TextBox> toAdd = new List<TextBox>();
            toAdd.Add(topRailTextBox);
            toAdd.Add(opening1TextBox);
            toAdd.Add(middleRail1TextBox);
            toAdd.Add(opening2TextBox);
            toAdd.Add(middleRail2TextBox);
            toAdd.Add(opening3TextBox);
            toAdd.Add(middleRail3TextBox);
            toAdd.Add(opening4TextBox);
            toAdd.Add(middleRail4TextBox);
            toAdd.Add(opening5TextBox);
            toAdd.Add(bottomRailTextBox);

            try
            {
                foreach (TextBox myTB in toAdd)
                {
                    if (myTB.Visible.Equals(true))
                    {
                        calculatedHeight += Double.Parse(myTB.Text);
                    }
                }
            

            totalvsheight.Text = "Cabinet Height: " + cabinetDimensionHeightTextBox.Text + "\nCalculated Height: " + calculatedHeight;
            if (Double.Parse(cabinetDimensionHeightTextBox.Text) != calculatedHeight)
            {
                totalvsheight.Font = new Font(totalvsheight.Font.Name, totalvsheight.Font.Size, FontStyle.Bold);
                totalvsheight.ForeColor = Color.Red;
            }
            else
            {
                totalvsheight.Font = new Font(totalvsheight.Font.Name, totalvsheight.Font.Size);
                totalvsheight.ForeColor = Color.Black;
            }
            }
            catch (Exception ex)
            {
            }
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label120_Click(object sender, EventArgs e)
        {

        }

        private void materialComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void boxDrawerCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

      
       
    }
}
