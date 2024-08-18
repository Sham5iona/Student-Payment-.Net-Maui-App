using System.Text.Json.Serialization;

namespace StudentPaymentApp.Model
{
    public class ForecastAPIResponse
    {
        [JsonPropertyName("location")]
        public ForecastAPIResponseLocation Location { get; set; }

        [JsonPropertyName("current")]
        public ForecastAPIResponseCurrent Current { get; set; }
    }

    public class ForecastAPIResponseLocation
    {
        public string name { get; set; }
        public string country { get; set; }
        public string localtime { get; set; }

    }

    public class ForecastAPIResponseCurrent
    {
        public double temperature { get; set; }
        public string[] weather_icons { get; set; }
        public string[] weather_descriptions { get; set; }
        public double wind_speed { get; set; }
        public double humidity { get; set; }
        public double cloudcover { get; set; }
        public double feelslike { get; set; }
        public double uv_index { get; set; }
        public double visibility { get; set; }
        public string is_day { get; set; }
    }
}
