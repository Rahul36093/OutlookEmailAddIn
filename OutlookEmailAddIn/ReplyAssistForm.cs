using System;
using System.Drawing;
using System.Windows.Forms;

namespace OutlookEmailAddIn
{
    public partial class ReplyAssistForm : Form
    {
        private Label lblMailContent;
        private TextBox txtMailContent;
        private Label lblInstructions;
        private TextBox txtInstructions;
        private Button btnGenerate;
        private Button btnCancel;

        public string MailContent { get; private set; }
        public string Instructions { get; private set; }

        public ReplyAssistForm()
        {
            InitializeComponent();

            this.Text = "AI Reply Assist";
            this.Width = 600;
            this.Height = 500;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Mail Content Label
            lblMailContent = new Label();
            lblMailContent.Text = "Mail Content:";
            lblMailContent.Left = 20;
            lblMailContent.Top = 20;
            lblMailContent.Width = 150;

            // Mail Content TextBox
            txtMailContent = new TextBox();
            txtMailContent.Left = 20;
            txtMailContent.Top = 45;
            txtMailContent.Width = 540;
            txtMailContent.Height = 150;
            txtMailContent.Multiline = true;
            txtMailContent.ScrollBars = ScrollBars.Vertical;

            // Instructions Label
            lblInstructions = new Label();
            lblInstructions.Text = "Reply Instructions:";
            lblInstructions.Left = 20;
            lblInstructions.Top = 215;
            lblInstructions.Width = 200;

            // Instructions TextBox
            txtInstructions = new TextBox();
            txtInstructions.Left = 20;
            txtInstructions.Top = 240;
            txtInstructions.Width = 540;
            txtInstructions.Height = 100;
            txtInstructions.Multiline = true;
            txtInstructions.ScrollBars = ScrollBars.Vertical;

            // Generate Button
            btnGenerate = new Button();
            btnGenerate.Text = "Generate Reply";
            btnGenerate.Left = 350;
            btnGenerate.Top = 370;
            btnGenerate.Width = 100;
            btnGenerate.Click += BtnGenerate_Click;

            // Cancel Button
            btnCancel = new Button();
            btnCancel.Text = "Cancel";
            btnCancel.Left = 460;
            btnCancel.Top = 370;
            btnCancel.Width = 100;
            btnCancel.DialogResult = DialogResult.Cancel;

            // Add controls
            this.Controls.Add(lblMailContent);
            this.Controls.Add(txtMailContent);
            this.Controls.Add(lblInstructions);
            this.Controls.Add(txtInstructions);
            this.Controls.Add(btnGenerate);
            this.Controls.Add(btnCancel);

            this.CancelButton = btnCancel;
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMailContent.Text))
            {
                MessageBox.Show(
                    "Mail content cannot be empty.",
                    "Reply Assist",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (string.IsNullOrWhiteSpace(txtInstructions.Text))
            {
                MessageBox.Show(
                    "Reply instructions cannot be empty.",
                    "Reply Assist",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            MailContent = txtMailContent.Text.Trim();
            Instructions = txtInstructions.Text.Trim();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}