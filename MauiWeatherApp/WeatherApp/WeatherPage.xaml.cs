using WeatherApp.Common;
using WeatherApp.Services;

namespace WeatherApp;

public partial class WeatherPage : ContentPage
{
	public WeatherPage()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
		base.OnAppearing();
        var result = await ApiService.GetWeather(41.140453, -96.243683);
        lblCity.Text = result.City.Name;
        lblWeatherDescription.Text = result.List[0].Weather[0].Description;
        lblTemperature.Text = $"{result.List[0].Main.Temperature}{GlobalConstants.TemperatureFahrenheit}";
        lblHumidity.Text = $"{result.List[0].Main.Humidity}%";
        lblWind.Text = $"{result.List[0].Wind.Speed}mph";

    }

}