namespace BackupWizard
{
    partial class BackupWizard
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
            groupBox1 = new GroupBox();
            label2 = new Label();
            label1 = new Label();
            label3 = new Label();
            locTextBox = new TextBox();
            browseBtn = new Button();
            nameTextBox = new TextBox();
            label4 = new Label();
            saveBtn = new Button();
            cancelBtn = new Button();
            sourceBtn = new Button();
            sourceTextBox = new TextBox();
            label5 = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.BackColor = SystemColors.ButtonFace;
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(488, 70);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(28, 41);
            label2.Name = "label2";
            label2.Size = new Size(366, 15);
            label2.TabIndex = 1;
            label2.Text = "Your files and settings will be stored in the destination you specify.";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 18);
            label1.Name = "label1";
            label1.Size = new Size(240, 19);
            label1.TabIndex = 0;
            label1.Text = "Backup Destination and Name";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(13, 140);
            label3.Name = "label3";
            label3.Size = new Size(345, 15);
            label3.TabIndex = 2;
            label3.Text = "Choose the location where you want to save your backup files.";
            // 
            // locTextBox
            // 
            locTextBox.Location = new Point(12, 160);
            locTextBox.Name = "locTextBox";
            locTextBox.Size = new Size(369, 23);
            locTextBox.TabIndex = 3;
            // 
            // browseBtn
            // 
            browseBtn.Location = new Point(388, 160);
            browseBtn.Name = "browseBtn";
            browseBtn.Size = new Size(81, 23);
            browseBtn.TabIndex = 4;
            browseBtn.Text = "Browse";
            browseBtn.UseVisualStyleBackColor = true;
            browseBtn.Click += browseBtn_Click;
            // 
            // nameTextBox
            // 
            nameTextBox.Location = new Point(11, 219);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.Size = new Size(369, 23);
            nameTextBox.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(12, 199);
            label4.Name = "label4";
            label4.Size = new Size(157, 15);
            label4.TabIndex = 5;
            label4.Text = "Type a name for the backup";
            // 
            // saveBtn
            // 
            saveBtn.Location = new Point(305, 276);
            saveBtn.Name = "saveBtn";
            saveBtn.Size = new Size(75, 23);
            saveBtn.TabIndex = 7;
            saveBtn.Text = "Save";
            saveBtn.UseVisualStyleBackColor = true;
            saveBtn.Click += saveBtn_Click;
            // 
            // cancelBtn
            // 
            cancelBtn.Location = new Point(394, 276);
            cancelBtn.Name = "cancelBtn";
            cancelBtn.Size = new Size(75, 23);
            cancelBtn.TabIndex = 8;
            cancelBtn.Text = "Cancel";
            cancelBtn.UseVisualStyleBackColor = true;
            cancelBtn.Click += cancelBtn_Click;
            // 
            // sourceBtn
            // 
            sourceBtn.Location = new Point(388, 101);
            sourceBtn.Name = "sourceBtn";
            sourceBtn.Size = new Size(81, 23);
            sourceBtn.TabIndex = 11;
            sourceBtn.Text = "Browse";
            sourceBtn.UseVisualStyleBackColor = true;
            sourceBtn.Click += sourceBtn_Click;
            // 
            // sourceTextBox
            // 
            sourceTextBox.Location = new Point(12, 101);
            sourceTextBox.Name = "sourceTextBox";
            sourceTextBox.Size = new Size(369, 23);
            sourceTextBox.TabIndex = 10;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(13, 81);
            label5.Name = "label5";
            label5.Size = new Size(164, 15);
            label5.TabIndex = 9;
            label5.Text = "Choose the folder to backup.";
            // 
            // BackupWizard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ScrollBar;
            ClientSize = new Size(484, 311);
            Controls.Add(sourceBtn);
            Controls.Add(sourceTextBox);
            Controls.Add(label5);
            Controls.Add(cancelBtn);
            Controls.Add(saveBtn);
            Controls.Add(nameTextBox);
            Controls.Add(label4);
            Controls.Add(browseBtn);
            Controls.Add(locTextBox);
            Controls.Add(label3);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "BackupWizard";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Backup Wizard";
            Load += BackupWizard_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox locTextBox;
        private Button browseBtn;
        private TextBox nameTextBox;
        private Label label4;
        private Button saveBtn;
        private Button cancelBtn;
        private Button sourceBtn;
        private TextBox sourceTextBox;
        private Label label5;
    }
}
