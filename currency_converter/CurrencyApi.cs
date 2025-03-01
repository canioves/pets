using DotNetEnv;

namespace currency_converter
{
    public class CurrencyApi
    {
        public string BaseCurrency { get; set; }
        private string _apiKey;
        private string _baseUrl = "https://api.currencyapi.com/v3/latest";
        private HttpClient _httpClient = new HttpClient();

        public CurrencyApi(string baseCurrency)
        {
            Env.Load(@"..\..\..\.env");
            _apiKey = Env.GetString("API_KEY");
            BaseCurrency = baseCurrency.ToUpper();
        }

        private async Task<HttpRequestMessage> _CreateRequest(string targetCurrency)
        {
            Dictionary<string, string> queryParameters = new Dictionary<string, string>()
            {
                { "base_currency", BaseCurrency },
                { "currencies", targetCurrency },
            };

            string queryPart = await new FormUrlEncodedContent(queryParameters).ReadAsStringAsync();
            HttpRequestMessage message = new HttpRequestMessage(
                HttpMethod.Get,
                $"{_baseUrl}?{queryPart}"
            );
            message.Headers.Add("apiKey", _apiKey);
            return message;
        }

        public async Task<string> GetCurrencyValue(string targetCurrency)
        {
            using HttpRequestMessage message = await _CreateRequest(targetCurrency);
            using HttpResponseMessage response = await _httpClient.SendAsync(message);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
