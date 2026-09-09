using Newtonsoft.Json;
using OutlookEmailAddIn.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OutlookEmailAddIn.Services
{
    public class AIService
    {
        private readonly HttpClient _httpClient;

        public AIService()
        {
            _httpClient = new HttpClient();
        }

        // =====================================================
        // SPELL CHECK & GRAMMAR CORRECTION
        // =====================================================
        public async Task<SpellCheckResult> SpellCheckAsync(
            string subject,
            string body)
        {
            string apiKey =
                ConfigurationManager.AppSettings["AI_API_KEY"];

            string endpoint =
                ConfigurationManager.AppSettings["AI_API_ENDPOINT"];

            string model =
                ConfigurationManager.AppSettings["AI_MODEL"];

            ValidateConfiguration(apiKey, endpoint, model);

            if (string.IsNullOrWhiteSpace(subject) &&
                string.IsNullOrWhiteSpace(body))
            {
                throw new Exception(
                    "Email subject and body cannot both be empty.");
            }

            string prompt =
                "You are an email proofreading assistant.\n\n" +
                "Correct spelling, grammar, punctuation, and awkward wording " +
                "in the email below.\n" +
                "Preserve the original meaning and tone.\n" +
                "Do not add new information.\n" +
                "Return ONLY the corrected email using exactly this structure:\n\n" +
                "SUBJECT:\n" +
                "<corrected subject>\n\n" +
                "BODY:\n" +
                "<corrected body>\n\n" +
                "Original Subject:\n" +
                subject +
                "\n\n" +
                "Original Body:\n" +
                body;

            string responseText =
                await SendGeminiRequestAsync(
                    prompt,
                    apiKey,
                    endpoint,
                    model);

            return ParseSubjectBodyResult(responseText);
        }

        // =====================================================
        // LANGUAGE CONVERSION
        // =====================================================
        public async Task<SpellCheckResult> TranslateEmailAsync(
            string subject,
            string body,
            string targetLanguage)
        {
            string apiKey =
                ConfigurationManager.AppSettings["AI_API_KEY"];

            string endpoint =
                ConfigurationManager.AppSettings["AI_API_ENDPOINT"];

            string model =
                ConfigurationManager.AppSettings["AI_MODEL"];

            ValidateConfiguration(apiKey, endpoint, model);

            if (string.IsNullOrWhiteSpace(targetLanguage))
            {
                throw new Exception(
                    "Target language is required.");
            }

            string prompt =
                "You are an email translation assistant.\n\n" +
                "Translate the following email into " +
                targetLanguage +
                ".\n\n" +
                "Requirements:\n" +
                "- Preserve the original meaning.\n" +
                "- Preserve the original tone.\n" +
                "- Do not add new information.\n" +
                "- Do not remove important information.\n" +
                "- Return ONLY the translated email using exactly this structure:\n\n" +
                "SUBJECT:\n" +
                "<translated subject>\n\n" +
                "BODY:\n" +
                "<translated body>\n\n" +
                "Original Subject:\n" +
                subject +
                "\n\n" +
                "Original Body:\n" +
                body;

            string responseText =
                await SendGeminiRequestAsync(
                    prompt,
                    apiKey,
                    endpoint,
                    model);

            return ParseSubjectBodyResult(responseText);
        }

        // =====================================================
        // AI REPLY ASSIST
        // =====================================================
        public async Task<string> GenerateReplyAsync(
            string mailContent,
            string instructions)
        {
            string apiKey =
                ConfigurationManager.AppSettings["AI_API_KEY"];

            string endpoint =
                ConfigurationManager.AppSettings["AI_API_ENDPOINT"];

            string model =
                ConfigurationManager.AppSettings["AI_MODEL"];

            ValidateConfiguration(apiKey, endpoint, model);

            if (string.IsNullOrWhiteSpace(mailContent))
            {
                throw new Exception(
                    "Mail content cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(instructions))
            {
                throw new Exception(
                    "Reply instructions cannot be empty.");
            }

            string prompt =
                "You are an AI email reply assistant.\n\n" +
                "Generate a reply to the email content provided below.\n\n" +
                "Requirements:\n" +
                "- Follow the user's reply instructions.\n" +
                "- Understand and preserve the context of the original email.\n" +
                "- Preserve the intended meaning.\n" +
                "- Use an appropriate professional tone unless the instructions specify another tone.\n" +
                "- Do not invent facts.\n" +
                "- Do not add unrelated information.\n" +
                "- Return ONLY the reply text.\n" +
                "- Do not include explanations.\n\n" +
                "EMAIL CONTENT:\n" +
                mailContent +
                "\n\n" +
                "REPLY INSTRUCTIONS:\n" +
                instructions;

            string responseText =
                await SendGeminiRequestAsync(
                    prompt,
                    apiKey,
                    endpoint,
                    model);

            return responseText.Trim();
        }

        // =====================================================
        // AI CHATBOT
        // =====================================================
        public async Task<string> GenerateChatResponseAsync(
            List<ChatMessage> conversationHistory)
        {
            if (conversationHistory == null ||
                conversationHistory.Count == 0)
            {
                throw new Exception(
                    "Chat conversation cannot be empty.");
            }

            string apiKey =
                ConfigurationManager.AppSettings["AI_API_KEY"];

            string endpoint =
                ConfigurationManager.AppSettings["AI_API_ENDPOINT"];

            string model =
                ConfigurationManager.AppSettings["AI_MODEL"];

            ValidateConfiguration(apiKey, endpoint, model);

            StringBuilder conversation =
                new StringBuilder();

            conversation.AppendLine(
                "You are a helpful AI chatbot.");

            conversation.AppendLine(
                "Answer the user's questions clearly and accurately.");

            conversation.AppendLine(
                "Use the previous conversation to understand context.");

            conversation.AppendLine();

            foreach (ChatMessage message in conversationHistory)
            {
                if (message == null ||
                    string.IsNullOrWhiteSpace(message.Content))
                {
                    continue;
                }

                string role =
                    message.Role == "model"
                        ? "AI"
                        : "User";

                conversation.AppendLine(
                    role + ": " + message.Content);

                conversation.AppendLine();
            }

            conversation.AppendLine(
                "AI:");

            string responseText =
                await SendGeminiRequestAsync(
                    conversation.ToString(),
                    apiKey,
                    endpoint,
                    model);

            return responseText.Trim();
        }

        // =====================================================
        // COMMON GEMINI API REQUEST
        // =====================================================
        private async Task<string> SendGeminiRequestAsync(
            string prompt,
            string apiKey,
            string endpoint,
            string model)
        {
            string url =
                $"{endpoint}{model}:generateContent?key={apiKey}";

            var requestBody = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new
                            {
                                text = prompt
                            }
                        }
                    }
                }
            };

            string json =
                JsonConvert.SerializeObject(requestBody);

            using (var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json"))
            {
                HttpResponseMessage response =
                    await _httpClient.PostAsync(
                        url,
                        content);

                string responseJson =
                    await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(
                        $"Gemini API Error: {response.StatusCode}\n{responseJson}");
                }

                dynamic result =
                    JsonConvert.DeserializeObject(responseJson);

                if (result == null ||
                    result.candidates == null ||
                    result.candidates.Count == 0)
                {
                    throw new Exception(
                        "Gemini returned an invalid response.");
                }

                string text =
                    result.candidates[0]
                        .content.parts[0].text;

                if (string.IsNullOrWhiteSpace(text))
                {
                    throw new Exception(
                        "Gemini returned an empty response.");
                }

                return text.Trim();
            }
        }

        // =====================================================
        // CONFIGURATION VALIDATION
        // =====================================================
        private void ValidateConfiguration(
            string apiKey,
            string endpoint,
            string model)
        {
            if (string.IsNullOrWhiteSpace(apiKey) ||
                apiKey == "YOUR_API_KEY_HERE")
            {
                throw new Exception(
                    "Gemini API key is not configured in App.config.");
            }

            if (string.IsNullOrWhiteSpace(endpoint))
            {
                throw new Exception(
                    "Gemini API endpoint is not configured in App.config.");
            }

            if (string.IsNullOrWhiteSpace(model))
            {
                throw new Exception(
                    "Gemini model is not configured in App.config.");
            }
        }

        // =====================================================
        // PARSE SUBJECT + BODY
        // =====================================================
        private SpellCheckResult ParseSubjectBodyResult(
            string text)
        {
            string subject = null;
            string body = text;

            string subjectMarker = "SUBJECT:";
            string bodyMarker = "BODY:";

            int subjectIndex =
                text.IndexOf(
                    subjectMarker,
                    StringComparison.OrdinalIgnoreCase);

            int bodyIndex =
                text.IndexOf(
                    bodyMarker,
                    StringComparison.OrdinalIgnoreCase);

            if (subjectIndex >= 0 &&
                bodyIndex > subjectIndex)
            {
                int subjectStart =
                    subjectIndex + subjectMarker.Length;

                subject =
                    text.Substring(
                        subjectStart,
                        bodyIndex - subjectStart)
                    .Trim();

                body =
                    text.Substring(
                        bodyIndex + bodyMarker.Length)
                    .Trim();
            }

            return new SpellCheckResult
            {
                Subject = subject,
                Body = body
            };
        }
    }
}