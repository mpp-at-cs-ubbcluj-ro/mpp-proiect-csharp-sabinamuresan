using System;
using System.Threading;
using System.Windows.Forms;
using CharityTeledon.model;
using CharityTeledon.service;

namespace CharityTeledon.forms
{
    public class LoginForm : Form
    {
        private Service _service;
        public LoginForm(Service service)
        {
            _service = service;
            InitializeComponent();
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.passwordField = new System.Windows.Forms.TextBox();
            this.usernameField = new System.Windows.Forms.TextBox();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonLogin);
            this.panel1.Controls.Add(this.passwordField);
            this.panel1.Controls.Add(this.usernameField);
            this.panel1.Controls.Add(this.passwordLabel);
            this.panel1.Controls.Add(this.usernameLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(595, 414);
            this.panel1.TabIndex = 0;
            // 
            // buttonLogin
            // 
            this.buttonLogin.Font = new System.Drawing.Font("Modern No. 20", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLogin.Location = new System.Drawing.Point(171, 295);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(220, 49);
            this.buttonLogin.TabIndex = 4;
            this.buttonLogin.Text = "LogIn";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // passwordField
            // 
            this.passwordField.Location = new System.Drawing.Point(264, 176);
            this.passwordField.Name = "passwordField";
            this.passwordField.PasswordChar = '*';
            this.passwordField.Size = new System.Drawing.Size(229, 26);
            this.passwordField.TabIndex = 3;
            // 
            // usernameField
            // 
            this.usernameField.Location = new System.Drawing.Point(264, 102);
            this.usernameField.Name = "usernameField";
            this.usernameField.Size = new System.Drawing.Size(229, 26);
            this.usernameField.TabIndex = 2;
            // 
            // passwordLabel
            // 
            this.passwordLabel.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwordLabel.Location = new System.Drawing.Point(70, 175);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(171, 55);
            this.passwordLabel.TabIndex = 1;
            this.passwordLabel.Text = "password";
            // 
            // usernameLabel
            // 
            this.usernameLabel.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernameLabel.Location = new System.Drawing.Point(70, 101);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(187, 43);
            this.usernameLabel.TabIndex = 0;
            this.usernameLabel.Text = "username";
            // 
            // LoginForm
            // 
            this.ClientSize = new System.Drawing.Size(595, 414);
            this.Controls.Add(this.panel1);
            this.Name = "LoginForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.TextBox passwordField;
        private System.Windows.Forms.Button buttonLogin;

        private System.Windows.Forms.TextBox usernameField;

        private System.Windows.Forms.Label passwordLabel;

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label usernameLabel;
        

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string username = usernameField.Text;
            if (username.Length == 0)
            {
                MessageBox.Show("Please enter the username!");
            }
            string password = passwordField.Text;
            if (password.Length == 0)
            {
                MessageBox.Show("Please enter the password!");
            }

            Volunteer volunteer = _service.FindVolunteerAccount(username, password);
            if (volunteer == null)
            {
                MessageBox.Show("Username or Password is wrong!");
            }
            else
            {
                this.Hide();
                HomeForm homeForm = new HomeForm(_service);
                homeForm.Show();
            }
        }
    }
}