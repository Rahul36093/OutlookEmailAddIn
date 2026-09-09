namespace OutlookEmailAddIn
{
    partial class Ribbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        private System.ComponentModel.IContainer components = null;

        public Ribbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.grpAIAssist = this.Factory.CreateRibbonGroup();
            this.btnGenerateEmail = this.Factory.CreateRibbonButton();
            this.btnSpellCheck = this.Factory.CreateRibbonButton();
            this.btnLanguageConversion = this.Factory.CreateRibbonButton();
            this.btnReplyAssist = this.Factory.CreateRibbonButton();
            this.btnAIChatbot = this.Factory.CreateRibbonButton();
            this.tabAIAssist = this.Factory.CreateRibbonTab();
            this.grpAIAssist.SuspendLayout();
            this.tabAIAssist.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpAIAssist
            // 
            this.grpAIAssist.Items.Add(this.btnGenerateEmail);
            this.grpAIAssist.Items.Add(this.btnSpellCheck);
            this.grpAIAssist.Items.Add(this.btnLanguageConversion);
            this.grpAIAssist.Items.Add(this.btnReplyAssist);
            this.grpAIAssist.Items.Add(this.btnAIChatbot);
            this.grpAIAssist.Label = "AI Assist";
            this.grpAIAssist.Name = "grpAIAssist";
            // 
            // btnGenerateEmail
            // 
            this.btnGenerateEmail.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnGenerateEmail.Image = global::OutlookEmailAddIn.Properties.Resources.icons8_email_writing_32;
            this.btnGenerateEmail.Label = "Generate Email";
            this.btnGenerateEmail.Name = "btnGenerateEmail";
            this.btnGenerateEmail.ShowImage = true;
            this.btnGenerateEmail.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnGenerateEmail_Click);
            // 
            // btnSpellCheck
            // 
            this.btnSpellCheck.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnSpellCheck.Image = global::OutlookEmailAddIn.Properties.Resources.spell_check_3;
            this.btnSpellCheck.Label = "Spell Check";
            this.btnSpellCheck.Name = "btnSpellCheck";
            this.btnSpellCheck.ShowImage = true;
            this.btnSpellCheck.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnSpellCheck_Click);
            // 
            // btnLanguageConversion
            // 
            this.btnLanguageConversion.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnLanguageConversion.Image = global::OutlookEmailAddIn.Properties.Resources.icons8_language_50;
            this.btnLanguageConversion.Label = "Language Conversion";
            this.btnLanguageConversion.Name = "btnLanguageConversion";
            this.btnLanguageConversion.ShowImage = true;
            this.btnLanguageConversion.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnLanguageConversion_Click);
            // 
            // btnReplyAssist
            // 
            this.btnReplyAssist.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnReplyAssist.Image = global::OutlookEmailAddIn.Properties.Resources.calendar_someday_svgrepo_com;
            this.btnReplyAssist.Label = "Reply Assist";
            this.btnReplyAssist.Name = "btnReplyAssist";
            this.btnReplyAssist.ShowImage = true;
            this.btnReplyAssist.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnReplyAssist_Click);
            // 
            // btnAIChatbot
            // 
            this.btnAIChatbot.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnAIChatbot.Image = global::OutlookEmailAddIn.Properties.Resources.robot_assistant;
            this.btnAIChatbot.Label = "AI Chatbot";
            this.btnAIChatbot.Name = "btnAIChatbot";
            this.btnAIChatbot.ShowImage = true;
            this.btnAIChatbot.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnAIChatbot_Click);
            // 
            // tabAIAssist
            // 
            this.tabAIAssist.Groups.Add(this.grpAIAssist);
            this.tabAIAssist.Label = "AI Assist";
            this.tabAIAssist.Name = "tabAIAssist";
            // 
            // Ribbon
            // 
            this.Name = "Ribbon";
            this.RibbonType = "Microsoft.Outlook.Mail.Compose, Microsoft.Outlook.Mail.Read";
            this.Tabs.Add(this.tabAIAssist);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.Ribbon_Load);
            this.grpAIAssist.ResumeLayout(false);
            this.grpAIAssist.PerformLayout();
            this.tabAIAssist.ResumeLayout(false);
            this.tabAIAssist.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Office.Tools.Ribbon.RibbonButton btnGenerateEmail;
        private Microsoft.Office.Tools.Ribbon.RibbonButton btnSpellCheck;
        private Microsoft.Office.Tools.Ribbon.RibbonButton btnLanguageConversion;
        private Microsoft.Office.Tools.Ribbon.RibbonButton btnReplyAssist;
        private Microsoft.Office.Tools.Ribbon.RibbonButton btnAIChatbot;

        private Microsoft.Office.Tools.Ribbon.RibbonGroup grpAIAssist;
        private Microsoft.Office.Tools.Ribbon.RibbonTab tabAIAssist;
    }
}