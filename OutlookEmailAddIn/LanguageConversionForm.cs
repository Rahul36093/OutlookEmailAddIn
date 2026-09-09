using System;
using System.Windows.Forms;

namespace OutlookEmailAddIn
{
    public partial class LanguageConversionForm : Form
    {
        public string TargetLanguage { get; private set; }

        public LanguageConversionForm()
        {
            InitializeComponent();

            cmbLanguage.Items.AddRange(new object[]
            {
                "English",
                "Malayalam",
                "Hindi",
                "Tamil",
                "Telugu",
                "Kannada",
                "Bengali",
                "Marathi",
                "Gujarati",
                "French",
                "German",
                "Spanish",
                "Italian",
                "Portuguese",
                "Japanese",
                "Korean",
                "Chinese"
            });
        }

        private void BtnConvert_Click(
            object sender,
            EventArgs e)
        {
            if (cmbLanguage.SelectedItem == null)
            {
                MessageBox.Show(
                    "Please select a target language.",
                    "Language Conversion",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            TargetLanguage =
                cmbLanguage.SelectedItem.ToString();

            this.DialogResult =
                DialogResult.OK;

            this.Close();
        }

        private void LanguageConversionForm_Load(
            object sender,
            EventArgs e)
        {
        }
    }
}