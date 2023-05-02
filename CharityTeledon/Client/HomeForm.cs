using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CharityTeledon.model;
using Client;

namespace CharityTeledon.forms
{
    public class HomeForm : Form
    {
        private ClientController ctrl;

        BindingSource bsCases = new BindingSource();
        BindingSource bsDonors = new BindingSource();
        public HomeForm(ClientController ctrl)
        {
            this.ctrl = ctrl;
            InitializeComponent();
            initializeCasesList();
            this.ctrl.updateEvent += userUpdate;
        }
        
        public void userUpdate(object sender, TeledonUserEventArgs e)
        {
            if (e.UserEventType==TeledonUserEvent.UpdatedCase)
            {
                Console.WriteLine("Update event for UpdatedCase in HomeForm");
                BeginInvoke(new UpdateListBoxCallback(initializeCasesList), new object[] {});

            }
            if (e.UserEventType == TeledonUserEvent.AddedDonor)
            {
                Console.WriteLine("Update event for AddedDonor in HomeForm");
                BeginInvoke( new UpdateListBoxCallback(initializeDonorsList), new object[] {});
            }
        }
        public delegate void UpdateListBoxCallback();

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.loggedUser = new System.Windows.Forms.Label();
            this.labelDonors = new System.Windows.Forms.Label();
            this.dataGridViewDonors = new System.Windows.Forms.DataGridView();
            this.labelSum = new System.Windows.Forms.Label();
            this.labelPhone = new System.Windows.Forms.Label();
            this.labelAddress = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.labelCase = new System.Windows.Forms.Label();
            this.textBoxCase = new System.Windows.Forms.TextBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.textBoxAddress = new System.Windows.Forms.TextBox();
            this.textBoxPhone = new System.Windows.Forms.TextBox();
            this.textBoxSum = new System.Windows.Forms.TextBox();
            this.buttonAddDonation = new System.Windows.Forms.Button();
            this.buttonNewDonation = new System.Windows.Forms.Button();
            this.dataGridViewCases = new System.Windows.Forms.DataGridView();
            this.labelCases = new System.Windows.Forms.Label();
            this.logoutButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDonors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCases)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.logoutButton);
            this.panel1.Controls.Add(this.loggedUser);
            this.panel1.Controls.Add(this.labelDonors);
            this.panel1.Controls.Add(this.dataGridViewDonors);
            this.panel1.Controls.Add(this.labelSum);
            this.panel1.Controls.Add(this.labelPhone);
            this.panel1.Controls.Add(this.labelAddress);
            this.panel1.Controls.Add(this.labelName);
            this.panel1.Controls.Add(this.labelCase);
            this.panel1.Controls.Add(this.textBoxCase);
            this.panel1.Controls.Add(this.textBoxName);
            this.panel1.Controls.Add(this.textBoxAddress);
            this.panel1.Controls.Add(this.textBoxPhone);
            this.panel1.Controls.Add(this.textBoxSum);
            this.panel1.Controls.Add(this.buttonAddDonation);
            this.panel1.Controls.Add(this.buttonNewDonation);
            this.panel1.Controls.Add(this.dataGridViewCases);
            this.panel1.Controls.Add(this.labelCases);
            this.panel1.Location = new System.Drawing.Point(0, -2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(975, 631);
            this.panel1.TabIndex = 0;
            // 
            // loggedUser
            // 
            this.loggedUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.loggedUser.Location = new System.Drawing.Point(600, 10);
            this.loggedUser.Name = "loggedUser";
            this.loggedUser.Size = new System.Drawing.Size(239, 31);
            this.loggedUser.TabIndex = 17;
            this.loggedUser.Text = ctrl.getLoggedUser().Username;
            // 
            // labelDonors
            // 
            this.labelDonors.Font = new System.Drawing.Font("Modern No. 20", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDonors.Location = new System.Drawing.Point(508, 84);
            this.labelDonors.Name = "labelDonors";
            this.labelDonors.Size = new System.Drawing.Size(413, 37);
            this.labelDonors.TabIndex = 16;
            this.labelDonors.Text = "Donors";
            this.labelDonors.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dataGridViewDonors
            // 
            this.dataGridViewDonors.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewDonors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDonors.Location = new System.Drawing.Point(506, 174);
            this.dataGridViewDonors.Name = "dataGridViewDonors";
            this.dataGridViewDonors.RowTemplate.Height = 28;
            this.dataGridViewDonors.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewDonors.Size = new System.Drawing.Size(415, 131);
            this.dataGridViewDonors.TabIndex = 15;
            this.dataGridViewDonors.Visible = false;
            this.dataGridViewDonors.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewDonors_CellContentClick);
            // 
            // labelSum
            // 
            this.labelSum.Location = new System.Drawing.Point(508, 512);
            this.labelSum.Name = "labelSum";
            this.labelSum.Size = new System.Drawing.Size(151, 21);
            this.labelSum.TabIndex = 14;
            this.labelSum.Text = "Sum";
            this.labelSum.Visible = false;
            // 
            // labelPhone
            // 
            this.labelPhone.Location = new System.Drawing.Point(508, 464);
            this.labelPhone.Name = "labelPhone";
            this.labelPhone.Size = new System.Drawing.Size(176, 21);
            this.labelPhone.TabIndex = 13;
            this.labelPhone.Text = "Donor Phone Number";
            this.labelPhone.Visible = false;
            // 
            // labelAddress
            // 
            this.labelAddress.Location = new System.Drawing.Point(508, 419);
            this.labelAddress.Name = "labelAddress";
            this.labelAddress.Size = new System.Drawing.Size(163, 22);
            this.labelAddress.TabIndex = 12;
            this.labelAddress.Text = "Donor Address";
            this.labelAddress.Visible = false;
            // 
            // labelName
            // 
            this.labelName.Location = new System.Drawing.Point(508, 372);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(142, 25);
            this.labelName.TabIndex = 11;
            this.labelName.Text = "Donor Name";
            this.labelName.Visible = false;
            // 
            // labelCase
            // 
            this.labelCase.Location = new System.Drawing.Point(508, 326);
            this.labelCase.Name = "labelCase";
            this.labelCase.Size = new System.Drawing.Size(147, 32);
            this.labelCase.TabIndex = 10;
            this.labelCase.Text = "Case";
            this.labelCase.Visible = false;
            // 
            // textBoxCase
            // 
            this.textBoxCase.Location = new System.Drawing.Point(707, 323);
            this.textBoxCase.Name = "textBoxCase";
            this.textBoxCase.ReadOnly = true;
            this.textBoxCase.Size = new System.Drawing.Size(215, 26);
            this.textBoxCase.TabIndex = 9;
            this.textBoxCase.Visible = false;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(707, 372);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(215, 26);
            this.textBoxName.TabIndex = 8;
            this.textBoxName.Visible = false;
            // 
            // textBoxAddress
            // 
            this.textBoxAddress.Location = new System.Drawing.Point(707, 419);
            this.textBoxAddress.Name = "textBoxAddress";
            this.textBoxAddress.Size = new System.Drawing.Size(215, 26);
            this.textBoxAddress.TabIndex = 7;
            this.textBoxAddress.Visible = false;
            // 
            // textBoxPhone
            // 
            this.textBoxPhone.Location = new System.Drawing.Point(707, 464);
            this.textBoxPhone.Name = "textBoxPhone";
            this.textBoxPhone.Size = new System.Drawing.Size(215, 26);
            this.textBoxPhone.TabIndex = 6;
            this.textBoxPhone.Visible = false;
            // 
            // textBoxSum
            // 
            this.textBoxSum.Location = new System.Drawing.Point(707, 509);
            this.textBoxSum.Name = "textBoxSum";
            this.textBoxSum.Size = new System.Drawing.Size(215, 26);
            this.textBoxSum.TabIndex = 5;
            this.textBoxSum.Visible = false;
            // 
            // buttonAddDonation
            // 
            this.buttonAddDonation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonAddDonation.Location = new System.Drawing.Point(635, 566);
            this.buttonAddDonation.Name = "buttonAddDonation";
            this.buttonAddDonation.Size = new System.Drawing.Size(205, 38);
            this.buttonAddDonation.TabIndex = 4;
            this.buttonAddDonation.Text = "Add Donation";
            this.buttonAddDonation.UseVisualStyleBackColor = true;
            this.buttonAddDonation.Visible = false;
            this.buttonAddDonation.Click += new System.EventHandler(this.buttonAddDonation_Click);
            // 
            // buttonNewDonation
            // 
            this.buttonNewDonation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonNewDonation.Location = new System.Drawing.Point(89, 566);
            this.buttonNewDonation.Name = "buttonNewDonation";
            this.buttonNewDonation.Size = new System.Drawing.Size(205, 38);
            this.buttonNewDonation.TabIndex = 3;
            this.buttonNewDonation.Text = "New Donation";
            this.buttonNewDonation.UseVisualStyleBackColor = true;
            this.buttonNewDonation.Click += new System.EventHandler(this.buttonNewDonation_Click);
            // 
            // dataGridViewCases
            // 
            this.dataGridViewCases.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCases.Location = new System.Drawing.Point(24, 136);
            this.dataGridViewCases.Name = "dataGridViewCases";
            this.dataGridViewCases.RowTemplate.Height = 28;
            this.dataGridViewCases.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewCases.Size = new System.Drawing.Size(342, 399);
            this.dataGridViewCases.TabIndex = 2;
            this.dataGridViewCases.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewCases_CellContentClick);
            // 
            // labelCases
            // 
            this.labelCases.Font = new System.Drawing.Font("Modern No. 20", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCases.Location = new System.Drawing.Point(24, 84);
            this.labelCases.Name = "labelCases";
            this.labelCases.Size = new System.Drawing.Size(342, 37);
            this.labelCases.TabIndex = 1;
            this.labelCases.Text = "Cases";
            this.labelCases.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // logoutButton
            // 
            this.logoutButton.Location = new System.Drawing.Point(859, 6);
            this.logoutButton.Name = "logoutButton";
            this.logoutButton.Size = new System.Drawing.Size(105, 36);
            this.logoutButton.TabIndex = 18;
            this.logoutButton.Text = "LogOut";
            this.logoutButton.UseVisualStyleBackColor = true;
            this.logoutButton.Click += new System.EventHandler(this.logoutButton_Click);
            // 
            // HomeForm
            // 
            this.ClientSize = new System.Drawing.Size(976, 628);
            this.Controls.Add(this.panel1);
            this.Name = "HomeForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDonors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCases)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button logoutButton;

        private System.Windows.Forms.Label loggedUser;

        private System.Windows.Forms.Label labelDonors;

        private System.Windows.Forms.DataGridView dataGridViewDonors;

        private System.Windows.Forms.Label labelCase;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelAddress;
        private System.Windows.Forms.Label labelPhone;
        private System.Windows.Forms.Label labelSum;

        private System.Windows.Forms.TextBox textBoxSum;
        private System.Windows.Forms.TextBox textBoxPhone;
        private System.Windows.Forms.TextBox textBoxAddress;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.TextBox textBoxCase;

        private System.Windows.Forms.Button buttonAddDonation;

        private System.Windows.Forms.Button buttonNewDonation;

        private System.Windows.Forms.DataGridView dataGridViewCases;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelCases;

        private void initializeCasesList()
        {
            bsCases.DataSource = ctrl.GetAllCases();
            dataGridViewCases.DataSource = bsCases;
            dataGridViewCases.Columns["Id"].Visible = true;
            dataGridViewCases.ClearSelection();
        }
        
        private void initializeDonorsList()
        {
            bsDonors.DataSource = ctrl.GetAllDonors();
            dataGridViewDonors.DataSource = bsDonors;
            dataGridViewDonors.Columns["Id"].Visible = true;
            dataGridViewDonors.ClearSelection();
        }

        private void showDonationInputs()
        {
            dataGridViewDonors.Visible = true;
            labelCase.Visible = true;
            textBoxCase.Visible = true;
            labelName.Visible = true;
            textBoxName.Visible = true;
            labelAddress.Visible = true;
            textBoxAddress.Visible = true;
            labelPhone.Visible = true;
            textBoxPhone.Visible = true;
            labelSum.Visible = true;
            textBoxSum.Visible = true;
            buttonAddDonation.Visible = true;
        }
        private void dataGridViewCases_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            buttonNewDonation.Visible = true;
        }

        private void buttonNewDonation_Click(object sender, EventArgs e)
        {
            showDonationInputs();
            initializeDonorsList();
            string caseName = dataGridViewCases.CurrentRow.Cells[0].Value.ToString();
            textBoxCase.Text = caseName;
        }

        private void dataGridViewDonors_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string donorName = dataGridViewDonors.CurrentRow.Cells[0].Value.ToString();
            string donorAddress = dataGridViewDonors.CurrentRow.Cells[1].Value.ToString();
            string donorPhoneNumber = dataGridViewDonors.CurrentRow.Cells[2].Value.ToString();
            textBoxName.Text = donorName;
            textBoxAddress.Text = donorAddress;
            textBoxPhone.Text = donorPhoneNumber;
        }

        private void buttonAddDonation_Click(object sender, EventArgs e)
        {
            int idCase = Convert.ToInt32(dataGridViewCases.CurrentRow.Cells[2].Value);
            string donorName = textBoxName.Text;
            string donorAddress = textBoxAddress.Text;
            string donorPhoneNumber = textBoxPhone.Text;
            float sum = float.Parse(textBoxSum.Text);

            Donor donor = ctrl.FindDonorByName(donorName);
            int donorId;
            
            if (donor == null)
            {
                Console.WriteLine("Donor is not in database");
                Donor newDonor = new Donor(donorName, donorAddress, donorPhoneNumber);
                ctrl.AddDonor(newDonor);
                Donor addedDonor = ctrl.FindDonorByName(donorName);
                donorId = addedDonor.Id;
                //initializeDonorsList();
            }
            else
            {
                Console.WriteLine("Donor is in database");
                donorId = donor.Id;
            }

            Donation donation = new Donation(sum, idCase, donorId);
            Console.WriteLine("Created new donation");
            ctrl.AddDonation(donation);
            Console.WriteLine("Added new donation");
            //initializeCasesList();
        }

        private void logoutButton_Click(object sender, EventArgs e)
        {
            ctrl.Logout(ctrl.getLoggedUser());
            this.Hide();
            LoginForm loginForm = new LoginForm(ctrl);
            loginForm.Show();
        }
    }
}