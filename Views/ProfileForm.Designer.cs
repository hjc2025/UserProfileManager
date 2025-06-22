namespace UserProfileManager
{
    partial class ProfileForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblName = new Label();
            lblEmail = new Label();
            lblSaveFormat = new Label();
            btnLoad = new Button();
            btnSave = new Button();
            cmbSaveFormat = new ComboBox();
            txtName = new TextBox();
            txtEmail = new TextBox();
            SuspendLayout();
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(226, 114);
            lblName.Name = "lblName";
            lblName.Size = new Size(104, 41);
            lblName.TabIndex = 0;
            lblName.Text = "Name:";
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(226, 203);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(95, 41);
            lblEmail.TabIndex = 1;
            lblEmail.Text = "Email:";
            // 
            // lblSaveFormat
            // 
            lblSaveFormat.AutoSize = true;
            lblSaveFormat.Location = new Point(226, 295);
            lblSaveFormat.Name = "lblSaveFormat";
            lblSaveFormat.Size = new Size(188, 41);
            lblSaveFormat.TabIndex = 2;
            lblSaveFormat.Text = "Save Format:";
            // 
            // btnLoad
            // 
            btnLoad.Location = new Point(226, 431);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(188, 58);
            btnLoad.TabIndex = 3;
            btnLoad.Text = "Load Data";
            btnLoad.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(914, 431);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(188, 58);
            btnSave.TabIndex = 4;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // cmbSaveFormat
            // 
            cmbSaveFormat.FormattingEnabled = true;
            cmbSaveFormat.Location = new Point(509, 295);
            cmbSaveFormat.Name = "cmbSaveFormat";
            cmbSaveFormat.Size = new Size(302, 49);
            cmbSaveFormat.TabIndex = 5;
            // 
            // txtName
            // 
            txtName.Location = new Point(509, 111);
            txtName.Name = "txtName";
            txtName.Size = new Size(307, 47);
            txtName.TabIndex = 6;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(509, 200);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(593, 47);
            txtEmail.TabIndex = 7;
            // 
            // ProfileForm
            // 
            AutoScaleDimensions = new SizeF(17F, 41F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1341, 634);
            Controls.Add(txtEmail);
            Controls.Add(txtName);
            Controls.Add(cmbSaveFormat);
            Controls.Add(btnSave);
            Controls.Add(btnLoad);
            Controls.Add(lblSaveFormat);
            Controls.Add(lblEmail);
            Controls.Add(lblName);
            Name = "ProfileForm";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblName;
        private Label lblEmail;
        private Label lblSaveFormat;
        private Button btnLoad;
        private Button btnSave;
        private ComboBox cmbSaveFormat;
        private TextBox txtName;
        private TextBox txtEmail;
    }
}
