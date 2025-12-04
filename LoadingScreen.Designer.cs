namespace BackupWizard
{
    partial class LoadingScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            progressBar = new ProgressBar();
            label1 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // progressBar
            // 
            progressBar.Location = new Point(12, 71);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(346, 15);
            progressBar.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 53);
            label1.Name = "label1";
            label1.Size = new Size(0, 15);
            label1.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 20);
            label2.Name = "label2";
            label2.Size = new Size(103, 15);
            label2.TabIndex = 2;
            label2.Text = "Copying Backup...";
            // 
            // LoadingScreen
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(370, 98);
            ControlBox = false;
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(progressBar);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "LoadingScreen";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Compressing";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        public Label label1;
        public ProgressBar progressBar;
        private Label label2;
    }
}