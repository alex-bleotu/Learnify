using UnityEngine;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Threading.Tasks;

public class JsonGenerator : MonoBehaviour
{
    private const string Endpoint = "https://api.openai.com/v1/engines/davinci-codex/completions";
    private const string Model = "davinci-codex";
    private const string Prompt = "Please generate a JSON file similar to this:\n";
    private const string Stop = "\n";
    private const int MaxTokens = 1024;
    private const float Temperature = 0.5f;
    private const string ApiKey = "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";

    private async void Start()
    {
        string referenceFilePath = "reference.json";
        string referenceJson = await File.ReadAllTextAsync(referenceFilePath);

        var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiKey);

        string reference = referenceJson.Replace("\"", "\\\"");
        string parameters = $"\"prompt\": \"{Prompt}{reference}\", \"model\": \"{Model}\", \"temperature\": {Temperature}, \"max_tokens\": {MaxTokens}, \"stop\": \"{Stop}\"";

        var requestContent = new StringContent($"{{{parameters}}}", Encoding.UTF8, "application/json");
        var response = await client.PostAsync(Endpoint, requestContent);
        var responseContent = await response.Content.ReadAsStringAsync();

        var generatedJson = (string)JObject.Parse(responseContent)["choices"][0]["text"];
        var generatedObject = JsonConvert.DeserializeObject(generatedJson);
        var generatedJsonFormatted = JsonConvert.SerializeObject(generatedObject, Formatting.Indented);
        await File.WriteAllTextAsync("generated.json", generatedJsonFormatted, Encoding.UTF8);

        Debug.Log("JSON file generated successfully!");
    }
}