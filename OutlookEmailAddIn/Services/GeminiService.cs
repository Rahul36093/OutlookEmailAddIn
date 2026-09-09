using Newtonsoft.Json;
using OutlookEmailAddIn.Models;
using System;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OutlookEmailAddIn.Services
{
    internal class GeminiService
    {
        private readonly HttpClient _httpClient;

        public GeminiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> GenerateEmailAsync(
            GenerateEmailRequest request)
        {
            string apiKey =
                ConfigurationManager.AppSettings["AI_API_KEY"];

            string endpoint =
                ConfigurationManager.AppSettings["AI_API_ENDPOINT"];

            string model =
                ConfigurationManager.AppSettings["AI_MODEL"];

            string url =
                $"{endpoint}{model}:generateContent?key={apiKey}";

            string prompt =
                "Generate a professional email based on the following instruction.\n\n" +
                "Return the response EXACTLY in this format:\n\n" +
                "Subject: <email subject>\n" +
                "Body:\n" +
                "<complete email body>\n\n" +
                "Do not add any other headings or explanations.\n\n" +
                "Instruction:\n" +
                request.UserInstruction;

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
                    await _httpClient.PostAsync(url, content);

                string responseJson =
                    await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(
                        $"Gemini API Error: {response.StatusCode}\n{responseJson}");
                }

                dynamic result =
                    JsonConvert.DeserializeObject(responseJson);

                string generatedText =
                    result.candidates[0].content.parts[0].text;

                return generatedText;
            }
        }
    }
}