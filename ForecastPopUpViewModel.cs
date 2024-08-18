
using CommunityToolkit.Mvvm.ComponentModel;
using StudentPaymentApp.Model.Services;

namespace StudentPaymentApp.ViewModel
{
    public partial class ForecastPopUpViewModel : ObservableObject
    {
        [ObservableProperty]
        private string city;

        [ObservableProperty]
        private string country;

        [ObservableProperty]
        private string localtime;

        [ObservableProperty]
        private string temperature;

        [ObservableProperty]
        private string weather_icon;

        [ObservableProperty]
        private string weather_description;

        [ObservableProperty]
        private string additionalInfo;

        private readonly ForecastApiService _forecastApiService;
        public ForecastPopUpViewModel()
        {
            _forecastApiService = new ForecastApiService();
        }

        public async void ShowForecastInformationAsync()
        {
            var Current_location = await Geolocation.GetLocationAsync();

            var forecast_data = await _forecastApiService
                                .GetForecastInformationAsync(Current_location.Latitude.ToString(),
                                Current_location.Longitude.ToString());

            if (forecast_data is null)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No internet connection!", "OK");
                return;
            }

            City = forecast_data.Location.name;
            Country = forecast_data.Location.country;
            Localtime = forecast_data.Location.localtime;
            Temperature = forecast_data.Current.temperature.ToString();
            Weather_icon = forecast_data.Current.weather_icons[0];
            Weather_description = forecast_data.Current.weather_descriptions[0];
            AdditionalInfo = $"Wind Speed: {forecast_data.Current.wind_speed} km/h\n\n" +
                                 $"Humidity: {forecast_data.Current.humidity}%\n\n" +
                                 $"Cloud Cover: {forecast_data.Current.cloudcover}%\n\n" +
                                 $"Feels Like: {forecast_data.Current.feelslike}°C\n\n" +
                                 $"UV Index: {forecast_data.Current.uv_index}\n\n" +
                                 $"Visibility: {forecast_data.Current.visibility} km\n\n" +
                                 $"Is Day: {(forecast_data.Current.is_day == "yes" ? "Yes" : "No")}";



        }

    }
}
