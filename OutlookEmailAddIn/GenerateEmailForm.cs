
using System;
using System.Windows.Forms;

namespace OutlookEmailAddIn
{
    public partial class GenerateEmailForm : Form
    {
        private TextBox txtInstruction;
        private Button btnGenerate;
        private Button btnCancel;
        private Label lblInstruction;

        public string UserInstruction { get; private set; }

        public GenerateEmailForm()
        {
            InitializeComponent();

            this.Text = "Generate Email";
            this.Width = 500;
            this.Height = 300;
            this.StartPosition = FormStartPosition.CenterScreen;

            lblInstruction = new Label();
            lblInstruction.Text = "Enter your email instruction:";
            lblInstruction.Left = 20;
            lblInstruction.Top = 20;
            lblInstruction.Width = 400;

            txtInstruction = new TextBox();
            txtInstruction.Left = 20;
            txtInstruction.Top = 50;
            txtInstruction.Width = 440;
            txtInstruction.Height = 100;
            txtInstruction.Multiline = true;
            txtInstruction.ScrollBars = ScrollBars.Vertical;

            btnGenerate = new Button();
            btnGenerate.Text = "Generate";
            btnGenerate.Left = 280;
            btnGenerate.Top = 180;
            btnGenerate.Width = 100;
            btnGenerate.Click += BtnGenerate_Click;

            btnCancel = new Button();
            btnCancel.Text = "Cancel";
            btnCancel.Left = 390;
            btnCancel.Top = 180;
            btnCancel.Width = 70;
            btnCancel.DialogResult = DialogResult.Cancel;

            this.Controls.Add(lblInstruction);
            this.Controls.Add(txtInstruction);
            this.Controls.Add(btnGenerate);
            this.Controls.Add(btnCancel);

            this.AcceptButton = btnGenerate;
            this.CancelButton = btnCancel;
        }

        private void BtnGenerate_Click(
            object sender,
            EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(
                txtInstruction.Text))
            {
                MessageBox.Show(
                    "Please enter an instruction.",
                    "Generate Email",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            UserInstruction =
                txtInstruction.Text.Trim();

            this.DialogResult =
                DialogResult.OK;

            this.Close();
        }
    }
}

