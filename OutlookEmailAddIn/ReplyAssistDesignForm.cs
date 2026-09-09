
using System;
using System.Windows.Forms;
using OutlookEmailAddIn.Models;
using OutlookEmailAddIn.Services;

namespace OutlookEmailAddIn
{
    public partial class ReplyAssistDesignForm : Form
    {
        public ReplyAssistDesignForm()
        {
            InitializeComponent();

            // Select Professional by default
            if (cmbTone.Items.Count > 0)
            {
                cmbTone.SelectedIndex = 0;
            }

            // Connect buttons
            btnGenerateReply.Click += BtnGenerateReply_Click;
            btnCopyReply.Click += BtnCopyReply_Click;
            btnInsertReply.Click += BtnInsertReply_Click;
        }

        // =====================================================
        // RECEIVE EMAIL CONTENT FROM OUTLOOK
        // =====================================================

        public void SetEmailContent(string content)
        {
            txtEmailContent.Text = content ?? "";
        }

        // =====================================================
        // GENERATED REPLY
        // =====================================================

        public string GeneratedReply
        {
            get
            {
                return txtGeneratedReply.Text;
            }
        }

        // =====================================================
        // EMAIL CONTENT
        // =====================================================

        public string EmailContent
        {
            get
            {
                return txtEmailContent.Text;
            }
        }

        // =====================================================
        // SELECTED TONE
        // =====================================================

        public string SelectedTone
        {
            get
            {
                if (cmbTone.SelectedItem == null)
                {
                    return "";
                }

                return cmbTone.SelectedItem.ToString();
            }
        }

        // =====================================================
        // GENERATE REPLY
        // =====================================================

        private async void BtnGenerateReply_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                string emailContent =
                    txtEmailContent.Text.Trim();

                string tone =
                    SelectedTone;

                if (string.IsNullOrWhiteSpace(emailContent))
                {
                    MessageBox.Show(
                        "Please enter the email content.",
                        "Reply Assist",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                if (string.IsNullOrWhiteSpace(tone))
                {
                    MessageBox.Show(
                        "Please select a reply tone.",
                        "Reply Assist",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                btnGenerateReply.Enabled = false;
                btnGenerateReply.Text = "Generating...";

                ReplyAssistRequest request =
                    new ReplyAssistRequest
                    {
                        EmailContent = emailContent,
                        Tone = tone
                    };

                ReplyAssistService service =
                    new ReplyAssistService();

                string reply =
                    await service.GenerateReplyAsync(request);

                if (string.IsNullOrWhiteSpace(reply))
                {
                    MessageBox.Show(
                        "The AI returned an empty reply.",
                        "Reply Assist",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                txtGeneratedReply.Text = reply;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Reply Assist Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                btnGenerateReply.Enabled = true;
                btnGenerateReply.Text = "Generate Reply";
            }
        }

        // =====================================================
        // COPY REPLY
        // =====================================================

        private void BtnCopyReply_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                string reply =
                    txtGeneratedReply.Text.Trim();

                if (string.IsNullOrWhiteSpace(reply))
                {
                    MessageBox.Show(
                        "There is no generated reply to copy.",
                        "Reply Assist",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                Clipboard.SetText(reply);

                MessageBox.Show(
                    "Reply copied to clipboard.",
                    "Reply Assist",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Copy Reply Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =====================================================
        // INSERT REPLY
        // =====================================================

        private void BtnInsertReply_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                string reply =
                    txtGeneratedReply.Text.Trim();

                if (string.IsNullOrWhiteSpace(reply))
                {
                    MessageBox.Show(
                        "There is no generated reply to insert.",
                        "Reply Assist",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                // The Ribbon will handle inserting the
                // generated reply into Outlook.
                DialogResult = DialogResult.OK;

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Insert Reply Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
