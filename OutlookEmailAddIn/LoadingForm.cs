using System;
using System.Drawing;
using System.Windows.Forms;

namespace OutlookEmailAddIn
{
    public partial class LoadingForm : Form
    {
        private Label lblLoading;

        public LoadingForm()
        {
            InitializeComponent();

            this.Text = "Please Wait";
            this.Width = 350;
            this.Height = 150;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.ControlBox = false;
            this.ShowInTaskbar = false;

            lblLoading = new Label();
            lblLoading.Text = "AI is checking your email...\nPlease wait.";
            lblLoading.AutoSize = false;
            lblLoading.TextAlign = ContentAlignment.MiddleCenter;
            lblLoading.Dock = DockStyle.Fill;
            lblLoading.Font = new Font(
                "Segoe UI",
                10,
                FontStyle.Regular);

            this.Controls.Add(lblLoading);
        }
    }
}