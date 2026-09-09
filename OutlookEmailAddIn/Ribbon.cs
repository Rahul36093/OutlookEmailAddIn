using Microsoft.Office.Tools.Ribbon;
using OutlookEmailAddIn.Models;
using OutlookEmailAddIn.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace OutlookEmailAddIn
{
    public partial class Ribbon
    {
        private void Ribbon_Load(
            object sender,
            RibbonUIEventArgs e)
        {
        }

        // =====================================================
        // GENERATE EMAIL
        // =====================================================

        private async void btnGenerateEmail_Click(
            object sender,
            RibbonControlEventArgs e)
        {
            try
            {
                using (var instructionForm =
                       new GenerateEmailForm())
                {
                    if (instructionForm.ShowDialog() !=
                        DialogResult.OK)
                    {
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(
                        instructionForm.UserInstruction))
                    {
                        MessageBox.Show(
                            "Please enter an email instruction.",
                            "Generate Email",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        return;
                    }

                    var request =
                        new GenerateEmailRequest
                        {
                            UserInstruction =
                                instructionForm.UserInstruction
                        };

                    using (var loadingForm =
                           new LoadingForm())
                    {
                        loadingForm.Show();
                        loadingForm.Refresh();

                        var geminiService =
                            new GeminiService();

                        string generatedEmail =
                            await geminiService
                                .GenerateEmailAsync(request);

                        loadingForm.Close();

                        if (string.IsNullOrWhiteSpace(
                            generatedEmail))
                        {
                            MessageBox.Show(
                                "The AI returned an empty email.",
                                "Generate Email",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                            return;
                        }

                        // =====================================
                        // CREATE NEW OUTLOOK EMAIL
                        // =====================================

                        Outlook.Application outlookApp =
                            Globals.ThisAddIn.Application;

                        Outlook.MailItem newMail =
                            (Outlook.MailItem)
                            outlookApp.CreateItem(
                                Outlook.OlItemType.olMailItem);

                        // =====================================
                        // SPLIT AI RESPONSE INTO SUBJECT + BODY
                        // =====================================

                        string subject = "";
                        string body = generatedEmail;

                        int subjectIndex =
                            generatedEmail.IndexOf(
                                "Subject:",
                                StringComparison.OrdinalIgnoreCase);

                        int bodyIndex =
                            generatedEmail.IndexOf(
                                "Body:",
                                StringComparison.OrdinalIgnoreCase);

                        if (subjectIndex >= 0)
                        {
                            int subjectStart =
                                subjectIndex +
                                "Subject:".Length;

                            if (bodyIndex > subjectStart)
                            {
                                subject =
                                    generatedEmail
                                        .Substring(
                                            subjectStart,
                                            bodyIndex -
                                            subjectStart)
                                        .Trim();
                            }
                            else
                            {
                                subject =
                                    generatedEmail
                                        .Substring(subjectStart)
                                        .Trim();
                            }
                        }

                        if (bodyIndex >= 0)
                        {
                            int bodyStart =
                                bodyIndex +
                                "Body:".Length;

                            body =
                                generatedEmail
                                    .Substring(bodyStart)
                                    .Trim();
                        }

                        // =====================================
                        // PUT SUBJECT AND BODY INTO OUTLOOK
                        // =====================================

                        newMail.Subject = subject;
                        newMail.Body = body;

                        // Display the new email.
                        newMail.Display(false);
                    }
                }
            }
            catch (System.Net.Http.HttpRequestException)
            {
                MessageBox.Show(
                    "Network error. Please check your internet connection and try again.",
                    "Generate Email",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (TaskCanceledException)
            {
                MessageBox.Show(
                    "The AI request timed out. Please try again.",
                    "Generate Email",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Generate Email Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =====================================================
        // SPELL CHECK
        // =====================================================

        private async void btnSpellCheck_Click(
            object sender,
            RibbonControlEventArgs e)
        {
            try
            {
                Outlook.Application outlookApp =
                    Globals.ThisAddIn.Application;

                Outlook.MailItem mailItem =
                    GetActiveMailItem(outlookApp);

                if (mailItem == null)
                {
                    MessageBox.Show(
                        "Please open or select an email before using Spell Check.",
                        "Spell Check",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                string subject =
                    mailItem.Subject ?? "";

                string body =
                    mailItem.Body ?? "";

                if (string.IsNullOrWhiteSpace(subject) &&
                    string.IsNullOrWhiteSpace(body))
                {
                    MessageBox.Show(
                        "The email subject and body are both empty.",
                        "Spell Check",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                using (var loadingForm =
                       new LoadingForm())
                {
                    loadingForm.Show();
                    loadingForm.Refresh();

                    var aiService =
                        new AIService();

                    var result =
                        await aiService.SpellCheckAsync(
                            subject,
                            body);

                    loadingForm.Close();

                    if (!string.IsNullOrWhiteSpace(
                        result.Subject))
                    {
                        mailItem.Subject =
                            result.Subject;
                    }

                    if (!string.IsNullOrWhiteSpace(
                        result.Body))
                    {
                        mailItem.Body =
                            result.Body;
                    }

                    mailItem.Save();
                }

                MessageBox.Show(
                    "Spell check and grammar correction completed successfully.",
                    "Spell Check",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Spell Check Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =====================================================
        // LANGUAGE CONVERSION
        // =====================================================

        private async void btnLanguageConversion_Click(
            object sender,
            RibbonControlEventArgs e)
        {
            try
            {
                Outlook.Application outlookApp =
                    Globals.ThisAddIn.Application;

                Outlook.MailItem mailItem =
                    GetActiveMailItem(outlookApp);

                if (mailItem == null)
                {
                    MessageBox.Show(
                        "Please open or select an email before using Language Conversion.",
                        "Language Conversion",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                string subject =
                    mailItem.Subject ?? "";

                string body =
                    mailItem.Body ?? "";

                if (string.IsNullOrWhiteSpace(subject) &&
                    string.IsNullOrWhiteSpace(body))
                {
                    MessageBox.Show(
                        "The email subject and body are both empty.",
                        "Language Conversion",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                using (var languageForm =
                       new LanguageConversionForm())
                {
                    if (languageForm.ShowDialog() !=
                        DialogResult.OK)
                    {
                        return;
                    }

                    string targetLanguage =
                        languageForm.TargetLanguage;

                    if (string.IsNullOrWhiteSpace(
                        targetLanguage))
                    {
                        MessageBox.Show(
                            "Please select a target language.",
                            "Language Conversion",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        return;
                    }

                    using (var loadingForm =
                           new LoadingForm())
                    {
                        loadingForm.Show();
                        loadingForm.Refresh();

                        var aiService =
                            new AIService();

                        var result =
                            await aiService.TranslateEmailAsync(
                                subject,
                                body,
                                targetLanguage);

                        loadingForm.Close();

                        if (!string.IsNullOrWhiteSpace(
                            result.Subject))
                        {
                            mailItem.Subject =
                                result.Subject;
                        }

                        if (!string.IsNullOrWhiteSpace(
                            result.Body))
                        {
                            mailItem.Body =
                                result.Body;
                        }

                        mailItem.Save();
                    }
                }

                MessageBox.Show(
                    "Email converted successfully.",
                    "Language Conversion",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Language Conversion Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =====================================================
        // REPLY ASSIST
        // =====================================================

        private async void btnReplyAssist_Click(
            object sender,
            RibbonControlEventArgs e)
        {
            try
            {
                Outlook.Application outlookApp =
                    Globals.ThisAddIn.Application;

                Outlook.MailItem mailItem =
                    GetActiveMailItem(outlookApp);

                if (mailItem == null)
                {
                    MessageBox.Show(
                        "Please open or select an email before using Reply Assist.",
                        "Reply Assist",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                string body =
                    mailItem.Body ?? "";

                if (string.IsNullOrWhiteSpace(body))
                {
                    MessageBox.Show(
                        "The selected email does not contain any content.",
                        "Reply Assist",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                using (var replyForm =
                       new ReplyAssistDesignForm())
                {
                    replyForm.SetEmailContent(body);

                    if (replyForm.ShowDialog() !=
                        DialogResult.OK)
                    {
                        return;
                    }

                    string generatedReply =
                        replyForm.GeneratedReply;

                    if (string.IsNullOrWhiteSpace(
                        generatedReply))
                    {
                        return;
                    }

                    mailItem.Body =
                        generatedReply +
                        "\r\n\r\n" +
                        mailItem.Body;

                    mailItem.Save();
                }

                MessageBox.Show(
                    "AI reply generated and inserted successfully.",
                    "Reply Assist",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Reply Assist Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =====================================================
        // AI CHATBOT
        // =====================================================

        private void btnAIChatbot_Click(
            object sender,
            RibbonControlEventArgs e)
        {
            ChatbotForm chatbotForm =
                new ChatbotForm();

            chatbotForm.Show();
        }

        // =====================================================
        // GET ACTIVE OUTLOOK MAIL ITEM
        // =====================================================

        private Outlook.MailItem GetActiveMailItem(
            Outlook.Application outlookApp)
        {
            try
            {
                if (outlookApp.ActiveInspector() != null)
                {
                    object currentItem =
                        outlookApp.ActiveInspector()
                            .CurrentItem;

                    if (currentItem is Outlook.MailItem)
                    {
                        return (Outlook.MailItem)
                            currentItem;
                    }
                }

                if (outlookApp.ActiveExplorer() != null)
                {
                    Outlook.Selection selection =
                        outlookApp.ActiveExplorer()
                            .Selection;

                    if (selection != null &&
                        selection.Count > 0)
                    {
                        object selectedItem =
                            selection[1];

                        if (selectedItem is Outlook.MailItem)
                        {
                            return (Outlook.MailItem)
                                selectedItem;
                        }
                    }
                }
            }
            catch
            {
            }

            return null;
        }
    }
}