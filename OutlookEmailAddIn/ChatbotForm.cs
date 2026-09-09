using OutlookEmailAddIn.Models;
using OutlookEmailAddIn.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OutlookEmailAddIn
{
    public partial class ChatbotForm : Form
    {
        private readonly AIService _aiService;

        private readonly List<ChatMessage> _conversationHistory;

        public ChatbotForm()
        {
            InitializeComponent();

            _aiService = new AIService();

            _conversationHistory =
                new List<ChatMessage>();

            // Connect buttons
            btnSend.Click += BtnSend_Click;
            btnClearChat.Click += BtnClearChat_Click;

            // Allow Enter to send message
            txtMessage.KeyDown += TxtMessage_KeyDown;
        }

        // =====================================================
        // SEND MESSAGE
        // =====================================================
        private async void BtnSend_Click(
            object sender,
            EventArgs e)
        {
            await SendMessageAsync();
        }

        // =====================================================
        // ENTER KEY
        // =====================================================
        private async void TxtMessage_KeyDown(
            object sender,
            KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter &&
                !e.Shift)
            {
                e.SuppressKeyPress = true;

                await SendMessageAsync();
            }
        }

        // =====================================================
        // SEND MESSAGE TO AI
        // =====================================================
        private async Task SendMessageAsync()
        {
            string userMessage =
                txtMessage.Text.Trim();

            if (string.IsNullOrWhiteSpace(userMessage))
            {
                return;
            }

            // Disable controls while waiting
            btnSend.Enabled = false;
            txtMessage.Enabled = false;

            try
            {
                // Add user message to history
                ChatMessage userChatMessage =
                    new ChatMessage
                    {
                        Role = "user",
                        Content = userMessage
                    };

                _conversationHistory.Add(
                    userChatMessage);

                // Display user message
                AddMessageToChat(
                    "You",
                    userMessage);

                // Clear input
                txtMessage.Clear();

                // Generate AI response
                string aiResponse =
                    await _aiService.GenerateChatResponseAsync(
                        _conversationHistory);

                // Add AI response to history
                ChatMessage aiChatMessage =
                    new ChatMessage
                    {
                        Role = "model",
                        Content = aiResponse
                    };

                _conversationHistory.Add(
                    aiChatMessage);

                // Display AI response
                AddMessageToChat(
                    "AI",
                    aiResponse);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "AI Chatbot Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                btnSend.Enabled = true;
                txtMessage.Enabled = true;

                txtMessage.Focus();
            }
        }

        // =====================================================
        // DISPLAY MESSAGE
        // =====================================================
        private void AddMessageToChat(
            string senderName,
            string message)
        {
            Label messageLabel =
                new Label();

            messageLabel.AutoSize = true;
            messageLabel.MaximumSize =
                new Size(
                    flpChatHistory.Width - 30,
                    0);

            messageLabel.Text =
                senderName + ": " + message;

            messageLabel.Padding =
                new Padding(10);

            messageLabel.Margin =
                new Padding(5);

            messageLabel.Font =
                new Font(
                    "Segoe UI",
                    10);

            flpChatHistory.Controls.Add(
                messageLabel);

            flpChatHistory.ScrollControlIntoView(
                messageLabel);
        }

        // =====================================================
        // CLEAR CHAT / NEW SESSION
        // =====================================================
        private void BtnClearChat_Click(
            object sender,
            EventArgs e)
        {
            _conversationHistory.Clear();

            flpChatHistory.Controls.Clear();

            txtMessage.Clear();

            txtMessage.Focus();
        }
    }
}