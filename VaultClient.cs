using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace VaultNETClient
{
    public class VaultClient
    {
        HttpClient HttpClient;
        private readonly string _secretsPath;
        public VaultClient(VaultOptions vaultOptions)
        {
            HttpClient = HttpClientFactory.Create();
            HttpClient.BaseAddress = new Uri(vaultOptions.BaseUrl);
            HttpClient.DefaultRequestHeaders.Add("X-Vault-Token", vaultOptions.Token);
            // Setting the Content-type to application/json
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _secretsPath = vaultOptions.SecretsPath;
        }
        public async Task<Dictionary<string, string>> GetSecrets()
        {
            _ = string.IsNullOrEmpty(_secretsPath) ? throw new ArgumentException($"{nameof(_secretsPath)} cannot be empty"):true;

            return await GetSecrets(_secretsPath);
        }
        public async Task<Dictionary<string, string>> GetSecrets(string path)
        {
            // Making the HTTP Get call to consult our Secret
            JObject json = JObject.Parse(await HttpClient.GetStringAsync(path));
            JToken secrets = json["data"]["data"];
            // Storing our key-value pairs to a Dictionary for future data manipulation
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(secrets.ToString());
        }
    }
}