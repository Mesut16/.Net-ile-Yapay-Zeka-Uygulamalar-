using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;

class Program
{
    private static async Task Main(string[] args)
    {
        Console.Write("Lütfen çevirmek istediğinz cümleyi giriniz: ");
        string inputText = Console.ReadLine();

        string apiKey = "API_KEY";

        string translateText = await TraslateTextToEnglish(inputText, apiKey);

        if (!string.IsNullOrEmpty(translateText))
        {
        Console.WriteLine();
        Console.Write($"Çeviri (İNGİLİZCE): {translateText}");
        Console.WriteLine();    
        }
        else
        {
            Console.WriteLine("Beklenmeyen bir hata oluştu");
        }
    }

    private static async Task<string> TraslateTextToEnglish(string text, string apiKey)
    {
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
            {
                new{role = "user",content = $"Please translate this text to English: {text}"},
                new{role = "system",content = "You are a helpful asistans"}

            },
            };

            string jsonBody = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
                string responseString = await response.Content.ReadAsStringAsync();
                dynamic responseObject = JsonConvert.DeserializeObject(responseString);
                string translation = responseObject.choices[0].message.content;
                return translation;

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Bir hata oluştu: {ex.Message}");
                return null;
            }
        }

    }
}