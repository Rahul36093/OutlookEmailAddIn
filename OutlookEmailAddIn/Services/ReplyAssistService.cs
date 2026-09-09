
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OutlookEmailAddIn.Models;

namespace OutlookEmailAddIn.Services
{
    public class ReplyAssistService
    {
        private readonly HttpClient _httpClient;

        public ReplyAssistService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> GenerateReplyAsync(
            ReplyAssistRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.EmailContent))
            {
                throw new ArgumentException(
                    "Email content cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(request.Tone))
            {
                throw new ArgumentException(
                    "Reply tone cannot be empty.");
            }

            string apiKey =
                System.Configuration.ConfigurationManager
                    .AppSettings["AI_API_KEY"];

            string endpoint =
                System.Configuration.ConfigurationManager
                    .AppSettings["AI_API_ENDPOINT"];

            string model =
                System.Configuration.ConfigurationManager
                    .AppSettings["AI_MODEL"];

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new Exception(
                    "AI API key is missing from App.config.");
            }

            if (string.IsNullOrWhiteSpace(endpoint))
            {
                throw new Exception(
                    "AI API endpoint is missing from App.config.");
            }

            if (string.IsNullOrWhiteSpace(model))
            {
                throw new Exception(
                    "AI model is missing from App.config.");
            }

            string url =
                endpoint.TrimEnd('/') +
                "/" +
                model +
                ":generateContent?key=" +
                apiKey;

            string prompt =
                "You are an email reply assistant.\n\n" +
                "Generate a suitable reply to the following email.\n\n" +
                "Reply tone: " + request.Tone + "\n\n" +
                "Email:\n" +
                request.EmailContent + "\n\n" +
                "Return only the reply text. " +
                "Do not add explanations or headings.";

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

            using (var content =
                   new StringContent(
                       json,
                       Encoding.UTF8,
                       "application/json"))
            {
                HttpResponseMessage response =
                    await _httpClient.PostAsync(
                        url,
                        content);

                string responseText =
                    await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(
                        "Gemini API request failed.\n\n" +
                        "Status: " +
                        response.StatusCode +
                        "\n\n" +
                        responseText);
                }

                dynamic result =
                    JsonConvert.DeserializeObject(
                        responseText);

                string reply =
                    result?.candidates?[0]?
                        .content?.parts?[0]?.text;

                if (string.IsNullOrWhiteSpace(reply))
                {
                    throw new Exception(
                        "Gemini returned an empty reply.");
                }

                return reply.Trim();
            }
        }
    }
}

