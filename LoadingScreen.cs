using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System;

namespace BackupWizard
{
    public partial class LoadingScreen : Form
    {
      
        public LoadingScreen()
        {
            InitializeComponent();

       
            if (progressBar != null)
            {
                progressBar.Minimum = 0;
                progressBar.Maximum = 100;
                progressBar.Value = 0;
            }

            if (label1 != null)
            {
                label1.Text = "Starting backup process...";
            }
        }
    }
}