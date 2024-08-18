
using System.Net.Http.Json;

namespace StudentPaymentApp.Model.Services
{
    public class ForecastApiService
    {
        private readonly HttpClient _httpClient;
        public ForecastApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(Constants.API_BASE_URL);
        }

        public async Task<ForecastAPIResponse> GetForecastInformationAsync(string latitude, string longitude)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                return null;
            }
             return await _httpClient.GetFromJsonAsync<ForecastAPIResponse>
                    ($"current?access_key={Constants.API_KEY}&query={latitude}, {longitude}");
        }
    }
}
