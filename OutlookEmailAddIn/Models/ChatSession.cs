using System.Collections.Generic;

namespace OutlookEmailAddIn.Models
{
    public class ChatSession
    {
        public List<ChatMessage> Messages { get; set; }

        public ChatSession()
        {
            Messages = new List<ChatMessage>();
        }

        public void AddUserMessage(string content)
        {
            Messages.Add(new ChatMessage
            {
                Role = "user",
                Content = content
            });
        }

        public void AddAIMessage(string content)
        {
            Messages.Add(new ChatMessage
            {
                Role = "model",
                Content = content
            });
        }

        public void Clear()
        {
            Messages.Clear();
        }
    }
}