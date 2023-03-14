using WeatherApp.Common;
using WeatherApp.Services;

namespace WeatherApp;

public partial class WeatherPage : ContentPage
{
    public List<Models.List> WeatherList { get; set; }

	public WeatherPage()
	{
		InitializeComponent();
        WeatherList = new List<Models.List>();
	}

    protected override async void OnAppearing()
    {
		base.OnAppearing();
        var result = await ApiService.GetWeather(41.140453, -96.243683);

        foreach (var item in result.List)
        {
            WeatherList.Add(item);
        }

        CvWeather.ItemsSource = WeatherList;

        lblCity.Text = result.City.Name;
        lblWeatherDescription.Text = result.List[0].Weather[0].Description;
        lblTemperature.Text = $"{result.List[0].Main.Temperature}{GlobalConstants.TemperatureFahrenheit}";
        lblHumidity.Text = $"{result.List[0].Main.Humidity}%";
        lblWind.Text = $"{result.List[0].Wind.Speed}mph";
        //ImgWeatherIcon.Source = result.List[0].Weather[0].Icon;
        ImgWeatherIcon.Source = result.List[0].Weather[0].CustomIcon;

    }

}